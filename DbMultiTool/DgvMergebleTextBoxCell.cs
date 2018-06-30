using System;
using System.Drawing;
using System.Windows.Forms;

namespace DbMultiTool
{
    public class DgvMergebleTextBoxCell : DataGridViewTextBoxCell
    {
        public override object Clone()
        {
            DgvMergebleTextBoxCell cel = (DgvMergebleTextBoxCell)base.Clone();
            cel.MergeRows = MergeRows;
            return cel;
        }

        /// DataGridView 内でセルがいくつの行にまたがって表示されるかを
        /// 示す値を取得または設定します。
        /// 

        public int MergeRows { get; set; } = 1;

        // ホストされる編集コントロールの位置とサイズを設定します。
        public override void PositionEditingControl(bool setLocation,
                                                    bool setSize,
                                                    Rectangle cellBounds,
                                                    Rectangle cellClip,
                                                    DataGridViewCellStyle cellStyle,
                                                    bool singleVerticalBorderAdded,
                                                    bool singleHorizontalBorderAdded,
                                                    bool isFirstDisplayedColumn,
                                                    bool isFirstDisplayedRow)
        {
            // 結合するセルの高さに合わせるために、結合するセルの高さを足す
            Rectangle mergeCellBounds = cellBounds;
            for (int i = 1; i < MergeRows; i++)
            {
                if (RowIndex + i < DataGridView.Rows.Count)
                {
                    mergeCellBounds.Height += DataGridView.Rows.SharedRow(RowIndex + i).Height;
                }
            }
            if (RowIndex % MergeRows == 0)
            {
                base.PositionEditingControl(setLocation,
                                            setSize,
                                            mergeCellBounds,
                                            mergeCellBounds,
                                            cellStyle,
                                            singleVerticalBorderAdded,
                                            singleHorizontalBorderAdded,
                                            isFirstDisplayedColumn,
                                            isFirstDisplayedRow);
            }
            else
            {
                int row = (RowIndex / MergeRows) * MergeRows;
                for (int k = row; k < RowIndex; k++)
                {
                    mergeCellBounds.Y -= DataGridView.Rows.SharedRow(k).Height;
                }
                base.PositionEditingControl(setLocation,
                                            setSize,
                                            mergeCellBounds,
                                            mergeCellBounds,
                                            cellStyle,
                                            singleVerticalBorderAdded,
                                            singleHorizontalBorderAdded,
                                            isFirstDisplayedColumn,
                                            isFirstDisplayedRow);
            }
        }

        public override void InitializeEditingControl(int rowIndex,
                                                      object initialFormattedValue,
                                                      DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex,
                                          initialFormattedValue,
                                          dataGridViewCellStyle);
            DataGridViewTextBoxEditingControl ctl = DataGridView.EditingControl as DataGridViewTextBoxEditingControl;
            // 結合された列の上端の行インデックスを算出しセルを取得
            int row = (rowIndex / MergeRows) * MergeRows;
            DataGridViewCell cell = DataGridView[ColumnIndex, row];
            // 初期化した編集コントロールに、
            // 結合された列の上端セルの値を代入
            ctl.Text = Convert.ToString(cell.Value);
        }

        protected override object GetValue(int rowIndex)
        {
            if (rowIndex % MergeRows == 0)
            {
                // 結合された列の上端行のセルの場合のみ値を返す
                return base.GetValue(rowIndex);
            }
            else
            {
                // 結合された列の上端行のセル以外の場合は、null値を返す
                return null;
            }
        }

        protected override bool SetValue(int rowIndex, object value)
        {
            if (DataGridView == null || rowIndex % MergeRows == 0)
            {
                // DataGridViewにまだ追加されていないセルの場合と
                // 結合された列の上端行セルの場合は通常通り値を設定します。
                return base.SetValue(rowIndex, value);
            }
            else
            {
                try
                {
                    // 結合された列の上端行のインデックスを計算
                    int topRowIndex = (rowIndex / MergeRows) * MergeRows;
                    // DataGridViewにすでに追加されていて、結合された列の
                    // 上端行以外のセルの場合、結合された列の上端行のセルに
                    // 値を設定します。
                    DataGridView[ColumnIndex, RowIndex].Value = value;
                    //this.DataGridView[this.ColumnIndex, TopRowIndex(rowIndex)].Value = value;
                    return true;
                }
                catch (Exception ex)
                {
                    // 何らかのエラーが発生した場合は、falseを返します。
                    return false;
                }
            }
        }
    }
}
