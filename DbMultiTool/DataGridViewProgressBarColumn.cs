using System;
using System.Windows.Forms;

namespace DbMultiTool
{
    /// <summary>
    /// DataGridViewMaskedTextBoxCellオブジェクトの列を表します。
    /// </summary>
    public class DataGridViewProgressBarColumn : DataGridViewColumn
    {
        //CellTemplateとするDataGridViewMaskedTextBoxCellオブジェクトを指定して
        //基本クラスのコンストラクタを呼び出す
        public DataGridViewProgressBarColumn() : base(new DataGridViewProgressBarCell())
        {
        }

        //新しいプロパティを追加しているため、
        // Cloneメソッドをオーバーライドする必要がある
        public override object Clone()
        {
            DataGridViewProgressBarColumn col = (DataGridViewProgressBarColumn)base.Clone();
            return col;
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
                //DataGridViewMaskedTextBoxCellしか
                // CellTemplateに設定できないようにする
                if (!(value is DataGridViewProgressBarCell))
                {
                    throw new InvalidCastException("DataGridViewMaskedTextBoxCellオブジェクトを指定してください。");
                }
                base.CellTemplate = value;
            }
        }
    }

    /// <summary>
    /// MaskedTextBoxで編集できるテキスト情報を
    /// DataGridViewコントロールに表示します。
    /// </summary>
    public class DataGridViewProgressBarCell : DataGridViewTextBoxCell
    {
        //コンストラクタ
        public DataGridViewProgressBarCell()
        {
        }

        //編集コントロールを初期化する
        //編集コントロールは別のセルや列でも使いまわされるため、初期化の必要がある
        public override void InitializeEditingControl(int rowIndex,
                                                      object initialFormattedValue,
                                                      DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);

            //編集コントロールの取得
            DataGridViewProgressBarEditingControl maskedBox = DataGridView.EditingControl as DataGridViewProgressBarEditingControl;
            if (maskedBox != null)
            {
                //Textを設定
                string maskedText = initialFormattedValue as string;
                maskedBox.Text = maskedText != null ? maskedText : "";
                //カスタム列のプロパティを反映させる
                DataGridViewProgressBarColumn column = OwningColumn as DataGridViewProgressBarColumn;
                if (column != null)
                {
                    //maskedBox.Mask = column.Mask;
                }
            }
        }

        //編集コントロールの型を指定する
        public override Type EditType
        {
            get
            {
                return typeof(DataGridViewProgressBarEditingControl);
            }
        }

        //セルの値のデータ型を指定する
        //ここでは、Object型とする
        //基本クラスと同じなので、オーバーライドの必要なし
        public override Type ValueType
        {
            get
            {
                return typeof(object);
            }
        }

        //新しいレコード行のセルの既定値を指定する
        public override object DefaultNewRowValue
        {
            get
            {
                return base.DefaultNewRowValue;
            }
        }
    }

    /// <summary>
    /// DataGridViewMaskedTextBoxCellでホストされる
    /// MaskedTextBoxコントロールを表します。
    /// </summary>
    public class DataGridViewProgressBarEditingControl : ProgressBar, IDataGridViewEditingControl
    {
        //編集コントロールが表示されているDataGridView
        DataGridView dataGridView;
        //編集コントロールが表示されている行
        int rowIndex;
        //編集コントロールの値とセルの値が違うかどうか
        bool valueChanged;

        //コンストラクタ
        public DataGridViewProgressBarEditingControl()
        {
            TabStop = false;
        }

        #region IDataGridViewEditingControl メンバ

        //編集コントロールで変更されたセルの値
        public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            return Text;
        }

        //編集コントロールで変更されたセルの値
        public object EditingControlFormattedValue
        {
            get
            {
                return GetEditingControlFormattedValue(DataGridViewDataErrorContexts.Formatting);
            }
            set
            {
                Text = (string)value;
            }
        }

        //セルスタイルを編集コントロールに適用する
        //編集コントロールの前景色、背景色、フォントなどをセルスタイルに合わせる
        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            Font = dataGridViewCellStyle.Font;
            ForeColor = dataGridViewCellStyle.ForeColor;
            BackColor = dataGridViewCellStyle.BackColor;
            switch (dataGridViewCellStyle.Alignment)
            {
                case DataGridViewContentAlignment.BottomCenter:
                    break;
                case DataGridViewContentAlignment.MiddleCenter:
                    break;
                case DataGridViewContentAlignment.TopCenter:
                    break;
                case DataGridViewContentAlignment.BottomRight:
                    break;
                case DataGridViewContentAlignment.MiddleRight:
                    break;
                case DataGridViewContentAlignment.TopRight:
                    break;
                default:
                    break;
            }
        }

        //編集するセルがあるDataGridView
        public DataGridView EditingControlDataGridView
        {
            get
            {
                return dataGridView;
            }
            set
            {
                dataGridView = value;
            }
        }

        //編集している行のインデックス
        public int EditingControlRowIndex
        {
            get
            {
                return rowIndex;
            }
            set
            {
                rowIndex = value;
            }
        }

        //値が変更されたかどうか
        //編集コントロールの値とセルの値が違うかどうか
        public bool EditingControlValueChanged
        {
            get
            {
                return valueChanged;
            }
            set
            {
                valueChanged = value;
            }
        }

        //指定されたキーをDataGridViewが処理するか、編集コントロールが処理するか
        //Trueを返すと、編集コントロールが処理する
        //dataGridViewWantsInputKeyがTrueの時は、DataGridViewが処理できる
        public bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
        {
            //Keys.Left、Right、Home、Endの時は、Trueを返す
            //このようにしないと、これらのキーで別のセルにフォーカスが移ってしまう
            switch (keyData & Keys.KeyCode)
            {
                case Keys.Right:
                case Keys.End:
                case Keys.Left:
                case Keys.Home:
                    return true;
                default:
                    return !dataGridViewWantsInputKey;
            }
        }

        //マウスカーソルがEditingPanel上にあるときのカーソルを指定する
        //EditingPanelは編集コントロールをホストするパネルで、
        //編集コントロールがセルより小さいとコントロール以外の部分がパネルとなる
        public Cursor EditingPanelCursor
        {
            get
            {
                return base.Cursor;
            }
        }

        //コントロールで編集する準備をする
        //テキストを選択状態にしたり、挿入ポインタを末尾にしたりする
        public void PrepareEditingControlForEdit(bool selectAll)
        {
            if (selectAll)
            {
                //選択状態にする
                //this.SelectAll();
            }
            else
            {
                //挿入ポインタを末尾にする
                //this.SelectionStart = this.TextLength;
            }
        }

        //値が変更した時に、セルの位置を変更するかどうか
        //値が変更された時に編集コントロールの大きさが変更される時はTrue
        public bool RepositionEditingControlOnValueChange
        {
            get
            {
                return false;
            }
        }

        #endregion

        //値が変更された時
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            //値が変更されたことをDataGridViewに通知する
            valueChanged = true;
            dataGridView.NotifyCurrentCellDirty(true);
        }
    }
}

