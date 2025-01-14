using CathyTest2025.Controllers;
using Xunit.Abstractions;

namespace CathyTest2025.xUnit.Controller
{
    public class CurrencyControllerTest
    {
        private ITestOutputHelper _output;
        private readonly CurrencyController _controller;

        public CurrencyControllerTest(ITestOutputHelper output, CurrencyController controller)
        {
            _output = output;
            _controller = controller;
        }

        [Fact]
        public void GetCurrencyAllTest()
        {
            _output.WriteLine("Current time: {0}", DateTime.Now);
            var result = _controller.GetCurrencyAll();
            _output.WriteLine("Get Result:{0}", result);
        }
    }
}