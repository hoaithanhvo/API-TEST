using System;
using System.Collections.Generic;

namespace MCSAndroidAPI.Data;

public partial class MShift
{
    public string DivisionCd { get; set; } = null!;

    public string ProcessCd { get; set; } = null!;

    public string ShiftCd { get; set; } = null!;

    public string? ShiftName { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public TimeOnly? Start1 { get; set; }

    public TimeOnly? End1 { get; set; }

    public TimeOnly? Start2 { get; set; }

    public TimeOnly? End2 { get; set; }

    public TimeOnly? Start3 { get; set; }

    public TimeOnly? End3 { get; set; }

    public TimeOnly? Start4 { get; set; }

    public TimeOnly? End4 { get; set; }

    public TimeOnly? Start5 { get; set; }

    public TimeOnly? End5 { get; set; }
}
