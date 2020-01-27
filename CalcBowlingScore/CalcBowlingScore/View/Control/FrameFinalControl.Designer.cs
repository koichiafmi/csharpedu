namespace CalcBowlingScore
{
    partial class FrameFinalControl
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
            this.pictureBoxThrow3 = new System.Windows.Forms.PictureBox();
            this.throwFinalControl = new CalcBowlingScore.ThrowFinalControl();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxThrow1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxThrow2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxThrow3)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxThrow1
            // 
            this.pictureBoxThrow1.Size = new System.Drawing.Size(41, 50);
            this.pictureBoxThrow1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // pictureBoxThrow2
            // 
            this.pictureBoxThrow2.Location = new System.Drawing.Point(40, -1);
            this.pictureBoxThrow2.Size = new System.Drawing.Size(31, 50);
            this.pictureBoxThrow2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // pictureBoxThrow3
            // 
            this.pictureBoxThrow3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxThrow3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxThrow3.Location = new System.Drawing.Point(70, -1);
            this.pictureBoxThrow3.Name = "pictureBoxThrow3";
            this.pictureBoxThrow3.Size = new System.Drawing.Size(31, 50);
            this.pictureBoxThrow3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxThrow3.TabIndex = 3;
            this.pictureBoxThrow3.TabStop = false;
            // 
            // throwFinalControl
            // 
            this.throwFinalControl.Font = new System.Drawing.Font("Meiryo UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.throwFinalControl.Location = new System.Drawing.Point(0, 105);
            this.throwFinalControl.Margin = new System.Windows.Forms.Padding(0);
            this.throwFinalControl.Name = "throwFinalControl";
            this.throwFinalControl.Size = new System.Drawing.Size(100, 70);
            this.throwFinalControl.TabIndex = 2;
            // 
            // FrameFinalControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.throwFinalControl);
            this.Controls.Add(this.pictureBoxThrow3);
            this.Name = "FrameFinalControl";
            this.Size = new System.Drawing.Size(100, 180);
            this.Controls.SetChildIndex(this.labelScore, 0);
            this.Controls.SetChildIndex(this.pictureBoxThrow1, 0);
            this.Controls.SetChildIndex(this.pictureBoxThrow2, 0);
            this.Controls.SetChildIndex(this.pictureBoxThrow3, 0);
            this.Controls.SetChildIndex(this.throwFinalControl, 0);
            this.Controls.SetChildIndex(this.panel, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxThrow1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxThrow2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxThrow3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxThrow3;
        private ThrowFinalControl throwFinalControl;
    }
}
