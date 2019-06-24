using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpeechApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //SpeechRecognition.CreateEngine("SR_MS_ja-JP_TELE_11.0");
            SpeechRecognition.CreateEngine(new System.Globalization.CultureInfo("ja-JP"));

            foreach (RecognizerInfo ri in SpeechRecognition.InstalledRecognizers)
            {
                logTextBox1Write(ri.Name + "(" + ri.Culture + ")");
            }

            SpeechRecognition.SpeechRecognitionRejectedEvent = (e) =>
            {
                logTextBox1Write("認識できません。");
            };

            SpeechRecognition.SpeechRecognizedEvent = (e) =>
            {
                logTextBox1Write("確定：" + e.Result.Grammar.Name + " " + e.Result.Text + "(" + e.Result.Confidence + ")");
            };

            SpeechRecognition.SpeechHypothesizedEvent = (e) =>
            {
                logTextBox1Write("候補：" + e.Result.Grammar.Name + " " + e.Result.Text + "(" + e.Result.Confidence + ")");
            };

            SpeechRecognition.SpeechRecognizeCompletedEvent = (e) =>
            {
                if (e.Cancelled)
                {
                    logTextBox1Write("キャンセルされました。");
                }

                logTextBox1Write("認識終了");
            };
        }

        private void logTextBox1Write(string txt)
        {
            this.logTextBox1.Text += (txt + Environment.NewLine);
            this.logTextBox1.SelectionStart = this.logTextBox1.TextLength;
            this.logTextBox1.ScrollToCaret();
        }

        private void AddGrammar()
        {
            string[] words = new string[] { "赤", "白", "黄", "ピンク" };
            SpeechRecognition.AddGrammar("words", words);

            Choices choices1 = new Choices();
            choices1.Add(new string[] { "カレーライス", "ラーメン", "すし", "そば", "ハンバーグ", "スパゲッティ", "うどん", "ピザ" });
            GrammarBuilder grammarBuilder1 = new GrammarBuilder();
            grammarBuilder1.Append(choices1);
            grammarBuilder1.Append("が好きだ");
            SpeechRecognition.AddGrammar("seelowe", grammarBuilder1);

            Choices choices2 = new Choices();
            choices2.Add(new string[] { "平原", "街道", "塹壕", "草原", "凍土", "砂漠", "海上", "空中", "泥中", "湿原" });
            GrammarBuilder grammarBuilder2 = new GrammarBuilder();
            grammarBuilder2.AppendWildcard();
            grammarBuilder2.Append("は");
            grammarBuilder2.Append(new SemanticResultKey("field", new GrammarBuilder(choices2)));
            grammarBuilder2.Append("が好きです");
            SpeechRecognition.AddGrammar("field", grammarBuilder2);
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            SpeechRecognition.RecognizeAsync(true);
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            SpeechRecognition.RecognizeAsyncCancel();
        }

        private void button3_Click(object sender, System.EventArgs e)
        {
            SpeechRecognition.RecognizeAsyncStop();
        }

        private void button4_Click(object sender, System.EventArgs e)
        {
            //SpeechRecognition.CreateEngine("SR_MS_ja-JP_TELE_11.0");
            SpeechRecognition.CreateEngine(new System.Globalization.CultureInfo("ja-JP"));
        }

        private void button5_Click(object sender, System.EventArgs e)
        {
            SpeechRecognition.DestroyEngine();
        }

        private void button6_Click(object sender, System.EventArgs e)
        {
            AddGrammar();
        }

        private void button7_Click(object sender, System.EventArgs e)
        {
            SpeechRecognition.ClearGrammar();
        }
    }
}