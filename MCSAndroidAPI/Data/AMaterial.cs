using System;
using System.Collections.Generic;

namespace MCSAndroidAPI.Data;

public partial class AMaterial
{
    public string MaterialCd { get; set; } = null!;

    public string MaterialName { get; set; } = null!;

    public string? MaterialType { get; set; }

    public string? Division { get; set; }

    public string? ValuationClass { get; set; }

    public string? DrawingNo { get; set; }

    public string? MrpController { get; set; }

    public decimal? UnitPrice { get; set; }

    public string? Spec01 { get; set; }

    public string? Spec02 { get; set; }

    public string? Spec03 { get; set; }

    public string? Spec04 { get; set; }

    public string? Spec05 { get; set; }

    public string? Spec06 { get; set; }

    public string? Spec07 { get; set; }

    public string? Spec08 { get; set; }

    public string? Spec09 { get; set; }

    public string? Spec10 { get; set; }

    public decimal? NumSpec01 { get; set; }

    public decimal? NumSpec02 { get; set; }

    public decimal? NumSpec03 { get; set; }

    public decimal? NumSpec04 { get; set; }

    public decimal? NumSpec05 { get; set; }

    public decimal? NumSpec06 { get; set; }

    public decimal? NumSpec07 { get; set; }

    public decimal? NumSpec08 { get; set; }

    public decimal? NumSpec09 { get; set; }

    public decimal? NumSpec10 { get; set; }

    public DateOnly CreateDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateOnly UpdateDate { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public string? Unit { get; set; }

    public decimal? PackingNum { get; set; }

    public string? ProdRef01 { get; set; }

    public string? ProdRef02 { get; set; }

    public string? ProdRef03 { get; set; }

    public string? ProdRef04 { get; set; }

    public string? ProdRef05 { get; set; }

    public string? ProdRef06 { get; set; }

    public string? ProdRef07 { get; set; }

    public string? ProdRef08 { get; set; }

    public string? ProdRef09 { get; set; }
}
