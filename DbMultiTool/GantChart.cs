using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DbMultiTool
{
    public partial class GantChart : UserControl
    {
        public GantChart()
        {
            InitializeComponent();
        }

        private void GantChart_Load(object sender, EventArgs e)
        {
            DataGridViewProgressBarColumn pbColumn = new DataGridViewProgressBarColumn();
            pbColumn.DataPropertyName = "Column1";
            dataGridView2.Columns.Add(pbColumn);

            dataGridView2.Columns.Add("col2", "col2");
            dataGridView2.Columns.Add("col3", "col3");
            dataGridView2.Columns.Add("col4", "col4");
            dataGridView2.Columns.Add("col5", "col5");
            dataGridView2.Columns.Add("col6", "col6");
            dataGridView2.Columns.Add("col7", "col7");

            treeView1.ExpandAll();

            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();

            dataGridView2.Rows.Add();
            dataGridView2.Rows.Add();
            dataGridView2.Rows.Add();
            dataGridView2.Rows.Add();
            dataGridView2.Rows.Add();
            dataGridView2.Rows.Add();
            dataGridView2.Rows.Add();
            dataGridView2.Rows.Add();
            dataGridView2.Rows.Add();
            dataGridView2.Rows.Add();
            dataGridView2.Rows.Add();
            dataGridView2.Rows.Add();
            dataGridView2.Rows.Add();
            dataGridView2.Rows.Add();
            dataGridView2.Rows.Add();
            dataGridView2.Rows.Add();
            dataGridView2.Rows.Add();
            dataGridView2.Rows.Add();
            dataGridView2.Rows.Add();
        }
    }
}
