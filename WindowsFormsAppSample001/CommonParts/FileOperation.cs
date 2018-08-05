using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;

namespace CommonParts
{
    public partial class FileObject
    {

        private string readFileType;
        private int nmberOfLines;
        private int numberOfColumns;
        public DataGridViewRow[] rows;
        private double numberOfDrawingTimesReferenceValue;//描画回数基準値
        private bool multiselectFlag; // 複数のファイルを選択可能フラグ
        private bool firstlLneOfFileIsColumnNameFlag; // 先頭行を列名フラグ

        public string getReadFileType() { return readFileType; }
        public void setReadFileType(string x) { this.readFileType = x; }

        public int getNmberOfLines() { return nmberOfLines; }
        public void setNmberOfLines(int x) { this.nmberOfLines = x; }

        public int getNumberOfColumns() { return numberOfColumns; }
        public void setNumberOfColumns(int x) { this.numberOfColumns = x; }

        public double getNumberOfDrawingTimesReferenceValue() { return numberOfDrawingTimesReferenceValue; }
        public void setNumberOfDrawingTimesReferenceValue(double x) { this.numberOfDrawingTimesReferenceValue = x; }

        public bool getMultiselectFlag() { return multiselectFlag; }
        public void setMultiselectFlag(bool x) { this.multiselectFlag = x; }

        public bool getFirstlLneOfFileIsColumnNameFlag() { return firstlLneOfFileIsColumnNameFlag; }
        public void setFirstlLneOfFileIsColumnNameFlag(bool x) { this.firstlLneOfFileIsColumnNameFlag = x; }


        /// <summary>
        /// ファイルを選択する
        /// </summary>
        public void GetFilePathUseFileDialog()
        {         
            /*　CSVファイル選択(単ファイル)　*/
            //            string filePath = "";

            //OpenFileDialogクラスのインスタンスを作成
            OpenFileDialog ofd = new OpenFileDialog();

            if (CommonParts.Constants.FILE_TYPE_CSV == getReadFileType())
            {
                //[ファイルの種類]に表示される選択肢を指定する
                //指定しないとすべてのファイルが表示される
                ofd.Filter = "CSVファイル(*.csv)|*.csv;";
                //はじめのファイル名を指定する
                //はじめに「ファイル名」で表示される文字列を指定する
                ofd.FileName = "default.csv";
            }
            else if (CommonParts.Constants.FILE_TYPE_XML == getReadFileType())
            {
                //[ファイルの種類]に表示される選択肢を指定する
                ofd.Filter = "XMLファイル(*.xml)|*.xml";
                //はじめのファイル名を指定する
                //はじめに「ファイル名」で表示される文字列を指定する
                ofd.FileName = "";
            }
            else
            {
                //[ファイルの種類]に表示される選択肢を指定する
                ofd.Filter = "CSVファイル(*.csv)|*.csv;|XMLファイル(*.xml)|*.xml";
                //はじめのファイル名を指定する
                //はじめに「ファイル名」で表示される文字列を指定する
                ofd.FileName = "";
            }

            //★★★複数のファイルを選択できるようにするかどうかの設定★★★
            ofd.Multiselect = getMultiselectFlag();

            //はじめに表示されるフォルダを指定する
            //指定しない（空の文字列）の時は、現在のディレクトリが表示される
            ofd.InitialDirectory = @"C:\";

            //[ファイルの種類]ではじめに選択されるものを指定する
            //2番目の「すべてのファイル」が選択されているようにする
            ofd.FilterIndex = 1;
            //タイトルを設定する
            ofd.Title = "開くファイルを選択してください";
            //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            ofd.RestoreDirectory = true;
            //存在しないファイルの名前が指定されたとき警告を表示する
            //デフォルトでTrueなので指定する必要はない
            ofd.CheckFileExists = true;
            //存在しないパスが指定されたとき警告を表示する
            //デフォルトでTrueなので指定する必要はない
            ofd.CheckPathExists = true;

            ofd.ReadOnlyChecked = false;
            ofd.RestoreDirectory = true;
            //ダイアログを表示する
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //OKボタンがクリックされたとき、選択されたファイル名を表示する
                DataGridViewObject.dataGridObj.setOpenFileDialog(ofd);

            }
            ofd.Dispose();
        }

