using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Data;


namespace CommonParts
{
    public partial class DataGridViewObject
    {
        private string column1; // 実部を記憶しておく
        private string column2; // 実部を記憶しておく
        private string column3; // 実部を記憶しておく
        private string column4; // 実部を記憶しておく
        private string column5; // 実部を記憶しておく
        private string column6; // 実部を記憶しておく
        private string column7; // 実部を記憶しておく
        private string column8; // 実部を記憶しておく
        private string column9; // 実部を記憶しておく
        private string column10; // 実部を記憶しておく
        private DataGridViewObject dataGridViewObject;


        private List<DataGridViewRow[]> dataGridViewRow1;
        private DataGridViewRow[] dataGridViewRow2;
        private DataGridView dataGridView;
        private int　numberOfColumns; //列数
        private int  numberOfLines; //行数数
        private OpenFileDialog openFileDialog;//選択ファイル管理メンバ
        private string progresMethod; //プログレスバー（ファイル単位or行単位で管理？）
        private System.Windows.Forms.DataGridViewColumn[] dataGridViewColumnList;
        private string strFileName;
        private DataTable dt;
        private bool addRangeFlag;
        private DataSet dataset;

        internal static readonly DataGridViewObject dataGridObj = new DataGridViewObject();

        public DataGridViewObject getDAtaGridViewObject() { return dataGridViewObject; }
        public void setDAtaGridViewObject(DataGridViewObject x) { this.dataGridViewObject = x; }


        public List<DataGridViewRow[]> getBeforeEditingDataGridViewRow() { return dataGridViewRow1; }　//編集前
        public void setBeforeEditingDataGridViewRow(List<DataGridViewRow[]> x) { this.dataGridViewRow1 = x; }

        public DataGridViewRow[] getAfterEditingDataGridViewRow() { return dataGridViewRow2; }　//編集後
        public void setAfterEditingDataGridViewRow(DataGridViewRow[] x) { this.dataGridViewRow2 = x; }

        public int getNumberOfColumns() { return numberOfColumns; }
        public void setNumberOfColumns(int x) { this.numberOfColumns = x; }

        public int getNmberOfLines() { return numberOfLines; }
        public void setNmmberOfLines(int x) { this.numberOfLines = x; }

        public DataGridView getDataGridView() { return dataGridView; }
        public void setDataGridView(DataGridView x) { dataGridView = x; }

        public OpenFileDialog getOpenFileDialog() { return openFileDialog; }
        public void setOpenFileDialog(OpenFileDialog x) { openFileDialog = x; }

        /* プログレスバー情報 */
        public string getProgresMethod() { return progresMethod; }
        public void setProgresMethod(string x) { progresMethod = x; }

        /* 取得ファイルカラム列分設定 */
        public DataGridViewColumn[] getDataGridViewColumnList() { return dataGridViewColumnList; }
        public void setDataGridViewColumnList(DataGridViewColumn[] x) { dataGridViewColumnList = x; }



        public DataTable getDataTable() { return dt; }
        public void setDataTable(DataTable x) { dt = x; }


        public bool getAddRangeFlag() { return addRangeFlag; }
        public void setAddRangeFlag(bool x) { addRangeFlag = x; }

        public DataSet getDataset() { return dataset; }
        public void setDataset(DataSet x) { dataset = x; }


        /* ファイル名 */
        public string getFileName() { return strFileName; }
        public void setFileName(string x) { strFileName = x; }

        /* カラム(列)情報 */
        public string getColumn1() { return column1; }
        public void setColumn1(string x) { this.column1 = x; }

        public string getColumn2() { return column2; }
        public void setColumn2(string x) { this.column2 = x; }

        public string getColumn3() { return column3; }
        public void setColumn3(string x) { this.column3 = x; }

        public string getColumn4() { return column4; }
        public void setColumn4(string x) { this.column4 = x; }

        public string getColumn5() { return column5; }
        public void setColumn5(string x) { this.column5 = x; }

        public string getColumn6() { return column6; }
        public void setColumn6(string x) { this.column6 = x; }

        public string getColumn7() { return column7; }
        public void setColumn7(string x) { this.column7 = x; }

        public string getColumn8() { return column8; }
        public void setColumn8(string x) { this.column8 = x; }

        public string getColumn9() { return column9; }
        public void setColumn9(string x) { this.column9 = x; }

        public string getColumn10() { return column10; }
        public void setColumn10(string x) { this.column10= x; }


    }

    public partial class DataGridViewSettings
    {
        System.Windows.Forms.DataGridViewTextBoxColumn Column1 = null;
        System.Windows.Forms.DataGridViewTextBoxColumn Column2 = null;
        System.Windows.Forms.DataGridViewTextBoxColumn Column3 = null;
        System.Windows.Forms.DataGridViewTextBoxColumn Column4 = null;
        System.Windows.Forms.DataGridViewTextBoxColumn Column5 = null;
        System.Windows.Forms.DataGridViewTextBoxColumn Column6 = null;
        System.Windows.Forms.DataGridViewTextBoxColumn Column7 = null;
        System.Windows.Forms.DataGridViewTextBoxColumn Column8 = null;
        System.Windows.Forms.DataGridViewTextBoxColumn Column9 = null;
        System.Windows.Forms.DataGridViewTextBoxColumn Column10 = null;

