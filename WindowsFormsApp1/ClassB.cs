using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    [System.SerializableAttribute]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Size = 16)]
    public struct ClassB
    {
        public int intVal;
        public double doubleVal;
        public string stringVal;

        //public ClassB()
        //{

        //}

        public override string ToString()
        {
            return String.Format("intVal = {0}, doubleVal = {1}, stringVal = {2}",
                                 intVal.ToString(),
                                 doubleVal.ToString(),
                                 ((stringVal == null) ? "null" : stringVal));
        }
    }
}