        /// <summary>
        /// フォルダーを選択する(未使用)
        /// </summary>
        public FolderBrowserDialog GetFolderPathUseFolderBrowserDialog()
        {
            /*　フォルダー選択　*/
            //FolderBrowserDialogクラスのインスタンスを作成
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            //上部に表示する説明テキストを指定する
            fbd.Description = "フォルダを指定してください。";
            //ルートフォルダを指定する
            //デフォルトでDesktop
            fbd.RootFolder = Environment.SpecialFolder.Desktop;
            //最初に選択するフォルダを指定する
            //RootFolder以下にあるフォルダである必要がある
            fbd.SelectedPath = @"C:\Windows";
            //ユーザーが新しいフォルダを作成できるようにする
            //デフォルトでTrue
            fbd.ShowNewFolderButton = true;
            //ダイアログを表示する
            if (fbd.ShowDialog() == DialogResult.OK)
            {
/*
                //選択されたフォルダを表示する
                FolderPath = fbd.SelectedPath;
 */
            }
            fbd.Dispose();
            return fbd;
        }

        /// <summary>
        /// CSVファイルを読込む
        /// </summary>
        public List<DataGridViewRow[]> ReadCsvFile()
        {
            List<DataGridViewRow[]> dataGridViewRow = null;
            int lines = 0;
            ProgressDialog pd = new ProgressDialog("進行状況ダイアログ",
                new DoWorkEventHandler(ProgressDialog_DoWork_CsvLoad),lines);

            //ファイル単位でプログレスバー表示　or　1ファイルの行数単位でプログレスバー表示
            int fileCounts = DataGridViewObject.dataGridObj.getOpenFileDialog().FileNames.Count();
            if (1 < fileCounts)
            {
                setNumberOfDrawingTimesReferenceValue(DisplayFrequencySetting(Digit(fileCounts)));
                DataGridViewObject.dataGridObj.setProgresMethod(CommonParts.Constants.PROGRESS_DIALOG_VIEW_FILE_UNIT);
                //pd.SetProgressRange(プログレスバーMin値設定, プログレスバーMax設定,マーキースタイル(true：表示),ブロックの移動速度(ミリ秒));
                pd.SetProgressRange(0, fileCounts, true, 100);
            }
            else
            {
                lines = NmberOfLines(DataGridViewObject.dataGridObj.getOpenFileDialog().FileName);
                setNumberOfDrawingTimesReferenceValue(DisplayFrequencySetting(Digit(lines)));
                DataGridViewObject.dataGridObj.setProgresMethod(CommonParts.Constants.PROGRESS_DIALOG_VIEW_FILE_LINE);
                //pd.SetProgressRange(プログレスバーMin値設定, プログレスバーMax設定,マーキースタイル(true：表示),ブロックの移動速度(ミリ秒));
                pd.SetProgressRange(0, lines, false, 0);
            }

            //進行状況ダイアログを表示する
            DialogResult result = pd.ShowDialog();
            //結果を取得する
            if (result == DialogResult.Cancel)
            {
                MessageBox.Show("キャンセルされました");
                return null;
            }
            else if (result == DialogResult.Abort)
            {
                //エラー情報を取得する
                Exception ex = pd.Error;
                MessageBox.Show("エラー: " + ex.Message);
                return null;

            }
            else if (result == DialogResult.OK)
            {
                dataGridViewRow = DataGridViewObject.dataGridObj.getBeforeEditingDataGridViewRow();
            }
            return dataGridViewRow;
        }

