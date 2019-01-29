using Accord.Imaging;
using Accord.MachineLearning;
using Accord.MachineLearning.VectorMachines;
using Accord.MachineLearning.VectorMachines.Learning;
using Accord.Statistics.Kernels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DbMultiTool.accord
{
    public partial class AccordForm : Form
    {
        public AccordForm()
        {
            InitializeComponent();

            inkCanvas = new InkCanvas();
            elementHost3 = new ElementHost();

            elementHost3.Location = new System.Drawing.Point(12, 12);
            elementHost3.Name = "elementHost3";
            elementHost3.Size = new System.Drawing.Size(493, 426);
            elementHost3.TabIndex = 1;
            elementHost3.Text = "elementHost3";
            elementHost3.Child = inkCanvas;

            Controls.Add(elementHost3);

            //　既存の画像データ集を用いて学習を開始する
            StartLearning();
        }

        private MediaPlayer mediaPlayer = new MediaPlayer();
        private List<ImageItem> itemList = new List<ImageItem>();
        private string directoryPath = ConfigurationManager.AppSettings["directoryPath"].ToString();
        private int codeWordCount = 200;
        private BagOfVisualWords bagofVW;
        private int classes = 3;
        private MulticlassSupportVectorMachine<ChiSquare> msvm;

        private ElementHost elementHost3;
        private InkCanvas inkCanvas;

        #region イベント
        private void AccordForm_Load(object sender, EventArgs e)
        {
        }

        //　アプリを終了する
        private void おわろうかな_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void かけたよ_Click(object sender, EventArgs e)
        {
            judge();
        }

        //　お描き画像を消す
        private void もういちどかくよ_Click(object sender, EventArgs e)
        {
            inkCanvas.Strokes.Clear();
        }
        #endregion

        // 学習を開始する
        private void StartLearning()
        {
            var catsCreator = new ItemsFactory("cats", directoryPath, itemList, 0);
            var birdsCreator = new ItemsFactory("birds", directoryPath, itemList, 1);
            var dogsCreator = new ItemsFactory("dogs", directoryPath, itemList, 2);
            bagofVW = new TrainFactory(codeWordCount, bagofVW, itemList).bagofVW;

            var inputs = new InputFactory(itemList).input;
            var outputs = new OutputFactory(itemList).output;

            msvm = new MulticlassSupportVectorMachine<ChiSquare>(0, new ChiSquare(), classes);

            // 学習アルゴリズムを作成する
            var teacher = new MulticlassSupportVectorLearning<ChiSquare>()
            {
                Learner = (param) => new SequentialMinimalOptimization<ChiSquare>()
                {
                    UseComplexityHeuristic = true,
                    UseKernelEstimation = true
                }
            };

            msvm = teacher.Learn(inputs, outputs);

            var calibration = new MulticlassSupportVectorLearning<ChiSquare>()
            {
                Model = msvm,
                Learner = (param) => new ProbabilisticOutputCalibration<ChiSquare>()
                {
                    Model = param.Model
                }
            };

            calibration.ParallelOptions.MaxDegreeOfParallelism = 1;
            calibration.Learn(inputs, outputs);

            inkCanvas.Strokes.Clear();
            textBox1.Clear();
        }

        private void judge()
        {
            Bitmap bitmap = new BitmapFactory(inkCanvas).bitmap;

            double[] codeword = bagofVW.Transform(bitmap);
            int classResult = msvm.Decide(codeword);

            textBox1.Text = "Result:" + Convert.ToString(classResult) + "\r\n";

            String cryStr;

            if (classResult == 0)
            {
                //cryStr = @"・・・ねこのなきごえ.mp3";
                cryStr = "にゃー";
            }
            else if (classResult == 1)
            {
                cryStr = "こけこっこー";
            }
            else
            {
                cryStr = "わんわん";
            }

            System.Windows.MessageBox.Show(cryStr);
            //Uri cryFile = new Uri(cryStr);
            //mediaPlayer.Open(cryFile);
            //mediaPlayer.Play();

            FeedBack();
        }

        //フィードバック
        private void FeedBack()
        {
            // Configure the message box to be displayed
            string messageBoxText = "猫ならYes、犬ならNo、鳥ならCancelを押してください。";
            string caption = "フィードバック";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Question;

            // Display message box
            MessageBoxResult result = System.Windows.MessageBox.Show(messageBoxText, caption, button, icon);

            string animalStr = "";

            // Process message box results
            switch (result)
            {
                //猫
                case MessageBoxResult.Yes:
                    animalStr = "cats";
                    break;
                //犬
                case MessageBoxResult.No:
                    animalStr = "dogs";
                    break;
                //鳥
                case MessageBoxResult.Cancel:
                    animalStr = "birds";
                    break;
            }

            string saveDi = Path.Combine(directoryPath, animalStr);
            string imagePath = DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpg";

            string savePath = Path.Combine(saveDi, imagePath);

            SaveImage(savePath);

            //再学習
            StartLearning();
        }

        // InkCanvasを画像として保存する
        private void SaveImage(string file)
        {
            Rect rectBounds = inkCanvas.Strokes.GetBounds();
            DrawingVisual dv = new DrawingVisual();
            DrawingContext dc = dv.RenderOpen();
            dc.PushTransform(new TranslateTransform(-rectBounds.X, -rectBounds.Y));
            dc.DrawRectangle(inkCanvas.Background, null, rectBounds);
            inkCanvas.Strokes.Draw(dc);
            dc.Close();

            RenderTargetBitmap rtb = new RenderTargetBitmap(
                (int)rectBounds.Width, (int)rectBounds.Height,
                96, 96,
                PixelFormats.Default);
            rtb.Render(dv);

            BitmapEncoder enc = new JpegBitmapEncoder();

            if (enc != null)
            {
                enc.Frames.Add(BitmapFrame.Create(rtb));
                System.IO.Stream stream = System.IO.File.Create(file);
                enc.Save(stream);
                stream.Close();
            }

        }

        #region BitmapFactoryクラス
        //　InkCanvasをBitMapで返す
        internal class BitmapFactory
        {
            public Bitmap bitmap;
            private InkCanvas inkCanvas;

            public BitmapFactory(InkCanvas inkCanvas)
            {
                this.inkCanvas = inkCanvas;
                double width = inkCanvas.ActualWidth;
                double height = inkCanvas.ActualHeight;
                RenderTargetBitmap bmpCopied = new RenderTargetBitmap((int)Math.Round(width), (int)Math.Round(height), 96, 96, PixelFormats.Default);
                DrawingVisual dv = new DrawingVisual();
                using (DrawingContext dc = dv.RenderOpen())
                {
                    VisualBrush vb = new VisualBrush(inkCanvas);
                    dc.DrawRectangle(vb, null, new Rect(new System.Windows.Point(), new System.Windows.Size(width, height)));
                }
                bmpCopied.Render(dv);
                System.Drawing.Bitmap bitmap;
                using (MemoryStream outStream = new MemoryStream())
                {
                    BitmapEncoder enc = new BmpBitmapEncoder();
                    enc.Frames.Add(BitmapFrame.Create(bmpCopied));
                    enc.Save(outStream);
                    bitmap = new System.Drawing.Bitmap(outStream);
                }

                this.bitmap = bitmap;
            }
        }
        #endregion

        #region InputFactoryクラス
        //　学習用インプットデータを作成する
        internal class InputFactory
        {
            public double[][] input;

            public InputFactory(List<ImageItem> list)
            {
                var inputList = new List<double[]>();

                foreach (ImageItem item in list)
                {
                    inputList.Add(item.codeWord);
                }

                input = inputList.ToArray();
            }
        }
        #endregion

        #region OutputFactoryクラス
        //　学習用アウトプットデータを作成する
        internal class OutputFactory
        {
            public int[] output;

            public OutputFactory(List<ImageItem> list)
            {
                var outputList = new List<int>();

                foreach (ImageItem item in list)
                {
                    outputList.Add(item.Classification);
                }

                output = outputList.ToArray();
            }
        }
        #endregion

        #region TrainFactoryクラス
        //　訓練用インプットデータを作成する
        internal class TrainFactory
        {
            public BagOfVisualWords bagofVW;

            public TrainFactory(int codeWordCount, BagOfVisualWords bagofVW, List<ImageItem> itemList)
            {
                BinarySplit binarySplit = new BinarySplit(codeWordCount);
                bagofVW = new BagOfVisualWords(binarySplit);
                List<Bitmap> bitmapList = new List<Bitmap>();

                foreach (ImageItem item in itemList)
                {
                    bitmapList.Add(item.bmp);
                }

                Bitmap[] trainImages = bitmapList.ToArray();
                bagofVW.Learn(trainImages);

                foreach (ImageItem item in itemList)
                {
                    item.codeWord = bagofVW.Transform(item.bmp);
                }

                this.bagofVW = bagofVW;
            }
        }
        #endregion

        #region ItemsFactoryクラス
        //　各動物の画像を学習用データに加える
        internal class ItemsFactory
        {
            private string directoryPath;
            private List<ImageItem> itemList;

            public ItemsFactory(string animalName, string directoryPath, List<ImageItem> itemList, int classNum)
            {
                this.directoryPath = directoryPath;
                this.itemList = itemList;

                List<String> list = new List<String>();

                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(Path.Combine(directoryPath, animalName));
                IEnumerable<System.IO.FileInfo> files =
                    di.EnumerateFiles("*", System.IO.SearchOption.AllDirectories);

                //ファイルを列挙する
                foreach (System.IO.FileInfo f in files)
                {
                    list.Add(f.FullName);
                }

                foreach (String fileName in list)
                {
                    ImageItem ii;
                    ii = new ImageItem();
                    ii.FileName = fileName;
                    FileStream fs;
                    fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    Bitmap source = (Bitmap)System.Drawing.Bitmap.FromStream(fs);
                    fs.Close();
                    ii.bmp = new Bitmap(source);
                    ii.Classification = classNum;
                    itemList.Add(ii);
                }
            }
        }
        #endregion

        #region ImageItemクラス
        /// <summary>
        /// 学習に利用する画像データ
        /// </summary>
        internal class ImageItem
        {
            public String FileName;
            public Bitmap bmp;
            public int Classification;
            public double[] codeWord;
            public ImageItem(){}
        }
        #endregion
    }
}
