//https://qiita.com/tomochan154/items/1ce33f2aef167c0fed9d

using System.Runtime.InteropServices;

namespace DbtoolExt
{

    [Guid("35B76279-4D6E-4ED6-A5B4-423CFFF82CD9")]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface ICcwSample
    {
        void CallMsgBox(string message);
        int CalcSum(int num1, int num2);
    }
}
