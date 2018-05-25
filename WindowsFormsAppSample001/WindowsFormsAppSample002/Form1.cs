using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;
using System.Xml;

namespace WindowsFormsAppSample002
{
    public partial class Form1 : Form
    {
        private String READ_FILE_USED_IN_VISUALBASIC_FILEIO = @"C:\Sample001\Sample002.csv";
        private String READ_FILE_USED_IN_FILEIO = @"C:\Sample001\Sample002_5column10000rows.csv";
        //private String READ_FILE_USED_IN_FILEIO  = @"C:\Sample002_5column100rows.csv";

        private int READ_CSV_TEXTFIELDPARSER = 2; // Microsoft.VisualBasic.FileIO : dataGridViewに一行ずつADD
        private int READ_CSV_FILE_READLINES_DATAGRIDVIEW_ROWS_ADD_ONELINE = 0; // System.IO : dataGridViewに一行ずつADD
        private int READ_CSV_FILE_READLINES_DATAGRIDVIEW_ROWS_ADD_ALLLINE = 1; // System.IO : dataGridViewに一括ADD

        private String READ_CSV_PERSONAL_INFOMATION = @"C:\Sample001\personal_infomation.csv";
        private String READ_XML_PERSONAL_INFOMATION = @"C:\Sample001\personal_infomation.xml";

        private int DEFAULT_DISPLAY = 1;
        private int FIRSTLINE_OF_CSV = 2;


        private int FILE_TYPE_CSV = 0;
        private int FILE_TYPE_XML = 1;


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            // データをすべてクリア
            this.Initial_Preparation();
            this.Set_DataGridView_Columns_Visible(READ_CSV_TEXTFIELDPARSER);
            this.DataLoad(READ_CSV_TEXTFIELDPARSER);
            this.Enabled = true;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            // データをすべてクリア
            this.Initial_Preparation();
            this.Set_DataGridView_Columns_Visible(READ_CSV_FILE_READLINES_DATAGRIDVIEW_ROWS_ADD_ONELINE);
            this.DataLoad(READ_CSV_FILE_READLINES_DATAGRIDVIEW_ROWS_ADD_ONELINE);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // データをすべてクリア
            this.Initial_Preparation();
            this.Set_DataGridView_Columns_Visible(READ_CSV_FILE_READLINES_DATAGRIDVIEW_ROWS_ADD_ALLLINE);
            this.DataLoad(READ_CSV_FILE_READLINES_DATAGRIDVIEW_ROWS_ADD_ALLLINE);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            // データをすべてクリア
            this.Initial_Preparation();
            this.Set_DataGridView_Columns_Visible(READ_CSV_FILE_READLINES_DATAGRIDVIEW_ROWS_ADD_ALLLINE);
            //CSV読込表示
            this.DependsOnFileTypeDataLoad(FILE_TYPE_CSV);
            this.Enabled = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            dataGridView1.DataSource = null; // 不要かも
            dataGridView1.Columns.Clear(); ;
            dataGridView1.Refresh();
            this.TextBox_Clear();
            //XML読込表示
            this.DependsOnFileTypeDataLoad(FILE_TYPE_XML);
            this.Enabled = true;
        }


