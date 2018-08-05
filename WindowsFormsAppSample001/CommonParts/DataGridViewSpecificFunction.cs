using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CommonParts
{
    public partial class DataGridViewSpecificFunction
    {
        FileObject fileObject = new FileObject();

        /// <summary>
        /// ファイルを選択する
        /// </summary>
        public OpenFileDialog GetFilePath()
        {
            OpenFileDialog ofd = null;
            fileObject.GetFilePathUseFileDialog();
            ofd = DataGridViewObject.dataGridObj.getOpenFileDialog();
            return ofd;
        }

        /// <summary>
        /// 取扱ファイルの種類を設定
        /// </summary>
        public void SetFileType(string x)
        {
            if ( CommonParts.Constants.FILE_TYPE_CSV == x )
            {
                fileObject.setReadFileType(CommonParts.Constants.FILE_TYPE_CSV);
            }

            if (CommonParts.Constants.FILE_TYPE_XML == x)
            {
                fileObject.setReadFileType(CommonParts.Constants.FILE_TYPE_XML);
            }
        }

        /// <summary>
        /// CSVファイル読込
        /// </summary>
        public void ReadFile(TextBox x)
        {
            if (DataGridViewObject.dataGridObj.getAddRangeFlag())
            {
                fileObject.ReadCsvFile();
                fileObject.FilesNmberOfLines();
                string message = string.Concat("読込んだレコード合計数 [", DataGridViewObject.dataGridObj.getNmberOfLines(), "]");
                x.Text += message + "\r\n";
            }
            else
            {
                DataTableParts dataTableParts = new DataTableParts();
                //dataTableParts.UsingJetProvider();
                //dataTableParts.UsingOdbcProvider();
                dataTableParts.UsingOLEdbcProvider();
            }
        }

        public int GetReadLine()
        {
            return fileObject.getNmberOfLines();
        }

        public void CreateColumn(DataGridView x)
        {
            // DataGridView情報登録
            DataGridViewObject.dataGridObj.setDataGridView(x);
            // 選択したファイル名を取得
            fileObject.getFileName(0);
            int colCount = fileObject.GetNumberOfColumns(DataGridViewObject.dataGridObj.getFileName());
            if (colCount < 0)
            {
                return;
            }

            DataGridViewSettings.dataGridViewSettings.InitialSettingDataGridView();
        }
        /// <summary>
        /// 表示用データ設定
        /// </summary>
        public void Set_DisplayData()
        {
            if (DataGridViewObject.dataGridObj.getAddRangeFlag())
            {
                Set_DisplayDataByRowsAddRange();
            }
            else
            {
                Set_DisplayDataByDataTable();

            }
        }

        private void Set_DisplayDataByRowsAddRange()
        {

            foreach (DataGridViewRow[] dataGridViewRow in DataGridViewObject.dataGridObj.getBeforeEditingDataGridViewRow()) // 先頭から最後まで順番に表示
            {
                DataGridViewObject.dataGridObj.getDataGridView().Rows.AddRange(dataGridViewRow);
            }

        }

        private void Set_DisplayDataByDataTable()
        {
            //DataGridViewObject.dataGridObj.getDataGridView().DataSource = DataGridViewObject.dataGridObj.getDataTable() ;
            DataGridViewObject.dataGridObj.getDataGridView().DataMember = DataGridViewObject.dataGridObj.getDataset().Tables[0].TableName;
            DataGridViewObject.dataGridObj.getDataGridView().DataSource = DataGridViewObject.dataGridObj.getDataset().Tables[0];
        }


        public void SortData()
        {

            DataGridViewObject.dataGridObj.getDataGridView().Sort(new CustomComparer(SortOrder.Ascending));
        }

        public void SetSearchkey(string fistkey, string secondkey, string thirdKey)
        {

            if (fistkey != "")
            {
                SearchCondition.searchCondition.setFirstSearchkey(fistkey);
            }

            if (secondkey != "")
            {
                SearchCondition.searchCondition.setSecondSearchkey(secondkey);
            }

            if (thirdKey != "")
            {
                SearchCondition.searchCondition.setThirdSearchkey(thirdKey);
            }
        }

        public void DataSearch()
        {

            SearchParts searchParts = new SearchParts();
            searchParts.LoopSearch();
            if (0 < DataGridViewObject.dataGridObj.getAfterEditingDataGridViewRow().Length)
            {
                DataGridViewObject.dataGridObj.getDataGridView().Rows.Clear();
                DataGridViewObject.dataGridObj.getDataGridView().Rows.AddRange(DataGridViewObject.dataGridObj.getAfterEditingDataGridViewRow());
            }
            else
            {
                MessageBox.Show("検索キーにヒットしませんでした.");
                return;

            }
        }

        public void SetAddRangeFlag(bool x)
        {
            DataGridViewObject.dataGridObj.setAddRangeFlag(x);
        }

        public void SetMultiselectFlag(bool x)
        {
            fileObject.setMultiselectFlag(x);
        }

        public void SetFirstlLneOfFileIsColumnNameFlag(bool x)
        {
            fileObject.setFirstlLneOfFileIsColumnNameFlag(x);
        }

        public void SetDataGridView(DataGridView x)
        {
            // DataGridView情報登録
            DataGridViewObject.dataGridObj.setDataGridView(x);
        }

        public void ReadFileUsingOLEdbcProvider(TextBox x)
        {
            DataGridViewSettings.dataGridViewSettings.InitialSettingDataGridView();
            DataTableParts dataTableParts = new DataTableParts();
            dataTableParts.UsingOLEdbcProvider();
        }

        public void ReadFileUsingOdbcProvider(TextBox x)
        {
            DataTableParts dataTableParts = new DataTableParts();
            dataTableParts.UsingOdbcProvider();
        }

        public void ReadFileUsingJetProvider(TextBox x)
        {
            DataTableParts dataTableParts = new DataTableParts();
            dataTableParts.UsingJetProvider();
        }
        public void Select()
        {
            DataTableParts dataTableParts = new DataTableParts();
            dataTableParts.SelectDataTable();
        }

    }
}
