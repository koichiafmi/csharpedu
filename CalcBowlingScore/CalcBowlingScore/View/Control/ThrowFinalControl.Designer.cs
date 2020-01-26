namespace CalcBowlingScore
{
    partial class ThrowFinalControl
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
            this.label3rdThrow = new System.Windows.Forms.Label();
            this.comboBox3rdThrow = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label3rdThrow
            // 
            this.label3rdThrow.Font = new System.Drawing.Font("Meiryo UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3rdThrow.Location = new System.Drawing.Point(0, 42);
            this.label3rdThrow.Margin = new System.Windows.Forms.Padding(0);
            this.label3rdThrow.Name = "label3rdThrow";
            this.label3rdThrow.Size = new System.Drawing.Size(40, 20);
            this.label3rdThrow.TabIndex = 4;
            this.label3rdThrow.Text = "3 投目";
            this.label3rdThrow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBox3rdThrow
            // 
            this.comboBox3rdThrow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox3rdThrow.Enabled = false;
            this.comboBox3rdThrow.Font = new System.Drawing.Font("Meiryo UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.comboBox3rdThrow.FormattingEnabled = true;
            this.comboBox3rdThrow.Location = new System.Drawing.Point(45, 42);
            this.comboBox3rdThrow.Margin = new System.Windows.Forms.Padding(0);
            this.comboBox3rdThrow.Name = "comboBox3rdThrow";
            this.comboBox3rdThrow.Size = new System.Drawing.Size(50, 22);
            this.comboBox3rdThrow.TabIndex = 5;
            this.comboBox3rdThrow.SelectedIndexChanged += new System.EventHandler(this.comboBox3rdThrow_SelectedIndexChanged);
            // 
            // ThrowFinalControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.label3rdThrow);
            this.Controls.Add(this.comboBox3rdThrow);
            this.Name = "ThrowFinalControl";
            this.Controls.SetChildIndex(this.comboBox1stThrow, 0);
            this.Controls.SetChildIndex(this.label1stThrow, 0);
            this.Controls.SetChildIndex(this.comboBox2ndThrow, 0);
            this.Controls.SetChildIndex(this.label2ndThrow, 0);
            this.Controls.SetChildIndex(this.comboBox3rdThrow, 0);
            this.Controls.SetChildIndex(this.label3rdThrow, 0);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Label label3rdThrow;
        protected System.Windows.Forms.ComboBox comboBox3rdThrow;
    }
}
