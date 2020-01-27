using System.Drawing;

namespace CalcBowlingScore
{
    public partial class FrameFinalControl : FrameControl
    {
        public FrameFinalControl() : base()
        {
            InitializeComponent();

            if (this.throwFinalControl.Thrown == null)
            {
                this.throwFinalControl.Thrown += this.thrown;
            }
        }

        public override void Initialize()
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

            if (this.pictureBoxThrow3.Image != null)
            {
                this.pictureBoxThrow3.Image.Dispose();
                this.pictureBoxThrow3.Image = null;
            }

            this.labelScore.Text = string.Empty;

            this.throwFinalControl.Initialize();
        }

        public override void Finished()
        {
            this.throwFinalControl.Finished();
        }

        public override Image getScoreImage(int throwCount, int pinCount)
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
                if (totalPins == 20)
                {
                    path = Common.ImageStrike;
                }
                else if (totalPins == 10)
                {
                    path = Common.ImageSpare;
                }
                else if (pinCount == 0)
                {
                    path = Common.ImageBar;
                }
            }
            else if (throwCount == 3)
            {
                if (totalPins == 30)
                {
                    path = Common.ImageStrike;
                }
                else if (totalPins == 20)
                {
                    if (pinCount == 10)
                    {
                        path = Common.ImageStrike;
                    }
                    else
                    {
                        path = Common.ImageSpare;
                    }
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

        protected override void setScoreImage(int throwCount, Image image)
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
            else if (throwCount == 3)
            {
                if (this.pictureBoxThrow3.Image != null)
                {
                    this.pictureBoxThrow3.Image.Dispose();
                    this.pictureBoxThrow3.Image = null;
                }

                this.pictureBoxThrow3.Image = image;
            }
        }
    }
}
