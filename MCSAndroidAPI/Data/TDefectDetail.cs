using System;
using System.Collections.Generic;

namespace MCSAndroidAPI.Data;

public partial class TDefectDetail
{
    public int Id { get; set; }

    public string? DivisionCd { get; set; }

    public string? ProcessCd { get; set; }

    public string? Leader { get; set; }

    public string? WorkerCd { get; set; }

    public string? Model { get; set; }

    public string? LotNo { get; set; }

    public string? LineCd { get; set; }

    public string? ShiftCd { get; set; }

    public decimal? NgQty { get; set; }

    public string? DefectCd { get; set; }

    public string? StageCd { get; set; }

    public string? StatusFlg { get; set; }

    public string? DeleteFlg { get; set; }

    public string? TerminalNo { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public string? Materials { get; set; }
}
