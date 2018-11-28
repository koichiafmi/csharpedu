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
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Chart
            // 
            this.Chart.AutoSize = true;
            this.Chart.Location = new System.Drawing.Point(184, 195);
            this.Chart.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Chart.Name = "Chart";
            this.Chart.Size = new System.Drawing.Size(52, 16);
            this.Chart.TabIndex = 0;
            this.Chart.Text = "Chart";
            this.Chart.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 49);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "日付：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 104);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "体重：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 165);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "体脂肪：";
            // 
            // Date
            // 
            this.Date.Location = new System.Drawing.Point(69, 49);
            this.Date.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Date.Name = "Date";
            this.Date.Size = new System.Drawing.Size(76, 19);
            this.Date.TabIndex = 4;
            // 
            // Fat
            // 
            this.Fat.Location = new System.Drawing.Point(69, 102);
            this.Fat.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Fat.Name = "Fat";
            this.Fat.Size = new System.Drawing.Size(76, 19);
            this.Fat.TabIndex = 5;
            // 
            // Weight
            // 
            this.Weight.Location = new System.Drawing.Point(69, 162);
            this.Weight.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Weight.Name = "Weight";
            this.Weight.Size = new System.Drawing.Size(76, 19);
            this.Weight.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(69, 195);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(56, 18);
            this.button1.TabIndex = 7;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(151, 49);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(20, 18);
            this.button2.TabIndex = 8;
            this.button2.Text = "...";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // HearthLogger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(262, 234);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Weight);
            this.Controls.Add(this.Fat);
            this.Controls.Add(this.Date);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Chart);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
        private System.Windows.Forms.Button button2;
    }
}

