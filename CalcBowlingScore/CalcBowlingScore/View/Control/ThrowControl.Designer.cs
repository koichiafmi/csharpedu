namespace CalcBowlingScore
{
    partial class ThrowControl
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.label2ndThrow = new System.Windows.Forms.Label();
            this.comboBox2ndThrow = new System.Windows.Forms.ComboBox();
            this.label1stThrow = new System.Windows.Forms.Label();
            this.comboBox1stThrow = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label2ndThrow
            // 
            this.label2ndThrow.Font = new System.Drawing.Font("Meiryo UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2ndThrow.Location = new System.Drawing.Point(0, 22);
            this.label2ndThrow.Margin = new System.Windows.Forms.Padding(0);
            this.label2ndThrow.Name = "label2ndThrow";
            this.label2ndThrow.Size = new System.Drawing.Size(40, 20);
            this.label2ndThrow.TabIndex = 2;
            this.label2ndThrow.Text = "2 投目";
            this.label2ndThrow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBox2ndThrow
            // 
            this.comboBox2ndThrow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2ndThrow.Enabled = false;
            this.comboBox2ndThrow.Font = new System.Drawing.Font("Meiryo UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.comboBox2ndThrow.FormattingEnabled = true;
            this.comboBox2ndThrow.Location = new System.Drawing.Point(45, 22);
            this.comboBox2ndThrow.Margin = new System.Windows.Forms.Padding(0);
            this.comboBox2ndThrow.Name = "comboBox2ndThrow";
            this.comboBox2ndThrow.Size = new System.Drawing.Size(50, 22);
            this.comboBox2ndThrow.TabIndex = 3;
            this.comboBox2ndThrow.SelectedIndexChanged += new System.EventHandler(this.comboBox2ndThrow_SelectedIndexChanged);
            // 
            // label1stThrow
            // 
            this.label1stThrow.Font = new System.Drawing.Font("Meiryo UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1stThrow.Location = new System.Drawing.Point(0, 1);
            this.label1stThrow.Margin = new System.Windows.Forms.Padding(0);
            this.label1stThrow.Name = "label1stThrow";
            this.label1stThrow.Size = new System.Drawing.Size(40, 20);
            this.label1stThrow.TabIndex = 0;
            this.label1stThrow.Text = "1 投目";
            this.label1stThrow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBox1stThrow
            // 
            this.comboBox1stThrow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1stThrow.Enabled = false;
            this.comboBox1stThrow.Font = new System.Drawing.Font("Meiryo UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.comboBox1stThrow.FormattingEnabled = true;
            this.comboBox1stThrow.Location = new System.Drawing.Point(45, 1);
            this.comboBox1stThrow.Margin = new System.Windows.Forms.Padding(0);
            this.comboBox1stThrow.Name = "comboBox1stThrow";
            this.comboBox1stThrow.Size = new System.Drawing.Size(50, 22);
            this.comboBox1stThrow.TabIndex = 1;
            this.comboBox1stThrow.SelectedIndexChanged += new System.EventHandler(this.comboBox1stThrow_SelectedIndexChanged);
            // 
            // ThrowControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.label2ndThrow);
            this.Controls.Add(this.comboBox2ndThrow);
            this.Controls.Add(this.label1stThrow);
            this.Controls.Add(this.comboBox1stThrow);
            this.Font = new System.Drawing.Font("Meiryo UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ThrowControl";
            this.Size = new System.Drawing.Size(100, 70);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Label label2ndThrow;
        protected System.Windows.Forms.ComboBox comboBox2ndThrow;
        protected System.Windows.Forms.Label label1stThrow;
        protected System.Windows.Forms.ComboBox comboBox1stThrow;
    }
}
