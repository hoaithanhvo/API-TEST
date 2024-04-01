using System;
using System.Collections.Generic;

namespace MCSAndroidAPI.Data;

public partial class TLotProduct
{
    public string DivisionCd { get; set; } = null!;

    public string ProcessCd { get; set; } = null!;

    public string ProductNo { get; set; } = null!;

    public string LotNo { get; set; } = null!;

    public string? ProductDay { get; set; }

    public string? ShiftCd { get; set; }

    public string? LineCd { get; set; }

    public decimal? PlanQty { get; set; }

    public decimal? GoodQty { get; set; }

    public string? MoNo { get; set; }

    public decimal? RestHr { get; set; }

    public DateTime? ProductStrDt { get; set; }

    public DateTime? ProductEndDt { get; set; }

    public string? ProdRef01 { get; set; }

    public string? ProdRef02 { get; set; }

    public string? ProdRef03 { get; set; }

    public string? ProdRef04 { get; set; }

    public string? ProdRef05 { get; set; }

    public string? ProdRef06 { get; set; }

    public string? ProdRef07 { get; set; }

    public string? ProdRef08 { get; set; }

    public string? ProdRef09 { get; set; }

    public decimal? LotStatusCode { get; set; }

    public decimal? BoxNoCount { get; set; }

    public decimal? WorkerNum { get; set; }

    public decimal? WorkunitNum { get; set; }

    public decimal? TactNum { get; set; }

    public decimal? DefectQty { get; set; }

    public decimal? DefectRate { get; set; }

    public decimal? Defect2Qty { get; set; }

    public decimal? Defect2Rate { get; set; }

    public decimal? OnlineHr { get; set; }

    public decimal? StopHr { get; set; }

    public decimal? OnlineRate { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public string? LeaderWorkerNo { get; set; }

    public int? Cavity { get; set; }
}
