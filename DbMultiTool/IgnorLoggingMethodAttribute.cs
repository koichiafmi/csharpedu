using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Collections.Concurrent;
using Newtonsoft.Json;

// https://qiita.com/MakotoTaniguchi/items/f2b8a13ac21b5c56a3f1
namespace DbMultiTool
{
    /// <summary>
    /// ロギングクラス
    /// アスペクト指向言語よりに実装しました
    /// </summary>
    /// <typeparam name="T">クラス内の全メソッドでロギングしたいクラス名</typeparam>
    /// <memo>
    /// Tに入るクラスは必ずMarshalByRefObjectクラスを継承して下さい
    /// https://msdn.microsoft.com/ja-jp/library/5shkd9h3%28v=vs.110%29.aspx?f=255&MSPPError=-2147217396
    /// </memo>
    public class MethodBaseProxy<T> : RealProxy
    {
        protected virtual string KeyPrefix
        {
            get
            {
                return "FuncKey";
            }
        }

        protected readonly T realObject;

        /***************************
        * 透過プロキシのインタフェースを指定
        ***************************/
        public MethodBaseProxy(params object[] objs) : base(typeof(T))
        {
            var templateType = typeof(T);
            var paramTypes = objs.Select(obj =>
            {
                var type = obj.GetType();
                return string.Format("{0}.{1}", type.Namespace, type.Name);
            }).ToArray();

            var funcKey = string.Format("{0}-{1}.{2}-{3}", this.KeyPrefix, templateType.Namespace, templateType.Name, string.Join("-", paramTypes));
            //https://msdn.microsoft.com/ja-jp/library/bb549151%28v=vs.110%29.aspx?f=255&MSPPError=-2147217396
            Func<object[], object> funcConstractor;
            //ConstructorFunc.ConstractorList.Clear();
            ConstructorFunc.ConstractorList.TryGetValue(funcKey, out funcConstractor);
            if (null == funcConstractor)
            {
                /*****************************
                * 実行するコンストラクタ情報を取得する
                *****************************/
                Func<System.Type, object[], ConstructorInfo> findConstructor = (type, paramInfos) =>
                {
                    var targetConstructor = type.GetConstructor(paramInfos.Select(i => i.GetType()).ToArray());
                    if (null != targetConstructor)
                    {
                        return targetConstructor;
                    }

                    /********************************************
                    * デフォルト引数のあるコンストラクタを検索
                    ********************************************/
                    var constructors = type.GetConstructors();
                    var constructorInfos = constructors.Select(constructorInfo =>
                    {
                        var paramsInfos = constructorInfo.GetParameters();
                        var needNum = paramsInfos.Count() - paramInfos.Count();
                        return new
                        {
                            isDefaultValue = paramsInfos.Any(),
                            defaultValues = paramsInfos.Select(paramsInfo => paramsInfo.DefaultValue).Reverse().Take(needNum).Reverse().ToArray()
                        };
                    }).Where(constructorInfo => constructorInfo.isDefaultValue).ToArray();

                    foreach (var constructorInfo in constructorInfos)
                    {
                        var paramInfo = paramInfos != null ? paramInfos.Concat(constructorInfo.defaultValues) : constructorInfo.defaultValues;
                        var a = paramInfo.Select(i => i.GetType()).ToArray();
                        targetConstructor = type.GetConstructor(paramInfo.Select(i => i.GetType()).ToArray());
                        if (null != targetConstructor)
                        {
                            break;
                        }
                    }

                    return targetConstructor;
                };

                /********************
                * デフォルト引数を取得する
                ********************/
                var constructor = findConstructor(typeof(T), objs);
                var parameters = constructor.GetParameters();
                var needParamNum = parameters.Count() - objs.Count();
                if (needParamNum != 0)
                {
                    var defaultParams = parameters.Where(x => x.HasDefaultValue).Reverse().Select(x => x.DefaultValue).Take(needParamNum).Reverse();
                    objs = objs.Concat(defaultParams).ToArray();
                }

                /***********************
                * コンストラクタを式木にして対応
                ***********************/
                Expression<Func<object[], object>> constractorEx = (paramObjs) => findConstructor(typeof(T), paramObjs).Invoke(paramObjs);
                funcConstractor = constractorEx.Compile();
                ConstructorFunc.ConstractorList[funcKey] = funcConstractor;
            }

            realObject = (T)funcConstractor(objs);
        }

        /// <summary>
        /// Tクラスのインスタンスを取得する
        /// </summary>
        /// <returns></returns>
        public T GetInstance()
        {
            return (T)this.GetTransparentProxy();
        }

