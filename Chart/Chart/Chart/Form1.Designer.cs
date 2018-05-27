namespace Chart
{
    partial class HearthLogger
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.Chart = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Date = new System.Windows.Forms.TextBox();
            this.Fat = new System.Windows.Forms.TextBox();
            this.Weight = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Chart
            // 
            this.Chart.AutoSize = true;
            this.Chart.Location = new System.Drawing.Point(246, 244);
            this.Chart.Name = "Chart";
            this.Chart.Size = new System.Drawing.Size(64, 19);
            this.Chart.TabIndex = 0;
            this.Chart.Text = "Chart";
            this.Chart.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "日付：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 130);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "体重：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 206);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "体脂肪：";
            // 
            // Date
            // 
            this.Date.Location = new System.Drawing.Point(92, 61);
            this.Date.Name = "Date";
            this.Date.Size = new System.Drawing.Size(100, 22);
            this.Date.TabIndex = 4;
            // 
            // Fat
            // 
            this.Fat.Location = new System.Drawing.Point(92, 127);
            this.Fat.Name = "Fat";
            this.Fat.Size = new System.Drawing.Size(100, 22);
            this.Fat.TabIndex = 5;
            // 
            // Weight
            // 
            this.Weight.Location = new System.Drawing.Point(92, 203);
            this.Weight.Name = "Weight";
            this.Weight.Size = new System.Drawing.Size(100, 22);
            this.Weight.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(92, 244);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // HearthLogger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 292);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Weight);
            this.Controls.Add(this.Fat);
            this.Controls.Add(this.Date);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Chart);
            this.Name = "HearthLogger";
            this.Text = "Hearth Logger";
            this.Load += new System.EventHandler(this.HearthLogger_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox Chart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Date;
        private System.Windows.Forms.TextBox Fat;
        private System.Windows.Forms.TextBox Weight;
        private System.Windows.Forms.Button button1;
    }
}

