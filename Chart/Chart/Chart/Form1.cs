using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Chart
{
    public partial class HearthLogger : Form
    {
        public string SetData
        {
            set
            {
                Date.Text = value;
            }
        }
 
        public HearthLogger()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double weight;
            double fat;
            if (double.TryParse(Weight.Text, out weight) == true &&
                double.TryParse(Fat.Text, out fat) == true)
            {
                using (StreamWriter writer = new StreamWriter("data.txt", true))
                //書き込む先のファイル名は"data.text"
                {
                    writer.Write(Date.Text);　//textboxの内容を書き込む
                    writer.Write(",");　　　　　　　　　　//任意（ここではカンマ）のTextを書き込む。
                    writer.Write(Weight.Text);
                    writer.Write(",");
                    writer.WriteLine(Fat.Text);
                    writer.Flush();
                    Weight.Text = "";　　//入力欄であるtextboxをクリア
                    Fat.Text = "";
                }
            }
            Form2 Form2 = new Form2();   //グラフのインスタンス生成
            if (Chart.Checked == true)　　//Checkbox判断
            {
                if (Form2.ShowDialog() == DialogResult.OK) //OKボタンが押されたら
                {

                }
            }

            Form2.Dispose();
        }

        private void HearthLogger_Load(object sender, EventArgs e)
        {   //Form1が開いたときの動作
            DateTime dt = DateTime.Now;
            Date.Text = dt.ToString("yyyy/MM/dd");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Calendar carender = new Calendar();
            carender.Show();
        }
    }
}
