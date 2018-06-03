using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;
using System.Threading;

namespace WindowsFormsAppSample003
{

    public partial class Form1 : Form
    {
        private String READ_FILE_USED_IN_FILEIO = @"C:\Sample003\Sample002_5column10000rows.csv";

        // PROGRESSBAR:通常-表示なめらかではない
        private int VIEW_PROGRESSBAR_PATTERN_1 = 1;

        // PROGRESSBAR:通常-表示なめらか
        private int VIEW_PROGRESSBAR_PATTERN_2 = 2;

        // ToolStripProgressBar
        private int VIEW_PROGRESSBAR_PATTERN_3 = 3;

        private int STATE_ACTIVITY = 0; //活性状態
        private int STATE_INACTIVE = 1; //非活性状態

        private int DEFAULT_DISPLAY = 1;
        private int FIRSTLINE_OF_CSV = 2;


        private DataGridViewRow[] _dataGridViewRows; // 1. フィールド
        public DataGridViewRow[] DataGridViewRow
        { // 3. プロパティ
            get { return _dataGridViewRows; }
            set
            {
                _dataGridViewRows = value;
            }
        }

        public Form1()
        {
            InitializeComponent();
            this.Set_ComboBox_Items();
            Sample.sample.SetCsvfilePath("");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int minValueBeforeChange;
            int minValueAfterChange;
            int maxnValueBeforeChange;
            int maxValueAfterChange;

            // データをすべてクリア
            TextBox_Clear();

            if (Sample.sample.GetCsvFilePahe() == "")
            {
                MessageBox.Show("読込むCSVファイル選択して！！");
                return;
            }

            int lines = Get_ReadCsvFike_NumberOfLines();

            //Buttonを無効にする
            this.Set_Button_Enablede(STATE_INACTIVE);
            //ProgressDialogオブジェクトを作成する

            ProgressDialog pd = new ProgressDialog("進行状況ダイアログのテスト",
                                new DoWorkEventHandler(ProgressDialog_DoWork_CsvLoad),
                                lines);

            //参照プログレスバーのxxを変更
            minValueBeforeChange = pd.GetProgressMinValue;
            maxnValueBeforeChange = pd.GetProgressMaxValue;
            //pd.SetProgressRange(プログレスバーMin値設定, プログレスバーMax設定,マーキースタイル(true：表示),ブロックの移動速度(ミリ秒));
            pd.SetProgressRange(0, lines, false, 0);
            minValueAfterChange = pd.GetProgressMinValue;
            maxValueAfterChange = pd.GetProgressMaxValue;
            //Stopwatchオブジェクトを作成する
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            //進行状況ダイアログを表示する
            DialogResult result = pd.ShowDialog(this);
            //結果を取得する
            if (result == DialogResult.Cancel)
            {
                MessageBox.Show("キャンセルされました");
            }
            else if (result == DialogResult.Abort)
            {
                //エラー情報を取得する
                Exception ex = pd.Error;
                MessageBox.Show("エラー: " + ex.Message);
                sw.Stop();
            }
            else if (result == DialogResult.OK)
            {
                //結果を取得する
                int readCount = (int)pd.Result;
                sw.Stop();
                // 列名設定
                this.dataGridView1.Rows.AddRange(Sample.sample.GetDataGridViewRow());
                string message = string.Concat("経過時間[", lines, "行][ ", sw.Elapsed.Hours.ToString("00"), ":", sw.Elapsed.Minutes.ToString("00"), ":", sw.Elapsed.Seconds.ToString("00"), ":", sw.ElapsedMilliseconds.ToString("000"), " ]\r\nプログレスバーについて\r\n変更前のminValue値：", minValueBeforeChange, "変更後のminValue値：", minValueAfterChange, "\r\n変更前のmaxValue値：", maxnValueBeforeChange, "変更後のmaxValue値：", maxValueAfterChange);
                textBox1.Text = message;
            }

            //後始末
            pd.Dispose();
            //Buttonを有効効にする
            this.Set_Button_Enablede(STATE_ACTIVITY);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // データをすべてクリア
            TextBox_Clear();
            if (Sample.sample.GetCsvFilePahe() == "")
            {
                MessageBox.Show("読込むCSVファイル選択して！！");
                return;
            }

            int lines = Get_ReadCsvFike_NumberOfLines();

            //Buttonを無効にする
            this.Set_Button_Enablede(STATE_INACTIVE);
            //ProgressDialogオブジェクトを作成する

            ProgressDialog pd = new ProgressDialog("進行状況ダイアログ(流れるタイプ)のテスト",
                                new DoWorkEventHandler(ProgressDialog_DoWork_CsvLoad),
                                lines);

            //参照プログレスバーのxxを変更
            //pd.SetProgressRange(プログレスバーMin値設定, プログレスバーMax設定,マーキースタイル(true：表示),ブロックの移動速度(ミリ秒));
            pd.SetProgressRange(0, lines, true, 1000);
            //Stopwatchオブジェクトを作成する
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            //進行状況ダイアログを表示する
            DialogResult result = pd.ShowDialog(this);
            //結果を取得する
            if (result == DialogResult.Cancel)
            {
                MessageBox.Show("キャンセルされました");
            }
            else if (result == DialogResult.Abort)
            {
                //エラー情報を取得する
                Exception ex = pd.Error;
                MessageBox.Show("エラー: " + ex.Message);
                sw.Stop();
            }
            else if (result == DialogResult.OK)
            {
                //結果を取得する
                int readCount = (int)pd.Result;
                sw.Stop();
                this.dataGridView1.Rows.AddRange(Sample.sample.GetDataGridViewRow());
                string message = string.Concat("経過時間[", lines, "行][ ", sw.Elapsed.Hours.ToString("00"), ":", sw.Elapsed.Minutes.ToString("00"), ":", sw.Elapsed.Seconds.ToString("00"), ":", sw.ElapsedMilliseconds.ToString("000"), " ]");
                textBox1.Text = message;
            }

            //後始末
            pd.Dispose();
            //Buttonを有効効にする
            this.Set_Button_Enablede(STATE_ACTIVITY);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // データをすべてクリア
            TextBox_Clear();
            this.checkBox1.Checked = true;
            //Buttonを無効にする
            this.Set_Button_Enablede(STATE_INACTIVE);
            this.DataLoad(VIEW_PROGRESSBAR_PATTERN_1);
            //Buttonを有効効にする
            this.Set_Button_Enablede(STATE_ACTIVITY);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // データをすべてクリア
            TextBox_Clear();
            this.checkBox1.Checked = true;
            //Buttonを無効にする
            this.Set_Button_Enablede(STATE_INACTIVE);
            this.DataLoad(VIEW_PROGRESSBAR_PATTERN_2);
            //Buttonを有効効にする
            this.Set_Button_Enablede(STATE_ACTIVITY);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // データをすべてクリア
            TextBox_Clear();
            this.checkBox1.Checked = false;
            this.statusStrip1.Visible = true;
            
            //Buttonを無効にする
            this.Set_Button_Enablede(STATE_INACTIVE);
            this.DataLoad(VIEW_PROGRESSBAR_PATTERN_3);
            //Buttonを有効効にする
            this.Set_Button_Enablede(STATE_ACTIVITY);
            this.statusStrip1.Visible = false;
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            // データをすべてクリア
            TextBox_Clear();
            progressBar1.Value = 0;
            dataGridView1.RowsDefaultCellStyle.BackColor=Color.White;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            dataGridView1.RowsDefaultCellStyle.BackColor = Color.White;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.Blue;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // データをすべてクリア
            TextBox_Clear();
            //Buttonを無効にする
            this.Set_Button_Enablede(STATE_INACTIVE);
            Drawing_WaitDialog();

            //Buttonを有効効にする
            this.Set_Button_Enablede(STATE_ACTIVITY);
        }