//using System;
//using System.Drawing;
//using System.Windows.Forms;

//namespace DbMultiTool
//{
//    /// <summary>
//    /// DataGridViewProgressBarCellオブジェクトの列
//    /// </summary>
//    public class DataGridViewProgressBarColumn : DataGridViewTextBoxColumn
//    {
//        // 結合行数
//        private int mergeRows = 1;
//        //コンストラクタ
//        public DataGridViewProgressBarColumn()
//        {
//            CellTemplate = new DataGridViewProgressBarCell();
//        }

//        // 結合行数を設定または取得します。
//        public int MergeRows
//        {
//            get
//            {
//                return
//                ((DataGridViewProgressBarCell)CellTemplate).MergeRows;
//            }
//            set
//            {
//                if (this.mergeRows == value)
//                {
//                    return;
//                }

//                //セルテンプレートの値を変更する
//                ((DataGridViewProgressBarCell)CellTemplate).MergeRows = value;
//                //DataGridViewにすでに追加されているセルの値を変更する
//                if (DataGridView == null)
//                {
//                    return;
//                }
//                int rowCount = DataGridView.RowCount;
//                for (int i = 0; i < rowCount; i++)
//                {
//                    DataGridViewRow r = DataGridView.Rows.SharedRow(i);
//                    ((DataGridViewProgressBarCell)r.Cells[Index]).MergeRows = value;
//                }
//            }
//        }

