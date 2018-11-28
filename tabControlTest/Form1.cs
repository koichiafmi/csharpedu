using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tabControlTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBoxT1_Validating(object sender, CancelEventArgs e)
        {
            var textBox = (sender as TextBox);
            if (string.IsNullOrEmpty(textBox.Text))
            {
                e.Cancel = true;
                MessageBox.Show("Please enter texts.");
                textBox.Select(0, textBox.Text.Length);
                return;
            }
        }

        private void textBoxT2_Validating(object sender, CancelEventArgs e)
        {
            var textBox = (sender as TextBox);
            if (string.IsNullOrEmpty(textBox.Text))
            {
                e.Cancel = true;
                MessageBox.Show("Please enter texts.");
                textBox.Focus();
                textBox.Select(0, textBox.Text.Length);
                return;
            }
        }
    }
}
