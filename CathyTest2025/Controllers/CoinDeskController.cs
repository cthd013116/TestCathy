using Microsoft.AspNetCore.Mvc;

namespace CathyTest2025.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CoinDeskController : ControllerBase
    {

        [HttpGet]
        public string CoinDeskContent()
        {
            string result = "{\"time\":{\"updated\":\"Jan 9, 2025 06:59:23 UTC\",\"updatedISO\":\"2025-01-09T06:59:23+00:00\",\"updateduk\":\"Jan 9, 2025 at 06:59 GMT\"},\"disclaimer\":\"This data was produced from the CoinDesk Bitcoin Price Index (USD). Non-USD currency data converted using hourly conversion rate from openexchangerates.org\",\"chartName\":\"Bitcoin\",\"bpi\":{\"USD\":{\"code\":\"USD\",\"symbol\":\"&#36;\",\"rate\":\"94,988.721\",\"description\":\"United States Dollar\",\"rate_float\":94988.7209},\"GBP\":{\"code\":\"GBP\",\"symbol\":\"&pound;\",\"rate\":\"75,760.629\",\"description\":\"British Pound Sterling\",\"rate_float\":75760.6291},\"EUR\":{\"code\":\"EUR\",\"symbol\":\"&euro;\",\"rate\":\"91,353.883\",\"description\":\"Euro\",\"rate_float\":91353.8825}}}";
            return result;
        }

    }
}
