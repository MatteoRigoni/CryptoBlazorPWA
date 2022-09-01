using GloboCrypto.Model.Data;
using GloboCrypto.WebAPI.Services.Coins;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GloboCrypto.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoinController : ControllerBase
    {
        private readonly ICoinService _coinService;

        public CoinController(ICoinService coinService)
        {
            _coinService = coinService;
        }

        [HttpGet("{coinId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CoinInfo>> Get([FromRoute] string coinId)
        {
            var result = await _coinService.GetCoinInfo(coinId);
            if (result is null)
                return NotFound();
            else
                return Ok(result);
        }

        [HttpGet("prices/{coinIds}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]        
        public async Task<ActionResult<CoinInfo>> GetPrices([FromRoute] string coinIds, string currency, string intervals)
        {
            var result = await _coinService.GetCoinPriceInfo(coinIds, currency, intervals);
            if (result is null)
                return NotFound();
            else
                return Ok(result);
        }
    }
}
