using CathyTest2025.Interface;
using CathyTest2025.Model;
using Microsoft.AspNetCore.Mvc;

namespace CathyTest2025.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyController : ControllerBase
    {
        public ILogger _logger;
        private readonly IRepositoryFactory _repositoryFactory;
        public CurrencyController(ILogger<CurrencyController> logger, IRepositoryFactory repositoryFactory)
        {
            _logger = logger;
            _repositoryFactory = repositoryFactory;
        }

        /// <summary>
        /// 取得所有資料庫貨幣資料
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCurrencyAll")]
        public async Task<ActionResult> GetCurrencyAll()
        {
            var repository = _repositoryFactory.CreateRepository<CurrencyModel>();
            var currency = await repository.GetAllAsync();
            var sortedCurrencyList = currency.OrderBy(p=> p.CurrencyEn).ToList();// 根據 CurrencyEn 屬性進行排序
            return sortedCurrencyList.Count != 0 ? Ok(sortedCurrencyList) : NotFound();
        }

        /// <summary>
        /// 取出特定名稱的貨幣資訊
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetByNameAsync")]
        public async Task<ActionResult> GetByNameAsync(string name)
        {
            var repository = _repositoryFactory.CreateRepository<CurrencyModel>();
            var currency = await repository.GetByNameAsync(name);
            return currency != null ? Ok(currency) : NotFound();
        }

        /// <summary>
        /// 新增單筆貨幣資料
        /// </summary>
        /// <param name="currencyChName"></param>
        /// <param name="currencyEnName"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddCurrency")]
        public async Task<ActionResult> AddCurrency(string currencyEnName,string currencyChName)
        {
            CurrencyModel model = new CurrencyModel() 
            { 
                CurrencyEn = currencyEnName,
                CurrencyCh = currencyChName
            };
            var repository = _repositoryFactory.CreateRepository<CurrencyModel>();
            await repository.AddAsync(model);
            return CreatedAtAction(nameof(AddCurrency), new { CurrencyCh = model.CurrencyCh,currencyEnName = model.CurrencyEn }, model);
        }

        /// <summary>
        /// 修改單筆貨幣資料
        /// </summary>
        /// <param name="currencyChName">貨幣中文名稱</param>
        /// <param name="currencyEnName">貨幣代碼</param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateCurrency")]
        public async Task<ActionResult> UpdateCurrency(string currencyEnName, string currencyChName)
        {
            CurrencyModel model = new CurrencyModel()
            {
                CurrencyEn = currencyEnName,
                CurrencyCh = currencyChName
            };
            var repository = _repositoryFactory.CreateRepository<CurrencyModel>();
            await repository.UpdateAsync(model);
            return CreatedAtAction(nameof(UpdateCurrency), new { CurrencyCh = model.CurrencyCh, currencyEnName = model.CurrencyEn }, model);
        }

        /// <summary>
        /// 刪除貨幣代碼
        /// </summary>
        /// <param name="currencyEnName"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteCurrency")]
        public async Task<ActionResult> DeleteCurrency(string currencyEnName)
        {
            var repository = _repositoryFactory.CreateRepository<CurrencyModel>();
            await repository.DeleteAsync(currencyEnName);
            return CreatedAtAction(nameof(UpdateCurrency), new { CurrencyEn = currencyEnName });
        }

    }
}
