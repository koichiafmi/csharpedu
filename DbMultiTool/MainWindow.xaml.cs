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
            string databaseName = textBox_Copy.Text;
            string serverName = textBox.Text;
            string userName = textBox_Copy1.Text;
            string password = passwordBox.Password;

            DbConnection dbcon = new DbConnection(serverName, databaseName, userName, password);

            SqlEditor mainFrm = new SqlEditor();
            this.Hide();
            mainFrm.ShowDialog();
            this.Close();
        }
    }
}
