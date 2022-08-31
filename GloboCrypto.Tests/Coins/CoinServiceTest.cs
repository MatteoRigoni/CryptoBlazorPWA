using GloboCrypto.WebAPI.Services.Coins;
using GloboCrypto.WebAPI.Services.Http;
using GloboCrypto.WebAPI.Services.Models;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GloboCrypto.Tests.Coins
{
    [TestFixture]
    internal class CoinServiceTest
    {
        private ICoinService _coinService;
        private Mock<IConfiguration> _mockConfiguration;
        private Mock<IHttpService> _mockHttpService;

        [SetUp]
        public void Setup()
        {
            _mockConfiguration = new Mock<IConfiguration>();
            _mockHttpService = new Mock<IHttpService>();

            _mockConfiguration.Setup(p => p["NomicsAPIKey"]).Returns("test-key");

            _coinService = new CoinService(_mockHttpService.Object, _mockConfiguration.Object);
        }

        [Test]
        public async Task GetCoinInfo_Success()
        {
            _mockHttpService.Setup(p => p.GetAsync<NomicsCoinInfo[]>(
                It.IsAny<string>()
                ))
                .ReturnsAsync(new NomicsCoinInfo[] { new NomicsCoinInfo { Id = "BTC" } });

            var result = await _coinService.GetCoinInfo("BTC");
            Assert.AreEqual("BTC", result.Id);
        }
    }
}
