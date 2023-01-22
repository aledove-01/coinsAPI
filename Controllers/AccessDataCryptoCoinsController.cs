using System.Net;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using coins.Model;

namespace coins.Controllers;

[ApiController]
[Route("API/[controller]")]
public class AccessDataCryptoCoinsController : ControllerBase
{
    private readonly ILogger<AccessDataCryptoCoinsController> _logger;
    private readonly string? _APIKEY;
    private readonly ICryptoCurrencyRepository _repository;
    public AccessDataCryptoCoinsController(ILogger<AccessDataCryptoCoinsController> logger, IConfiguration config, ICryptoCurrencyRepository repository)
    {
        _logger = logger;
        _APIKEY = config.GetValue<string>("AccessAPI:API_KEY_CK");
        _repository = repository;
    }

    [HttpGet("TopCoins")]
    public async Task<IActionResult> GetTopCoins()
    {
        try
        {
            var topCoins = await _repository.GetTopCoins();
            _logger.LogInformation("Retrieve top coins");
            return Ok(topCoins);
        }
        catch (System.Exception ex)
        {
            _logger.LogError("Error in GetTopCoins. " + ex.Message);
            return HandleException(ex);
        }
    }

    [HttpGet("PriceCoins")]
    public async Task<IActionResult> GetPriceCoins([FromQuery]string[] ids)
    {
        try
        {
            var pricesCoins = await _repository.GetPriceCoins(ids);
            _logger.LogInformation("Retrieve prices coins");
            return Ok(pricesCoins);
        }
        catch (System.Exception ex)
        {
            _logger.LogError("Error in GetPriceCoins. " + ex.Message);
            return HandleException(ex);
        }       
    }

    [HttpGet("MetadataCoins")]
    public async Task<IActionResult> GetMetadataCoins([FromQuery]string[] ids)
    {
        try
        {
            var metadataCoins = await _repository.GetMetadataCoins(ids);
            _logger.LogInformation("Retrieve metada data coins");
            return Ok(metadataCoins);
        }
        catch (System.Exception ex)
        {
            _logger.LogError("Error in MetadataCoins. " + ex.Message);
            return HandleException(ex);
        }       
    }

    private IActionResult HandleException(Exception ex)
    {
        return StatusCode(500, "Unexpected error occurred. Please try again later.");
    }
}