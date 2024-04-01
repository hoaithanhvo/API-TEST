using System;
using System.Collections.Generic;

namespace MCSAndroidAPI.Data;

public partial class MLine
{
    public string DivisionCd { get; set; } = null!;

    public string ProcessCd { get; set; } = null!;

    public string LineCd { get; set; } = null!;

    public string? LineName { get; set; }

    public decimal? StdOnlineRate { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? UpdatedBy { get; set; }
}
