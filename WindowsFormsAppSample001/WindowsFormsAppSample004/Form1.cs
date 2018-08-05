using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommonParts;

namespace WindowsFormsAppSample004
{
    public partial class Form1 : Form
    {
        private DataGridViewSpecificFunction dataGridViewSpecificFunctio;
        public System.Diagnostics.Stopwatch sw;

        public Form1()
        {
            InitializeComponent();
            dataGridViewSpecificFunctio = new DataGridViewSpecificFunction();
            if (checkBox1.Checked == true)
            {
                dataGridViewSpecificFunctio.SetFileType(CommonParts.Constants.FILE_TYPE_CSV);
            }

            if (checkBox5.Checked == true)
            {
                dataGridViewSpecificFunctio.SetAddRangeFlag(CommonParts.Constants.FLAG_BOOL_TYPE_ON);
            }
            //ストップウォッチを開始する
            sw = System.Diagnostics.Stopwatch.StartNew();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            OpenFileDialog ofd = dataGridViewSpecificFunctio.GetFilePath();
            // 選択されたファイルをテキストボックスに表示する
            try
            {
                foreach (string strFilePath in ofd.FileNames)
                {
                    // ファイルパスからファイル名を取得
                    string strFileName = System.IO.Path.GetFileName(strFilePath);
                    textBox1.Text += strFileName + "\r\n";
                }
                if (checkBox5.Checked)
                {
                    this.button4.Enabled = true;
                }
            }
            catch
            {
                string message = string.Concat("エラーが発生しました。", e.ToString());
                MessageBox.Show(message, "Err");
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                this.button1.Text = "CSVファイル\r\n検索";
                this.checkBox2.Visible = true;
                this.checkBox3.Visible = true;
                dataGridViewSpecificFunctio.SetFileType(CommonParts.Constants.FILE_TYPE_CSV);
            }
            else
            {
                this.button1.Text = "XMLファイル\r\n検索";
                this.checkBox2.Visible = false;
                this.checkBox3.Visible = false;
                dataGridViewSpecificFunctio.SetFileType(CommonParts.Constants.FILE_TYPE_XML);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // DataGridView初期化（データクリア）
            if (this.checkBox5.Checked)
            {
                dataGridView1.Columns.Clear();
                dataGridView1.Rows.Clear();
            }
            else
            {
                dataGridView1.DataSource = "";
            }

            textBox1.Text = "";
            textBox2.Text = "";
            this.button3.Enabled = false;
            this.button4.Enabled = false;
            this.button5.Enabled = false;
            this.button7.Enabled = false;
            this.button8.Enabled = false;
        }



        private void button4_Click(object sender, EventArgs e)
        {
            this.button4.Enabled = false;
            // DataGridViewの列情報作成
            dataGridViewSpecificFunctio.CreateColumn(this.dataGridView1);
            this.button3.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            sw.Reset();
            sw.Start();

            this.button3.Enabled = false;
            // ファイル読込
            dataGridViewSpecificFunctio.ReadFile(textBox1);
            this.button5.Enabled = true;
            sw.Stop();
            textBox1.Text += string.Concat("読込：",sw.Elapsed,"\r\n");
        }


        private void button5_Click(object sender, EventArgs e)
        {
            sw.Reset();
            sw.Start();
            this.button5.Enabled = false;
            this.Enabled = false;
            // データ反映
            dataGridViewSpecificFunctio.Set_DisplayData();
            this.Enabled = true;
            if (this.checkBox5.Checked)
            {
                DataGridViewDisplayCheck();
            }
            sw.Stop();
            textBox1.Text += string.Concat("反映：", sw.Elapsed,"\r\n");
            this.button13.Enabled = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            this.button5.Enabled = true;
            this.button7.Enabled = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            dataGridViewSpecificFunctio.SortData();
        }

        private void DataGridViewDisplayCheck()
        {
            if (dataGridView1.Rows[0].Displayed)
            {
                this.button3.Enabled = false;
                this.button4.Enabled = false;
                this.button5.Enabled = false;
                this.button7.Enabled = true;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string textValue = textBox2.Text;
            if (textValue == "")
            {
                MessageBox.Show("検索キーを入力してください.");
                return;
            }
            dataGridViewSpecificFunctio.DataSearch();
            this.button8.Enabled = true;
            this.button12.Enabled = true;
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
            {
                dataGridViewSpecificFunctio.SetAddRangeFlag(CommonParts.Constants.FLAG_BOOL_TYPE_ON);
                this.checkBox2.Enabled = true;
                this.checkBox3.Enabled = true;
                this.button9.Enabled = false;
                this.button10.Enabled = false;
                this.button11.Enabled = false;
            }
            else
            {
                dataGridViewSpecificFunctio.SetAddRangeFlag(CommonParts.Constants.FLAG_BOOL_TYPE_OFF);
                this.checkBox3.Checked = false;
                dataGridViewSpecificFunctio.SetMultiselectFlag(CommonParts.Constants.FLAG_BOOL_TYPE_OFF);
                this.checkBox2.Checked = false;
                dataGridViewSpecificFunctio.SetFirstlLneOfFileIsColumnNameFlag(CommonParts.Constants.FLAG_BOOL_TYPE_OFF);
                this.checkBox2.Enabled = false;
                this.checkBox3.Enabled = false;

                this.button9.Enabled = true;
                this.button10.Enabled = true;
                this.button11.Enabled = true;
                this.button4.Enabled = false;
                this.button3.Enabled = false;
                this.button5.Enabled = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                dataGridViewSpecificFunctio.SetMultiselectFlag(CommonParts.Constants.FLAG_BOOL_TYPE_ON);
                this.checkBox2.Checked = false;
                dataGridViewSpecificFunctio.SetFirstlLneOfFileIsColumnNameFlag(CommonParts.Constants.FLAG_BOOL_TYPE_OFF);
                this.checkBox2.Enabled = false;
            }
            else
            {
                dataGridViewSpecificFunctio.SetMultiselectFlag(CommonParts.Constants.FLAG_BOOL_TYPE_OFF);
                this.checkBox2.Enabled = true;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                dataGridViewSpecificFunctio.SetFirstlLneOfFileIsColumnNameFlag(CommonParts.Constants.FLAG_BOOL_TYPE_ON);
            }
            else
            {
                dataGridViewSpecificFunctio.SetFirstlLneOfFileIsColumnNameFlag(CommonParts.Constants.FLAG_BOOL_TYPE_OFF);
            }

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                this.textBox5.Text = string.Concat(dataGridView1.CurrentCell.RowIndex + 1);
            }
            catch (System.NullReferenceException)
            {
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.button9.Enabled = false;
            this.button10.Enabled = false;
            this.button11.Enabled = false;
            sw.Reset();
            sw.Start();
            dataGridViewSpecificFunctio.SetDataGridView(this.dataGridView1);
            dataGridViewSpecificFunctio.ReadFileUsingOLEdbcProvider(this.textBox1);
            this.button5.Enabled = true;
            sw.Stop();
            textBox1.Text += string.Concat("読込(OleDbConnection)：", sw.Elapsed, "\r\n");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.button9.Enabled = false;
            this.button10.Enabled = false;
            this.button11.Enabled = false;
            sw.Reset();
            sw.Start();
            dataGridViewSpecificFunctio.SetDataGridView(this.dataGridView1);
            dataGridViewSpecificFunctio.ReadFileUsingOdbcProvider(this.textBox1);
            this.button5.Enabled = true;
            sw.Stop();
            textBox1.Text += string.Concat("読込(OdbConnection)：", sw.Elapsed, "\r\n");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.button9.Enabled = false;
            this.button10.Enabled = false;
            this.button11.Enabled = false;
            sw.Reset();
            sw.Start();
            dataGridViewSpecificFunctio.SetDataGridView(this.dataGridView1);
            dataGridViewSpecificFunctio.ReadFileUsingJetProvider(this.textBox1);
            this.button5.Enabled = true;
            sw.Stop();
            textBox1.Text += string.Concat("読込(JetConnection)：", sw.Elapsed, "\r\n");

        }

        private void button12_Click(object sender, EventArgs e)
        {
            dataGridViewSpecificFunctio.Select();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            string textValue = textBox2.Text;
            if (textValue == "")
            {
                MessageBox.Show("検索キーを入力してください.");
                return;
            }
            dataGridViewSpecificFunctio.SetSearchkey(textValue, "", "");
            if (checkBox5.Checked)
            {
                this.button8.Enabled = true;
            }
            else
            {
                this.button12.Enabled = true;
            }
        }
    }
}
