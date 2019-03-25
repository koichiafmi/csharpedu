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

//https://qiita.com/PeppermintJinro/items/344ed5772256b5aeb667 ←パクリ元
//https://www.ipentec.com/document/csharp-accord-net-create-simple-classification ←参考になりそう
//http://accord-framework.net/docs/html/R_Project_Accord_NET.htm　←本家
//http://d.hatena.ne.jp/n_hidekey/20111120/1321803326 ←bovwで参考にした
//http://puni-o.hatenablog.com/category/C%23?page=1458122455 ←ここも参考になりそう
//https://ai-kenkyujo.com/2017/03/29/machine-learning-2/ ←ここも入門編としてよさそう
namespace DbMultiTool.accord
{
    public partial class AccordForm : Form
    {
        #region 大事なものもあるが一旦どうでもいい
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

        //private MediaPlayer mediaPlayer = new MediaPlayer();
        private string directoryPath = ConfigurationManager.AppSettings["directoryPath"].ToString();
        private ElementHost elementHost3;
        private InkCanvas inkCanvas;
        #endregion

        //画像を数値化するときの特徴点抽出件数。たぶんこの数値を上げれば精度が高くなるが、処理時間もきっとマシマシ。
        private int codeWordCount = 200;
        //いぬ、ねこ、とりの３つ
        private int classes = 3;
        //↓この３つのオブジェクトが全てのカギを握っている。【超重要】
        private List<ImageItem> itemList = new List<ImageItem>();
        private BagOfVisualWords bagofVW;
        private MulticlassSupportVectorMachine<ChiSquare> msvm;

        #region 気にしなくていいところ

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

        #endregion

        #region 超絶重要！！！

        // 学習を開始する
        private void StartLearning()
        {
            var catsCreator = new ItemsFactory("cats", directoryPath, itemList, 0);
            var birdsCreator = new ItemsFactory("birds", directoryPath, itemList, 1);
            var dogsCreator = new ItemsFactory("dogs", directoryPath, itemList, 2);
            //BagOfVisualWords
            /*
             * Bag of visual words (BoVW)は、一般物体認識において現在最も広く普及している画像特徴表現で、
             * 画像中の多数の局所特徴をベクトル量子化しヒストグラムにしたものです。
             */
            bagofVW = new TrainFactory(codeWordCount, bagofVW, itemList).bagofVW;

            var inputs = new InputFactory(itemList).input;
            var outputs = new OutputFactory(itemList).output;

            //サポートベクターマシンのクラスを作成します。MulticlassSupportVectorMachineクラスを作成します。
            //入力数は不定なので、コンストラクタの第一引数は0を与えます。
            //サポートベクターマシンのカーネル(カーネル関数)は、ChiSquare(カイ二乗検定)を利用する。
            //今回判定するクラスは3つのため、サポートベクタマシンのクラスには3を与えます。
            /*
             *[サポートベクターマシン (SVM)]
             *単純パーセプトロンにマージン最大化を加えて教師ありデータを線形に2値分類する。
             *数値データより、カテゴリーデータの扱いに長けており基本的には線形の分離ができるが、
             *カーネル関数を使用することで次元を増やし、非線形の分離ができるようになっている。
             *多クラス分類をする場合は、1対他分類法または1対1分類法を使用する(複数のSVMを組み合わせる)ことで対応する。
             *通常のハードマージンSVMはノイズの影響を受けやすいため、ノイズにペナルティを課して
             *マージン幅を調整するソフトマージンSVMにより、ノイズに影響を受けづらくすることが出来る。
             */
            /*
             *[パーセプトロン]
             *パーセプトロンとは、人工ニューロンやニューラルネットワークの一種である。
             *入力層と出力層を持ち、パラメータを学習によって決められるが、中間層がなく
             *線形分離不可能な問題が解けなかったため「単純パーセプトロン」とも呼ばれる。 
             */
            /*
             * [カイ二乗検定]
             * https://ja.wikipedia.org/wiki/%E3%82%AB%E3%82%A4%E4%BA%8C%E4%B9%97%E6%A4%9C%E5%AE%9A
             * カイ二乗検定（カイにじょうけんてい、カイじじょうけんてい、英: Chi-squared test）、またはχ 2
             * {\displaystyle \chi ^{2}}  \chi ^{2}検定とは、帰無仮説が正しければ検定統計量が漸近的にカイ二乗分布に
             * 従うような統計的検定法の総称である。
             */
            /*
             * msvmの構成　※あやしさ満載※
             * msvm[MulticlassSupportVectorMachine<ChiSquare>]
             * 　├teacher
             * 　│  ├Learner[SequentialMinimalOptimization<ChiSquare>]
             * 　│  │ ├UseComplexityHeuristic = true
             * 　│  │ └UseKernelEstimation = true
             * 　│  └Learn(inputs, outputs)
             * 　└calibration(?)
             * 　
             * calibration[MulticlassSupportVectorMachine<ChiSquare>]
             * 　├Model=msvm
             * 　└Learner[ProbabilisticOutputCalibration<ChiSquare>]
             * 　    └Model=msvmのparam(?)
             */
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

            #region ここでinputsとoutputsの中身を見てみたい。
            Console.WriteLine("============== inputs DEBUG START  ==============");
            int i = 0;
            int j = 0;
            foreach (var item1 in inputs)
            {
                foreach (var item2 in item1)
                {
                    Console.WriteLine("inputs[" + i.ToString() + "][" + j.ToString() + "]:" + item2);
                    ++j;
                }
                ++i;
            }
            Console.WriteLine("============== inputs DEBUG END    ==============");
            Console.WriteLine("============== outputs DEBUG START ==============");
            i = 0;
            foreach (var item1 in outputs)
            {
                Console.WriteLine("outputs[" + i.ToString() + "]:" + item1);
                ++i;
            }
            Console.WriteLine("============== outputs DEBUG END   ==============");
            #endregion

            //calibration＝【較正・校正】 《名・ス他》計器類の狂い・精度を、標準器と比べて正すこと。
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

            //bagofVWを使用して描いた絵を数値化する。
            double[] codeword = bagofVW.Transform(bitmap);
            //学習したアルゴリズムに数値化した絵をぶつけて何の絵かを判断する。
            int classResult = 9;
            classResult = msvm.Decide(codeword);

            textBox1.Text = "Result:" + Convert.ToString(classResult) + "\r\n";

            string cryStr;

            if (classResult == 0)
            {
                //cryStr = @"・・・ねこのなきごえ.mp3";
                cryStr = "にゃー";
            }
            else if (classResult == 1)
            {
                cryStr = "こけこっこー";
            }
            else if (classResult == 2)
            {
                cryStr = "わんわん";
            }
            else
            {
                cryStr = "もういやだ・・・。帰りたいよぉ・・・（泣き声）";
            }

            System.Windows.MessageBox.Show(cryStr);

            FeedBack();
        }
        #endregion

