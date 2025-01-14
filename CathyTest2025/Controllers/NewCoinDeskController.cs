using CathyTest2025.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Writers;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Globalization;

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
            var result = new NewCoinDeskApiModel();
            //網址port號需自行調整
            var responsecoinAPI = await client.GetAsync("http://localhost:5136/api/CoinDesk/CoinDeskContent");

            //當取得api成功時，做新API內容整理
            if (responsecoinAPI.IsSuccessStatusCode)
            {
                try
                {
                    var coinDeskAPIResultString = await responsecoinAPI.Content.ReadAsStringAsync();
                    var coinDeskAPIResultModel = await responsecoinAPI.Content.ReadFromJsonAsync<CoinDeskApiModel>();
                    if (!string.IsNullOrEmpty(coinDeskAPIResultString))
                    {
                        //取得CoinDeskAPI幣別
                        var jsonObj = JObject.Parse(coinDeskAPIResultString);
                        // 取得 bpi 區塊
                        var bpi = jsonObj["bpi"];
                        // 取得各貨幣幣別及匯率
                        var currencyInfoList = new List<ApiCurrencyInfo>();
                        foreach (var currency in bpi)
                        {
                            ApiCurrencyInfo info = new ApiCurrencyInfo
                            { 
                                CurrencyEn = currency.First["code"].ToString(),
                                RateFloat = currency.First["rate"].ToString(),
                            };
                            currencyInfoList.Add(info);
                        }
                        //透過呼叫幣值CRUD API 取出DB內幣值資料
                        var responseCurrencyDBAPI = await client.GetAsync("http://localhost:5136/api/Currency/GetCurrencyAll");
                        var currencyDBResult = await responseCurrencyDBAPI.Content.ReadFromJsonAsync<List<CurrencyModel>>();
                        //當取得資料庫內容不為null時，繼續往下動作
                        if (currencyDBResult != null)
                        {
                            var query = from info in currencyInfoList
                                        join db in currencyDBResult on info.CurrencyEn equals db.CurrencyEn
                                        select new ApiCurrencyInfo
                                        {
                                            CurrencyEn = info.CurrencyEn,
                                            CurrencyCh = db.CurrencyCh,
                                            RateFloat = info.RateFloat
                                        };
                            var list = query.ToList();

                            // 取得UpdateDate 區塊
                            var time = coinDeskAPIResultModel.time;

                            //Case 1：updated 轉成 指定格式
                            DateTimeOffset dto = DateTimeOffset.ParseExact(time.updated,"MMM d, yyyy HH:mm:ss UTC",
                            System.Globalization.CultureInfo.InvariantCulture);
                            // 若要轉換為本地時間
                            DateTime localTime = dto.LocalDateTime;
                            result.UpdateDate = localTime.ToString("yyyy/MM/dd hh:mm:ss");

                            //Case 2：updatedISO 轉成 指定格式
                            result.UpdateDate = DateTime.Parse(time.updatedISO).ToString("yyyy/MM/dd hh:mm:ss");
                            
                            //Case 3：updateduk 轉成 指定格式
                            // 定義日期格式
                            string format = "MMM d, yyyy 'at' HH:mm 'GMT'";
                            // 解析字串為 DateTime
                            DateTime dateTime = DateTime.ParseExact(time.updateduk, format, CultureInfo.InvariantCulture);
                            // 格式化為標準日期格式 (ISO 8601)
                            result.UpdateDate = dateTime.ToString("yyyy/MM/dd hh:mm:ss");

                            result.CurrencyInfo = list;
                        }
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
