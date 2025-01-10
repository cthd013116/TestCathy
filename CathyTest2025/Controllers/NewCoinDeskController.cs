using CathyTest2025.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Writers;

namespace CathyTest2025.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewCoinDeskController : ControllerBase
    {
        private List<NewCoinDeskApiModel> _newCoinDeskApiResultList = new();
        public readonly ILogger _logger;
        private readonly IHttpClientFactory _clientFactory;

        public NewCoinDeskController(ILogger<NewCoinDeskController> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        [HttpPost]
        public async Task<IActionResult> NewCoinDeskAPI()
        {
            var client = _clientFactory.CreateClient();
            //網址port號需自行調整
            var response = await client.GetAsync("http://localhost:5136/api/CoinDesk/CoinDeskContent");

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    var result = await response.Content.ReadFromJsonAsync<CoinDeskApiModel>();

                    if (result != null)
                    {
                        NewCoinDeskApiModel newCoinDesk = new NewCoinDeskApiModel()
                        {
                            UpdateDate = result.time == null ? "": result.time.updated

                        };

                        return Ok(result);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }

            return BadRequest();
        }


    }
}
