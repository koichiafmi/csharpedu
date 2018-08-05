using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommonParts
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
            FileOperation fileOperation = new FileOperation();
            string fileName = fileOperation.GetFilePathUseFileDialog();
            textBox1.Text = string.Concat(fileName);
            int columnsCount = fileOperation.GetColumnsCount(fileName);
            DataGridViewRow[] rows = fileOperation.ReadCsvFileUseSystemIoFile(fileName,this.dataGridView1);
            this.dataGridView1.Rows.AddRange(rows);
            */
        }
    }
}