        private void DataLoad(int method)
        {
            int rowCount = 0;
            string filename = "";
            string[] lines;
            string message = "";
            //Stopwatchオブジェクトを作成する
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();

            try
            {
                lines = File.ReadAllLines(READ_FILE_USED_IN_FILEIO);

                if (VIEW_PROGRESSBAR_PATTERN_3 != method)
                {
                    // progressBar:Minmum 設定
                    progressBar1.Minimum = 0;

                    // progressBar:Maximum 設定
                    progressBar1.Maximum = lines.Length;
                }
                else
                {
                    // ToolStripProgressBarのMinimumプロパティで最小値
                    toolStripProgressBar1.Minimum = 0;

                    // ToolStripProgressBarのMaximumプロパティで最大値Minimum
                    toolStripProgressBar1.Maximum = lines.Length;
                }
                //ストップウォッチを開始する
                sw.Start();
                foreach (string line in File.ReadLines(READ_FILE_USED_IN_FILEIO, Encoding.Default))
                {
                    string[] csv = line.Split(',');
                    string[] data = new string[5];
                    Array.Copy(csv, 0, data, 0, 5);
                    this.dataGridView1.Rows.Add(data);
                    rowCount++;
                    if (VIEW_PROGRESSBAR_PATTERN_1 == method)
                    {
                        //ProgressBar1の値を変更する
                        progressBar1.Value = rowCount;
                    }
                    if (VIEW_PROGRESSBAR_PATTERN_2 == method)
                    {
                        this.Set_ProgressBar_Value(rowCount);
                    }
                    if (VIEW_PROGRESSBAR_PATTERN_3 == method)
                    {
                        //toolStripProgressBar1の値を変更する
                        this.Set_ToolStripProgressBar1_Value(rowCount);
                    }
                }
                //ストップウォッチを止める
                sw.Stop();
                //結果を表示する
                if (VIEW_PROGRESSBAR_PATTERN_1 == method)
                {
                    message = string.Concat("経過時間[", rowCount, "行][ ", sw.Elapsed.Hours.ToString("00"), ":", sw.Elapsed.Minutes.ToString("00"), ":", sw.Elapsed.Seconds.ToString("00"), ":", sw.ElapsedMilliseconds.ToString("000"), " ]\r\nSystem.IOクラス使用\r\ndataGridViewに一行ずつADD\r\nProgressBar同時完了しない");
                }
                if (VIEW_PROGRESSBAR_PATTERN_2 == method)
                {
                    message = string.Concat("経過時間[", rowCount, "行][ ", sw.Elapsed.Hours.ToString("00"), ":", sw.Elapsed.Minutes.ToString("00"), ":", sw.Elapsed.Seconds.ToString("00"), ":", sw.ElapsedMilliseconds.ToString("000"), " ]\r\nSystem.IOクラス使用\r\ndataGridViewに一行ずつADD\r\nProgressBar同時完了する");
                }
                if (VIEW_PROGRESSBAR_PATTERN_3 == method)
                {
                    message = string.Concat("経過時間[", rowCount, "行][ ", sw.Elapsed.Hours.ToString("00"), ":", sw.Elapsed.Minutes.ToString("00"), ":", sw.Elapsed.Seconds.ToString("00"), ":", sw.ElapsedMilliseconds.ToString("000"), " ]\r\nSystem.IOクラス使用\r\ndataGr表示idViewに一行ずつADD\r\n画面下にProgressBar");
                }
                textBox1.Text = message;// "System.IOクラス使用 | dataGridViewに一行ずつADD | ProgressBar同時完了しない";
            }
            catch (System.IO.FileNotFoundException)
            {
                //ストップウォッチを止める
                sw.Stop();
                filename = READ_FILE_USED_IN_FILEIO;
                message = string.Concat("読込むファイルが見つかりません！\n", "ファイル名：", filename);
                MessageBox.Show(message, "dialog");
                return;
            }
        }

