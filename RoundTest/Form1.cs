using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoundTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.labelRound149.Text = Math.Round(1.49).ToString();
            this.labelRound150.Text = Math.Round(1.50).ToString();
            this.labelRound151.Text = Math.Round(1.51).ToString();
            this.labelRound249.Text = Math.Round(2.49).ToString();
            this.labelRound250.Text = Math.Round(2.50).ToString();
            this.labelRound251.Text = Math.Round(2.51).ToString();
            this.labelRound349.Text = Math.Round(3.49).ToString();
            this.labelRound350.Text = Math.Round(3.50).ToString();
            this.labelRound351.Text = Math.Round(3.51).ToString();

            this.labelConvert149.Text = Convert.ToInt32(1.49).ToString();
            this.labelConvert150.Text = Convert.ToInt32(1.50).ToString();
            this.labelConvert151.Text = Convert.ToInt32(1.51).ToString();
            this.labelConvert249.Text = Convert.ToInt32(2.49).ToString();
            this.labelConvert250.Text = Convert.ToInt32(2.50).ToString();
            this.labelConvert251.Text = Convert.ToInt32(2.51).ToString();
            this.labelConvert349.Text = Convert.ToInt32(3.49).ToString();
            this.labelConvert350.Text = Convert.ToInt32(3.50).ToString();
            this.labelConvert351.Text = Convert.ToInt32(3.51).ToString();

            this.labelCast149.Text = ((int)1.49).ToString();
            this.labelCast150.Text = ((int)1.50).ToString();
            this.labelCast151.Text = ((int)1.99).ToString();
            this.labelCast249.Text = ((int)2.49).ToString();
            this.labelCast250.Text = ((int)2.50).ToString();
            this.labelCast251.Text = ((int)2.51).ToString();
            this.labelCast349.Text = ((int)3.49).ToString();
            this.labelCast350.Text = ((int)3.50).ToString();
            this.labelCast351.Text = ((int)3.51).ToString();
        }
    }
}
