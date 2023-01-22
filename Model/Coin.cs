using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace coins.Model;
public class Coin
{
    public int id { get; set; }
    public string name { get; set; }
    public string symbol { get; set; }
    public string slug { get; set; }
    public double circulating_supply { get; set; }
    public double total_supply { get; set; }
    public double max_supply { get; set; }
    [JsonConverter(typeof(IsoDateTimeConverter))]
    public DateTime last_updated { get; set; }
    public object self_reported_circulating_supply { get; set; }
    public object self_reported_market_cap { get; set; }
    public QuoteCoinUsd quote { get; set; }
    public double price { get; set; }
    public string logo {get; set;}
}
