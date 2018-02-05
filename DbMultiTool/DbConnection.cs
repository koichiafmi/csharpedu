using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace DbMultiTool
{
    /// <summary>
    /// データベース処理クラス
    /// </summary>
    class DbConnection : IDisposable
    {
        /// <summary>
        /// commandクラス（実行クエリを保持したり、実行する。）
        /// </summary>
        NpgsqlCommand _com;
        /// <summary>
        /// connectionクラス（データベースと接続する。）
        /// </summary>
        NpgsqlConnection _con;
        /// <summary>
        /// dataadapter（非接続型のクエリ実行を行うのに必要）
        /// </summary>
        NpgsqlDataAdapter _da;

        //TODO:トランザクション処理はまだ未実装。
        //NpgsqlTransaction _tran;
        /// <summary>
        /// 接続文字列作成クラス（パラメータがプロパティになっているので便利。）
        /// </summary>
        NpgsqlConnectionStringBuilder _constr;

        /// <summary>
        /// データベース処理クラス
        /// 接続できないパラメータだったら例外投げちゃうゾ
        /// </summary>
        /// <param name="server">サーバー名</param>
        /// <param name="database">データベース名</param>
        /// <param name="user">ユーザー名</param>
        /// <param name="password">パスワード</param>
        public DbConnection(string server, string database, string user, string password)
        {
            if(string.IsNullOrWhiteSpace(server) || 
               string.IsNullOrWhiteSpace(database) || 
               string.IsNullOrWhiteSpace(user) || 
               string.IsNullOrWhiteSpace(password))
            {
                throw new ApplicationException("パラメータが正しくありません。");
            }

            try
            {
                //接続文字列の作成
                _constr = new NpgsqlConnectionStringBuilder();
                _constr.Host = server;
                _constr.Database = database;
                _constr.Username = user;
                _constr.Password = password;

                //接続テスト
                _con = new NpgsqlConnection(_constr.ConnectionString);
                _con.Open();
                _con.Close();

            }catch(NpgsqlException e)
            {
                throw new ApplicationException("データベース初期化処理で例外が発生しました。", e);
            }
        }

        /// <summary>
        /// データベース接続
        /// </summary>
        public void Open()
        {
            try
            {
                _con = new NpgsqlConnection(_constr.ConnectionString);
                _con.Open();
            }
            catch (NpgsqlException e)
            {
                throw new ApplicationException("データベース接続処理で例外が発生しました。", e);
            }
        }

        /// <summary>
        /// クエリ実行（非接続型クラス）
        /// 
        /// これとは別に接続型クラスによるクエリ実行があるが、まだ未実装。
        /// 大雑把に説明すると・・・
        /// 
        /// 【非接続型クラス】
        /// 　（メリット）結果が帰ってきたら即切断できる。コレクションとして使用できるので、任意の行にアクセス可能。
        /// 　（デメリット）自動コミットされるので、ロールバックができない。
        /// 　　　　　　　　オブジェクトがでかくなるのでパフォーマンス悪し。
        /// 【接続型クラス】
        /// 　（メリット）パフォーマンスはめちゃ早い。オブジェクトは比較的小さめなので、軽快な処理が期待できる。
        /// 　　　　　　　実行メソッドが豊富なので、種類に応じて好きなクエリ実行方法が選べる。
        /// 　　　　　　　コミット、ロールバックが手動。
        /// 　（デメリット）結果のアクセスは1行目から順番にフェッチして行く方法なので、順番に参照する必要がある。
        /// 　　　　　　　　好きな行からアクセスしたり、逆戻りすることはできない。フェッチする間は接続している必要がある。
        /// 　　　　　　　　
        /// クエリ（CRUD）ごとに使い所を分類すると・・・
        /// select文：どちらでも可
        /// insert,update,delete文：接続型クラスが良い
        /// </summary>
        /// <param name="sql">SQL文</param>
        /// <returns>実行結果</returns>
        public DataSet ExecuteSql(string sql)
        {
            if(string.IsNullOrWhiteSpace(sql))
            {
                throw new ApplicationException("クエリの実行に失敗しました。クエリが入力されていません。");
            }else if(!sql.Trim().Substring(0, 5).ToUpper().Equals("SELECT"))
            {
                throw new NotSupportedException("SELECT文以外は未実装なので実行できません。");
            }

            try
            {
                //結果を格納するためのデータセット
                DataSet ds = new DataSet();

                //commandクラスにクエリとconnectionオブジェクトをつっこんでオブジェクトを生成している。
                _com = new NpgsqlCommand(sql, _con);
                //dataadapterにcommandオブジェクトをつっこんでオブジェクトを生成している。
                _da = new NpgsqlDataAdapter(_com);

                //dataadapterからFillメソッドでクエリ実行。実行した結果がdsにセットされる。
                _da.Fill(ds);

                return ds;
            }
            catch (NpgsqlException e)
            {
                throw new ApplicationException("クエリ実行処理で例外が発生しました。", e);
            }
        }

        //public void Commit()
        //{

        //}

        //public void RollBack()
        //{

        //}

        //アンマネージドクラスを多数使用するので、IDisposableをインタフェースにし、
        //それらのクラスを正しく確実に破棄できるようにした。
        //下記コードのテンプレは追加したら自動生成される。
        #region IDisposable Support
        private bool disposedValue = false; // 重複する呼び出しを検出するには

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: マネージ状態を破棄します (マネージ オブジェクト)。
                }

                // TODO: アンマネージ リソース (アンマネージ オブジェクト) を解放し、下のファイナライザーをオーバーライドします。
                // TODO: 大きなフィールドを null に設定します。
                if (_con.State != ConnectionState.Closed)
                {
                    _con.Close();
                }

                disposedValue = true;
            }
        }

        // TODO: 上の Dispose(bool disposing) にアンマネージ リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします。
        ~DbConnection()
        {
            // このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
            Dispose(false);
        }

        // このコードは、破棄可能なパターンを正しく実装できるように追加されました。
        public void Dispose()
        {
            // このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
            Dispose(true);
            // TODO: 上のファイナライザーがオーバーライドされる場合は、次の行のコメントを解除してください。
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
