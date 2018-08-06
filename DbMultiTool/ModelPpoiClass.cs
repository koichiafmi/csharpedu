using System;

namespace DbMultiTool
{
    class ModelPpoiClass : MarshalByRefObject
    {
        private int TestA { get; set; }

        private int TestB { get; set; }

        public ModelPpoiClass(int a = 1)
        {
            TestA = a;
            TestB = 9;
        }

        public ModelPpoiClass(int a, int b = 1, int c = 1, string asd = "")
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
