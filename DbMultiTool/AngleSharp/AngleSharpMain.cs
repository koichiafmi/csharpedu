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
namespace DbMultiTool.AngleSharp
{
    public partial class AngleSharpMain : Form
    {
        public AngleSharpMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Task.Run(() => HogeAsync());
            Task.Run(() => BaseBallAsync());
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
}
