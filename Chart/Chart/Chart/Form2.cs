using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chart
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            int n = 1;
            int no = 0;
            int Data_Count = 4;
            int Array_Length = 400;
            string[] date = new string[Array_Length];
            string[] Weight_text = new string[Array_Length];
            double[] Weight = new double[Array_Length];
            string[] Fat_text = new string[Array_Length];
            double[] Fat = new double[Array_Length];
            DateTime[] date_style = new DateTime[Array_Length];

            try
            { //csvファイルを開いて、日付、体重、体脂肪率にわけて配列に読み込む

                string[] StrArryData = new string[3]; // １行分のﾃﾞｰﾀを格納する
                using (var sr = new System.IO.StreamReader(@"data.txt")) //読み込むファイル名
                {

                    while (!sr.EndOfStream) //ファイルエンドまで
                    {
                        var line = sr.ReadLine();    // ファイルから一行読み込む
                        StrArryData = line.Split(','); //一行を区切る

                        foreach (string value in StrArryData) //strArrayDatsをStringとして、そのすべてに対して
                        {

                            //Data3つごとに分けて配列に格納
                            if (n % 3 == 1)
                            {
                                date[no] = value;
                                DateTime.TryParse(date[no], out date_style[no]); //textをDateTime型に
                            }
                            else if (n % 3 == 2)
                            {
                                Weight_text[no] = value;
                                double.TryParse(Weight_text[no], out Weight[no]); //Textを数値に型変換
                            }
                            else if (n % 3 == 0)
                            {
                                Fat_text[no] = value;
                                double.TryParse(Fat_text[no], out Fat[no]);
                                no++;
                            }
                            n++;
                        }
                        Data_Count = no;
                    }
                }
            }
            catch (System.Exception ee)
            {
                MessageBox.Show("Error");
                // ファイルを開くのに失敗したとき
            }

            // 1.Seriesの追加
            Chart1.Series.Clear();
            Chart1.Series.Add("Weight (kg)");
            Chart1.Series.Add("Body fat (%)");


            // 2.グラフのタイプの設定
            Chart1.Series["Weight (kg)"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            Chart1.Series["Weight (kg)"].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Diamond;
            Chart1.Series["Body fat (%)"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            Chart1.Series["Body fat (%)"].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Diamond;

            // 3.座標の入力
            for (no = 0; no < Data_Count; no++)
            {
                Chart1.Series["Weight (kg)"].Points.AddXY(date_style[no], Weight[no]);
                Chart1.Series["Body fat (%)"].Points.AddXY(date_style[no], Fat[no]);
            }
        }
    }
}
