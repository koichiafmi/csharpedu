using System;
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
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("こんにちは！", "dialog");
            //Stopwatchオブジェクトを作成する
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            //ストップウォッチを開始する
            sw.Start();
            TextFieldParser parser;
            int rowCount = 0;
            try
            {
                parser = new TextFieldParser(@"C:\Sample002.csv", Encoding.GetEncoding("Shift_JIS"));
            }
            catch (System.IO.FileNotFoundException ex)
            {
                //ストップウォッチを止める
                sw.Stop();
                MessageBox.Show("読込むファイルが見つかりません！", "dialog");
                return;
            }

            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(","); // 区切り文字はコンマ

            // データをすべてクリア
            dataGridView1.Rows.Clear();

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
            textBox1.Text = string.Concat("CSV読込1-経過時間[", rowCount,"行][ ", sw.Elapsed.Hours.ToString("00"), ":", sw.Elapsed.Minutes.ToString("00"), ":", sw.Elapsed.Seconds.ToString("00"), ":", sw.ElapsedMilliseconds.ToString("000"), " ]");
            //sw.Reset();
        }
    }
}
