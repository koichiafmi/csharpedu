﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppSample003
{
    /// <summary>
    /// バックグラウンド処理の進行状況を表示するフォーム
    /// </summary>

    public partial class ProgressDialog : Form
    {

        /// <summary>
        /// ProgressDialogクラスのコンストラクタ
        /// </summary>
        /// <param name="caption">タイトルバーに表示するテキスト</param>
        /// <param name="doWorkHandler">バックグラウンドで実行するメソッド</param>
        /// <param name="argument">doWorkで取得できるパラメータ</param>
        public ProgressDialog(string caption,
            DoWorkEventHandler doWork,
            object argument)
        {
            InitializeComponent();

            //初期設定
            this.workerArgument = argument;
            this.Text = caption;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.ControlBox = false;
            this.CancelButton = this.cancelAsyncButton;
            this.messageLabel.Text = "";
            this.progressBar1.Minimum = 0;
            this.progressBar1.Maximum = 100;
            this.progressBar1.Value = 0;
            this.cancelAsyncButton.Text = "キャンセル";
            this.cancelAsyncButton.Enabled = true;
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;

            //イベント
            this.Shown += new EventHandler(ProgressDialog_Shown);
            this.cancelAsyncButton.Click += new EventHandler(cancelAsyncButton_Click);
            this.backgroundWorker1.DoWork += doWork;
            this.backgroundWorker1.ProgressChanged +=
                new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted +=
                new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
        }

        /// <summary>
        /// ProgressDialogクラスのコンストラクタ
        /// </summary>
        public ProgressDialog(string formTitle,
            DoWorkEventHandler doWorkHandler)
            : this(formTitle, doWorkHandler, null)
        {
        }

        private object workerArgument = null;

        private object _result = null;
        /// <summary>
        /// DoWorkイベントハンドラで設定された結果
        /// </summary>
        public object Result
        {
            get
            {
                return this._result;
            }
        }

        private Exception _error = null;
        /// <summary>
        /// バックグラウンド処理中に発生したエラー
        /// </summary>
        public Exception Error
        {
            get
            {
                return this._error;
            }
        }

        /// <summary>
        /// 進行状況ダイアログで使用しているBackgroundWorkerクラス
        /// </summary>
        public BackgroundWorker BackgroundWorker
        {
            get
            {
                return this.backgroundWorker1;
            }
        }

        //フォームが表示されたときにバックグラウンド処理を開始
        private void ProgressDialog_Shown(object sender, EventArgs e)
        {
            this.backgroundWorker1.RunWorkerAsync(this.workerArgument);
        }

        //キャンセルボタンが押されたとき
        private void cancelAsyncButton_Click(object sender, EventArgs e)
        {
            cancelAsyncButton.Enabled = false;
            backgroundWorker1.CancelAsync();
        }
        //ReportProgressメソッドが呼び出されたとき
        private void backgroundWorker1_ProgressChanged(
            object sender, ProgressChangedEventArgs e)
        {
            //プログレスバーの値を変更する
            if (e.ProgressPercentage < this.progressBar1.Minimum)
            {
                this.progressBar1.Value = this.progressBar1.Minimum;
            }
            else if (this.progressBar1.Maximum < e.ProgressPercentage)
            {
                this.progressBar1.Value = this.progressBar1.Maximum;
            }
            else
            {
                //this.progressBar1.Value = e.ProgressPercentage;
                if (e.ProgressPercentage < progressBar1.Maximum)
                {
                    // 指定する値がMaximumより小さい場合
                    progressBar1.Value = e.ProgressPercentage + 1;
                    progressBar1.Value = e.ProgressPercentage;
                }
                else
                {
                    progressBar1.Maximum++;
                    progressBar1.Value = e.ProgressPercentage + 1;
                    progressBar1.Value = e.ProgressPercentage;
                    progressBar1.Maximum--;
                }
                this.messageLabel.Text = (string)e.UserState;
                this.messageLabel.Update();
            }
        }

        //バックグラウンド処理が終了したとき
        private void backgroundWorker1_RunWorkerCompleted(
            object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(this,
                    "エラー",
                    "エラーが発生しました。\n\n" + e.Error.Message,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                this._error = e.Error;
                this.DialogResult = DialogResult.Abort;
            }
            else if (e.Cancelled)
            {
                this.DialogResult = DialogResult.Cancel;
            }
            else
            {
                this._result = e.Result;
                this.DialogResult = DialogResult.OK;
            }

            this.Close();
        }

        private void InvokeControl(Control control, System.Threading.ThreadStart method)
        {
            if (control.IsHandleCreated)
                control.Invoke(method);
            else
                method();
        }

        public void SetProgressRange(int minimum, int maximum,bool marqueeStyle,int marqueeAnimationSpeed)
        {
            InvokeControl(progressBar1,
                new System.Threading.ThreadStart(() => {
                    progressBar1.Minimum = minimum;
                    progressBar1.Maximum = maximum;
                    messageLabel.Visible = true;
                    if (marqueeStyle == true)
                    {
                        //ProgressBar1をマーキースタイルにする
                        progressBar1.Style = ProgressBarStyle.Marquee;
                        //ブロックの移動速度をデフォルトの倍にする
                        progressBar1.MarqueeAnimationSpeed = marqueeAnimationSpeed;
                        messageLabel.Visible = false;
                    }
                }));
        }

        public int GetProgressMaxValue
        {
            get { return progressBar1.Maximum; }
        }

        public int GetProgressMinValue
        {
            get { return progressBar1.Minimum; }
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void cancelAsyncButton_Click_1(object sender, EventArgs e)
        {

        }
    }
}