        private void TextBox_Clear()
        {
            this.textBox1.Text = "";
            this.dataGridView1.Rows.Clear();
            this.textBox1.Refresh();
            this.dataGridView1.Refresh();
            this.progressBar1.Value = 0;
            this.checkBox1.Checked = false;
            this.toolStripProgressBar1.Value = 0;
        }

        private void Set_Button_Enablede(int state)
        {
            if (STATE_ACTIVITY == state)
            {
                //活性状態(有効)
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = true;
                button6.Enabled = true;
                button7.Enabled = true;
            }
            else if (STATE_INACTIVE == state)
            {
                //活性状態(有効)
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
                button6.Enabled = false;
                button7.Enabled = false;
            }
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                progressBar1.Visible = true; // 表示状態にする
            }
            else
            {
                progressBar1.Visible = false; // 非表示状態にする
            }
        }

        private void Set_ProgressBar_Value(int setValue)
        {
            // ProgressBarの更新描画を滑らかにする。
            if (setValue < progressBar1.Maximum)
            {
                // 指定する値がMaximumより小さい場合
                progressBar1.Value = setValue + 1;
                progressBar1.Value = setValue;
            }
            else
            {
                progressBar1.Maximum++;
                progressBar1.Value = setValue + 1;
                progressBar1.Value = setValue;
                progressBar1.Maximum--;
            }
        }

