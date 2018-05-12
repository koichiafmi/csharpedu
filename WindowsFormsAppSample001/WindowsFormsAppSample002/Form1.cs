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

namespace WindowsFormsAppSample002
{
    public partial class Form1 : Form
    {
        private String READ_FILE_USED_IN_VISUALBASIC_FILEIO = @"C:\Sample002.csv";
        private String READ_FILE_USED_IN_FILEIO = @"C:\Sample002_5column10000rows.csv";
        //private String READ_FILE_USED_IN_FILEIO  = @"C:\Sample002_5column100rows.csv";

        private int READ_CSV_TEXTFIELDPARSER = 2; // Microsoft.VisualBasic.FileIO : dataGridViewに一行ずつADD
        private int READ_CSV_FILE_READLINES_DATAGRIDVIEW_ROWS_ADD_ONELINE = 0; // System.IO : dataGridViewに一行ずつADD
        private int READ_CSV_FILE_READLINES_DATAGRIDVIEW_ROWS_ADD_ALLLINE = 1; // System.IO : dataGridViewに一括ADD


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // データをすべてクリア
            dataGridView1.Rows.Clear();
            this.TextBox_Clear();
            this.Set_DataGridView_Columns_Visible(READ_CSV_TEXTFIELDPARSER);
            this.DataLoad(READ_CSV_TEXTFIELDPARSER);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            // データをすべてクリア
            dataGridView1.Rows.Clear();
            this.TextBox_Clear();
            this.Set_DataGridView_Columns_Visible(READ_CSV_FILE_READLINES_DATAGRIDVIEW_ROWS_ADD_ONELINE);
            this.DataLoad(READ_CSV_FILE_READLINES_DATAGRIDVIEW_ROWS_ADD_ONELINE);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // データをすべてクリア
            dataGridView1.Rows.Clear();
            this.TextBox_Clear();
            this.Set_DataGridView_Columns_Visible(READ_CSV_FILE_READLINES_DATAGRIDVIEW_ROWS_ADD_ALLLINE);
            this.DataLoad(READ_CSV_FILE_READLINES_DATAGRIDVIEW_ROWS_ADD_ALLLINE);
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
                    textBox2.Text = string.Concat("CSV読込2-経過時間[", rowCount, "行][ ", sw.Elapsed.Hours.ToString("00"), ":", sw.Elapsed.Minutes.ToString("00"), ":", sw.Elapsed.Seconds.ToString("00"), ":", sw.ElapsedMilliseconds.ToString("000"), " ]");
                    textBox4.Text = "System.IOクラス使用 : dataGridViewに一行ずつADD";
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
                    textBox3.Text = string.Concat("CSV読込3-経過時間[", rowCount, "行][ ", sw.Elapsed.Hours.ToString("00"), ":", sw.Elapsed.Minutes.ToString("00"), ":", sw.Elapsed.Seconds.ToString("00"), ":", sw.ElapsedMilliseconds.ToString("000"), " ]");
                    textBox4.Text = "System.IOクラス使用 : dataGridViewに一括ADD";
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
                    textBox1.Text = string.Concat("CSV読込1-経過時間[", rowCount, "行][ ", sw.Elapsed.Hours.ToString("00"), ":", sw.Elapsed.Minutes.ToString("00"), ":", sw.Elapsed.Seconds.ToString("00"), ":", sw.ElapsedMilliseconds.ToString("000"), " ]");
                    textBox4.Text = "TextFieldParserクラスを使用したCSV読込";                }
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

        private void TextBox_Clear()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
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

        }
    }
}