        internal static readonly DataGridViewSettings dataGridViewSettings = new DataGridViewSettings();

        public void InitialSettingDataGridView()
        {
            //列が自動的に作成されないようにする
            DataGridViewObject.dataGridObj.getDataGridView().AutoGenerateColumns = false;

            System.Windows.Forms.DataGridViewColumn[] DataGridViewColumnList =
                new System.Windows.Forms.DataGridViewColumn[DataGridViewObject.dataGridObj.getNumberOfColumns()];

            for (int i = 0; i < DataGridViewObject.dataGridObj.getNumberOfColumns(); i++)
            {
                DataGridViewColumnList[i] = GetDataGridViewColumn(i);
            }

            if (DataGridViewObject.dataGridObj.getAddRangeFlag())
            {
                DataGridViewObject.dataGridObj.getDataGridView().Columns.AddRange(DataGridViewColumnList);
            }
            else
            {
                //データソースを設定する
                //DataGridViewObject.dataGridObj.getDataGridView().DataSource = DataGridViewObject.dataGridObj.getDataTable();
                DataGridViewObject.dataGridObj.getDataGridView().Columns.AddRange(DataGridViewColumnList);
            }
        }

        private DataGridViewTextBoxColumn GetDataGridViewColumn(int numberOfColumn)
        {
            switch (numberOfColumn)
            {
                case 0:
                    Column1 = new DataGridViewTextBoxColumn();
                    Column1.DataPropertyName = string.Concat("Column1");
                    Column1.HeaderText = string.Concat("Column1");
                    Column1.Name = string.Concat("column1");
                    Column1.Visible = true;
                    Column1.Width = 100;
                    //Column1.ReadOnly = true;
                    Column1.Resizable = DataGridViewTriState.True;
                    return Column1;
                case 1:
                    Column2 = new DataGridViewTextBoxColumn();
                    Column2.DataPropertyName = string.Concat("Column2");
                    Column2.HeaderText = string.Concat("Column2");
                    Column2.Name = string.Concat("column2");
                    Column2.Visible = true;
                    Column2.Width = 100;
                    //Column2.ReadOnly = true;
                    Column2.Resizable = DataGridViewTriState.True;
                    return Column2;
                case 2:
                    Column3 = new DataGridViewTextBoxColumn();
                    Column3.DataPropertyName = string.Concat("Column3");
                    Column3.HeaderText = string.Concat("Column3");
                    Column3.Name = string.Concat("column3");
                    Column3.Visible = true;
                    Column3.Width = 100;
                    //Column3.ReadOnly = true;
                    Column1.Resizable = DataGridViewTriState.True;
                    return Column3;
                case 3:
                    Column4 = new DataGridViewTextBoxColumn();
                    Column4.DataPropertyName = string.Concat("Column4");
                    Column4.HeaderText = string.Concat("Column4");
                    Column4.Name = string.Concat("column4");
                    Column4.Visible = true;
                    Column4.Width = 100;
                    //Column4.ReadOnly = true;
                    Column4.Resizable = DataGridViewTriState.True;
                    return Column4;
                case 4:
                    Column5 = new DataGridViewTextBoxColumn();
                    Column5.DataPropertyName = string.Concat("Column5");
                    Column5.HeaderText = string.Concat("Column5");
                    Column5.Name = string.Concat("column5");
                    Column5.Visible = true;
                    Column5.Width = 100;
                    //Column5.ReadOnly = true;
                    Column5.Resizable = DataGridViewTriState.True;
                    return Column5;
                case 5:
                    Column6 = new DataGridViewTextBoxColumn();
                    Column6.DataPropertyName = string.Concat("Column6");
                    Column6.HeaderText = string.Concat("Column6");
                    Column6.Name = string.Concat("column6");
                    Column6.Visible = true;
                    Column6.Width = 100;
                    Column6.ReadOnly = true;
                    Column6.Resizable = DataGridViewTriState.True;
                    return Column6;
                case 6:
                    Column7 = new DataGridViewTextBoxColumn();
                    Column7.DataPropertyName = string.Concat("Column7");
                    Column7.HeaderText = string.Concat("Column7");
                    Column7.Name = string.Concat("column7");
                    Column7.Visible = true;
                    Column7.Width = 100;
                    Column7.ReadOnly = true;
                    Column7.Resizable = DataGridViewTriState.True;
                    return Column7;
                case 7:
                    Column8 = new DataGridViewTextBoxColumn();
                    Column8.DataPropertyName = string.Concat("Column8");
                    Column8.HeaderText = string.Concat("Column8");
                    Column8.Name = string.Concat("column8");
                    Column8.Visible = true;
                    Column8.Width = 100;
                    Column8.ReadOnly = true;
                    Column8.Resizable = DataGridViewTriState.True;
                    return Column8;
                case 8:
                    Column9 = new DataGridViewTextBoxColumn();
                    Column9.DataPropertyName = string.Concat("Column9");
                    Column9.HeaderText = string.Concat("Column9");
                    Column9.Name = string.Concat("column9");
                    Column9.Visible = true;
                    Column9.Width = 100;
                    Column9.ReadOnly = true;
                    Column9.Resizable = DataGridViewTriState.True;
                    return Column9;
                case 9:
                    Column10 = new DataGridViewTextBoxColumn();
                    Column10.DataPropertyName = string.Concat("Column10");
                    Column10.HeaderText = string.Concat("Column10");
                    Column10.Name = string.Concat("column10");
                    Column10.Visible = true;
                    Column10.Width = 100;
                    Column10.ReadOnly = true;
                    Column10.Resizable = DataGridViewTriState.True;
                    return Column10;
                default:
                    return null;
            }
        }

