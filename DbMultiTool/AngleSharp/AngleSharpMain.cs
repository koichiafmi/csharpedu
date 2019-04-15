using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//https://qiita.com/matarillo/items/a92e7efbfd2fdec62595
//https://emergingtechnology.azurewebsites.net/2018/05/30/scraping/
//https://qiita.com/k-ohno/items/c74f71e5d79d6de05841
//http://38fanjia.hatenablog.com/entry/2016/10/02/170300
namespace DbMultiTool.AngleSharp
{
    public partial class AngleSharpMain : Form
    {
        public AngleSharpMain()
        {
            InitializeComponent();
        }

        public BindingSource sinkanData;

        private void button1_Click(object sender, EventArgs e)
        {
            Task.Run(() => HogeAsync());
            //Task.Run(() => BaseBallAsync());
            Task.Run(() => ShosekiAsync());
            //sinkanData = new BindingSource();
            //dataGridView1.DataSource = sinkanData;

            //ShosekiAsync().Wait();

            //DataGridViewTextBoxColumn textColumn1 = new DataGridViewTextBoxColumn();
            //textColumn1.DataPropertyName = "Date";
            //dataGridView1.Columns.Add(textColumn1);

            //DataGridViewTextBoxColumn textColumn2 = new DataGridViewTextBoxColumn();
            //textColumn2.DataPropertyName = "Kakaku";
            //dataGridView1.Columns.Add(textColumn2);

            //DataGridViewTextBoxColumn textColumn3 = new DataGridViewTextBoxColumn();
            //textColumn3.DataPropertyName = "Title";
            //dataGridView1.Columns.Add(textColumn3);
        }

        private async Task HogeAsync()
        {
            // 株価を取得したいサイトのURL
            var code = "4641.T";
            var urlstring = $"http://stocks.finance.yahoo.co.jp/stocks/detail/?code={code}";

            // 指定したサイトのHTMLをストリームで取得する
            var doc = default(IHtmlDocument);
            using (var client = new HttpClient())
            using (var stream = await client.GetStreamAsync(new Uri(urlstring)))
            {
                // AngleSharp.Parser.Html.HtmlParserオブジェクトにHTMLをパースさせる
                var parser = new HtmlParser();
                doc = await parser.ParseDocumentAsync(stream);
            }

            // クエリーセレクタを指定し株価部分を取得する
            var priceElement = doc.QuerySelector("#main td[class=stoksPrice]");

            // 取得した株価がstring型なのでint型にパースする
            int.TryParse(priceElement.TextContent, NumberStyles.AllowThousands, null, out var price);

            Debug.WriteLine("(株)アルプス技研(4641.T)の株価: {0}円", price);
        }

        private async Task ShosekiAsync()
        {

            // 新刊情報を取得したいサイトのURL
            var urlstring = "https://www.shuwasystem.co.jp/news/n27546.html";
            //var urlstring = "http://www.shuwasystem.co.jp/newbook.html";

            // 指定したサイトのHTMLをストリームで取得する
            var doc = default(IHtmlDocument);
            using (var client = new HttpClient())
            using (var stream = await client.GetStreamAsync(new Uri(urlstring)))
            {
                // AngleSharp.Parser.Html.HtmlParserオブジェクトにHTMLをパースさせる
                var parser = new HtmlParser();
                doc = await parser.ParseDocumentAsync(stream);
            }

            //var priceElement = doc.QuerySelectorAll("#main > div > div > table > tbody > tr:not(.g)");
            //DataTable listItems = new DataTable();
            //listItems.Columns.Add("TITLE");
            //listItems.Columns.Add("DATE");
            //foreach (var item in priceElement)
            //{
            //    DataRow dr = listItems.NewRow();
            //    dr["TITLE"] = item.ChildNodes[5].TextContent.Trim();
            //    dr["DATE"] = item.ChildNodes[1].TextContent.Trim();
            //    listItems.Rows.Add(dr);
            //}
            //foreach (DataRow item in listItems.Rows)
            //{
            //    Debug.WriteLine(item["TITLE"].ToString() + "," + item["DATE"].ToString());
            //}

            var priceElement = doc.QuerySelector("#main > div > div > table > tbody");
            var listItems = priceElement.GetElementsByTagName("tr").Select(n =>
                {
                    var date = n.Children[0].TextContent.Trim();
                    var kakaku = n.Children[1].TextContent.Trim();
                    var title = n.Children[2].TextContent.Trim();

                    return new { Title = title, Kakaku = kakaku, Date = date };
                });

            // 結果を出力する
            listItems.ToList().ForEach(item =>
            {
                Debug.WriteLine($"{item.Date} ({item.Kakaku}) : {item.Title}");
                //sinkanData.Add(new HonData(item.Date, item.Kakaku, item.Title));
            });
        }

        private async Task BaseBallAsync()
        {
            try
            {
                //日本ハム打撃成績
                var urlstring = $"https://baseball-data.com/stats/hitter-f/";

                // 指定したサイトのHTMLをストリームで取得する
                var doc = default(IHtmlDocument);
                using (var client = new HttpClient())
                using (var stream = await client.GetStreamAsync(new Uri(urlstring)))
                {
                    // AngleSharp.Parser.Html.HtmlParserオブジェクトにHTMLをパースさせる
                    var parser = new HtmlParser();
                    doc = await parser.ParseDocumentAsync(stream);
                }

                // クエリーセレクタを指定し株価部分を取得する
                //var priceElement = doc.QuerySelector("#main");
                //var statsElement = doc.GetElementsByClassName("tablesorter stats");

                var priceElement = doc.QuerySelector("#tbl");

                // 取得した株価がstring型なのでint型にパースする
                //int.TryParse(priceElement.TextContent, NumberStyles.AllowThousands, null, out var price);

                //Debug.WriteLine("(株)アルプス技研(4641.T)の株価: {0}円", price);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }

    public class HonData
    {
        public HonData(String date, String kakaku, String title)
        {
            Date = date;
            Kakaku = kakaku;
            Title = title;
        }
        public String Date { get; }
        public String Kakaku { get; }
        public String Title { get; }
    }
}
