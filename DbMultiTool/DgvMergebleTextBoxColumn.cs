using System.Windows.Forms;

namespace DbMultiTool
{
    public class DgvMergebleTextBoxColumn : DataGridViewTextBoxColumn
    {
        // 結合行数
        private int mergeRows = 1;
        // コンストラクタ
        public DgvMergebleTextBoxColumn()
        {
            CellTemplate = new DgvMergebleTextBoxCell();
        }
        // 結合行数を設定または取得します。
        public int MergeRows
        {
            get
            {
                return
                ((DgvMergebleTextBoxCell)CellTemplate).MergeRows;
            }
            set
            {
                if (this.mergeRows == value)
                {
                    return;
                }

                //セルテンプレートの値を変更する
                ((DgvMergebleTextBoxCell)CellTemplate).MergeRows = value;
                //DataGridViewにすでに追加されているセルの値を変更する
                if (DataGridView == null)
                {
                    return;
                }
                int rowCount = DataGridView.RowCount;
                for (int i = 0; i < rowCount; i++)
                {
                    DataGridViewRow r = DataGridView.Rows.SharedRow(i);
                    ((DgvMergebleTextBoxCell)r.Cells[Index]).MergeRows = value;
                }
            }
        }
        // MergeRowsプロパティを追加しているので、
        // Clone()をオーバーライドします。
        public override object Clone()
        {
            DgvMergebleTextBoxColumn col = (DgvMergebleTextBoxColumn)base.Clone();
            col.MergeRows = MergeRows;
            return col;
        }
    }
}