        /// <summary>
        /// プロキシがメッセージを受け取った時に呼ばれるメソッド。
        /// ここで色々な処理を噛ませることができる。
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override IMessage Invoke(IMessage msg)
        {
            IMethodMessage method = msg as IMethodMessage;

            Console.WriteLine(string.Format("--- Start {0} ---", method.MethodName));
            try
            {
                MethodInfo methodInfo = (MethodInfo)method.MethodBase;

                /**************************************
                * プロパティのGetメソッド,Setメソッドでない事を確認
                **************************************/
                var isProperty = methodInfo.DeclaringType.GetProperties().Any(x =>
                {
                    return (x.GetGetMethod() == methodInfo || x.GetSetMethod() == methodInfo);
                });

                /**************************
                * 対象のメソッドが監視対象か判定
                **************************/
                var attribute = methodInfo.GetCustomAttribute(typeof(IgnorLoggingMethodAttribute));
                IgnorLoggingMethodAttribute customAttribute = null;
                if (null != attribute)
                {
                    customAttribute = (IgnorLoggingMethodAttribute)attribute;
                }

                if (false == isProperty)
                {
                    if (customAttribute != null)
                    {
                        if (false == customAttribute.IgnorLoggingBeforeMethod)
                        {
                            BeforeExecuteMethod(method);
                        }
                    }
                    else
                    {
                        BeforeExecuteMethod(method);
                    }
                }

                var args = method.Args;

                /********************
                * Tオブジェクトのメソッド実行
                ********************/
                var result = methodInfo.Invoke(realObject, args);

                if (false == isProperty)
                {
                    if (customAttribute != null)
                    {
                        if (false == customAttribute.IgnorLoggingAffterMethod)
                        {
                            AfterExecuteMethod(method, args, result);
                        }
                    }
                    else
                    {
                        AfterExecuteMethod(method, args, result);
                    }
                }

                return new ReturnMessage(result, args, args.Length, method.LogicalCallContext, (IMethodCallMessage)msg);
            }
            catch (TargetInvocationException ex)
            {
                /****************************************************
                 * メソッド内部で例外が発生すると
                 * TargetInvocationExceptionが送出される。
                 * Echo内部で発生した例外は ex.InnerException で取得
                 ****************************************************/
                return new ReturnMessage(ex.InnerException, (IMethodCallMessage)msg);
            }
            catch (Exception ex)
            {
                /**************************************************
                 * TargetInvocationExceptionでキャッチ出来なかった
                 * Exceptionを全てキャッチする
                 **************************************************/
                return new ReturnMessage(ex.InnerException, (IMethodCallMessage)msg);
            }
            finally
            {
                Console.WriteLine(string.Format("--- End {0} ---", method.MethodName));
            }
        }

        /// <summary>
        /// 呼び出しメソッドが実行前に実行されるメソッド
        /// </summary>
        /// <param name="method"></param>
        protected virtual void BeforeExecuteMethod(IMethodMessage method)
        {
            var methodInfo = (MethodInfo)method.MethodBase;
            Console.WriteLine(string.Format("--- Logging Start MethodName={0} RequestParamater={1} ---", method.MethodName, JsonConvert.SerializeObject(method.Args)));
        }

        /// <summary>
        /// 呼び出しメソッドの実行後に実行されるメソッド
        /// </summary>
        /// <param name="method"></param>
        /// <param name="resultObject"></param>
        protected virtual void AfterExecuteMethod(IMethodMessage method, object[] args, object resultObject)
        {
            Console.WriteLine(string.Format("--- Logging End MethodName={0} RequestParamater={1} ResultData={2} ---", method.MethodName, JsonConvert.SerializeObject(args), JsonConvert.SerializeObject(resultObject)));
        }
    }

    /// <summary>
    /// ロギングしないメソッドに付与する属性
    /// </summary>
    /// <memo>
    /// メソッドにしか適用出来ません
    /// </memo>
    [AttributeUsage(AttributeTargets.Method)]
    public class IgnorLoggingMethodAttribute : Attribute
    {
        /// <summary>
        /// メソッドが実行される前にするロギングするか判断するフラグ
        /// </summary>
        public bool IgnorLoggingBeforeMethod { get; set; }

        /// <summary>
        /// メソッドが実行された後にするロギングするか判断するフラグ
        /// </summary>
        public bool IgnorLoggingAffterMethod { get; set; }

        /// <summary>
        /// コンストラクタ(ロギングフラグを設定する)
        /// </summary>
        /// <param name="isBefore">trueの場合、メソッドが実行される前のロギングをしない</param>
        /// <param name="isAffter">trueの場合、メソッドが実行された後のロギングをしない</param>
        public IgnorLoggingMethodAttribute(bool isBefore, bool isAffter) : base()
        {
            IgnorLoggingBeforeMethod = isBefore;
            IgnorLoggingAffterMethod = isAffter;
        }
    }

    /// <summary>
    /// コンストラクタの処理をキャッシュするクラス
    /// </summary>
    internal static class ConstructorFunc
    {
        /// <summary>
        /// コンストラクタのFuncが入っています
        /// </summary>
        public static ConcurrentDictionary<string, Func<object[], object>> ConstractorList = new ConcurrentDictionary<string, Func<object[], object>>();
    }
}
