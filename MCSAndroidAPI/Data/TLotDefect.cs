using System;
using System.Collections.Generic;

namespace MCSAndroidAPI.Data;

public partial class TLotDefect
{
    public string DivisionCd { get; set; } = null!;

    public string ProcessCd { get; set; } = null!;

    public string MaterialCd { get; set; } = null!;

    public string LotNo { get; set; } = null!;

    public decimal ReportId { get; set; }

    public DateTime? ReportTime { get; set; }

    public string? DefectRsnCd { get; set; }

    public decimal? DefectQty { get; set; }

    public string? DefectNote { get; set; }

    public decimal? InputDiv { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdatedBy { get; set; }
}
