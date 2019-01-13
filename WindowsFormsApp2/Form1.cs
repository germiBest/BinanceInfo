using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Timers;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public class RateLimit
        {
            public string rateLimitType { get; set; }
            public string interval { get; set; }
            public int intervalNum { get; set; }
            public int limit { get; set; }
        }

        public class Filter
        {
            public string filterType { get; set; }
            public string minPrice { get; set; }
            public string maxPrice { get; set; }
            public string tickSize { get; set; }
            public string multiplierUp { get; set; }
            public string multiplierDown { get; set; }
            public int? avgPriceMins { get; set; }
            public string minQty { get; set; }
            public string maxQty { get; set; }
            public string stepSize { get; set; }
            public string minNotional { get; set; }
            public bool? applyToMarket { get; set; }
            public int? limit { get; set; }
            public int? maxNumAlgoOrders { get; set; }
        }

        public class Symbol
        {
            public string symbol { get; set; }
            public string status { get; set; }
            public string baseAsset { get; set; }
            public int baseAssetPrecision { get; set; }
            public string quoteAsset { get; set; }
            public int quotePrecision { get; set; }
            public IList<string> orderTypes { get; set; }
            public bool icebergAllowed { get; set; }
            public IList<Filter> filters { get; set; }
        }

        public class Example
        {
            public string timezone { get; set; }
            public long serverTime { get; set; }
            public IList<RateLimit> rateLimits { get; set; }
            public IList<object> exchangeFilters { get; set; }
            public IList<Symbol> symbols { get; set; }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string site = "https://api.binance.com/api/v1/exchangeInfo";
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(site);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            StreamReader stream = new StreamReader(resp.GetResponseStream(), Encoding.UTF8);
            var json = stream.ReadToEnd().ToString();
            var examples = Newtonsoft.Json.JsonConvert.DeserializeObject<Example>(json);
            var milliseconds = examples.serverTime;

            List<string> coins = new List<string>();
            for(int i = 0; i < examples.symbols.Count; i++)
            {
                if(examples.symbols[i].quoteAsset == "BTC") coins.Add(examples.symbols[i].baseAsset);
                //HTTPResponse.Text += examples.symbols[i].baseAsset + '/' + examples.symbols[i].quoteAsset + Environment.NewLine;
            }
            coins = coins.Distinct().ToList();
            foreach(var i in coins)
            {
                HTTPResponse.Text += i + Environment.NewLine;
            }
           // HTTPResponse.Text += coins[1].ToString();
            /*
                var time = TimeSpan.FromMilliseconds(milliseconds);
                var timeNow = TimeSpan.FromMilliseconds((DateTime.Now.Ticks - new DateTime(1970, 1, 1).Ticks) / TimeSpan.TicksPerSecond);

                HTTPResponse.Text = time.ToString() + "\t" + timeNow.ToString();
                HTTPResponse.Text = time.CompareTo(timeNow).ToString();
            */
        }
    }
}
