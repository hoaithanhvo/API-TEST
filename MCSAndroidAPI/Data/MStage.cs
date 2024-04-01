using System;
using System.Collections.Generic;

namespace MCSAndroidAPI.Data;

public partial class MStage
{
    public string StageCd { get; set; } = null!;

    public string? StageName { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? UpdatedBy { get; set; }
}