//        // MergeRowsプロパティを追加しているので、
//        // Clone()をオーバーライドします。
//        public override object Clone()
//        {
//            DataGridViewProgressBarColumn col = (DataGridViewProgressBarColumn)base.Clone();
//            col.MergeRows = MergeRows;
//            return col;
//        }

//        //CellTemplateの取得と設定
//        public override DataGridViewCell CellTemplate
//        {
//            get
//            {
//                return base.CellTemplate;
//            }
//            set
//            {
//                //DataGridViewProgressBarCell以外はホストしない
//                if (!(value is DataGridViewProgressBarCell))
//                {
//                    throw new InvalidCastException("DataGridViewProgressBarCellオブジェクトを指定してください。");
//                }
//                base.CellTemplate = value;
//            }
//        }

//        /// <summary>
//        /// ProgressBarの最大値
//        /// </summary>
//        public int Maximum
//        {
//            get
//            {
//                return ((DataGridViewProgressBarCell)this.CellTemplate).Maximum;
//            }
//            set
//            {
//                if (Maximum == value)
//                {
//                    return;
//                }
//                //セルテンプレートの値を変更する
//                ((DataGridViewProgressBarCell)CellTemplate).Maximum = value;
//                //DataGridViewにすでに追加されているセルの値を変更する
//                if (DataGridView == null)
//                {
//                    return;
//                }
//                int rowCount = DataGridView.RowCount;
//                for (int i = 0; i < rowCount; i++)
//                {
//                    DataGridViewRow r = DataGridView.Rows.SharedRow(i);
//                    ((DataGridViewProgressBarCell)r.Cells[Index]).Maximum = value;
//                }
//            }
//        }

//        /// <summary>
//        /// ProgressBarの最小値
//        /// </summary>
//        public int Mimimum
//        {
//            get
//            {
//                return ((DataGridViewProgressBarCell)CellTemplate).Mimimum;
//            }
//            set
//            {
//                if (Mimimum == value)
//                    return;
//                //セルテンプレートの値を変更する
//                ((DataGridViewProgressBarCell)CellTemplate).Mimimum = value;
//                //DataGridViewにすでに追加されているセルの値を変更する
//                if (this.DataGridView == null)
//                {
//                    return;
//                }
//                int rowCount = DataGridView.RowCount;
//                for (int i = 0; i < rowCount; i++)
//                {
//                    DataGridViewRow r = this.DataGridView.Rows.SharedRow(i);
//                    ((DataGridViewProgressBarCell)r.Cells[Index]).Mimimum = value;
//                }
//            }
//        }
//    }

//    /// <summary>
//    /// ProgressBarをDataGridViewに表示する
//    /// </summary>
//    public class DataGridViewProgressBarCell : DataGridViewTextBoxCell
//    {
//        //コンストラクタ
//        public DataGridViewProgressBarCell()
//        {
//            Maximum = 100;
//            Mimimum = 0;
//        }
//        public int Maximum { get; set; }
//        public int Mimimum { get; set; }

//        //セルの値のデータ型を指定する
//        //ここでは、整数型とする
//        public override Type ValueType
//        {
//            get
//            {
//                return typeof(int);
//            }
//        }

//        //新しいレコード行のセルの既定値を指定する
//        public override object DefaultNewRowValue
//        {
//            get
//            {
//                return 0;
//            }
//        }

//        //新しいプロパティを追加しているため、
//        // Cloneメソッドをオーバーライドする必要がある
//        public override object Clone()
//        {
//            DataGridViewProgressBarCell cell = (DataGridViewProgressBarCell)base.Clone();
//            cell.Maximum = Maximum;
//            cell.Mimimum = Mimimum;
//            cell.MergeRows = MergeRows;
//            return cell;
//        }

