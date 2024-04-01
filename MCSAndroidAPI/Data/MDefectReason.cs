using System;
using System.Collections.Generic;

namespace MCSAndroidAPI.Data;

public partial class MDefectReason
{
    public string DivisionCd { get; set; } = null!;

    public string ProcessCd { get; set; } = null!;

    public string DefectRsnCd { get; set; } = null!;

    public string? DefectRsnName { get; set; }

    public DateOnly? CreateDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateOnly? UpdateDate { get; set; }

    public string? UpdatedBy { get; set; }

    public string? ReasonBy { get; set; }
}