        #region 重要そうだがさして重要ではない
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
        #endregion

        #region あまりおもしろくないところ
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
                Stream stream = File.Create(file);
                enc.Save(stream);
                stream.Close();
            }
        }
        #endregion

        #region 具体的な処理はさして重要ではない

        #region BitmapFactoryクラス
        //　InkCanvasをBitMapで返す。
        internal class BitmapFactory
        {
            //画像解析するときはこのbitmapを使用している。
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
                Bitmap bitmap;
                using (MemoryStream outStream = new MemoryStream())
                {
                    BitmapEncoder enc = new BmpBitmapEncoder();
                    enc.Frames.Add(BitmapFrame.Create(bmpCopied));
                    enc.Save(outStream);
                    bitmap = new Bitmap(outStream);
                }

                this.bitmap = bitmap;
            }
        }
        #endregion

        #endregion

        #region スーパー重要！！

        #region InputFactoryクラス
        //　学習用インプットデータを作成する
        internal class InputFactory
        {
            //これを作成する
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
            //これを作成する
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
            //これを作成する
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
                    //画像解析して数値化した値をcodeWordにセット。この値をInputFactoryで使用している。
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
            //itemListを作成することが重要
            private List<ImageItem> itemList;

            public ItemsFactory(string animalName, string directoryPath, List<ImageItem> itemList, int classNum)
            {
                this.directoryPath = directoryPath;
                this.itemList = itemList;

                List<string> list = new List<string>();

                DirectoryInfo di = new DirectoryInfo(Path.Combine(directoryPath, animalName));
                IEnumerable<FileInfo> files = di.EnumerateFiles("*", SearchOption.AllDirectories);

                //ファイルを列挙する
                foreach (FileInfo f in files)
                {
                    list.Add(f.FullName);
                }

                foreach (string fileName in list)
                {
                    ImageItem ii;
                    ii = new ImageItem();
                    ii.FileName = fileName;
                    FileStream fs;
                    fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    Bitmap source = (Bitmap)System.Drawing.Image.FromStream(fs);
                    fs.Close();
                    ii.bmp = new Bitmap(source);
                    ii.Classification = classNum;
                    itemList.Add(ii);
                }
            }
        }
        #endregion

        #region ImageItemクラス　地味に重要
        /// <summary>
        /// 学習に利用する画像データ
        /// ItemsFactoryクラスで作成する。
        /// TrainFactoryクラスでBagOfVisualWordsとcodeWordを作成するために必要（bmp）
        /// InputFactoryクラスの構成要素（codeWord）
        /// OutputFactoryクラスの構成要素（Classification）
        /// </summary>
        internal class ImageItem
        {
            //ファイル名
            public string FileName;
            //画像データ
            public Bitmap bmp;
            //なんのデータか（いぬかねこかとりか）を識別する値
            public int Classification;
            //画像を識別して数値化したデータ
            public double[] codeWord;
            //コンストラクタ
            public ImageItem(){}
        }
        #endregion

        #endregion
    }
}