        /// <summary>
        /// 列数を取得する
        /// </summary>
        public int GetNumberOfColumns(string csvfilepath)
        {
            int columnCount = 0;
            int i = 0;
            TextFieldParser parser;
            try
            {
                parser = new TextFieldParser(csvfilepath, Encoding.GetEncoding("Shift_JIS"));
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(","); // 区切り文字はコンマ
                while (i < 1)
                {
                    string[] row = parser.ReadFields(); // 1行読み込み
                    columnCount = row.Length;
                    i++;
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                string message = string.Concat("読込むファイルが見つかりません！\n", "ファイル名：", csvfilepath);
                MessageBox.Show(message, "Err");
                return -1;
            }
            DataGridViewObject.dataGridObj.setNumberOfColumns(columnCount);
            parser.Dispose();
            return columnCount;
        }

        /// <summary>
        /// 行数を取得する
        /// </summary>
        public int NmberOfLines(string csvfilepath)
        {
            try
            {
                string[] csvDataAll = File.ReadAllLines(csvfilepath, Encoding.Default);
                return csvDataAll.Length;
            }
            catch (System.IO.FileNotFoundException)
            {
                string message = string.Concat("読込むファイルが見つかりません！\n", "ファイル名：", csvfilepath);
                MessageBox.Show(message, "Err");
                return -1;
            }
        }

        /// <summary>
        /// 指定したファイル名を設定する
        /// </summary>
        public void getFileName(int x)
        {
            int roopcount = 0;
            foreach (string strFilePath in DataGridViewObject.dataGridObj.getOpenFileDialog().FileNames)
            {

                if (x == roopcount)
                {
                    // ファイルパスからファイル名を取得
                    string strFileName = System.IO.Path.GetFullPath(strFilePath);
                    DataGridViewObject.dataGridObj.setFileName(strFileName);
                }
                roopcount++;
            }
        }

        /// <summary>
        /// 複数のファイルの行数を合算する
        /// </summary>
        public void FilesNmberOfLines()
        {
            int rowCount = 0;
            foreach (string strFilePath in DataGridViewObject.dataGridObj.getOpenFileDialog().FileNames)
            {
                try
                {
                    string[] csvDataAll = File.ReadAllLines(strFilePath, Encoding.Default);
                    rowCount += csvDataAll.Length;
                }
                catch (System.IO.FileNotFoundException)
                {
                    //スルー
                }
            }
            DataGridViewObject.dataGridObj.setNmmberOfLines(rowCount);
        }


        /// <summary>
        /// TextFieldParserを使用したファイル読込(未検証)
        /// </summary>
        /// <param name="openFileDialog1"></param>
        /// <returns></returns>
        public DataGridViewRow[] ReadCsvFileUseTextFieldParser(OpenFileDialog openFileDialog1)
        {
            DataGridViewRow[] rows = null;
            foreach (string strFilePath in openFileDialog1.FileNames)
            {
                int i = 0;
                try
                {
                    //データの分配列を用意
                    string[] csvDataAll = File.ReadAllLines(strFilePath, Encoding.Default);
                    rows = new DataGridViewRow[csvDataAll.Length];

                    TextFieldParser parser;
                    parser = new TextFieldParser(strFilePath, Encoding.GetEncoding("Shift_JIS"));
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(","); // 区切り文字はコンマ
                    while (!parser.EndOfData)
                    {
                        string[] row = parser.ReadFields(); // 1行読み込み
                                                            // 読み込んだデータ(1行をDataGridViewに表示する)
                        string[] data = new string[row.Length];
                        Array.Copy(row, 0, data, 0, row.Length);

                        DataGridViewRow dataGridViewRow = new DataGridViewRow();
                        dataGridViewRow.CreateCells(DataGridViewObject.dataGridObj.getDataGridView());
                        dataGridViewRow.SetValues(data);
                        rows[i] = dataGridViewRow;
                    }
                }
                catch (System.IO.FileNotFoundException)
                {
                    string message = string.Concat("読込むファイルが見つかりません！\n", "ファイル名：", strFilePath);
                    MessageBox.Show(message, "Err");
                    return null;
                }
            }
            return rows;
        }

        /// <summary>
        /// 選択ダイアログ情報取得
        /// </summary>
        /// <returns></returns>
        public OpenFileDialog GetOpenFileDialogInfo()
        {
            return DataGridViewObject.dataGridObj.getOpenFileDialog();
        }

        /// <summary>
        /// DataGridView情報設定
        /// </summary>
        /// <param name="x"></param>
        public void SetDataGridView(DataGridView x)
        {
            DataGridViewObject.dataGridObj.setDataGridView(x);
        }

        /// <summary>
        /// SystemIoFileを使用したCSVファイル読込(未検証)
        /// </summary>
        /// <param name="openFileDialog1"></param>
        /// <returns></returns>
        private DataGridViewRow[] ReadCsvFileUseSystemIoFile(OpenFileDialog openFileDialog1)
        {
            DataGridViewRow[] rows = null;

            foreach (string strFilePath in openFileDialog1.FileNames)
            {
                // ファイルパスからファイル名を取得
                string strFileName = System.IO.Path.GetFileName(strFilePath);
                try
                {
                    string[] csvDataAll = File.ReadAllLines(strFileName, Encoding.Default);

                    //データの分配列を用意
                    rows = new DataGridViewRow[csvDataAll.Length];
                    for (int i = 0; i < csvDataAll.Length; i++)
                    {
                        string[] csv = csvDataAll[i].Split(',');
                        string[] data = new string[csv.Length];

                        Array.Copy(csv, 0, data, 0, csv.Length);

                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(DataGridViewObject.dataGridObj.getDataGridView());
                        row.SetValues(data);
                        rows[i] = row;
                    }
                }
                catch (System.IO.FileNotFoundException)
                {
                    string message = string.Concat("読込むファイルが見つかりません！\n", "ファイル名：", strFileName);
                    MessageBox.Show(message, "Err");
                    return null;
                }
            }
            return rows;
        }

        //DoWorkイベントハンドラ
        private void ProgressDialog_DoWork_CsvLoad(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = (BackgroundWorker)sender;
            //パラメータを取得する
            int totalRows = (int)e.Argument;
            //MessageBox.Show("パラメータを取得する: " + totalRows);
            //時間のかかる処理を開始する
            int rowCount = 0;
            rows = null;
            int fileCount = 0;
            List<DataGridViewRow[]> listDataGridViewRow = new List<DataGridViewRow[]>();
            // コントロールの表示を変更する
            foreach (string strFilePath in DataGridViewObject.dataGridObj.getOpenFileDialog().FileNames)
            {
                if (0 == fileCount)
                {
                    // 初回のファイルのカラム数を取得する。
                    setNumberOfColumns(GetNumberOfColumns(strFilePath));
                    DataGridViewObject.dataGridObj.setNumberOfColumns(GetNumberOfColumns(strFilePath));
                }
                //MessageBox.Show("キャンセルされたか調べる ");
                if (bw.CancellationPending)
                {
                    //キャンセルされたとき
                    e.Cancel = true;
                    string message = string.Concat("［キャンセル］処理を中断いたします！");
                    MessageBox.Show(message, "MESSAGE");
                    DataGridViewObject.dataGridObj.setBeforeEditingDataGridViewRow(null);
                    return;
                }

                int i = 0;
                try
                {
                    //MessageBox.Show("キャンセルされたか調べる ");
                    if (bw.CancellationPending)
                    {
                        //キャンセルされたとき
                        e.Cancel = true;
                        string message = string.Concat("［キャンセル］処理を中断いたします！");
                        MessageBox.Show(message, "MESSAGE");
                        DataGridViewObject.dataGridObj.setBeforeEditingDataGridViewRow(null);
                        return;
                    }

                    //データの分配列を用意
                    string[] csvDataAll = File.ReadAllLines(strFilePath, Encoding.Default);
                    if (getFirstlLneOfFileIsColumnNameFlag() == CommonParts.Constants.FLAG_BOOL_TYPE_ON)
                    {
                        rows = new DataGridViewRow[csvDataAll.Length - 1];
                    }
                    else
                    {
                        rows = new DataGridViewRow[csvDataAll.Length];
                    }

                    TextFieldParser parser;
                    parser = new TextFieldParser(strFilePath, Encoding.GetEncoding("Shift_JIS"));
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(","); // 区切り文字はコンマ
                    while (!parser.EndOfData)
                    {
                        string[] row = parser.ReadFields(); // 1行読み込み
                                                            // 読み込んだデータ(1行をDataGridViewに表示する)
                            
                        string[] data = new string[row.Length];
 
                        Array.Copy(row, 0, data, 0, row.Length);

                        if (getFirstlLneOfFileIsColumnNameFlag() == CommonParts.Constants.FLAG_BOOL_TYPE_ON && i == 0)
                        {
                            // checkBox1がチェックされている場合
                            DataGridViewSettings.dataGridViewSettings.Set_ColumnName(data);
                        }
                        else
                        {
                            if (getFirstlLneOfFileIsColumnNameFlag() == CommonParts.Constants.FLAG_BOOL_TYPE_ON)
                            {
                                i--;
                            }
                            DataGridViewRow dataGridViewRow = new DataGridViewRow();
                            dataGridViewRow.CreateCells(DataGridViewObject.dataGridObj.getDataGridView());
                            dataGridViewRow.SetValues(data);
                            rows[i] = dataGridViewRow;
                        }
                        //ProgressChangedイベントハンドラを呼び出し、
                        //コントロールの表示を変更する
                        //                        if (rowCount % getNumberOfDrawingTimesReferenceValue() == 0)
                        if (rowCount % 10000 == 0)
                        {
                            System.Threading.Thread.Sleep(1);
                            string message = string.Concat(csvDataAll.Length, "件中", i, "件終了しました");
                            bw.ReportProgress(i, message);
                        }
                        rowCount++;
                        i++;
                    }
                }
                catch (System.IO.FileNotFoundException)
                {
                    string message = string.Concat("読込むファイルが見つかりません！\n", "ファイル名：", strFilePath);
                    MessageBox.Show(message, "Err");
                    DataGridViewObject.dataGridObj.setBeforeEditingDataGridViewRow(null);
                    return;
                }
                fileCount++;
                listDataGridViewRow.Add(rows);
            }
            setNmberOfLines(rows.Count());
            DataGridViewObject.dataGridObj.setBeforeEditingDataGridViewRow(listDataGridViewRow);
        }

        /// <summary>
        /// 数値の桁数を調べる
        /// </summary>
        private int Digit(int num)
        {
            int digit = 1;
            for (int i = num; i >= 10; i /= 10)
            {
                digit++;
            }
            return digit;
        }

        /// <summary>
        /// 表示頻度
        /// </summary>
        private int DisplayFrequencySetting(int num)
        {
            // num→桁数
            switch (num)
            {
                case 1:
                    return 10;
                case 2:
                    return 10;
                case 3:
                    return 100;
                case 4:
                    return 1000;
                case 5:
                    return 10000;
                case 6:
                    return 100000;
                case 7:
                    return 1000000;
                case 8:
                    return 10000000;
                default:
                    return 1000;
            }
        }

    }

    //IComparerインターフェイスを実装した、並び替える方法を定義したクラス
    public class CustomComparer : IComparer
    {
        private int sortOrder;
        private Comparer comparer;

        public CustomComparer(SortOrder order)
        {
            this.sortOrder = (order == SortOrder.Descending ? -1 : 1);
            this.comparer = new Comparer(
                System.Globalization.CultureInfo.CurrentCulture);
        }

        //並び替え方を定義する
        public int Compare(object x, object y)
        {
            int result = 0;

            DataGridViewRow rowx = (DataGridViewRow)x;
            DataGridViewRow rowy = (DataGridViewRow)y;

            //はじめの列のセルの値を比較し、同じならば次の列を比較する
            for (int i = 0; i < rowx.Cells.Count; i++)
            {
                result = this.comparer.Compare(
                    rowx.Cells[i].Value, rowy.Cells[i].Value);
                if (result != 0)
                    break;
            }

            //結果を返す
            return result * this.sortOrder;
        }
    }
}
