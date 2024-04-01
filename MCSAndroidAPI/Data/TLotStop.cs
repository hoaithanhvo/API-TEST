using System;
using System.Collections.Generic;

namespace MCSAndroidAPI.Data;

public partial class TLotStop
{
    public string DivisionCd { get; set; } = null!;

    public string ProcessCd { get; set; } = null!;

    public string MaterialCd { get; set; } = null!;

    public string LotNo { get; set; } = null!;

    public decimal ReportId { get; set; }

    public DateTime? StopStrTime { get; set; }

    public DateTime? StopEndTime { get; set; }

    public decimal? StopHr { get; set; }

    public string? StopRsnCd { get; set; }

    public string? StopNote { get; set; }

    public decimal? InputDiv { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdatedBy { get; set; }
}
