using System;
using System.Drawing;
using System.Windows.Forms;

namespace CalcBowlingScore
{
    public partial class FrameControl : UserControl
    {
        public EventHandler UpdateScoreRequest = null;

        protected int index;
        protected ScoreController scoreController;


        public FrameControl()
        {
            InitializeComponent();

            if (this.throwControl.Thrown == null)
            {
                this.throwControl.Thrown += this.thrown;
            }
        }

        public void Setup(int idx, ScoreController sc)
        {
            this.index = idx;
            this.scoreController = sc;
        }

        public virtual void Initialize()
        {
            if (this.pictureBoxThrow1.Image != null)
            {
                this.pictureBoxThrow1.Image.Dispose();
                this.pictureBoxThrow1.Image = null;
            }

            if (this.pictureBoxThrow2.Image != null)
            {
                this.pictureBoxThrow2.Image.Dispose();
                this.pictureBoxThrow2.Image = null;
            }

            this.labelScore.Text = string.Empty;

            this.throwControl.Initialize();
        }

        public void SetScore(string score)
        {
            this.labelScore.Text = score;
        }

        public virtual void Finished()
        {
            this.throwControl.Finished();
        }

        protected void thrown(object sender, ThrowData e)
        {
            this.scoreController.SetPins(this.index, e);
            Image image = this.getScoreImage(e.ThrowCount, e.Pins);
            this.setScoreImage(e.ThrowCount, image);

            if (this.UpdateScoreRequest != null)
            {
                this.UpdateScoreRequest(this, EventArgs.Empty);
            }
        }

        public virtual Image getScoreImage(int throwCount, int pinCount)
        {
            int totalPins = this.scoreController.GetTotalPinsInFrame(this.index);
            var path = string.Empty;

            if (pinCount == 9)
            {
                path = Common.Image9;
            }
            else if (pinCount == 8)
            {
                path = Common.Image8;
            }
            else if (pinCount == 7)
            {
                path = Common.Image7;
            }
            else if (pinCount == 6)
            {
                path = Common.Image6;
            }
            else if (pinCount == 5)
            {
                path = Common.Image5;
            }
            else if (pinCount == 4)
            {
                path = Common.Image4;
            }
            else if (pinCount == 3)
            {
                path = Common.Image3;
            }
            else if (pinCount == 2)
            {
                path = Common.Image2;
            }
            else if (pinCount == 1)
            {
                path = Common.Image1;
            }

            if (throwCount == 1)
            {
                if (pinCount == 10)
                {
                    path = Common.ImageStrike;
                }
                else if (pinCount == 0)
                {
                    path = Common.ImageGutter;
                }
            }
            else if (throwCount == 2)
            {
                if (totalPins == 10)
                {
                    path = Common.ImageSpare;
                }
                else if (pinCount == 0)
                {
                    path = Common.ImageBar;
                }
            }

            if (string.IsNullOrEmpty(path))
            {
                return null;
            }
            else
            {
                return Image.FromFile(path);
            }
        }

        protected virtual void setScoreImage(int throwCount, Image image)
        {
            if (throwCount == 1)
            {
                if (this.pictureBoxThrow1.Image != null)
                {
                    this.pictureBoxThrow1.Image.Dispose();
                    this.pictureBoxThrow1.Image = null;
                }

                this.pictureBoxThrow1.Image = image;
            }
            else if (throwCount == 2)
            {
                if (this.pictureBoxThrow2.Image != null)
                {
                    this.pictureBoxThrow2.Image.Dispose();
                    this.pictureBoxThrow2.Image = null;
                }

                this.pictureBoxThrow2.Image = image;
            }
        }
    }
}
