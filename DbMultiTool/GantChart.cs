using System;
using System.Drawing;
using System.Windows.Forms;

namespace DbMultiTool
{
    public partial class GantChart : UserControl
    {
        public GantChart()
        {
            InitializeComponent();

            DataGridViewProgressBarColumn pbColumn1 = new DataGridViewProgressBarColumn
            {
                DataPropertyName = "Column1",
                HeaderText = "4/1"
            };
            dataGridView2.Columns.Add(pbColumn1);

            dataGridView2.Columns.Add("col2", "4/2");
            dataGridView2.Columns.Add("col3", "4/3");
            dataGridView2.Columns.Add("col4", "4/4");
            dataGridView2.Columns.Add("col5", "4/5");
            dataGridView2.Columns.Add("col6", "4/6");
            dataGridView2.Columns.Add("col7", "4/7");

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

        private void GantChart_Load(object sender, EventArgs e)
        {
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
        }

        private void dataGridView2_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            DataGridView dv = (DataGridView)sender;
            // 行・列共にヘッダは処理しない
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            Rectangle rect;
            DataGridViewCell cell;
            // 1列目の処理
            if (e.ColumnIndex == 0)
            {
                // 奇数行のみ列結合
                if (e.RowIndex % 2 == 0)
                {
                    rect = e.CellBounds;
                    cell = dv[e.ColumnIndex + 1, e.RowIndex];
                    // 一つ右のセルの幅を足す
                    rect.Width += cell.Size.Width;
                    rect.X -= 1;
                    rect.Y -= 1;
                    e.Graphics.FillRectangle(new SolidBrush(e.CellStyle.BackColor), rect);
                    e.Graphics.DrawRectangle(new Pen(dv.GridColor), rect);
                    ProgressBarRenderer.DrawHorizontalBar(e.Graphics, rect);
                    e.Handled = true;
                }
                else
                {
                    // ２列目の偶数行は、結合を行わないので、通常の描画処理に任せる
                    e.Paint(e.ClipBounds, e.PaintParts);
                }
            }
            else if(e.ColumnIndex == 2)
            {
                rect = e.CellBounds;
                cell = dv[e.ColumnIndex + 1, e.RowIndex];
                // 一つ右のセルの幅を足す
                rect.Width += cell.Size.Width;
                rect.X -= 1;
                rect.Y -= 1;
                e.Graphics.FillRectangle(new SolidBrush(e.CellStyle.BackColor), rect);
                e.Graphics.DrawRectangle(new Pen(dv.GridColor), rect);
                TextRenderer.DrawText(e.Graphics,
                                      e.FormattedValue.ToString(),
                                      e.CellStyle.Font, 
                                      rect, 
                                      e.CellStyle.ForeColor,
                                      TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                e.Handled = true;
            }
        }
    }
}
