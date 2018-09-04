using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class ClassA
    {
        public int intVal;
        public double doubleVal;
        public string stringVal;

        public ClassA()
        {
            //intVal = 1;
            //doubleVal = 2.2;
            //stringVal = "mori";
        }

        public override string ToString()
        {
            return String.Format("intVal = {0}, doubleVal = {1}, stringVal = {2}",
                                 intVal.ToString(),
                                 doubleVal.ToString(),
                                 ((stringVal == null) ? "null" : stringVal));
        }
    }
}
