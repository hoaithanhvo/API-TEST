using System;
using System.Collections.Generic;

namespace MCSAndroidAPI.Data;

public partial class MRouting
{
    public string DivisionCd { get; set; } = null!;

    public string ProcessCd { get; set; } = null!;

    public string RoutingCd { get; set; } = null!;

    public string? RoutingName { get; set; }

    public string? RoutingEndFlg { get; set; }

    public string? CostcenterCd { get; set; }

    public string? Costcenter2Cd { get; set; }

    public string? Costcenter3Cd { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public string? ProductionFlg { get; set; }
}