        private void Set_ToolStripProgressBar1_Value(int setValue)
        {
            // ProgressBarの更新描画を滑らかにする。
            if (setValue < toolStripProgressBar1.Maximum)
            {
                // 指定する値がMaximumより小さい場合
                toolStripProgressBar1.Value = setValue + 1;
                toolStripProgressBar1.Value = setValue;
            }
            else
            {
                toolStripProgressBar1.Maximum++;
                toolStripProgressBar1.Value = setValue + 1;
                toolStripProgressBar1.Value = setValue;
                progressBar1.Maximum--;
            }
        }


        //DoWorkイベントハンドラ
        private void ProgressDialog_DoWork_CsvLoad(object sender, DoWorkEventArgs e)
        {
            try
            {
                BackgroundWorker bw = (BackgroundWorker)sender;

                //パラメータを取得する
                int totalRows = (int)e.Argument;
                //MessageBox.Show("パラメータを取得する: " + totalRows);

                int rowCount = 0;
                string readFile = Sample.sample.GetCsvFilePahe();

                //時間のかかる処理を開始する
                string[] csvDataAll = File.ReadAllLines(readFile, Encoding.Default);
                //データの分配列を用意
                DataGridViewRow[] rows;

                rows = new DataGridViewRow[csvDataAll.Length];
                for (int i = 0; i < csvDataAll.Length; i++)
                {
                    int rowCounta = i;
                    //キャンセルされたか調べる
                    //MessageBox.Show("キャンセルされたか調べる ");
                    if (bw.CancellationPending)
                    {
                        //キャンセルされたとき
                        e.Cancel = true;
                        this.textBox1.Text = "キャンセル";
                        return;
                    }

                    string[] csv = csvDataAll[i].Split(',');
                    string[] data = new string[5];
                    Array.Copy(csv, 0, data, 0, 5);
                    //ProgressChangedイベントハンドラを呼び出し、
                    //コントロールの表示を変更する
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(this.dataGridView1);
                    row.SetValues(data);
                    rows[rowCounta] = row;
                    rowCount++;

                    if (rowCount % 1000 == 0)
                    {
                        System.Threading.Thread.Sleep(1);
                        string message = string.Concat(csvDataAll.Length, "件中", i, "件終了しました");
                        bw.ReportProgress(i, message);
                    }
                }
                //結果を設定する
                e.Result = rowCount;
                Sample.sample.SetDataGridViewRow(rows);
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("読込むCSVファイルを選択してください。");
            }

        }

