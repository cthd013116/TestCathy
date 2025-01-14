using CathyTest2025.Controllers;
using CathyTest2025.Interface;
using CathyTest2025.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CathyTest2025.Tests
{
    public class CurrencyControllerTests
    {
        private readonly Mock<IRepositoryFactory> _mockRepositoryFactory;
        private readonly Mock<IRepository<CurrencyModel>> _mockRepository;
        private readonly Mock<ILogger<CurrencyController>> _mockLogger;
        private readonly CurrencyController _controller;

        public CurrencyControllerTests()
        {
            _mockRepositoryFactory = new Mock<IRepositoryFactory>();
            _mockRepository = new Mock<IRepository<CurrencyModel>>();
            _mockLogger = new Mock<ILogger<CurrencyController>>();
            _controller = new CurrencyController(_mockLogger.Object, _mockRepositoryFactory.Object);
        }

        [Fact]
        public async Task GetCurrencyAll_ReturnsOkResult_WithSortedCurrencyList()
        {
            // Arrange
            var currencyList = new List<CurrencyModel>
            {
                new CurrencyModel { CurrencyEn = "USD", CurrencyCh = "美元" },
                new CurrencyModel { CurrencyEn = "EUR", CurrencyCh = "歐元" },
                new CurrencyModel { CurrencyEn = "GBP", CurrencyCh = "英鎊" }
            };
            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(currencyList);
            _mockRepositoryFactory.Setup(factory => factory.CreateRepository<CurrencyModel>()).Returns(_mockRepository.Object);

            // Act
            var result = await _controller.GetCurrencyAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<CurrencyModel>>(okResult.Value);
            Assert.Equal(3, returnValue.Count);
            Assert.Equal("EUR", returnValue[0].CurrencyEn);
            Assert.Equal("GBP", returnValue[1].CurrencyEn);
            Assert.Equal("USD", returnValue[2].CurrencyEn);
        }

        [Fact]
        public async Task GetCurrencyAll_ReturnsNotFound_WhenNoCurrency()
        {
            // Arrange
            var currencyList = new List<CurrencyModel>();
            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(currencyList);
            _mockRepositoryFactory.Setup(factory => factory.CreateRepository<CurrencyModel>()).Returns(_mockRepository.Object);

            // Act
            var result = await _controller.GetCurrencyAll();

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
