using System;
using System.Drawing;
using System.Windows.Forms;

/// <summary>
/// DataGridViewProgressBarCellオブジェクトの列
/// </summary>
public class DataGridViewProgressBarColumn : DataGridViewTextBoxColumn
{
    //コンストラクタ
    public DataGridViewProgressBarColumn()
    {
        this.CellTemplate = new DataGridViewProgressBarCell();
    }

    //CellTemplateの取得と設定
    public override DataGridViewCell CellTemplate
    {
        get
        {
            return base.CellTemplate;
        }
        set
        {
            //DataGridViewProgressBarCell以外はホストしない
            if (!(value is DataGridViewProgressBarCell))
            {
                throw new InvalidCastException("DataGridViewProgressBarCellオブジェクトを指定してください。");
            }
            base.CellTemplate = value;
        }
    }

    /// <summary>
    /// ProgressBarの最大値
    /// </summary>
    public int Maximum
    {
        get
        {
            return ((DataGridViewProgressBarCell)this.CellTemplate).Maximum;
        }
        set
        {
            if (Maximum == value)
            {
                return;
            }
            //セルテンプレートの値を変更する
            ((DataGridViewProgressBarCell)CellTemplate).Maximum = value;
            //DataGridViewにすでに追加されているセルの値を変更する
            if (DataGridView == null)
            {
                return;
            }
            int rowCount = DataGridView.RowCount;
            for (int i = 0; i < rowCount; i++)
            {
                DataGridViewRow r = DataGridView.Rows.SharedRow(i);
                ((DataGridViewProgressBarCell)r.Cells[Index]).Maximum = value;
            }
        }
    }

    /// <summary>
    /// ProgressBarの最小値
    /// </summary>
    public int Mimimum
    {
        get
        {
            return ((DataGridViewProgressBarCell)CellTemplate).Mimimum;
        }
        set
        {
            if (Mimimum == value)
                return;
            //セルテンプレートの値を変更する
            ((DataGridViewProgressBarCell)CellTemplate).Mimimum = value;
            //DataGridViewにすでに追加されているセルの値を変更する
            if (this.DataGridView == null)
            {
                return;
            }
            int rowCount = DataGridView.RowCount;
            for (int i = 0; i < rowCount; i++)
            {
                DataGridViewRow r = this.DataGridView.Rows.SharedRow(i);
                ((DataGridViewProgressBarCell)r.Cells[Index]).Mimimum = value;
            }
        }
    }
}

/// <summary>
/// ProgressBarをDataGridViewに表示する
/// </summary>
public class DataGridViewProgressBarCell : DataGridViewTextBoxCell
{
    //コンストラクタ
    public DataGridViewProgressBarCell()
    {
        Maximum = 100;
        Mimimum = 0;
    }
    public int Maximum { get; set; }
    public int Mimimum { get; set; }

    //セルの値のデータ型を指定する
    //ここでは、整数型とする
    public override Type ValueType
    {
        get
        {
            return typeof(int);
        }
    }

    //新しいレコード行のセルの既定値を指定する
    public override object DefaultNewRowValue
    {
        get
        {
            return 0;
        }
    }

    //新しいプロパティを追加しているため、
    // Cloneメソッドをオーバーライドする必要がある
    public override object Clone()
    {
        DataGridViewProgressBarCell cell = (DataGridViewProgressBarCell)base.Clone();
        cell.Maximum = Maximum;
        cell.Mimimum = Mimimum;
        return cell;
    }

