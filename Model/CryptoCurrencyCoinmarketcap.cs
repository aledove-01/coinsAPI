using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace coins.Model;
public class CryptoCurrencyCoinmarketcap : ICryptoCurrencyRepository
{
    private readonly HttpClient _client;
    public CryptoCurrencyCoinmarketcap(HttpClient client, IConfiguration config)
    {
        _client = client;
        var api = config.GetValue<string>("AccessAPI:API_KEY_CK");
        _client.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", api);
        _client.DefaultRequestHeaders.Add("Accepts", "application/json");
    }
    public async Task<List<Coin>> GetTopCoins(int _topRateMax=25)
    {
        var URL = new UriBuilder("https://pro-api.coinmarketcap.com/v1/cryptocurrency/map");
        var queryString = HttpUtility.ParseQueryString(string.Empty);
        queryString["limit"] = _topRateMax.ToString(); 
        queryString["sort"] = "cmc_rank";
        URL.Query = queryString.ToString();
        
        var response = await _client.GetAsync(URL.ToString());
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var jsonData = JsonConvert.DeserializeObject<CryptoDetails>(json, new JsonSerializerSettings{NullValueHandling = NullValueHandling.Ignore});

            List<Coin> coins = jsonData.data.Select(x => new Coin()
            {
                id = x.id,
                name = x.name,
                symbol = x.symbol,
                slug = x.slug,
            }).ToList();
            string[] ids = coins.Select(coins=>coins.id.ToString()).ToArray();
            List<Coin> coinsPrices = await GetPriceCoins(ids);
            List<Coin> coinsMeta = await GetMetadataCoins(ids);
            coins.ForEach(coin => {
                coin.logo = coinsMeta.FirstOrDefault(meta => meta.id == coin.id).logo;
                coin.price = coinsPrices.FirstOrDefault(price => price.id == coin.id).price ;
            });
            return coins; 
        }
       
        return null;
    }
    public async Task<List<Coin>> GetPriceCoins(string[] _ids)
    {
        var URL = new UriBuilder("https://pro-api.coinmarketcap.com/v1/cryptocurrency/quotes/latest");
        var queryString = HttpUtility.ParseQueryString(string.Empty);
        queryString["id"] = string.Join(",", _ids); 
        queryString["convert"] = "USD";
        URL.Query = queryString.ToString();
        
        var response = await _client.GetAsync(URL.ToString());
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var jsonData = JsonConvert.DeserializeObject<CryptoData>(json, new JsonSerializerSettings{NullValueHandling = NullValueHandling.Ignore});

            List<Coin> coins = jsonData.data.Select(x => new Coin()
            {
                id = x.Value.id,
                name = x.Value.name,
                symbol = x.Value.symbol,
                last_updated = x.Value.last_updated,
                slug = x.Value.slug,
                total_supply=x.Value.total_supply,
                max_supply=x.Value.max_supply,
                circulating_supply = x.Value.circulating_supply,
                self_reported_circulating_supply = x.Value.self_reported_circulating_supply,
                self_reported_market_cap = x.Value.self_reported_market_cap,
                price = x.Value.quote.USD.price
            }).ToList();
            return coins; 
        }
       
        return null;
    }
     public async Task<List<Coin>> GetMetadataCoins(string[] _ids)
    {
        var URL = new UriBuilder("https://pro-api.coinmarketcap.com/v2/cryptocurrency/info");
        var queryString = HttpUtility.ParseQueryString(string.Empty);
        queryString["id"] = string.Join(",", _ids); 
        URL.Query = queryString.ToString();
        
        var response = await _client.GetAsync(URL.ToString());
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var jsonData = JsonConvert.DeserializeObject<CryptoData>(json, new JsonSerializerSettings{NullValueHandling = NullValueHandling.Ignore});

            List<Coin> coins = jsonData.data.Select(x => new Coin()
            {
                id = x.Value.id,
                name = x.Value.name,
                symbol = x.Value.symbol,
                logo = x.Value.logo
            }).ToList();
            return coins; 
        }
       
        return null;
    }
}
