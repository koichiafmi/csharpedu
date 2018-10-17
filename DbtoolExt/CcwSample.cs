using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DbtoolExt
{
    [Guid("37186FD4-5CD3-4E94-AA3D-A88948A8DB6D")]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [ComDefaultInterface(typeof(ICcwSample))]
    public class CcwSample : ICcwSample
    {
        public CcwSample()
        {
        }

        public void CallMsgBox(string message)
        {
            MessageBox.Show(message);
        }

        public int CalcSum(int num1, int num2)
        {
            return num1 + num2;
        }
    }
}
