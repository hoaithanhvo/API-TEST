using System;
using System.Collections.Generic;

namespace MCSAndroidAPI.Data;

public partial class MProcess
{
    public string DivisionCd { get; set; } = null!;

    public string ProcessCd { get; set; } = null!;

    public string? ProcessMovexCd { get; set; }

    public string? ProcessName { get; set; }

    public string? HandyMenu { get; set; }

    public string? CostcenterCd { get; set; }

    public string? Costcenter2Cd { get; set; }

    public string? Costcenter3Cd { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? UpdatedBy { get; set; }
}
