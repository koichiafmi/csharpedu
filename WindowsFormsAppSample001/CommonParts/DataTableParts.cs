using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;

namespace CommonParts
{
    public partial class DataTableParts
    {
        public void UsingJetProvider()
        {
            string filename = "";
            foreach (string strFilePath in DataGridViewObject.dataGridObj.getOpenFileDialog().FileNames)
            {
                filename = strFilePath;
            }

            //接続文字列
            string conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source="
                + Path.GetDirectoryName(filename) + ";Extended Properties=\"text;HDR=No;FMT=Delimited\"";
            System.Data.OleDb.OleDbConnection con =
                new System.Data.OleDb.OleDbConnection(conString);

            string commText = "SELECT * FROM [" + Path.GetFileName(filename) + "]";
            System.Data.OleDb.OleDbDataAdapter da =
                new System.Data.OleDb.OleDbDataAdapter(commText, con);

            //DataTableに格納する
            DataTable dt = new DataTable();

            da.Fill(dt);
            DataGridViewObject.dataGridObj.getDataGridView().AutoGenerateColumns = true;
            DataGridViewObject.dataGridObj.setDataTable(dt);
        }

        public void UsingOdbcProvider()
        {
            string filename = "";
            foreach (string strFilePath in DataGridViewObject.dataGridObj.getOpenFileDialog().FileNames)
            {
                filename = strFilePath;
            }

            //接続文字列
            string conString = "Driver={Microsoft Text Driver (*.txt; *.csv)};Dbq="
                + Path.GetDirectoryName(filename) + ";Extensions=asc,csv,tab,txt;";
            System.Data.Odbc.OdbcConnection con =
                new System.Data.Odbc.OdbcConnection(conString);

            string commText = "SELECT * FROM [" + Path.GetFileName(filename) + "]";
            System.Data.Odbc.OdbcDataAdapter da =
                new System.Data.Odbc.OdbcDataAdapter(commText, con);

            // データセットの作成
            DataSet data_set = new DataSet("default_set");

            // データテーブルの作成
            DataTable data_table = new DataTable("default_table");
            // 任意のテーブル名で DataTable を初期化します。

            // データテーブルをデータセットに登録
            data_set.Tables.Add(data_table);

            //DataTableに格納する
            da.Fill(data_table);

            DataGridViewObject.dataGridObj.getDataGridView().AutoGenerateColumns = true;

            // DataGridViewにデータセットを設定
            DataGridViewObject.dataGridObj.setDataset(data_set);
        }

        public void UsingOLEdbcProvider()
        {

            OleDbConnection connection;
            OleDbCommand command;
            OleDbDataAdapter adapter;

            //MessageBox.Show(filename, "デバッグ");

            string filename = "";
            foreach (string strFilePath in DataGridViewObject.dataGridObj.getOpenFileDialog().FileNames)
            {
                filename = strFilePath;
            }

            // データを格納する DataSet を初期化します。
            DataSet dataset = new DataSet();

            // OLE DB 接続を初期化します。CSV ファイルを読み込む場合には、ここの Data Source で、CSV ファイルが保存されているフォルダーを指定する必要があります。
            connection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + Path.GetDirectoryName(filename) + "\\; Extended Properties=\"Text;HDR=NO;FMT=Delimited\"");

            // CSV を読み込むための SQL 文です。CSV ファイルを読み込む場合には、ここの FROM 句のテーブル名では、CSV ファイル名を指定する必要があります。
            command = new OleDbCommand("SELECT * FROM [" + Path.GetFileName(filename) + "]", connection);
            // 念のため DataSet の中身を消去します。
            dataset.Clear();

            // OleDbDataAdapter を、先ほど定義した OleDbCommand を使って初期化して、DataSet に CSV ファイルの内容を読み込みます。
            adapter = new OleDbDataAdapter(command);
            adapter.Fill(dataset);

            // 任意のテーブル名で DataTable を初期化します。
            DataTable table = new DataTable("Table");
            DataGridViewObject.dataGridObj.setDataTable(table);
            // DataTable の列名を設定します。

            table.Columns.Add("Columns1");

            table.Columns.Add("Columns2");

            table.Columns.Add("Columns3");

            table.Columns.Add("Columns4");

            table.Columns.Add("Columns5");


            DataGridViewObject.dataGridObj.getDataGridView().AutoGenerateColumns = true;


            // DataSet が持つ DataTable を直接 DataGridView の DataSource に設定することも可能です。
            DataGridViewObject.dataGridObj.setDataset(dataset);
        }
        public void SelectDataTable()
        {
            //DataTable table = new DataTable("Table");
            DataTable table = DataGridViewObject.dataGridObj.getDataTable();
            string a = DataGridViewObject.dataGridObj.getDataGridView().Columns[0].Name;
//            DataRow[] rows = table.Select(DataGridViewObject.dataGridObj.getDataGridView().Columns[0].Name
//                + " =" + SearchCondition.searchCondition.getFirstSearchkey());
//            DataRow[] rows = table.Select(string.Concat("Columns1 =", SearchCondition.searchCondition.getFirstSearchkey()));
            DataRow[] rows = table.Select("Columns1 = カサハラ" );
        }
    }
}