        public void Set_ColumnName(string[] columnName)
        {
            int columnIndex = 0;
            foreach (string item in columnName)
            {
                //itemをつかう
                DataGridViewObject.dataGridObj.getDataGridView().Columns[columnIndex].HeaderText = item;
                columnIndex++;
            }

        }


        /// <summary>
        /// 列名を設定
        /// </summary>
        private void SetDataGridObjColumnName(int numberOfColumn, string columnName)
        {
            /*
             *  列名を設定(setColumn[xx])
             *　第一引数：numberOfColumn(設定したい列(1→1列目に設定))
             *　第弐引数：columnName(設定したい列名) 
             */
            switch (numberOfColumn)
            {
                case 1:
                    DataGridViewObject.dataGridObj.setColumn1(columnName);
                    break;
                case 2:
                    DataGridViewObject.dataGridObj.setColumn2(columnName);
                    break;
                case 3:
                    DataGridViewObject.dataGridObj.setColumn3(columnName);
                    break;
                case 4:
                    DataGridViewObject.dataGridObj.setColumn4(columnName);
                    break;
                case 5:
                    DataGridViewObject.dataGridObj.setColumn5(columnName);
                    break;
                case 6:
                    DataGridViewObject.dataGridObj.setColumn6(columnName);
                    break;
                case 7:
                    DataGridViewObject.dataGridObj.setColumn7(columnName);
                    break;
                case 8:
                    DataGridViewObject.dataGridObj.setColumn8(columnName);
                    break;
                case 9:
                    DataGridViewObject.dataGridObj.setColumn9(columnName);
                    break;
                case 10:
                    DataGridViewObject.dataGridObj.setColumn10(columnName);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// DataGridViewに対して列名を設定
        /// </summary>
        private void SetDataGridViewColumnName(DataGridView dataGridView, int columnIndex)
        {
            /*
             *  DataGridViewに対して列名を設定
             *　第一引数：DataGridView
             *　第弐引数：columnIndex(設定したい列番号) 
             */
            switch (columnIndex)
            {
                case 0:
                    // 1列目
                    dataGridView.Columns[columnIndex].HeaderText =
                        DataGridViewObject.dataGridObj.getColumn1();
                    break;
                case 1:
                    // 2列目
                    dataGridView.Columns[columnIndex].HeaderText =
                        DataGridViewObject.dataGridObj.getColumn2();
                    break;
                case 2:
                    // 3列目
                    dataGridView.Columns[columnIndex].HeaderText =
                        DataGridViewObject.dataGridObj.getColumn3();
                    break;
                case 3:
                    // 4列目
                    dataGridView.Columns[columnIndex].HeaderText =
                        DataGridViewObject.dataGridObj.getColumn4();
                    break;
                case 4:
                    // 5列目
                    dataGridView.Columns[columnIndex].HeaderText =
                        DataGridViewObject.dataGridObj.getColumn5();
                    break;
                case 5:
                    // 6列目
                    dataGridView.Columns[columnIndex].HeaderText =
                        DataGridViewObject.dataGridObj.getColumn6();
                    break;
                case 6:
                    // 7列目
                    dataGridView.Columns[columnIndex].HeaderText =
                        DataGridViewObject.dataGridObj.getColumn7();
                    break;
                case 7:
                    // 8列目
                    dataGridView.Columns[columnIndex].HeaderText =
                        DataGridViewObject.dataGridObj.getColumn8();
                    break;
                case 8:
                    // 9列目
                    dataGridView.Columns[columnIndex].HeaderText =
                        DataGridViewObject.dataGridObj.getColumn9();
                    break;
                case 9:
                    // 5列目
                    dataGridView.Columns[columnIndex].HeaderText =
                        DataGridViewObject.dataGridObj.getColumn10();
                    break;
                default:
                    break;
            }
        }
    }
}
