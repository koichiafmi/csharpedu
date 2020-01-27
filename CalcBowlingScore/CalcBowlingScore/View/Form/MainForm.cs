using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CalcBowlingScore
{
    public partial class MainForm : Form
    {
        private ScoreController scoreController;
        private List<FrameControl> frameControls;

        public MainForm()
        {
            InitializeComponent();

            this.scoreController = new ScoreController();
            this.createFrames();
            this.setupFrames();
        }

        private void buttonGameStart_Click(object sender, EventArgs e)
        {
            this.SuspendLayout();
            this.frameControls.ForEach(f => f.Initialize());
            this.scoreController.Initialize();
            this.ResumeLayout();
        }

        private void buttonGameEnd_Click(object sender, EventArgs e)
        {
            this.SuspendLayout();
            this.frameControls.ForEach(f => f.Finished());
            this.ResumeLayout();
        }

        private void createFrames()
        {
            this.frameControls = new List<FrameControl>()
            {
                this.frameControl1,
                this.frameControl2,
                this.frameControl3,
                this.frameControl4,
                this.frameControl5,
                this.frameControl6,
                this.frameControl7,
                this.frameControl8,
                this.frameControl9,
                this.frameControl10,
            };
        }

        private void setupFrames()
        {
            for (var i = 0; i < this.frameControls.Count; i++)
            {
                this.frameControls[i].Setup((i + 1), this.scoreController);
                this.frameControls[i].UpdateScoreRequest += this.SetScoresPerFrame;
            }
        }

        private void SetScoresPerFrame(object sender, EventArgs e)
        {
            List<string> scores = this.scoreController.GetScores();
            for (var i = 0; i < this.frameControls.Count; i++)
            {
                this.frameControls[i].SetScore(scores[i]);
            }
        }
    }
}