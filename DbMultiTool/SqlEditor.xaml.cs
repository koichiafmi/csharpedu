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
using System.Windows.Shapes;
using System.Data;

namespace DbMultiTool
{
    /// <summary>
    /// SqlEditor.xaml の相互作用ロジック
    /// </summary>
    public partial class SqlEditor : Window
    {
        public SqlEditor(string server, string database, string user, string password)
        {
            InitializeComponent();

            _server = server;
            _database = database;
            _user = user;
            _password = password;

            _result = new DataSet();
        }

        string _server;
        string _database;
        string _user;
        string _password;
        DbConnection _dbCon;
        DataSet _result;

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _dbCon = new DbConnection(_server, _database, _user, _password);

                _dbCon.Open();

                _result = _dbCon.ExecuteSql(textBox.Text);

                //DataGridViewでいうところのDataSourceプロパティ
                //これ以外にItemsSource="{Binding}" AutoGenerateColumns="True"
                //する必要があるみたい。
                this.dataGrid.DataContext = _result.Tables[0];
                _dbCon.Dispose();
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
