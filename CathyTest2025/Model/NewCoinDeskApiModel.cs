namespace CathyTest2025.Model
{
    public class NewCoinDeskApiModel
    {
        /// <summary>
        /// 匯率更新日期
        /// </summary>
        public string UpdateDate { get; set; }
       

        /// <summary>
        /// 幣別資訊
        /// </summary>
        public List<ApiCurrencyInfo> CurrencyInfo { get; set; }

    }
    /// <summary>
    /// API幣別資訊 Model
    /// </summary>
    public class ApiCurrencyInfo
    { 
        /// <summary>
        /// 幣別
        /// </summary>
        public string CurrencyEn { get; set; }

        /// <summary>
        /// 幣別中文名稱
        /// </summary>
        public string CurrencyCh { get; set; }

        /// <summary>
        /// 匯率
        /// </summary>
        public string RateFloat { get; set; }
    }

    
}
