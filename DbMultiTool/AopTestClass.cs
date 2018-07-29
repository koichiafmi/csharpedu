using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;

namespace DbMultiTool
{
    class AopTestClass : MarshalByRefObject
    {
        private int TestA { get; set; }

        private int TestB { get; set; }

        public AopTestClass(int a = 1)
        {
            TestA = a;
            TestB = 9;
        }

        public AopTestClass(int a, int b = 1, int c = 1, string asd = "")
        {
            TestA = a;
            TestB = b + c;
        }

        public void GetOutPut()
        {
            Console.WriteLine(string.Format(@"GetOutPut(int {0},{1})", TestA, TestB));
        }
    }
}