        private void Drawing_WaitDialog()
        {      
            string readFile = Sample.sample.GetCsvFilePahe();
            if (readFile == "")
            {
                MessageBox.Show("読込むCSVファイル選択して！！");
                return;
            }
            int lines = File.ReadAllLines(readFile).Length;
            //MessageBox.Show("lines: " + lines);
            int intCountUpNumber = 1000;

            // 進行状況ダイアログの初期化処理
            WaitDialog waitDlg = new WaitDialog();
            waitDlg.Owner = this;  // ダイアログのオーナーを設定
            waitDlg.MainMsg = "進行状況を表示しています……";  // 処理の概要
            waitDlg.ProgressMax = lines;  // 全体の処理件数
            waitDlg.ProgressMin = 0;  // 処理件数の最小値（0件から開始）
            waitDlg.ProgressStep = 1;  // 何件ごとにメーターを進めるか
            waitDlg.ProgressValue = 0;  // 最初の件数

            // オーナーのフォームを無効にする
            this.Enabled = false;

            // 進行状況ダイアログを表示する
            waitDlg.Show();

            //Stopwatchオブジェクトを作成する
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();

            //ストップウォッチを開始する
            sw.Start();
            TextFieldParser parser;
            parser = new TextFieldParser(readFile, Encoding.GetEncoding("Shift_JIS"));
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(","); // 区切り文字はコンマ
            int rowCount = 0;
            while (!parser.EndOfData)
            {
                //待機中のイベントを処理する
                Application.DoEvents();
                // 処理中止かどうかをチェック
                if (waitDlg.IsAborting == true)
                {
                    break;
                }

                // 進行状況ダイアログの詳細な処理内容を設定
                // 
                if ((rowCount == 0) ||
                  ((rowCount != 1000) && (rowCount % 1000 == 1)))
                {
                    waitDlg.SubMsg = string.Concat("", intCountUpNumber, "件ごと表示を更新しています……：",
                   ((int)(rowCount / intCountUpNumber) + 1).ToString());
                }

                // メッセージ処理を促して表示を更新する
                //Application.DoEvents();
                waitDlg.PerformStep();
                string[] row = parser.ReadFields(); // 1行読み込み
                // 読み込んだデータ(1行をDataGridViewに表示する)
                dataGridView1.Rows.Add(row);

                if (rowCount % 1000 == 0)
                {
                    // 進行状況ダイアログのメーターを設定
                    waitDlg.ProgressMsg =
                      ((int)(rowCount * 100 / lines)).ToString() + "%　" +
                      "（" + rowCount.ToString() + " / " + lines.ToString() + " 件）";

                    waitDlg.UpdatePrts();
                }
                waitDlg.ProgressValueProceed(rowCount);
                rowCount++;
            }
            parser.Close();
            //ストップウォッチを止める
            sw.Stop();

            //結果を表示する
            string message = string.Concat("経過時間[", rowCount, "行][ ", sw.Elapsed.Hours.ToString("00"), ":", sw.Elapsed.Minutes.ToString("00"), ":", sw.Elapsed.Seconds.ToString("00"), ":", sw.ElapsedMilliseconds.ToString("000"), " ]\r\nTextFieldParserクラスを使用したCSV読込", "\r\n外部作成プログレスバー表示\r\nclass WaitDialog : System.Windows.Forms.Form\r\nApplication.DoEvents()を使用");
            textBox1.Text = message;

            // 最終メッセージを表示して、閉じるのを少し遅らせる
            if (waitDlg.DialogResult == DialogResult.Abort)
            {
                waitDlg.SubMsg = "処理を中断しました。";
            }
            else
            {
                waitDlg.SubMsg = "処理を完了しました。";
                waitDlg.ProgressMsg = string.Concat("100%　（", rowCount, "/ ", lines, "件");
            }
            Application.DoEvents();
            Thread.Sleep(100);

            // いったんオーナーをアクティブにする
            this.Activate();

            // 進行状況ダイアログを閉じる
            waitDlg.Close();

            // オーナーのフォームを有効に戻す
            this.Enabled = true;
        }

        private int Get_ReadCsvFike_NumberOfLines()
        {
            return File.ReadAllLines(Sample.sample.GetCsvFilePahe()).Length;
        }

