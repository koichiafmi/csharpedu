namespace DbMultiTool.accord
{
    partial class AccordForm
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
            this.おわろうかな = new System.Windows.Forms.Button();
            this.かけたよ = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.もういちどかくよ = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // おわろうかな
            // 
            this.おわろうかな.Location = new System.Drawing.Point(511, 12);
            this.おわろうかな.Name = "おわろうかな";
            this.おわろうかな.Size = new System.Drawing.Size(204, 44);
            this.おわろうかな.TabIndex = 2;
            this.おわろうかな.Text = "おわろうかな";
            this.おわろうかな.UseVisualStyleBackColor = true;
            this.おわろうかな.Click += new System.EventHandler(this.おわろうかな_Click);
            // 
            // かけたよ
            // 
            this.かけたよ.Location = new System.Drawing.Point(511, 62);
            this.かけたよ.Name = "かけたよ";
            this.かけたよ.Size = new System.Drawing.Size(204, 44);
            this.かけたよ.TabIndex = 3;
            this.かけたよ.Text = "かけたよ";
            this.かけたよ.UseVisualStyleBackColor = true;
            this.かけたよ.Click += new System.EventHandler(this.かけたよ_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(511, 112);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(204, 19);
            this.textBox1.TabIndex = 4;
            // 
            // もういちどかくよ
            // 
            this.もういちどかくよ.Location = new System.Drawing.Point(511, 137);
            this.もういちどかくよ.Name = "もういちどかくよ";
            this.もういちどかくよ.Size = new System.Drawing.Size(204, 44);
            this.もういちどかくよ.TabIndex = 5;
            this.もういちどかくよ.Text = "もういちどかくよ";
            this.もういちどかくよ.UseVisualStyleBackColor = true;
            this.もういちどかくよ.Click += new System.EventHandler(this.もういちどかくよ_Click);
            // 
            // AccordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(727, 450);
            this.Controls.Add(this.もういちどかくよ);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.かけたよ);
            this.Controls.Add(this.おわろうかな);
            this.Name = "AccordForm";
            this.Text = "AccordForm";
            this.Load += new System.EventHandler(this.AccordForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button おわろうかな;
        private System.Windows.Forms.Button かけたよ;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button もういちどかくよ;
    }
}