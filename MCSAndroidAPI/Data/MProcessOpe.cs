using System;
using System.Collections.Generic;

namespace MCSAndroidAPI.Data;

public partial class MProcessOpe
{
    public string DivisionCd { get; set; } = null!;

    public string ProcessCd { get; set; } = null!;

    public string OperationCd { get; set; } = null!;

    public string? OperationName { get; set; }

    public DateOnly? CreateDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateOnly? UpdateDate { get; set; }

    public string? UpdatedBy { get; set; }
}