//        /// DataGridView 内でセルがいくつの行にまたがって表示されるかを
//        /// 示す値を取得または設定します。
//        public int MergeRows { get; set; } = 1;

//        // ホストされる編集コントロールの位置とサイズを設定します。
//        public override void PositionEditingControl(bool setLocation,
//                                                    bool setSize,
//                                                    Rectangle cellBounds,
//                                                    Rectangle cellClip,
//                                                    DataGridViewCellStyle cellStyle,
//                                                    bool singleVerticalBorderAdded,
//                                                    bool singleHorizontalBorderAdded,
//                                                    bool isFirstDisplayedColumn,
//                                                    bool isFirstDisplayedRow)
//        {
//            // 結合するセルの高さに合わせるために、結合するセルの高さを足す
//            Rectangle mergeCellBounds = cellBounds;
//            for (int i = 1; i < MergeRows; i++)
//            {
//                if (RowIndex + i < DataGridView.Rows.Count)
//                {
//                    mergeCellBounds.Height += DataGridView.Rows.SharedRow(RowIndex + i).Height;
//                }
//            }
//            if (RowIndex % MergeRows == 0)
//            {
//                base.PositionEditingControl(setLocation,
//                                            setSize,
//                                            mergeCellBounds,
//                                            mergeCellBounds,
//                                            cellStyle,
//                                            singleVerticalBorderAdded,
//                                            singleHorizontalBorderAdded,
//                                            isFirstDisplayedColumn,
//                                            isFirstDisplayedRow);
//            }
//            else
//            {
//                int row = (RowIndex / MergeRows) * MergeRows;
//                for (int k = row; k < RowIndex; k++)
//                {
//                    mergeCellBounds.Y -= DataGridView.Rows.SharedRow(k).Height;
//                }
//                base.PositionEditingControl(setLocation,
//                                            setSize,
//                                            mergeCellBounds,
//                                            mergeCellBounds,
//                                            cellStyle,
//                                            singleVerticalBorderAdded,
//                                            singleHorizontalBorderAdded,
//                                            isFirstDisplayedColumn,
//                                            isFirstDisplayedRow);
//            }
//        }



//        protected override void Paint(Graphics graphics,
//                                      Rectangle clipBounds,
//                                      Rectangle cellBounds,
//                                      int rowIndex,
//                                      DataGridViewElementStates cellState,
//                                      object value,
//                                      object formattedValue,
//                                      string errorText,
//                                      DataGridViewCellStyle cellStyle,
//                                      DataGridViewAdvancedBorderStyle advancedBorderStyle,
//                                      DataGridViewPaintParts paintParts)
//        {
//            //値を決定する
//            int intValue = 0;
//            if (value is int)
//            {
//                intValue = (int)value;
//            }
//            if (intValue < Mimimum)
//            {
//                intValue = Mimimum;
//            }
//            if (intValue > Maximum)
//            {
//                intValue = Maximum;
//            }
//            //割合を計算する
//            double rate = (double)(intValue - Mimimum) / (Maximum - Mimimum);

//            //セルの境界線（枠）を描画する
//            if ((paintParts & DataGridViewPaintParts.Border) == DataGridViewPaintParts.Border)
//            {
//                PaintBorder(graphics, clipBounds, cellBounds, cellStyle, advancedBorderStyle);
//            }

//            //境界線の内側に範囲を取得する
//            Rectangle borderRect = BorderWidths(advancedBorderStyle);
//            Rectangle paintRect = new Rectangle(cellBounds.Left + borderRect.Left,
//                                                cellBounds.Top + borderRect.Top,
//                                                cellBounds.Width - borderRect.Right,
//                                                cellBounds.Height - borderRect.Bottom);

//            //背景色を決定する
//            //選択されている時とされていない時で色を変える
//            bool isSelected = (cellState & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected;
//            Color bkColor;
//            if (isSelected && (paintParts & DataGridViewPaintParts.SelectionBackground) == DataGridViewPaintParts.SelectionBackground)
//            {
//                bkColor = cellStyle.SelectionBackColor;
//            }
//            else
//            {
//                bkColor = cellStyle.BackColor;
//            }
//            //背景を描画する
//            if ((paintParts & DataGridViewPaintParts.Background) == DataGridViewPaintParts.Background)
//            {
//                using (SolidBrush backBrush = new SolidBrush(bkColor))
//                {
//                    graphics.FillRectangle(backBrush, paintRect);
//                }
//            }

