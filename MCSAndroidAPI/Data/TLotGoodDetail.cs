using System;
using System.Collections.Generic;

namespace MCSAndroidAPI.Data;

public partial class TLotGoodDetail
{
    public string DivisionCd { get; set; } = null!;

    public string ProcessCd { get; set; } = null!;

    public string MaterialCd { get; set; } = null!;

    public string LotNo { get; set; } = null!;

    public string BoxNo { get; set; } = null!;

    public string RoutingCd { get; set; } = null!;

    public DateTime? ProdScanDate { get; set; }

    public string? ProdPicCd { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? UpdatedBy { get; set; }
}