        private void DataLoad(int method)
        {
            int rowCount = 0;
            string filename = "";
            //Stopwatchオブジェクトを作成する
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();

            try
            {
                if (READ_CSV_FILE_READLINES_DATAGRIDVIEW_ROWS_ADD_ONELINE == method)
                {
                    //ストップウォッチを開始する
                    sw.Start();
                    foreach (string line in File.ReadLines(READ_FILE_USED_IN_FILEIO, Encoding.Default))
                    {
                        string[] csv = line.Split(',');
                        string[] data = new string[5];
                        Array.Copy(csv, 0, data, 0, 5);
                        this.dataGridView1.Rows.Add(data);
                        rowCount++;
                    }

                    //ストップウォッチを止める
                    sw.Stop();
                    //結果を表示する
                    textBox4.Text = string.Concat("CSV読込2-経過時間[", rowCount, "行][ ", sw.Elapsed.Hours.ToString("00"), ":", sw.Elapsed.Minutes.ToString("00"), ":", sw.Elapsed.Seconds.ToString("00"), ":", sw.ElapsedMilliseconds.ToString("000"), " ]");
                    textBox1.Text = "System.IOクラス使用\r\ndataGridViewに一行ずつADD";
                }
                else if (READ_CSV_FILE_READLINES_DATAGRIDVIEW_ROWS_ADD_ALLLINE == method)
                {
                    //ストップウォッチを開始する
                    sw.Start();

                    string[] csvDataAll = File.ReadAllLines(READ_FILE_USED_IN_FILEIO, Encoding.Default);

                    //データの分配列を用意
                    DataGridViewRow[] rows = new DataGridViewRow[csvDataAll.Length];

                    for (int i = 0; i < csvDataAll.Length; i++)
                    {
                        string[] csv = csvDataAll[i].Split(',');
                        string[] data = new string[5];
                        Array.Copy(csv, 0, data, 0, 5);

                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(this.dataGridView1);
                        row.SetValues(data);
                        rows[i] = row;
                        rowCount++;
                    }
                    this.dataGridView1.Rows.AddRange(rows);

                    //ストップウォッチを止める
                    sw.Stop();
                    //結果を表示する
                    textBox4.Text = string.Concat("CSV読込3-経過時間[", rowCount, "行][ ", sw.Elapsed.Hours.ToString("00"), ":", sw.Elapsed.Minutes.ToString("00"), ":", sw.Elapsed.Seconds.ToString("00"), ":", sw.ElapsedMilliseconds.ToString("000"), " ]");
                    textBox1.Text = "System.IOクラス使用\r\ndataGridViewに一括ADD";
                }
                else if (READ_CSV_TEXTFIELDPARSER == method)
                {
                    //ストップウォッチを開始する
                    sw.Start();
                    TextFieldParser parser;
                    parser = new TextFieldParser(READ_FILE_USED_IN_VISUALBASIC_FILEIO, Encoding.GetEncoding("Shift_JIS"));
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(","); // 区切り文字はコンマ
                    while (!parser.EndOfData)
                    {
                        string[] row = parser.ReadFields(); // 1行読み込み
                                                            // 読み込んだデータ(1行をDataGridViewに表示する)
                        dataGridView1.Rows.Add(row);
                        rowCount++;
                    }
                    //ストップウォッチを止める
                    sw.Stop();
                    //結果を表示する
                    textBox4.Text = string.Concat("CSV読込1-経過時間[", rowCount, "行][ ", sw.Elapsed.Hours.ToString("00"), ":", sw.Elapsed.Minutes.ToString("00"), ":", sw.Elapsed.Seconds.ToString("00"), ":", sw.ElapsedMilliseconds.ToString("000"), " ]");
                    textBox1.Text = "TextFieldParserクラスを使用したCSV読込";
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                //ストップウォッチを止める
                sw.Stop();
                if (READ_CSV_FILE_READLINES_DATAGRIDVIEW_ROWS_ADD_ONELINE == method)
                {
                    filename = READ_FILE_USED_IN_FILEIO;
                }
                else if (READ_CSV_FILE_READLINES_DATAGRIDVIEW_ROWS_ADD_ALLLINE == method)
                {
                    filename = READ_FILE_USED_IN_FILEIO;

                }
                else if (READ_CSV_TEXTFIELDPARSER == method)
                {
                    filename = READ_FILE_USED_IN_VISUALBASIC_FILEIO;

                }

                string message = string.Concat("読込むファイルが見つかりません！\n", "ファイル名：", filename);
                MessageBox.Show(message, "dialog");
                return;
            }
        }
        private void DependsOnFileTypeDataLoad(int fileType)
        {
            int rowCount = 0;
            string filename = "";

            try
            {
                filename = READ_CSV_PERSONAL_INFOMATION;
                string[] csvDataAll = File.ReadAllLines(filename, Encoding.UTF32);
                DataGridViewRow[] rows;
                //データの分配列を用意
                if (checkBox1.Checked == false)
                {
                    rows = new DataGridViewRow[csvDataAll.Length];
                }
                else
                {
                    rows = new DataGridViewRow[csvDataAll.Length - 1];
                }

                if (FILE_TYPE_CSV == fileType)
                {
                    int rowCounta = 0;
                    //CSVファイルの読み込み
                    for (int i = 0; i < csvDataAll.Length; i++)
                    {
                        rowCounta = i;
                        string[] csv = csvDataAll[i].Split(',');
                        string[] data = new string[5];
                        Array.Copy(csv, 0, data, 0, 5);
                        if (checkBox1.Checked == true && i == 0)
                        {
                            // checkBox1がチェックされている場合
                            Set_ColumnName(FIRSTLINE_OF_CSV, data);
                        }
                        else
                        {
                            if (checkBox1.Checked == true)
                            {
                                rowCounta--;
                            }
                            DataGridViewRow row = new DataGridViewRow();
                            row.CreateCells(this.dataGridView1);
                            row.SetValues(data);
                            rows[rowCounta] = row;
                            rowCount++;
                        }
                    }
                    if (checkBox1.Checked == false)
                    {
                        textBox4.Text = string.Concat("CSV読込");
                        textBox1.Text = "先頭行もデータとして表示";
                    } else
                    {
                        textBox4.Text = string.Concat("CSV読込");
                        textBox1.Text = "先頭行は列名として表示";
                    }
                    this.dataGridView1.Rows.AddRange(rows);
                }
                else if (FILE_TYPE_XML == fileType)
                {
                    filename = READ_XML_PERSONAL_INFOMATION;
                    //　XMLの読み込み
                    XmlTextReader xmlDataReader = new XmlTextReader(filename);

                    DataSet ds = new DataSet();
                    ds.ReadXml(xmlDataReader);

                    this.dataGridView1.DataSource = ds.Tables[0];
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                string message = string.Concat("読込むファイルが見つかりません！\n", "ファイル名：", filename);
                MessageBox.Show(message, "dialog");
                return;
            }
            catch (System.IO.DirectoryNotFoundException)
            {
                string message = string.Concat("読込むファイルが見つかりません！\n", "ファイル名：", filename);
                MessageBox.Show(message, "dialog");
                return;

            }
        }

        private void Initial_Preparation()
        {
            //初期準備(データのクリア等)
            dataGridView1.DataSource = null; // 不要かも
            dataGridView1.Rows.Clear();
            Set_ColumnName(DEFAULT_DISPLAY, null);
            TextBox_Clear();
            dataGridView1.Refresh();

        }
        private void TextBox_Clear()
        {
            textBox1.Text = "";
            textBox4.Text = "";
            textBox1.Refresh();
            textBox4.Refresh();
        }

        private void Set_DataGridView_Columns_Visible(int method)
        {
            if (READ_CSV_FILE_READLINES_DATAGRIDVIEW_ROWS_ADD_ONELINE == method)
            {
                //DataGridView1の列を3から4を非表示にする
                dataGridView1.Columns[0].Visible = true;
                dataGridView1.Columns[1].Visible = true;
                dataGridView1.Columns[2].Visible = true;
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.Columns[4].Visible = false;
            }
            else if (READ_CSV_FILE_READLINES_DATAGRIDVIEW_ROWS_ADD_ALLLINE == method)
            {
                //DataGridView1の全列を表示にする
                dataGridView1.Columns[0].Visible = true;
                dataGridView1.Columns[1].Visible = true;
                dataGridView1.Columns[2].Visible = true;
                dataGridView1.Columns[3].Visible = true;
                dataGridView1.Columns[4].Visible = true;
            }
            else if (READ_CSV_TEXTFIELDPARSER == method)
            {
                //DataGridView1の列を1から4を非表示にする
                dataGridView1.Columns[0].Visible = true;
                dataGridView1.Columns[1].Visible = false;
                dataGridView1.Columns[2].Visible = false;
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.Columns[4].Visible = false;
            }
            dataGridView1.Refresh();
        }

        private void Set_ColumnName(int ColumnNameSettingFlag, string[] columnName)
        {
            // DefaultDisplay(ex:Column1):0
            // FirstLine of CSV:1
            if (DEFAULT_DISPLAY == ColumnNameSettingFlag)
            {
                int columnNo = 1;
                for (int i=0; i < 5; i++)
                {
                    dataGridView1.Columns[i].HeaderText = string.Concat("Column", columnNo);
                    columnNo++;
                }
                //this.Column1.HeaderText = "Column1";
                //this.Column2.HeaderText = "Column2";
                //this.Column3.HeaderText = "Column3";
                //this.Column4.HeaderText = "Column4";
                //this.Column5.HeaderText = "Column5";
            }
            else if (FIRSTLINE_OF_CSV == ColumnNameSettingFlag) 
            {
                int columnIndex = 0;
                foreach (string item in columnName)
                {
                    //itemをつかう
                    dataGridView1.Columns[columnIndex].HeaderText = item;
                    columnIndex++;
                }
            }
            dataGridView1.Refresh();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                this.button1.Enabled = true;
                this.button2.Enabled = true;
                this.button3.Enabled = true;
                this.button4.Enabled = true;
                this.checkBox1.Enabled = true;
                this.button5.Enabled = false;
                this.button6.Enabled = false;
                this.button7.Enabled = false;

            }
            else
            {
                this.button1.Enabled = false;
                this.button2.Enabled = false;
                this.button3.Enabled = false;
                this.button4.Enabled = false;
                this.checkBox1.Enabled = false;
                this.button5.Enabled = true;
                this.button6.Enabled = true;
                this.button7.Enabled = true;
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null; // 不要かも
            dataGridView1.Columns.Clear();

        }

        private void button7_Click(object sender, EventArgs e)
        {
 
            for (int i=0; i < 5 ; i++)
            {
                // 1.DataGridViewColumnオブジェクトを生成
                DataGridViewTextBoxColumn textColumn = new DataGridViewTextBoxColumn();

                // 2.セルに表示するデータソースの指定(DataRowの列名や、独自クラスのプロパティ名を指定)
                textColumn.DataPropertyName = string.Concat("col1");

                // 3.ヘッダーのテキスト
                textColumn.HeaderText = string.Concat("Column", i + 1);

                // 4.列の表示／非表示
                textColumn.Visible = true;

                // 5.列幅
                textColumn.Width = 100;

                // 6.読み取り専用
                textColumn.ReadOnly = true;

                // 7.ユーザーによる列幅の変更可否
                textColumn.Resizable = DataGridViewTriState.True;

                // 8.列を追加
                this.dataGridView1.Columns.Add(textColumn);

            }
        }
    }
}
