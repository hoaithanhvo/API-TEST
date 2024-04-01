using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MCSAndroidAPI.Models
{
    public class LotScrapModel : LotBaseModel
    {
        public decimal? ReportId { get; set; }
        public string? ItemType { get; set; }
        public string? ItemNo { get; set; }
        public decimal? ScrapQty { get; set; }
        public string? ScrapRsnCd { get; set; }
        public string? ScrapRsnName { get; set; }
        public string? ScrapNote { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal ScrapAmt { get; set; }
    }

    public class LotScrapFields : LotBaseFields
    {
        public const string ReportId = "ReportId";
        public const string ItemType = "ItemType";
        public const string ItemNo = "ItemNo";
        public const string ScrapQty = "ScrapQty";
        public const string ScrapRsnCd = "ScrapRsnCd";
        public const string ScrapRsnName = "ScrapRsnName";
        public const string ScrapNote = "ScrapNote";
        public const string UnitPrice = "UnitPrice";
        public const string ScrapAmt = "ScrapAmt";
    }

    public class LotScrapDisplay : LotBaseDisplay
    {
        public const string ReportId = "Report";
        public const string ItemType = "Type";
        public const string ItemNo = "Item no";
        public const string ScrapQty = "Scrap";
        public const string ScrapRsnCd = "Reason";
        public const string ScrapNote = "Note";
        public const string UnitPrice = "Price";
        public const string ScrapAmt = "Amount";
    }

    public class LotScrapMaxlength : LotBaseMaxlength
    {
        public const int ItemNo = 15;
        public const int ScrapQty = 7;
        public const int ScrapRsnCd = 5;
        public const int ScrapNote = 100;
        public const int UnitPrice = 7;
        public const int ScrapAmt = 7;
    }
}