        private void Set_ColumnName(int ColumnNameSettingFlag, string[] columnName)
        {
            // DefaultDisplay(ex:Column1):0
            // FirstLine of CSV:1
            if (DEFAULT_DISPLAY == ColumnNameSettingFlag)
            {
                int columnNo = 1;
                for (int i = 0; i < 5; i++)
                {
                    dataGridView1.Columns[i].HeaderText = string.Concat("Column", columnNo);
                    Set_DataGridObjColumnName(i, string.Concat("Column", columnNo));
                    columnNo++;
                }
            }
            else if (FIRSTLINE_OF_CSV == ColumnNameSettingFlag)
            {
                int columnIndex = 0;
                foreach (string item in columnName)
                {
                    //itemをつかう
                    Set_DataGridObjColumnName(columnIndex, item);
                    columnIndex++;
                }
            }
        }

        private void Set_DataGridObjColumnName(int numberOfColumn, string columnName)
        {
            switch (numberOfColumn)
            {
                case 0:
                    DataGridViewObject.dataGridObj.Column1(columnName);
                    break;
                case 1:
                    DataGridViewObject.dataGridObj.Column2(columnName);
                    break;
                case 2:
                    DataGridViewObject.dataGridObj.Column3(columnName);
                    break;
                case 3:
                    DataGridViewObject.dataGridObj.Column4(columnName);
                    break;
                case 4:
                    DataGridViewObject.dataGridObj.Column5(columnName);
                    break;
                default:
                    break;
            }
        }

        private void Set_ColumnName()
        {
            for (int i = 0; i < 5; i++)
            {
                Set_DataGridViewColumnName(i);
            }
        }

        private void Set_DataGridViewColumnName(int columnIndex)
        {
            switch (columnIndex)
            {
                case 0:
                    // 1列目
                    columnIndex++;
                    dataGridView1.Columns[columnIndex].HeaderText = 
                        DataGridViewObject.dataGridObj.Column1();
                    break;
                case 1:
                    // 2列目
                    dataGridView1.Columns[columnIndex].HeaderText =
                        DataGridViewObject.dataGridObj.Column2();
                    break;
                case 2:
                    // 3列目
                    dataGridView1.Columns[columnIndex].HeaderText =
                        DataGridViewObject.dataGridObj.Column3();
                    break;
                case 3:
                    // 4列目
                    dataGridView1.Columns[columnIndex].HeaderText =
                        DataGridViewObject.dataGridObj.Column4();
                    break;
                case 4:
                    // 5列目
                    dataGridView1.Columns[columnIndex].HeaderText =
                        DataGridViewObject.dataGridObj.Column5();
                    break;
                default:
                    break;
            }
        }


