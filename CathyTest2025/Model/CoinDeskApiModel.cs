namespace CathyTest2025.Model
{
    public class CoinDeskApiModel
    {
        public Time _Time { get; set; }

        /// <summary>
        /// 免責聲明
        /// </summary>
        public string disclaimer { get; set; }

        /// <summary>
        /// 圖表名稱
        /// </summary>
        public string chartName { get; set; }



    }
    public class Time
    { 
        /// <summary>
        /// 更新時間_UTC
        /// </summary>
        public string updated { get; set; }

        /// <summary>
        /// 更新時間_ISO
        /// </summary>
        public string updatedISO { get; set; }

        /// <summary>
        /// 更新時間_UK
        /// </summary>
        public string updateduk { get; set; }
    }

    public class bpi
    { 
        /// <summary>
        /// 美金
        /// </summary>
        public CurrencyInfo USD { get; set; }

        /// <summary>
        /// 英鎊
        /// </summary>
        public CurrencyInfo GBP { get; set; }

        /// <summary>
        /// 歐元
        /// </summary>
        public CurrencyInfo EUR { get; set; }

    }
    public class CurrencyInfo
    { 
        /// <summary>
        /// 幣別代碼
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 幣別象徵
        /// </summary>
        public string symbol { get; set; }

        /// <summary>
        /// 匯率
        /// </summary>
        public string rate { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 詳細匯率
        /// </summary>
        public string rate_float { get; set; }
    }
}
