using System;
using System.Collections.Generic;

namespace MCSAndroidAPI.Data;

public partial class TLotScrap
{
    public string DivisionCd { get; set; } = null!;

    public string ProcessCd { get; set; } = null!;

    public string MaterialCd { get; set; } = null!;

    public string LotNo { get; set; } = null!;

    public decimal ReportId { get; set; }

    public DateTime? ReportTime { get; set; }

    public string? ItemNo { get; set; }

    public string? ItemName { get; set; }

    public string? ScrapRsnCd { get; set; }

    public decimal? ScrapQty { get; set; }

    public decimal? UnitPrice { get; set; }

    public decimal? ScrapAmt { get; set; }

    public string? ScrapNote { get; set; }

    public decimal? InputDiv { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdatedBy { get; set; }
}