    protected override void Paint(Graphics graphics,
                                  Rectangle clipBounds, 
                                  Rectangle cellBounds,
                                  int rowIndex, 
                                  DataGridViewElementStates cellState,
                                  object value, 
                                  object formattedValue, 
                                  string errorText,
                                  DataGridViewCellStyle cellStyle,
                                  DataGridViewAdvancedBorderStyle advancedBorderStyle,
                                  DataGridViewPaintParts paintParts)
    {
        //値を決定する
        int intValue = 0;
        if (value is int)
        {
            intValue = (int)value;
        }
        if (intValue < Mimimum)
        {
            intValue = Mimimum;
        }
        if (intValue > Maximum)
        {
            intValue = Maximum;
        }
        //割合を計算する
        double rate = (double)(intValue - Mimimum) / (Maximum - Mimimum);

        //セルの境界線（枠）を描画する
        if ((paintParts & DataGridViewPaintParts.Border) == DataGridViewPaintParts.Border)
        {
            PaintBorder(graphics, clipBounds, cellBounds, cellStyle, advancedBorderStyle);
        }

        //境界線の内側に範囲を取得する
        Rectangle borderRect = BorderWidths(advancedBorderStyle);
        Rectangle paintRect = new Rectangle(cellBounds.Left + borderRect.Left,
                                            cellBounds.Top + borderRect.Top,
                                            cellBounds.Width - borderRect.Right,
                                            cellBounds.Height - borderRect.Bottom);

        //背景色を決定する
        //選択されている時とされていない時で色を変える
        bool isSelected = (cellState & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected;
        Color bkColor;
        if (isSelected && (paintParts & DataGridViewPaintParts.SelectionBackground) == DataGridViewPaintParts.SelectionBackground)
        {
            bkColor = cellStyle.SelectionBackColor;
        }
        else
        {
            bkColor = cellStyle.BackColor;
        }
        //背景を描画する
        if ((paintParts & DataGridViewPaintParts.Background) == DataGridViewPaintParts.Background)
        {
            using (SolidBrush backBrush = new SolidBrush(bkColor))
            {
                graphics.FillRectangle(backBrush, paintRect);
            }
        }

        //Paddingを差し引く
        paintRect.Offset(cellStyle.Padding.Right, cellStyle.Padding.Top);
        paintRect.Width -= cellStyle.Padding.Horizontal;
        paintRect.Height -= cellStyle.Padding.Vertical;

        //ProgressBarを描画する
        if ((paintParts & DataGridViewPaintParts.ContentForeground) == DataGridViewPaintParts.ContentForeground)
        {
            if (ProgressBarRenderer.IsSupported)
            {
                //visualスタイルで描画する

                //ProgressBarの枠を描画する
                ProgressBarRenderer.DrawHorizontalBar(graphics, paintRect);
                //ProgressBarのバーを描画する
                Rectangle barBounds = new Rectangle(paintRect.Left + 3, paintRect.Top + 3, paintRect.Width - 4, paintRect.Height - 6);
                barBounds.Width = (int)Math.Round(barBounds.Width * rate);
                ProgressBarRenderer.DrawHorizontalChunks(graphics, barBounds);
            }
            else
            {
                //visualスタイルで描画できない時
                graphics.FillRectangle(Brushes.White, paintRect);
                graphics.DrawRectangle(Pens.Black, paintRect);
                Rectangle barBounds = new Rectangle(paintRect.Left + 1, paintRect.Top + 1, paintRect.Width - 1, paintRect.Height - 1);
                barBounds.Width = (int)Math.Round(barBounds.Width * rate);
                graphics.FillRectangle(Brushes.Blue, barBounds);
            }
        }

        //フォーカスの枠を表示する
        if (DataGridView.CurrentCellAddress.X == ColumnIndex &&
            DataGridView.CurrentCellAddress.Y == RowIndex &&
            (paintParts & DataGridViewPaintParts.Focus) == DataGridViewPaintParts.Focus && DataGridView.Focused)
        {
            //フォーカス枠の大きさを適当に決める
            Rectangle focusRect = paintRect;
            focusRect.Inflate(-3, -3);
            ControlPaint.DrawFocusRectangle(graphics, focusRect);
            //背景色を指定してフォーカス枠を描画する時
            //ControlPaint.DrawFocusRectangle(
            //    graphics, focusRect, Color.Empty, bkColor);
        }

        //テキストを表示する
        if ((paintParts & DataGridViewPaintParts.ContentForeground) == DataGridViewPaintParts.ContentForeground)
        {
            //表示するテキストを決定
            string txt = string.Format("{0}%", Math.Round(rate * 100));
            //string txt = formattedValue.ToString();

            //本来は、cellStyleによりTextFormatFlagsを決定すべき
            TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
            //色を決定
            Color fColor = cellStyle.ForeColor;
            //if (isSelected)
            //    fColor = cellStyle.SelectionForeColor;
            //else
            //    fColor = cellStyle.ForeColor;
            //テキストを描画する
            paintRect.Inflate(-2, -2);
            TextRenderer.DrawText(graphics, txt, cellStyle.Font, paintRect, fColor, flags);
        }

        //エラーアイコンの表示
        if ((paintParts & DataGridViewPaintParts.ErrorIcon) == DataGridViewPaintParts.ErrorIcon &&
            DataGridView.ShowCellErrors &&
            !string.IsNullOrEmpty(errorText))
        {
            //エラーアイコンを表示させる領域を取得
            Rectangle iconBounds = GetErrorIconBounds(graphics, cellStyle, rowIndex);
            iconBounds.Offset(cellBounds.X, cellBounds.Y);
            //エラーアイコンを描画
            PaintErrorIcon(graphics, iconBounds, cellBounds, errorText);
        }
    }
}
