using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //オブジェクト作成
            var canvas = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            var g = Graphics.FromImage(canvas);

            //画像を読み込む
            var img = Image.FromFile(@"..\..\test.png");

            //普通に画像を描画
            g.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height));

            //右に平行移動
            g.TranslateTransform(150, 0);
            //画像を描画
            g.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height));

            //45度回転
            g.RotateTransform(45F);
            //画像を描画
            g.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height));

            g.ResetTransform();


            //リソースを解放する
            img.Dispose();
            g.Dispose();

            //PictureBox1に表示する
            this.pictureBox1.Image = canvas;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.pictureBox1.Image.Dispose();

            //オブジェクト作成
            var canvas = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            var g = Graphics.FromImage(canvas);

            //画像を読み込む
            var img = Image.FromFile(@"..\..\test.png");

            //普通に画像を描画
            g.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height));

            //右に平行移動
            g.TranslateTransform(150, 0, MatrixOrder.Append);
            //画像を描画
            g.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height));

            //45度回転
            g.RotateTransform(45F, MatrixOrder.Append);
            //画像を描画
            g.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height));

            g.ResetTransform();

            Func<double,double> toRadian = (angle) => (double)(angle * Math.PI / 180);
            using (var pen = new Pen(Color.Yellow))
            {
                var x = 150 * Math.Cos(toRadian(45.0));
                g.DrawRectangle(pen, new Rectangle((int)x, (int)x, img.Width, img.Height));
            }

            //リソースを解放する
            img.Dispose();
            g.Dispose();

            //PictureBox1に表示する
            this.pictureBox1.Image = canvas;
        }
    }
}
