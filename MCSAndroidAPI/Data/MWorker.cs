using System;
using System.Collections.Generic;

namespace MCSAndroidAPI.Data;

public partial class MWorker
{
    public string DivisionCd { get; set; } = null!;

    public string? ProcessCd { get; set; }

    public string WorkerCd { get; set; } = null!;

    public string? WorkerName { get; set; }

    public DateTime? VdStrDay { get; set; }

    public DateTime? VdEndDay { get; set; }

    public string? DeptCd { get; set; }

    public string? RankCd { get; set; }

    public string? WorkerGrpCd { get; set; }

    public decimal? SearchFlg { get; set; }

    public decimal? RegistFlg { get; set; }

    public byte[]? Photo { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdatedBy { get; set; }
}
