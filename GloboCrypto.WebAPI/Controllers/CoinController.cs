using GloboCrypto.Model.Data;
using GloboCrypto.WebAPI.Services.Coins;
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
        public async Task<ActionResult<CoinInfo>> Get([FromRoute] string coindId)
        {
            var result = await _coinService.GetCoinInfo(coindId);
            if (result is null)
                return NotFound();
            else
                return Ok(result);
        }

        [HttpGet("price/{coinIds}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CoinInfo>> GetPrices([FromRoute] string coindIds, string currency, string intervals)
        {
            var result = await _coinService.GetCoinPriceInfo(coindIds, currency, intervals);
            if (result is null)
                return NotFound();
            else
                return Ok(result);
        }
    }
}
