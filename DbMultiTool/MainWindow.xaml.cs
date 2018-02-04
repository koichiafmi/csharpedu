using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DbMultiTool
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string databaseName = textBox_Copy.Text;
                string serverName = textBox.Text;
                string userName = textBox_Copy1.Text;
                string password = passwordBox.Password;

                using (DbConnection dbcon = new DbConnection(serverName, databaseName, userName, password)) { }

                //SqlEditorのフォームを作成し、ShowDialogで呼び出す。
                //ただ、そのまえにログイン画面はいらなくなるので、Hideで隠している。（オブジェクトは生きている）
                //SqlEditorが閉じられたらこっちに制御が戻るので、戻って即Closeして全体終了。
                SqlEditor mainFrm = new SqlEditor(serverName, databaseName, userName, password);
                Hide();
                mainFrm.ShowDialog();
                Close();
            }
            catch (Exception ex)
            {
                StringBuilder msgtxt = new StringBuilder();
                msgtxt.Append(ex.Message);
                if (ex.InnerException != null)
                {
                    msgtxt.Append(ex.InnerException.Message);
                }

                MessageBox.Show(msgtxt.ToString());
            }
        }
    }
}
