namespace coins.Model;
public interface ICryptoCurrencyRepository
{
    Task<List<Coin>> GetTopCoins(int _topRateMax = 25);
    Task<List<Coin>> GetPriceCoins(string[] _ids);
    Task<List<Coin>> GetMetadataCoins(string[] _ids);
}