using System;
using System.Windows.Forms;

namespace DbMultiTool
{
    public partial class NSProject_Main : Form
    {
        #region コンストラクタ
        public NSProject_Main()
        {
            InitializeComponent();
        }
        #endregion

        #region イベント
        private void NSProject_Main_Load(object sender, EventArgs e)
        {

        }

        private void TestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var modelPpoiNanika = new MethodBaseProxy<ModelPpoiClass>(1, 1).GetInstance();
            modelPpoiNanika.GetOutPut();
        }
        #endregion
    }
}
