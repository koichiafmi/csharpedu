namespace DbMultiTool
{
    partial class GantChart
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

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("計画書作成");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("DR");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("計画", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("機能DB作成");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("DR");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("要求", new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode5});
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("変更設計書作成");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("DR");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("設計", new System.Windows.Forms.TreeNode[] {
            treeNode7,
            treeNode8});
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("コーディング");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("単体テスト作成");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("単体テスト作成");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("DR");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("製造", new System.Windows.Forms.TreeNode[] {
            treeNode10,
            treeNode11,
            treeNode12,
            treeNode13});
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("機能テスト作成");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("DR");
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("機能テスト作成");
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("機能テスト", new System.Windows.Forms.TreeNode[] {
            treeNode15,
            treeNode16,
            treeNode17});
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("RX9999", new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode6,
            treeNode9,
            treeNode14,
            treeNode18});
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("V9.99R000開発", new System.Windows.Forms.TreeNode[] {
            treeNode19});
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(3, 3);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "ノード7";
            treeNode1.Text = "計画書作成";
            treeNode2.Name = "ノード10";
            treeNode2.Text = "DR";
            treeNode3.Name = "ノード1";
            treeNode3.Text = "計画";
            treeNode4.Name = "ノード8";
            treeNode4.Text = "機能DB作成";
            treeNode5.Name = "ノード9";
            treeNode5.Text = "DR";
            treeNode6.Name = "ノード2";
            treeNode6.Text = "要求";
            treeNode7.Name = "ノード11";
            treeNode7.Text = "変更設計書作成";
            treeNode8.Name = "ノード12";
            treeNode8.Text = "DR";
            treeNode9.Name = "ノード3";
            treeNode9.Text = "設計";
            treeNode10.Name = "ノード13";
            treeNode10.Text = "コーディング";
            treeNode11.Name = "ノード14";
            treeNode11.Text = "単体テスト作成";
            treeNode12.Name = "ノード15";
            treeNode12.Text = "単体テスト作成";
            treeNode13.Name = "ノード16";
            treeNode13.Text = "DR";
            treeNode14.Name = "ノード4";
            treeNode14.Text = "製造";
            treeNode15.Name = "ノード17";
            treeNode15.Text = "機能テスト作成";
            treeNode16.Name = "ノード18";
            treeNode16.Text = "DR";
            treeNode17.Name = "ノード19";
            treeNode17.Text = "機能テスト作成";
            treeNode18.Name = "ノード5";
            treeNode18.Text = "機能テスト";
            treeNode19.Name = "ノード0";
            treeNode19.Text = "RX9999";
            treeNode20.Name = "ノード20";
            treeNode20.Text = "V9.99R000開発";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode20});
            this.treeView1.ShowPlusMinus = false;
            this.treeView1.ShowRootLines = false;
            this.treeView1.Size = new System.Drawing.Size(194, 495);
            this.treeView1.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(203, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 21;
            this.dataGridView1.Size = new System.Drawing.Size(194, 495);
            this.dataGridView1.TabIndex = 1;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "担当者";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "開始日";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "終了日";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "工数";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "進捗率";
            this.Column5.Name = "Column5";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.dataGridView2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.treeView1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(905, 501);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.Location = new System.Drawing.Point(403, 3);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowTemplate.Height = 21;
            this.dataGridView2.Size = new System.Drawing.Size(499, 495);
            this.dataGridView2.TabIndex = 4;
            // 
            // GantChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "GantChart";
            this.Size = new System.Drawing.Size(905, 501);
            this.Load += new System.EventHandler(this.GantChart_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dataGridView2;
    }
}
