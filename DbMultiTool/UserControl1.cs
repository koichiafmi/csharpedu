using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace いろいろテスト_CS
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            DataGridView dv = (DataGridView)sender;
            // 行・列共にヘッダは処理しない
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            Rectangle rect;
            DataGridViewCell cell;
            // 1列目の処理
            if (e.ColumnIndex == 0)
            {
                rect = e.CellBounds;
                // 奇数行(1,3,5..行目 = RowIndexは0,2,4..)
                if (e.RowIndex % 2 == 0)
                {
                    cell = dataGridView1[e.ColumnIndex, e.RowIndex + 1];
                    //一つ下の次のセルの高さを足す
                    rect.Height += cell.Size.Height;
                }
                // 偶数行の処理
                else
                {
                    cell = dataGridView1[e.ColumnIndex, e.RowIndex - 1];
                    // 一つ上のセルの高さを足し、矩形の座標も一つ上のセルに合わす
                    rect.Height += cell.Size.Height;
                    rect.Y -= cell.Size.Height;
                }
                // セルボーダーライン分矩形の位置を補正
                rect.X -= 1;
                rect.Y -= 1;
                // 背景、セルボーダーライン、セルの値を描画
                e.Graphics.FillRectangle(
                new SolidBrush(e.CellStyle.BackColor), rect);
                e.Graphics.DrawRectangle(
                new Pen(dv.GridColor), rect);
                TextRenderer.DrawText(e.Graphics,
                cell.FormattedValue.ToString(),
                e.CellStyle.Font, rect, e.CellStyle.ForeColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                // イベント　ハンドラ内で処理を行ったことを通知
                e.Handled = true;
            }
            // ２列目と３列目の結合処理
            else if (e.ColumnIndex == 1)
            {
                // 奇数行のみ列結合
                if (e.RowIndex % 2 == 0)
                {
                    rect = e.CellBounds;
                    cell = dataGridView1[e.ColumnIndex + 1, e.RowIndex];
                    // 一つ右のセルの幅を足す
                    rect.Width += cell.Size.Width;
                    rect.X -= 1;
                    rect.Y -= 1;
                    e.Graphics.FillRectangle(new SolidBrush(e.CellStyle.BackColor), rect);
                    e.Graphics.DrawRectangle(new Pen(dv.GridColor), rect);
                    ProgressBarRenderer.DrawHorizontalBar(e.Graphics, rect);
                    //TextRenderer.DrawText(e.Graphics,
                    //e.FormattedValue.ToString(),
                    //e.CellStyle.Font, rect, e.CellStyle.ForeColor,
                    //TextFormatFlags.HorizontalCenter
                    //| TextFormatFlags.VerticalCenter);
                    e.Handled = true;
                }
                else
                {
                    // ２列目の偶数行は、結合を行わないので、通常の描画処理に任せる
                    e.Paint(e.ClipBounds, e.PaintParts);
                }
            }
            else
            {
                // ３列目の奇数行は描画処理をせずに、
                // イベントハンドラ内で処理を完了したこと通知
                if (e.RowIndex % 2 == 0 && e.ColumnIndex == 2)
                {
                    e.Handled = true;
                }
            }
        }
    }
}