//            //Paddingを差し引く
//            paintRect.Offset(cellStyle.Padding.Right, cellStyle.Padding.Top);
//            paintRect.Width -= cellStyle.Padding.Horizontal;
//            paintRect.Height -= cellStyle.Padding.Vertical;

//            //ProgressBarを描画する
//            if ((paintParts & DataGridViewPaintParts.ContentForeground) == DataGridViewPaintParts.ContentForeground)
//            {
//                if (ProgressBarRenderer.IsSupported)
//                {
//                    //visualスタイルで描画する

//                    //ProgressBarの枠を描画する
//                    ProgressBarRenderer.DrawHorizontalBar(graphics, paintRect);
//                    //ProgressBarのバーを描画する
//                    Rectangle barBounds = new Rectangle(paintRect.Left + 3, paintRect.Top + 3, paintRect.Width - 4, paintRect.Height - 6);
//                    barBounds.Width = (int)Math.Round(barBounds.Width * rate);
//                    ProgressBarRenderer.DrawHorizontalChunks(graphics, barBounds);
//                }
//                else
//                {
//                    //visualスタイルで描画できない時
//                    graphics.FillRectangle(Brushes.White, paintRect);
//                    graphics.DrawRectangle(Pens.Black, paintRect);
//                    Rectangle barBounds = new Rectangle(paintRect.Left + 1, paintRect.Top + 1, paintRect.Width - 1, paintRect.Height - 1);
//                    barBounds.Width = (int)Math.Round(barBounds.Width * rate);
//                    graphics.FillRectangle(Brushes.Blue, barBounds);
//                }
//            }

//            //フォーカスの枠を表示する
//            if (DataGridView.CurrentCellAddress.X == ColumnIndex &&
//                DataGridView.CurrentCellAddress.Y == RowIndex &&
//                (paintParts & DataGridViewPaintParts.Focus) == DataGridViewPaintParts.Focus && DataGridView.Focused)
//            {
//                //フォーカス枠の大きさを適当に決める
//                Rectangle focusRect = paintRect;
//                focusRect.Inflate(-3, -3);
//                ControlPaint.DrawFocusRectangle(graphics, focusRect);
//                //背景色を指定してフォーカス枠を描画する時
//                ControlPaint.DrawFocusRectangle(graphics, focusRect, Color.Empty, bkColor);
//            }

//            //テキストを表示する
//            if ((paintParts & DataGridViewPaintParts.ContentForeground) == DataGridViewPaintParts.ContentForeground)
//            {
//                //表示するテキストを決定
//                string txt = string.Format("{0}%", Math.Round(rate * 100));
//                //string txt = formattedValue.ToString();

//                //本来は、cellStyleによりTextFormatFlagsを決定すべき
//                TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
//                //色を決定
//                Color fColor = cellStyle.ForeColor;
//                if (isSelected)
//                {
//                    fColor = cellStyle.SelectionForeColor;
//                }
//                else
//                {
//                    fColor = cellStyle.ForeColor;
//                }
//                //テキストを描画する
//                paintRect.Inflate(-2, -2);
//                TextRenderer.DrawText(graphics, txt, cellStyle.Font, paintRect, fColor, flags);
//            }

//            //エラーアイコンの表示
//            if ((paintParts & DataGridViewPaintParts.ErrorIcon) == DataGridViewPaintParts.ErrorIcon &&
//                DataGridView.ShowCellErrors &&
//                !string.IsNullOrEmpty(errorText))
//            {
//                //エラーアイコンを表示させる領域を取得
//                Rectangle iconBounds = GetErrorIconBounds(graphics, cellStyle, rowIndex);
//                iconBounds.Offset(cellBounds.X, cellBounds.Y);
//                //エラーアイコンを描画
//                PaintErrorIcon(graphics, iconBounds, cellBounds, errorText);
//            }
//        }
//    }
//}