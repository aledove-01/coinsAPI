using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public class StatusCoinmarketcap
{
    [JsonConverter(typeof(IsoDateTimeConverter))]
    public DateTime timestamp { get; set; }
    public int error_code { get; set; }
    public string error_message { get; set; }
    public int elapsed { get; set; }
    public int credit_count { get; set; }
}