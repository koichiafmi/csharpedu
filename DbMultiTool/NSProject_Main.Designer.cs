namespace DbMultiTool
{
    partial class NSProject_Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gantChart1 = new DbMultiTool.GantChart();
            this.SuspendLayout();
            // 
            // gantChart1
            // 
            this.gantChart1.Location = new System.Drawing.Point(12, 12);
            this.gantChart1.Name = "gantChart1";
            this.gantChart1.Size = new System.Drawing.Size(905, 501);
            this.gantChart1.TabIndex = 0;
            // 
            // NSProject_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(922, 519);
            this.Controls.Add(this.gantChart1);
            this.Name = "NSProject_Main";
            this.Text = "NSProject_Main";
            this.Load += new System.EventHandler(this.NSProject_Main_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private GantChart gantChart1;
    }
}