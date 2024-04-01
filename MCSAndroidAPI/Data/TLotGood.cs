using System;
using System.Collections.Generic;

namespace MCSAndroidAPI.Data;

public partial class TLotGood
{
    public string DivisionCd { get; set; } = null!;

    public string ProcessCd { get; set; } = null!;

    public string MaterialCd { get; set; } = null!;

    public string LotNo { get; set; } = null!;

    public string BoxNo { get; set; } = null!;

    public string? ProdStatus { get; set; }

    public DateTime? ProdDate { get; set; }

    public DateTime? ProdStartTime { get; set; }

    public DateTime? ProdEndTime { get; set; }

    public decimal? ProdQty { get; set; }

    public decimal? ProdWorkunit { get; set; }

    public decimal? ProdTact { get; set; }

    public string? SapUpdateFlg { get; set; }

    public string? MoNo { get; set; }

    public string? SapLinkStatus { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public decimal? SapRemainQty { get; set; }

    public string? SapLinkResult { get; set; }
}
