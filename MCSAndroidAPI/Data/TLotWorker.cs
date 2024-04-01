using System;
using System.Collections.Generic;

namespace MCSAndroidAPI.Data;

public partial class TLotWorker
{
    public string DivisionCd { get; set; } = null!;

    public string ProcessCd { get; set; } = null!;

    public string MaterialCd { get; set; } = null!;

    public string LotNo { get; set; } = null!;

    public decimal ReportId { get; set; }

    public DateTime? ReportTime { get; set; }

    public string? WorkerCd { get; set; }

    public string? JobCd { get; set; }

    public string? MachineCd { get; set; }

    public string? LotWorkerNote { get; set; }

    public string? InputDiv { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdatedBy { get; set; }
}
