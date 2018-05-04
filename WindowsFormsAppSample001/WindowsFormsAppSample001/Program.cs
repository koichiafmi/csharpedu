using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsAppSample001
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary?
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        class Form1 : Form
        {
            Button button1;
            int count = 0;

            public Form1()
            {
                this.Width = 245;
                this.Height = 100;
                this.Text = "sample001";


                this.button1 = new Button();
                this.button1.Location = new Point(10, 10);
                this.button1.Size = new Size(170, 30);
                this.button1.Text = "ここを押して";

                this.button1.Click += new EventHandler(this.Button1_Click);

                this.Controls.Add(this.button1);
            }

            void Button1_Click(object sender, EventArgs e)
            {
                this.count++;
                this.button1.Text = this.count.ToString();
            }
        }
    }
}
