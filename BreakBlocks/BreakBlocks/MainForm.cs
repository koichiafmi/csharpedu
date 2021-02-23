using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;

namespace BreakBlocks
{
    public partial class MainForm : Form
    {
        private const int RADIUS = 10;
        private const int BAR_WIDTH = 100;
        private const int BAR_HEIGHT = 5;

        private Vector ballPosition;
        private Vector ballSpeed;
        private Timer timer;

        private Rectangle barRectangle;
        private List<Rectangle> blocks;

        public MainForm()
        {
            InitializeComponent();

            this.ballPosition = new Vector(this.DisplayRectangle.Width / 2,
                                           this.DisplayRectangle.Height - 50);
            this.ballSpeed = new Vector(-4, -6);

            this.barRectangle = new Rectangle((this.DisplayRectangle.Width / 2) - (BAR_WIDTH / 2),
                                     (this.DisplayRectangle.Height - BAR_HEIGHT),
                                     BAR_WIDTH,
                                     BAR_HEIGHT);

            this.blocks = new List<Rectangle>();
            for (int x = 0; x <= this.Width - 130; x += 100)
            {
                for (int y = 0; y <= 150; y += 40)
                {
                    this.blocks.Add(new Rectangle(25 + x, y, 80, 20));
                }
            }

            this.timer = new Timer();
            this.timer.Interval = 35;
            this.timer.Tick += new EventHandler(this.update);
            this.timer.Start();
        }

        private bool isBallHitTheBar()
        {
            var ballBottom = (int)this.ballPosition.Y + RADIUS;

            return
                ((this.barRectangle.Left <= this.ballPosition.X &&      // 左端チェック
                  this.ballPosition.X <= this.barRectangle.Right) &&    // 右端チェック
                 (this.barRectangle.Top <= ballBottom));                // 当たりチェック
        }

        private int isBallHitTheBlock(Rectangle block)
        {
            if (block.Left <= this.ballPosition.X && this.ballPosition.X <= block.Right &&
                block.Top <= this.ballPosition.Y && this.ballPosition.Y <= block.Bottom)
            {
                var x = this.ballPosition.X - (block.Left + block.Width / 2);
                var y = this.ballPosition.Y - (block.Top + block.Height / 2);
                if (x > 0 && y > 0)
                {
                    if (block.Bottom - this.ballPosition.Y < block.Right - this.ballPosition.X)
                    {
                        // 下から当たった場合
                        return 1;
                    }
                    else
                    {
                        // 右から当たった場合
                        return 2;
                    }
                }
                else if (x > 0 && y < 0)
                {
                    if (this.ballPosition.Y - block.Top < block.Right - this.ballPosition.X)
                    {
                        // 上から当たった場合
                        return 3;
                    }
                    else
                    {
                        // 右から当たった場合
                        return 2;
                    }
                }
                else if (x < 0 && y > 0)
                {
                    if (block.Bottom - this.ballPosition.Y < this.ballPosition.X - block.Left)
                    {
                        // 下から当たった場合
                        return 1;
                    }
                    else
                    {
                        // 左から当たった場合
                        return 4;
                    }
                }
                else if (x < 0 && y < 0)
                {
                    if (this.ballPosition.Y - block.Top < this.ballPosition.X - block.Left)
                    {
                        // 上から当たった場合
                        return 3;
                    }
                    else
                    {
                        // 左から当たった場合
                        return 4;
                    }
                }
            }

            return 0;
        }

        private void update(object sender, EventArgs e)
        {
            // ボールの移動
            this.ballPosition += this.ballSpeed;

            // 左右の壁でのバウンド
            if (this.ballPosition.X + RADIUS > this.DisplayRectangle.Width ||
                this.ballPosition.X - RADIUS < 0)
            {
                this.ballSpeed.X *= -1;
            }

            // 上の壁でバウンド
            if (this.ballPosition.Y - RADIUS < 0)
            {
                this.ballSpeed.Y *= -1;
            }

            // バーに当たってバウンド
            if (this.isBallHitTheBar())
            {
                this.ballSpeed.Y *= -1;
            }

            // ブロックに当たってバウンド
            for (int i = 0; i < this.blocks.Count; i++)
            {
                var result = this.isBallHitTheBlock(blocks[i]);
                if (result == 1 || result == 3)
                {
                    this.ballSpeed.Y *= -1;
                    this.blocks.Remove(this.blocks[i]);
                    break;
                }
                else if (result == 2 || result == 4)
                {
                    this.ballSpeed.X *= -1;
                    this.blocks.Remove(this.blocks[i]);
                    break;
                }
            }

            if (this.blocks.Count == 0)
            {
                this.timer.Stop();
                MessageBox.Show("CONGRATULATIONS!!" + Environment.NewLine + "GAME CLEARED.");
                this.Close();
                return;
            }

            // 下の壁を通過した場合は終了
            if (this.ballPosition.Y + RADIUS > this.Bounds.Height)
            {
                this.timer.Stop();
                MessageBox.Show("GAME OVER...");
                this.Close();
                return;
            }

            // 再描画
            this.Invalidate();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            var ballBrush = new SolidBrush(Color.Red);
            var barBrush = new SolidBrush(Color.DarkGray);
            var blockBrush = new SolidBrush(Color.LightBlue);

            float px = (float)this.ballPosition.X - RADIUS;
            float py = (float)this.ballPosition.Y - RADIUS;

            e.Graphics.FillEllipse(ballBrush, px, py, RADIUS * 2, RADIUS * 2);
            e.Graphics.FillRectangle(barBrush, this.barRectangle);
            this.blocks.ForEach(block => e.Graphics.FillRectangle(blockBrush, block));
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.timer.Dispose();
            this.timer = null;
        }

        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'a')       // a キーが押されたとき
            {
                this.barRectangle.X -= 20;
            }
            else if (e.KeyChar == 's') // s キーが押されたとき
            {
                this.barRectangle.X += 20;
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)       // ←キーが押されたとき
            {
                this.barRectangle.X -= 20;
                if (this.barRectangle.X < 0)
                {
                    this.barRectangle.X = 0;
                }
            }
            else if (e.KeyCode == Keys.Right) // →キーが押されたとき
            {
                this.barRectangle.X += 20;
                if (this.barRectangle.Right > this.Bounds.Width)
                {
                    this.barRectangle.X = this.Bounds.Width - BAR_WIDTH;
                }
            }
        }
    }
}
