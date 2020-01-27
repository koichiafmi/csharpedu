namespace CalcBowlingScore
{
    partial class FrameControl
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
            this.labelScore = new System.Windows.Forms.Label();
            this.pictureBoxThrow1 = new System.Windows.Forms.PictureBox();
            this.pictureBoxThrow2 = new System.Windows.Forms.PictureBox();
            this.panel = new System.Windows.Forms.Panel();
            this.throwControl = new CalcBowlingScore.ThrowControl();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxThrow1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxThrow2)).BeginInit();
            this.SuspendLayout();
            // 
            // labelScore
            // 
            this.labelScore.Font = new System.Drawing.Font("Meiryo UI", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelScore.Location = new System.Drawing.Point(0, 50);
            this.labelScore.Margin = new System.Windows.Forms.Padding(0);
            this.labelScore.Name = "labelScore";
            this.labelScore.Size = new System.Drawing.Size(100, 50);
            this.labelScore.TabIndex = 0;
            this.labelScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBoxThrow1
            // 
            this.pictureBoxThrow1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxThrow1.Location = new System.Drawing.Point(0, -1);
            this.pictureBoxThrow1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBoxThrow1.Name = "pictureBoxThrow1";
            this.pictureBoxThrow1.Size = new System.Drawing.Size(50, 50);
            this.pictureBoxThrow1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxThrow1.TabIndex = 1;
            this.pictureBoxThrow1.TabStop = false;
            // 
            // pictureBoxThrow2
            // 
            this.pictureBoxThrow2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxThrow2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxThrow2.Location = new System.Drawing.Point(50, -1);
            this.pictureBoxThrow2.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBoxThrow2.Name = "pictureBoxThrow2";
            this.pictureBoxThrow2.Size = new System.Drawing.Size(50, 50);
            this.pictureBoxThrow2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxThrow2.TabIndex = 2;
            this.pictureBoxThrow2.TabStop = false;
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.Color.Black;
            this.panel.Location = new System.Drawing.Point(0, 100);
            this.panel.Margin = new System.Windows.Forms.Padding(0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(102, 1);
            this.panel.TabIndex = 1;
            // 
            // throwControl
            // 
            this.throwControl.Font = new System.Drawing.Font("Meiryo UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.throwControl.Location = new System.Drawing.Point(0, 105);
            this.throwControl.Margin = new System.Windows.Forms.Padding(0);
            this.throwControl.Name = "throwControl";
            this.throwControl.Size = new System.Drawing.Size(100, 70);
            this.throwControl.TabIndex = 2;
            // 
            // FrameControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.panel);
            this.Controls.Add(this.throwControl);
            this.Controls.Add(this.pictureBoxThrow2);
            this.Controls.Add(this.pictureBoxThrow1);
            this.Controls.Add(this.labelScore);
            this.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "FrameControl";
            this.Size = new System.Drawing.Size(100, 180);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxThrow1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxThrow2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        protected System.Windows.Forms.PictureBox pictureBoxThrow1;
        protected System.Windows.Forms.PictureBox pictureBoxThrow2;
        protected System.Windows.Forms.Label labelScore;
        private ThrowControl throwControl;
        protected System.Windows.Forms.Panel panel;
    }
}
