using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = string.Empty;

            var x = new ClassA();
            x.intVal = 1;
            x.doubleVal = 2.2;
            x.stringVal = "Mori";

            this.textBox1.Text = "x: " + x.ToString();

            var y = x;
            y.intVal = 11;
            y.doubleVal = 22.22;
            y.stringVal = "Ryuichi";

            this.textBox1.Text += Environment.NewLine;
            this.textBox1.Text += "x: " + x.ToString();
            this.textBox1.Text += Environment.NewLine;
            this.textBox1.Text += "y: " + y.ToString();

            this.textBox1.Text += Environment.NewLine;
            this.textBox1.Text += "x.HashCode = " + x.GetHashCode().ToString();
            this.textBox1.Text += Environment.NewLine;
            this.textBox1.Text += "y.HashCode = " + y.GetHashCode().ToString();

            this.calc(y);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = string.Empty;

            var x = new ClassB();
            x.intVal = 1;
            x.doubleVal = 2.2;
            x.stringVal = "Mori";

            this.textBox1.Text = "x: " + x.ToString();

            var y = x;
            y.intVal = 11;
            y.doubleVal = 22.22;
            y.stringVal = "Ryuichi";

            this.textBox1.Text += Environment.NewLine;
            this.textBox1.Text += "x: " + x.ToString();
            this.textBox1.Text += Environment.NewLine;
            this.textBox1.Text += "y: " + y.ToString();

            this.textBox1.Text += Environment.NewLine;
            this.textBox1.Text += "x.HashCode = " + x.GetHashCode().ToString();
            this.textBox1.Text += Environment.NewLine;
            this.textBox1.Text += "y.HashCode = " + y.GetHashCode().ToString();

            this.calc(y);
        }

        public void calc(ClassA a)
        {
            var z = a;
            z.intVal = 111;
            z.doubleVal = 222.222;
            z.stringVal = "ALPS";

            this.textBox1.Text += Environment.NewLine;
            this.textBox1.Text += "a: " + a.ToString();
            this.textBox1.Text += Environment.NewLine;
            this.textBox1.Text += "z: " + z.ToString();

            this.textBox1.Text += Environment.NewLine;
            this.textBox1.Text += "a.HashCode = " + a.GetHashCode().ToString();
            this.textBox1.Text += Environment.NewLine;
            this.textBox1.Text += "z.HashCode = " + z.GetHashCode().ToString();
        }

        public void calc(ClassB b)
        {
            var z = b;
            z.intVal = 111;
            z.doubleVal = 222.222;
            z.stringVal = "ALPS";

            this.textBox1.Text += Environment.NewLine;
            this.textBox1.Text += "b: " + b.ToString();
            this.textBox1.Text += Environment.NewLine;
            this.textBox1.Text += "z: " + z.ToString();

            this.textBox1.Text += Environment.NewLine;
            this.textBox1.Text += "b.HashCode = " + b.GetHashCode().ToString();
            this.textBox1.Text += Environment.NewLine;
            this.textBox1.Text += "z.HashCode = " + z.GetHashCode().ToString();
        }
    }
}
