using System;
using System.Windows.Forms;

namespace DbMultiTool
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            NSProject_Main mainForm = new NSProject_Main();

            //スプラッシュウィンドウを表示
            SplashForm.ShowSplash(mainForm);

            //メインウィンドウを表示
            Application.Run(mainForm);
        }
    }
}
