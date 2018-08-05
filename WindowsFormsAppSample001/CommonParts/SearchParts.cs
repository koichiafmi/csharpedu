using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommonParts
{

    public partial class SearchCondition
    {
        private string firstSearchkey;  // 第一検索キー
        private string secondSearchkey; // 第弐検索キー
        private string thirdSearchkey;  // 第参検索キー

        internal static readonly SearchCondition searchCondition = new SearchCondition();

        public string getFirstSearchkey() { return firstSearchkey; }
        public void setFirstSearchkey(string x) { firstSearchkey = x; }

        public string getSecondSearchkey() { return secondSearchkey; }
        public void setSecondSearchkey(string x) { secondSearchkey = x; }

        public string getThirdSearchkey() { return thirdSearchkey; }
        public void setThirdSearchkey(string x) { thirdSearchkey = x; }

    }

    public partial class SearchParts
    {
        private int firstsearchkey = 0;
        //private int secondsearchkey = 1;
        //private int thirdsearchkey = 2;

        public void LoopSearch()
        {
            var listDataGridViewRow = new List<DataGridViewRow>();
            // 行情報取得

            foreach (DataGridViewRow[] dataGridViewRow in DataGridViewObject.dataGridObj.getBeforeEditingDataGridViewRow())
            {
                foreach (DataGridViewRow row in dataGridViewRow)
                {
                    string columnFirstInfo = row.Cells[firstsearchkey].Value.ToString();
                    if (SearchCondition.searchCondition.getFirstSearchkey().Equals(columnFirstInfo))
                    {
                        listDataGridViewRow.Add(row);
                    }
                }
            }


            DataGridViewRow[] rows = new DataGridViewRow[listDataGridViewRow.Count];
            int i = 0;
            foreach (DataGridViewRow dataGridViewRow in listDataGridViewRow)
            {
                rows[i] = dataGridViewRow;
                i++;
            }

            DataGridViewObject.dataGridObj.setAfterEditingDataGridViewRow(rows);
        }
    }
}
