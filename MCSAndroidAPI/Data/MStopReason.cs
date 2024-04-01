using System;
using System.Collections.Generic;

namespace MCSAndroidAPI.Data;

public partial class MStopReason
{
    public string DivisionCd { get; set; } = null!;

    public string ProcessCd { get; set; } = null!;

    public string StopRsnCd { get; set; } = null!;

    public string? StopRsnName { get; set; }

    public DateOnly? CreateDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateOnly? UpdateDate { get; set; }

    public string? UpdatedBy { get; set; }
}
