using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CalcBowlingScore
{
    public partial class MainForm : Form
    {
        private ScoreController scoreController;
        private List<FlameControl> flameControls;

        public MainForm()
        {
            InitializeComponent();

            this.scoreController = new ScoreController();
            this.createFlames();
            this.setupFlames();
        }

        private void buttonGameStart_Click(object sender, EventArgs e)
        {
            this.SuspendLayout();
            this.flameControls.ForEach(f => f.Initialize());
            this.scoreController.Initialize();
            this.ResumeLayout();
        }

        private void buttonGameEnd_Click(object sender, EventArgs e)
        {
            this.SuspendLayout();
            this.flameControls.ForEach(f => f.Finished());
            this.ResumeLayout();
        }

        private void createFlames()
        {
            this.flameControls = new List<FlameControl>()
            {
                this.flameControl1,
                this.flameControl2,
                this.flameControl3,
                this.flameControl4,
                this.flameControl5,
                this.flameControl6,
                this.flameControl7,
                this.flameControl8,
                this.flameControl9,
                this.flameControl10,
            };
        }

        private void setupFlames()
        {
            for (var i = 0; i < this.flameControls.Count; i++)
            {
                this.flameControls[i].Setup((i + 1), this.scoreController);
                this.flameControls[i].UpdateScoreRequest += this.SetScoresPerFlame;
            }
        }

        private void SetScoresPerFlame(object sender, EventArgs e)
        {
            List<string> scores = this.scoreController.GetScores();
            for (var i = 0; i < this.flameControls.Count; i++)
            {
                this.flameControls[i].SetScore(scores[i]);
            }
        }
    }
}