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

        private void button1_Click(object sender, EventArgs e)
        {
            Task.Run(() => ShosekiAsync());
            Task.Run(() => HogeAsync());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Task.Run(() => BaseBallFielderAsync2("1", "巨人"));
            Task.Run(() => BaseBallFielderAsync2("2", "ヤクルト"));
            Task.Run(() => BaseBallFielderAsync2("3", "横浜"));
            Task.Run(() => BaseBallFielderAsync2("4", "中日"));
            Task.Run(() => BaseBallFielderAsync2("5", "阪神"));
            Task.Run(() => BaseBallFielderAsync2("6", "広島"));
            Task.Run(() => BaseBallFielderAsync2("7", "西武"));
            Task.Run(() => BaseBallFielderAsync2("8", "日本ハム"));
            Task.Run(() => BaseBallFielderAsync2("9", "ロッテ"));
            Task.Run(() => BaseBallFielderAsync2("11", "オリックス"));
            Task.Run(() => BaseBallFielderAsync2("12", "ソフトバンク"));
            Task.Run(() => BaseBallFielderAsync2("376", "楽天"));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Task.Run(() => BaseBallPitcherAsync2("1", "巨人"));
            Task.Run(() => BaseBallPitcherAsync2("2", "ヤクルト"));
            Task.Run(() => BaseBallPitcherAsync2("3", "横浜"));
            Task.Run(() => BaseBallPitcherAsync2("4", "中日"));
            Task.Run(() => BaseBallPitcherAsync2("5", "阪神"));
            Task.Run(() => BaseBallPitcherAsync2("6", "広島"));
            Task.Run(() => BaseBallPitcherAsync2("7", "西武"));
            Task.Run(() => BaseBallPitcherAsync2("8", "日本ハム"));
            Task.Run(() => BaseBallPitcherAsync2("9", "ロッテ"));
            Task.Run(() => BaseBallPitcherAsync2("11", "オリックス"));
            Task.Run(() => BaseBallPitcherAsync2("12", "ソフトバンク"));
            Task.Run(() => BaseBallPitcherAsync2("376", "楽天"));
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

        private async Task BaseBallFielderAsync(string code, string name)
        {
            try
            {
                //日本ハム打撃成績
                var urlstring = $"https://baseball-data.com/stats/hitter-" + code + "/";

                // 指定したサイトのHTMLをストリームで取得する
                var doc = default(IHtmlDocument);
                using (var client = new HttpClient())
                using (var stream = await client.GetStreamAsync(new Uri(urlstring)))
                {
                    // AngleSharp.Parser.Html.HtmlParserオブジェクトにHTMLをパースさせる
                    var parser = new HtmlParser();
                    doc = await parser.ParseDocumentAsync(stream);
                }

                var ballElement = doc.QuerySelector("#tbl > tbody");
                var listItems = ballElement.GetElementsByTagName("tr").Select(n =>
                {
                    return new {
                        Team = name,
                        No = n.Children[0].TextContent.Trim(),
                        Name = n.Children[1].TextContent.Trim(),
                        Daritsu = n.Children[2].TextContent.Trim(),
                        Siai = n.Children[3].TextContent.Trim(),
                        Dasekisuu = n.Children[4].TextContent.Trim(),
                        Daseki = n.Children[5].TextContent.Trim(),
                        Anda = n.Children[6].TextContent.Trim(),
                        Honruida = n.Children[7].TextContent.Trim(),
                        Daten = n.Children[8].TextContent.Trim(),
                        Torui = n.Children[9].TextContent.Trim(),
                        Shikyu = n.Children[10].TextContent.Trim(),
                        Sanshin = n.Children[11].TextContent.Trim(),
                        Gida = n.Children[12].TextContent.Trim(),
                        Heisatuda = n.Children[13].TextContent.Trim(),
                        Shuturuiritu = n.Children[14].TextContent.Trim(),
                        Chodaritsu = n.Children[15].TextContent.Trim(),
                        Ops = n.Children[16].TextContent.Trim(),
                        RC27 = n.Children[17].TextContent.Trim(),
                        XR27 = n.Children[18].TextContent.Trim()
                    };
                });

                // 結果を出力する
                listItems.ToList().ForEach(item =>
                {
                    Debug.WriteLine($"" +
                        $"{item.Team}  " +
                        $"{item.No}  " +
                        $"{item.Name}  " +
                        $"{item.Daritsu}  " +
                        $"{item.Siai}  " +
                        $"{item.Dasekisuu}  " +
                        $"{item.Daseki}  " +
                        $"{item.Anda}  " +
                        $"{item.Honruida}  " +
                        $"{item.Daten}  " +
                        $"{item.Torui}  " +
                        $"{item.Shikyu}  " +
                        $"{item.Sanshin}  " +
                        $"{item.Gida}  " +
                        $"{item.Heisatuda}  " +
                        $"{item.Shuturuiritu}  " +
                        $"{item.Chodaritsu}  " +
                        $"{item.Ops}  " +
                        $"{item.RC27}  " +
                        $"{item.XR27}  " +
                        $"");
                    //sinkanData.Add(new HonData(item.Date, item.Kakaku, item.Title));
                });
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        private async Task BaseBallPitcherAsync(string code, string name)
        {
            try
            {
                var urlstring = $"https://baseball-data.com/stats/pitcher-" + code + "/";

                // 指定したサイトのHTMLをストリームで取得する
                var doc = default(IHtmlDocument);
                using (var client = new HttpClient())
                using (var stream = await client.GetStreamAsync(new Uri(urlstring)))
                {
                    // AngleSharp.Parser.Html.HtmlParserオブジェクトにHTMLをパースさせる
                    var parser = new HtmlParser();
                    doc = await parser.ParseDocumentAsync(stream);
                }

                var ballElement = doc.QuerySelector("#tbl > tbody");
                var listItems = ballElement.GetElementsByTagName("tr").Select(n =>
                {
                    return new
                    {
                        Team = name,
                        No = n.Children[0].TextContent.Trim(),
                        Name = n.Children[1].TextContent.Trim(),
                        Bogyoritu = n.Children[2].TextContent.Trim(),
                        Siai = n.Children[3].TextContent.Trim(),
                        Shori = n.Children[4].TextContent.Trim(),
                        Haiboku = n.Children[5].TextContent.Trim(),
                        Save = n.Children[6].TextContent.Trim(),
                        Hold = n.Children[7].TextContent.Trim(),
                        Shoritsu = n.Children[8].TextContent.Trim(),
                        Dasha = n.Children[9].TextContent.Trim(),
                        Tokyukai = n.Children[10].TextContent.Trim(),
                        Hianda = n.Children[11].TextContent.Trim(),
                        Hihonruida = n.Children[12].TextContent.Trim(),
                        Yoshikyu = n.Children[13].TextContent.Trim(),
                        Yodeadball = n.Children[14].TextContent.Trim(),
                        Datsusanshin = n.Children[15].TextContent.Trim(),
                        Shitten = n.Children[16].TextContent.Trim(),
                        Jisekiten = n.Children[17].TextContent.Trim(),
                        Whip = n.Children[18].TextContent.Trim(),
                        Dips = n.Children[19].TextContent.Trim()
                    };
                });

                // 結果を出力する
                listItems.ToList().ForEach(item =>
                {
                    Debug.WriteLine($"" +
                        $"{item.Team}  " +
                        $"{item.No}  " +
                        $"{item.Name}  " +
                        $"{item.Bogyoritu}  " +
                        $"{item.Siai}  " +
                        $"{item.Shori}  " +
                        $"{item.Haiboku}  " +
                        $"{item.Save}  " +
                        $"{item.Hold}  " +
                        $"{item.Shoritsu}  " +
                        $"{item.Dasha}  " +
                        $"{item.Tokyukai}  " +
                        $"{item.Hianda}  " +
                        $"{item.Hihonruida}  " +
                        $"{item.Yoshikyu}  " +
                        $"{item.Yodeadball}  " +
                        $"{item.Datsusanshin}  " +
                        $"{item.Shitten}  " +
                        $"{item.Jisekiten}  " +
                        $"{item.Whip}  " +
                        $"{item.Dips}  " +
                        $"");
                });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private async Task BaseBallFielderAsync2(string code, string name)
        {
            try
            {
                var urlstring = $"https://baseball.yahoo.co.jp/npb/teams/" + code + "/memberlist?type=b";

                // 指定したサイトのHTMLをストリームで取得する
                var doc = default(IHtmlDocument);
                using (var client = new HttpClient())
                using (var stream = await client.GetStreamAsync(new Uri(urlstring)))
                {
                    // AngleSharp.Parser.Html.HtmlParserオブジェクトにHTMLをパースさせる
                    var parser = new HtmlParser();
                    doc = await parser.ParseDocumentAsync(stream);
                }

                var ballElement = doc.QuerySelector("#NpbPlStTable > table > tbody");
                var listItems = ballElement.GetElementsByTagName("tr").Where(n => n.Children[1].TextContent.Trim() != "選手").Select(n =>
                {
                    return new
                    {
                        Key = name + n.Children[0].TextContent.Trim(),
                        Team = name,
                        No = n.Children[0].TextContent.Trim(),
                        Name = n.Children[1].TextContent.Trim(),
                        Daritsu = n.Children[2].TextContent.Trim(),
                        Siai = n.Children[3].TextContent.Trim(),
                        Daseki = n.Children[4].TextContent.Trim(),
                        Dasuu = n.Children[5].TextContent.Trim(),
                        Anda = n.Children[6].TextContent.Trim(),
                        Niruida = n.Children[7].TextContent.Trim(),
                        Sanruida = n.Children[8].TextContent.Trim(),
                        Honruida = n.Children[9].TextContent.Trim(),
                        Ruida = n.Children[10].TextContent.Trim(),
                        Daten = n.Children[11].TextContent.Trim(),
                        Tokuten = n.Children[12].TextContent.Trim(),
                        Sanshin = n.Children[13].TextContent.Trim(),
                        Shikyu = n.Children[14].TextContent.Trim(),
                        Deadball = n.Children[15].TextContent.Trim(),
                        Gida = n.Children[16].TextContent.Trim(),
                        Gihi = n.Children[17].TextContent.Trim(),
                        Torui = n.Children[18].TextContent.Trim(),
                        Toruishi = n.Children[19].TextContent.Trim(),
                        Heisatsuda = n.Children[20].TextContent.Trim(),
                        Shuturuiritu = n.Children[21].TextContent.Trim(),
                        Chodaritsu = n.Children[22].TextContent.Trim(),
                        Ops = n.Children[23].TextContent.Trim(),
                        Tokutenken = n.Children[24].TextContent.Trim(),
                        Shissaku = n.Children[25].TextContent.Trim()
                    };
                });

                // 結果を出力する
                listItems.ToList().ForEach(item =>
                {
                    Debug.WriteLine($"" +
                        $"{item.Key}\t" +
                        $"{item.Team}\t" +
                        $"{item.No}\t" +
                        $"{item.Name}\t" +
                        $"{item.Daritsu}\t" +
                        $"{item.Siai}\t" +
                        $"{item.Daseki}\t" +
                        $"{item.Dasuu}\t" +
                        $"{item.Anda}\t" +
                        $"{item.Niruida}\t" +
                        $"{item.Sanruida}\t" +
                        $"{item.Honruida}\t" +
                        $"{item.Ruida}\t" +
                        $"{item.Daten}\t" +
                        $"{item.Tokuten}\t" +
                        $"{item.Sanshin}\t" +
                        $"{item.Shikyu}\t" +
                        $"{item.Deadball}\t" +
                        $"{item.Gida}\t" +
                        $"{item.Gihi}\t" +
                        $"{item.Torui}\t" +
                        $"{item.Toruishi}\t" +
                        $"{item.Heisatsuda}\t" +
                        $"{item.Shuturuiritu}\t" +
                        $"{item.Chodaritsu}\t" +
                        $"{item.Ops}\t" +
                        $"{item.Tokutenken}\t" +
                        $"{item.Shissaku}\t" +
                        $"");
                });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private async Task BaseBallPitcherAsync2(string code, string name)
        {
            try
            {
                var urlstring = $"https://baseball.yahoo.co.jp/npb/teams/" + code + "/memberlist?type=p";

                // 指定したサイトのHTMLをストリームで取得する
                var doc = default(IHtmlDocument);
                using (var client = new HttpClient())
                using (var stream = await client.GetStreamAsync(new Uri(urlstring)))
                {
                    // AngleSharp.Parser.Html.HtmlParserオブジェクトにHTMLをパースさせる
                    var parser = new HtmlParser();
                    doc = await parser.ParseDocumentAsync(stream);
                }

                var ballElement = doc.QuerySelector("#NpbPlStTable > table > tbody");
                var listItems = ballElement.GetElementsByTagName("tr").Where(n => n.Children[1].TextContent.Trim() != "選手").Select(n =>
                {
                    return new
                    {
                        Key = name + n.Children[0].TextContent.Trim(),
                        Team = name,
                        No = n.Children[0].TextContent.Trim(),
                        Name = n.Children[1].TextContent.Trim(),
                        Bogyoritu = n.Children[2].TextContent.Trim(),
                        Toban = n.Children[3].TextContent.Trim(),
                        Senpatsu = n.Children[4].TextContent.Trim(),
                        Kanto = n.Children[5].TextContent.Trim(),
                        Kanpu = n.Children[6].TextContent.Trim(),
                        Qs = n.Children[7].TextContent.Trim(),
                        Shori = n.Children[8].TextContent.Trim(),
                        Haisen = n.Children[9].TextContent.Trim(),
                        Hold = n.Children[10].TextContent.Trim(),
                        Hp = n.Children[11].TextContent.Trim(),
                        Save = n.Children[12].TextContent.Trim(),
                        Shoritsu = n.Children[13].TextContent.Trim(),
                        Tokyukai = n.Children[14].TextContent.Trim(),
                        Hianda = n.Children[15].TextContent.Trim(),
                        Hihonruida = n.Children[16].TextContent.Trim(),
                        Datsusanshin = n.Children[17].TextContent.Trim(),
                        Datsusanshinritsu = n.Children[18].TextContent.Trim(),
                        Yoshikyu = n.Children[19].TextContent.Trim(),
                        Yodeadball = n.Children[20].TextContent.Trim(),
                        Boto = n.Children[21].TextContent.Trim(),
                        Boke = n.Children[22].TextContent.Trim(),
                        Shitten = n.Children[23].TextContent.Trim(),
                        Jisekiten = n.Children[24].TextContent.Trim(),
                        Hidaritsu = n.Children[25].TextContent.Trim(),
                        Kbb = n.Children[26].TextContent.Trim(),
                        Whip = n.Children[27].TextContent.Trim()
                    };
                });

                // 結果を出力する
                listItems.ToList().ForEach(item =>
                {
                    Debug.WriteLine($"" +
                        $"{item.Key}\t" +
                        $"{item.Team}\t" +
                        $"{item.No}\t" +
                        $"{item.Name}\t" +
                        $"{item.Bogyoritu}\t" +
                        $"{item.Toban}\t" +
                        $"{item.Senpatsu}\t" +
                        $"{item.Kanto}\t" +
                        $"{item.Kanpu}\t" +
                        $"{item.Qs}\t" +
                        $"{item.Shori}\t" +
                        $"{item.Haisen}\t" +
                        $"{item.Hold}\t" +
                        $"{item.Hp}\t" +
                        $"{item.Save}\t" +
                        $"{item.Shoritsu}\t" +
                        $"{item.Tokyukai}\t" +
                        $"{item.Hianda}\t" +
                        $"{item.Hihonruida}\t" +
                        $"{item.Datsusanshin}\t" +
                        $"{item.Datsusanshinritsu}\t" +
                        $"{item.Yoshikyu}\t" +
                        $"{item.Yodeadball}\t" +
                        $"{item.Boto}\t" +
                        $"{item.Boke}\t" +
                        $"{item.Shitten}\t" +
                        $"{item.Jisekiten}\t" +
                        $"{item.Hidaritsu}\t" +
                        $"{item.Kbb}\t" +
                        $"{item.Whip}\t" +
                        $"");
                });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }

    //public class FielderData
    //{
    //    public FielderData(string team, int no, string name, double daritsu,
    //        int siai, int dasekisuu, int daseki, int anda, int honruida,
    //        int daten, int torui, int shikyu, int sanshin, int gida,
    //        int heisatuda, double shuturuiritu, double chodaritsu,
    //        double ops, double rc27, double xr27)
    //    {
    //        Team = team;
    //        No = no;
    //        Name = name;
    //        Daritsu = daritsu;
    //        Siai = siai;
    //        Dasekisuu = dasekisuu;
    //        Daseki = daseki;
    //        Anda = anda;
    //        Honruida = honruida;
    //        Daten = daten;
    //        Torui = torui;
    //        Shikyu = shikyu;
    //        Sanshin = sanshin;
    //        Gida = gida;
    //        Heisatuda = heisatuda;
    //        Shuturuiritu = shuturuiritu;
    //        Chodaritsu = chodaritsu;
    //        Ops = ops;
    //        RC27 = rc27;
    //        XR27 = xr27;
    //    }
    //    public string Team { get; }
    //    public int No { get; }
    //    public string Name { get; }
    //    public double Daritsu { get; }
    //    public int Siai { get; }
    //    public int Dasekisuu { get; }
    //    public int Daseki { get; }
    //    public int Anda { get; }
    //    public int Honruida { get; }
    //    public int Daten { get; }
    //    public int Torui { get; }
    //    public int Shikyu { get; }
    //    public int Sanshin { get; }
    //    public int Gida { get; }
    //    public int Heisatuda { get; }
    //    public double Shuturuiritu { get; }
    //    public double Chodaritsu { get; }
    //    public double Ops { get; }
    //    public double RC27 { get; }
    //    public double XR27 { get; }
    //}

    //public class HonData
    //{
    //    public HonData(String date, String kakaku, String title)
    //    {
    //        Date = date;
    //        Kakaku = kakaku;
    //        Title = title;
    //    }
    //    public String Date { get; }
    //    public String Kakaku { get; }
    //    public String Title { get; }
    //}
}
