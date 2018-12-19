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

            //DataGridViewProgressBarColumn pbColumn1 = new DataGridViewProgressBarColumn
            ////DgvMergebleTextBoxColumn pbColumn1 = new DgvMergebleTextBoxColumn
            //{
            //    DataPropertyName = "Column1",
            //    HeaderText = "4/1"
            //};
            //dataGridView2.Columns.Add(pbColumn1);


            //dataGridView2.Columns.Add("col2", "4/2");
            //dataGridView2.Columns.Add("col3", "4/3");
            //dataGridView2.Columns.Add("col4", "4/4");
            //dataGridView2.Columns.Add("col5", "4/5");
            //dataGridView2.Columns.Add("col6", "4/6");
            //dataGridView2.Columns.Add("col7", "4/7");

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

            dataGridView2.Columns.Add("4/1", "4/1");
            dataGridView2.Columns.Add("4/2", "4/2");
            dataGridView2.Columns.Add("4/3", "4/3");
            dataGridView2.Columns.Add("4/4", "4/4");

            for (int i = 0; i < 10; i++)
            {
                dataGridView2.Rows.Add(25, 25, 25, 25);
            }

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
            switch (e.RowIndex)
            {
                case 0:
                    //1列目のみ
                    if (e.ColumnIndex == 0)
                    {
                        rect = e.CellBounds;
                        rect.X -= 1;
                        rect.Y -= 1;
                        e.Graphics.FillRectangle(new SolidBrush(e.CellStyle.BackColor), rect);
                        e.Graphics.DrawRectangle(new Pen(dv.GridColor), rect);
                        //プログレスバー（後ろの灰色部分）レンダリング
                        ProgressBarRenderer.DrawHorizontalBar(e.Graphics, rect);
                        //プログレスバー（緑色の実績部分）レンダリング
                        rect.Width = rect.Width * ((int)e.Value) / 100;
                        ProgressBarRenderer.DrawHorizontalChunks(e.Graphics, rect);
                        e.Handled = true;
                    }
                    else
                    {
                        dv[e.ColumnIndex, e.RowIndex].Value = string.Empty;
                        e.Paint(e.ClipBounds, e.PaintParts);
                    }
                    break;
                case 1:
                    //1列目と2列目を結合してみる
                    if (e.ColumnIndex == 0)
                    {
                        rect = e.CellBounds;
                        rect.Width += dv[e.ColumnIndex + 1, e.RowIndex].Size.Width;
                        rect.X -= 1;
                        rect.Y -= 1;
                        e.Graphics.FillRectangle(new SolidBrush(e.CellStyle.BackColor), rect);
                        e.Graphics.DrawRectangle(new Pen(dv.GridColor), rect);
                        //プログレスバー（後ろの灰色部分）レンダリング
                        ProgressBarRenderer.DrawHorizontalBar(e.Graphics, rect);
                        //プログレスバー（緑色の実績部分）レンダリング
                        rect.Width = rect.Width * ((int)e.Value) / 100;
                        ProgressBarRenderer.DrawHorizontalChunks(e.Graphics, rect);
                        e.Handled = true;
                    }
                    else if (e.ColumnIndex == 1)
                    {
                        e.Handled = true;
                    }
                    else
                    {
                        dv[e.ColumnIndex, e.RowIndex].Value = string.Empty;
                        e.Paint(e.ClipBounds, e.PaintParts);
                    }
                    break;
                case 2:
                    //1列目と2列目と3列目を結合してみる
                    if (e.ColumnIndex == 0)
                    {
                        rect = e.CellBounds;
                        rect.Width += dv[e.ColumnIndex + 1, e.RowIndex].Size.Width;
                        rect.Width += dv[e.ColumnIndex + 2, e.RowIndex].Size.Width;
                        rect.X -= 1;
                        rect.Y -= 1;
                        e.Graphics.FillRectangle(new SolidBrush(e.CellStyle.BackColor), rect);
                        e.Graphics.DrawRectangle(new Pen(dv.GridColor), rect);
                        //プログレスバー（後ろの灰色部分）レンダリング
                        ProgressBarRenderer.DrawHorizontalBar(e.Graphics, rect);
                        //プログレスバー（緑色の実績部分）レンダリング
                        rect.Width = rect.Width * ((int)e.Value) / 100;
                        ProgressBarRenderer.DrawHorizontalChunks(e.Graphics, rect);
                        e.Handled = true;
                    }
                    else if (e.ColumnIndex == 1 || e.ColumnIndex == 2)
                    {
                        e.Handled = true;
                    }
                    else
                    {
                        dv[e.ColumnIndex, e.RowIndex].Value = string.Empty;
                        e.Paint(e.ClipBounds, e.PaintParts);
                    }
                    break;
                case 3:
                    //1列目と2列目と3列目と4列目を結合してみる
                    if (e.ColumnIndex == 0)
                    {
                        rect = e.CellBounds;
                        rect.Width += dv[e.ColumnIndex + 1, e.RowIndex].Size.Width;
                        rect.Width += dv[e.ColumnIndex + 2, e.RowIndex].Size.Width;
                        rect.Width += dv[e.ColumnIndex + 3, e.RowIndex].Size.Width;
                        rect.X -= 1;
                        rect.Y -= 1;
                        e.Graphics.FillRectangle(new SolidBrush(e.CellStyle.BackColor), rect);
                        e.Graphics.DrawRectangle(new Pen(dv.GridColor), rect);
                        //プログレスバー（後ろの灰色部分）レンダリング
                        ProgressBarRenderer.DrawHorizontalBar(e.Graphics, rect);
                        //プログレスバー（緑色の実績部分）レンダリング
                        rect.Width = rect.Width * ((int)e.Value) / 100;
                        ProgressBarRenderer.DrawHorizontalChunks(e.Graphics, rect);
                        e.Handled = true;
                    }
                    else if (e.ColumnIndex == 1 || e.ColumnIndex == 2 || e.ColumnIndex == 3)
                    {
                        e.Handled = true;
                    }
                    else
                    {
                        dv[e.ColumnIndex, e.RowIndex].Value = string.Empty;
                        e.Paint(e.ClipBounds, e.PaintParts);
                    }
                    break;
                case 4:
                    //2列目のみ
                    if (e.ColumnIndex == 1)
                    {
                        rect = e.CellBounds;
                        rect.X -= 1;
                        rect.Y -= 1;
                        e.Graphics.FillRectangle(new SolidBrush(e.CellStyle.BackColor), rect);
                        e.Graphics.DrawRectangle(new Pen(dv.GridColor), rect);
                        //プログレスバー（後ろの灰色部分）レンダリング
                        ProgressBarRenderer.DrawHorizontalBar(e.Graphics, rect);
                        //プログレスバー（緑色の実績部分）レンダリング
                        rect.Width = rect.Width * ((int)e.Value) / 100;
                        ProgressBarRenderer.DrawHorizontalChunks(e.Graphics, rect);
                        e.Handled = true;
                    }
                    else
                    {
                        dv[e.ColumnIndex, e.RowIndex].Value = string.Empty;
                        e.Paint(e.ClipBounds, e.PaintParts);
                    }
                    break;
                case 5:
                    //2列目と3列目を結合してみる
                    if (e.ColumnIndex == 1)
                    {
                        rect = e.CellBounds;
                        rect.Width += dv[e.ColumnIndex + 1, e.RowIndex].Size.Width;
                        rect.X -= 1;
                        rect.Y -= 1;
                        e.Graphics.FillRectangle(new SolidBrush(e.CellStyle.BackColor), rect);
                        e.Graphics.DrawRectangle(new Pen(dv.GridColor), rect);
                        //プログレスバー（後ろの灰色部分）レンダリング
                        ProgressBarRenderer.DrawHorizontalBar(e.Graphics, rect);
                        //プログレスバー（緑色の実績部分）レンダリング
                        rect.Width = rect.Width * ((int)e.Value) / 100;
                        ProgressBarRenderer.DrawHorizontalChunks(e.Graphics, rect);
                        e.Handled = true;
                    }
                    else if (e.ColumnIndex == 2)
                    {
                        e.Handled = true;
                    }
                    else
                    {
                        dv[e.ColumnIndex, e.RowIndex].Value = string.Empty;
                        e.Paint(e.ClipBounds, e.PaintParts);
                    }
                    break;
                case 6:
                    //2列目と3列目と4列目を結合してみる
                    if (e.ColumnIndex == 1)
                    {
                        rect = e.CellBounds;
                        rect.Width += dv[e.ColumnIndex + 1, e.RowIndex].Size.Width;
                        rect.Width += dv[e.ColumnIndex + 2, e.RowIndex].Size.Width;
                        rect.X -= 1;
                        rect.Y -= 1;
                        e.Graphics.FillRectangle(new SolidBrush(e.CellStyle.BackColor), rect);
                        e.Graphics.DrawRectangle(new Pen(dv.GridColor), rect);
                        //プログレスバー（後ろの灰色部分）レンダリング
                        ProgressBarRenderer.DrawHorizontalBar(e.Graphics, rect);
                        //プログレスバー（緑色の実績部分）レンダリング
                        rect.Width = rect.Width * ((int)e.Value) / 100;
                        ProgressBarRenderer.DrawHorizontalChunks(e.Graphics, rect);
                        e.Handled = true;
                    }
                    else if(e.ColumnIndex == 2 || e.ColumnIndex == 3)
                    {
                        e.Handled = true;
                    }
                    else
                    {
                        dv[e.ColumnIndex, e.RowIndex].Value = string.Empty;
                        e.Paint(e.ClipBounds, e.PaintParts);
                    }
                    break;
                case 7:
                    //3列目のみ
                    if (e.ColumnIndex == 2)
                    {
                        rect = e.CellBounds;
                        rect.X -= 1;
                        rect.Y -= 1;
                        e.Graphics.FillRectangle(new SolidBrush(e.CellStyle.BackColor), rect);
                        e.Graphics.DrawRectangle(new Pen(dv.GridColor), rect);
                        //プログレスバー（後ろの灰色部分）レンダリング
                        ProgressBarRenderer.DrawHorizontalBar(e.Graphics, rect);
                        //プログレスバー（緑色の実績部分）レンダリング
                        rect.Width = rect.Width * ((int)e.Value) / 100;
                        ProgressBarRenderer.DrawHorizontalChunks(e.Graphics, rect);
                        e.Handled = true;
                    }
                    else
                    {
                        dv[e.ColumnIndex, e.RowIndex].Value = string.Empty;
                        e.Paint(e.ClipBounds, e.PaintParts);
                    }
                    break;
                case 8:
                    //3列目と4列目を結合してみる
                    if (e.ColumnIndex == 2)
                    {
                        rect = e.CellBounds;
                        rect.Width += dv[e.ColumnIndex + 1, e.RowIndex].Size.Width;
                        rect.X -= 1;
                        rect.Y -= 1;
                        e.Graphics.FillRectangle(new SolidBrush(e.CellStyle.BackColor), rect);
                        e.Graphics.DrawRectangle(new Pen(dv.GridColor), rect);
                        //プログレスバー（後ろの灰色部分）レンダリング
                        ProgressBarRenderer.DrawHorizontalBar(e.Graphics, rect);
                        //プログレスバー（緑色の実績部分）レンダリング
                        rect.Width = rect.Width * ((int)e.Value) / 100;
                        ProgressBarRenderer.DrawHorizontalChunks(e.Graphics, rect);
                        e.Handled = true;
                    }
                    else if (e.ColumnIndex == 3)
                    {
                        e.Handled = true;
                    }
                    else
                    {
                        dv[e.ColumnIndex, e.RowIndex].Value = string.Empty;
                        e.Paint(e.ClipBounds, e.PaintParts);
                    }
                    break;
                default:
                    // 通常の描画処理
                    dv[e.ColumnIndex, e.RowIndex].Value = string.Empty;
                    e.Paint(e.ClipBounds, e.PaintParts);
                    break;
            }
        }
    }
}
