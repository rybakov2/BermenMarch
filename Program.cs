using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text;
using System.Linq;


namespace BermenMarch
{
    class Program
    {
        public string ReverseString(string input)
        {
            int stringLength = input.Length;

            if (stringLength < 2)   //No action is needed.
                return input;       //The reverse will be the same as the input.

            //First method - work with the string directly
            StringBuilder sb = new StringBuilder(stringLength);

            for (int i = stringLength - 1; i >= 0; i--)
            {
                sb.Append(input.Substring(i, 1));
            }

            string resul = sb.ToString();

            //Second method - use LINQ
            string result2 = new string(Enumerable.Range(1, stringLength).Select(i => input[stringLength - i]).ToArray());

            return resul;
        }

        public static async Task Main(string[] args)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var dataTask = client.GetStreamAsync("https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol=IBM&apikey=demo");
            var timeSeries = await JsonSerializer.DeserializeAsync<Series>(await dataTask);

            List<DataPoint> result = new List<DataPoint>();

            foreach (var day in timeSeries.TimeSeries)
            {
                DataPoint dataPoint = new DataPoint()
                {
                    Date = DateTime.Parse(day.Key),
                    Open = decimal.Parse(day.Value.Open),
                    High = decimal.Parse(day.Value.High),
                    Low = decimal.Parse(day.Value.Low),
                    Close = decimal.Parse(day.Value.Close),
                    Volume = long.Parse(day.Value.Volume)
                };

                result.Add(dataPoint);
            }

            foreach (var day in result)
            {
                Console.WriteLine("Date: {0}, Open: {1}, High: {2}, Low: {3}, Close: {4}, Volume: {5}",
                    day.Date.ToShortDateString(), day.Open, day.High, day.Low, day.Close, day.Volume);
            }
        }
    }

    public class Series
    {
        [JsonPropertyName("Meta Data")]
        public MetaDataDetails MetaData { get; set; }

        [JsonPropertyName("Time Series (Daily)")]
        public Dictionary<string, Daily> TimeSeries
        {
            get; set;
        }
    }

    public class MetaDataDetails
    {
        [JsonPropertyName("1. Information")]
        public string Information { get; set; }

        [JsonPropertyName("2. Symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("3. Last Refreshed")]
        public DateTime LastRefreshed { get; set; }

        [JsonPropertyName("4. Output Size")]
        public string OutputSize { get; set; }

        [JsonPropertyName("5. Time Zone")]
        public string TimeZone { get; set; }
    }

    public class Daily
    {
        [JsonPropertyName("1. open")]
        public string Open { get; set; }

        [JsonPropertyName("2. high")]
        public string High { get; set; }

        [JsonPropertyName("3. low")]
        public string Low { get; set; }

        [JsonPropertyName("4. close")]
        public string Close { get; set; }

        [JsonPropertyName("5. volume")]
        public string Volume { get; set; }
    }

    public class DataPoint
    {
        public DateTime Date { get; set; }

        public decimal Open { get; set; }

        public decimal High { get; set; }

        public decimal Low { get; set; }

        public decimal Close { get; set; }

        public long Volume { get; set; }
    }
}