        private void Set_ComboBox_Items ()
        {
            // リストにKey, Value要素を追加する
            CmbObject obj1 = new CmbObject("", "");
            CmbObject obj2 = new CmbObject(@"C:\Sample003\Sample002_5column10000rows.csv", "5column10000rows.csv");
            CmbObject obj3 = new CmbObject(@"C:\Sample003\TM-WebTools_5-50000rows.csv", "5-50000rows.csv");
            CmbObject obj4 = new CmbObject(@"C:\Sample003\TM-WebTools_5-100000rows.csv", "5-100000rows.csv");
            CmbObject obj5 = new CmbObject(@"C:\Sample003\TM-WebTools_5-500000rows.csv", "5-500000rows.csv");
            CmbObject obj6 = new CmbObject(@"C:\Sample003\TM-WebTools_5-1049958rows.csv", "5-1049958rows.csv");
            comboBox1.Items.Add(obj1);
            comboBox1.Items.Add(obj2);
            comboBox1.Items.Add(obj3);
            comboBox1.Items.Add(obj4);
            comboBox1.Items.Add(obj5);
            comboBox1.Items.Add(obj6);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 選択されたリストのKeyを取得する
            CmbObject obj = (CmbObject)comboBox1.SelectedItem;
            string readFile = obj.Key;
            //MessageBox.Show("comboBox-選択項目: " + readFile);
            Sample.sample.SetCsvfilePath(readFile);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if ( 0 == dataGridView1.RowCount)
            {
                MessageBox.Show("dataGridViewにデータを表示してください。");
                return;
            }
            //ヘッダーを含まないすべてのセルの背景色をXXXにする
            dataGridView1.RowsDefaultCellStyle.BackColor = DataGridViewObject.dataGridObj.getAllRowBackgroundColor();
            //奇数行のセルの背景色をXXXにする
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = DataGridViewObject.dataGridObj.getOddRowBackgroundColor();
            // 選択セルの背景色
            dataGridView1.DefaultCellStyle.SelectionBackColor = DataGridViewObject.dataGridObj.getSelectionBackColor(); ;
            //　選択セルの前景色
            dataGridView1.DefaultCellStyle.SelectionForeColor = DataGridViewObject.dataGridObj.getSelectionForeColor(); ;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ColorSelectDialog f = new ColorSelectDialog();
            //ここではモーダルダイアログボックスとして表示する
            //オーナーウィンドウにthisを指定する
            f.ShowDialog(this);
            //フォームが必要なくなったところで、Disposeを呼び出す
            f.Dispose();
        }
    }

    public partial class Sample

    {
        // メンバー変数の定義 ここから↓
        private DataGridViewRow[] x;
        private string filePath;

        // メンバー変数の定義 ここまで↑
        internal static readonly Sample sample = new Sample();
        // メソッドの定義 ここから↓

        public DataGridViewRow[] GetDataGridViewRow()
        {
            return this.x;
        }

        public void SetDataGridViewRow(DataGridViewRow[] a)
        {
            x = a;
        }

        public string GetCsvFilePahe()
        {
            return this.filePath;
        }

        public void SetCsvfilePath(string path)
        {
            filePath = path;
        }

        // メソッドの定義 ここまで↑
    }

    // データクラス
    class CmbObject
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public CmbObject(string Key, string Value)
        {
            this.Key = Key;
            this.Value = Value;
        }

        public override string ToString()
        {
            return Value;
        }
    }

    // データクラス
    class DataGridViewObject
    {
        private string column1; // 実部を記憶しておく
        private string column2; // 実部を記憶しておく
        private string column3; // 実部を記憶しておく
        private string column4; // 実部を記憶しておく
        private string column5; // 実部を記憶しておく

        private Color allRowBackgroundColor; // 全体行の背景色
        private Color oddRowBackgroundColor; // 奇数行の背景色
        private Color selectedrowBackgroundColor; // 指定行の背景色
        private Color selectionBackColor;
        private Color selectionForeColor;


        internal static readonly DataGridViewObject dataGridObj = new DataGridViewObject();

        public Color getAllRowBackgroundColor() { return allRowBackgroundColor; }
        public void setAllRowBackgroundColor(Color x) { this.allRowBackgroundColor = x; }

        public Color getOddRowBackgroundColor() { return oddRowBackgroundColor; }
        public void setOddRowBackgroundColor(Color x) { this.oddRowBackgroundColor = x; }

        public Color getSelectedrowBackgroundColor() { return selectedrowBackgroundColor; }
        public void setSelectedrowBackgroundColor(Color x) { this.selectedrowBackgroundColor = x; }

        public Color getSelectionBackColor() { return selectionBackColor; }
        public void setSelectionBackColor(Color x) { this.selectionBackColor = x; }

        public Color getSelectionForeColor() { return selectionForeColor; }
        public void setSelectionForeColor(Color x) { this.selectionForeColor = x; }



        public string Column1() { return column1; }
        public void Column1(string x) { this.column1 = x; }

        public string Column2() { return column2; }
        public void Column2(string x) { this.column2 = x; }

        public string Column3() { return column3; }
        public void Column3(string x) { this.column3 = x; }

        public string Column4() { return column4; } 
        public void Column4(string x) { this.column4 = x; } 
        
        public string Column5() { return column5; } 
        public void Column5(string x) { this.column5 = x; }

    }
}
