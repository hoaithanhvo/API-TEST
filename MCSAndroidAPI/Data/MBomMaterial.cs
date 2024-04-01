using System;
using System.Collections.Generic;

namespace MCSAndroidAPI.Data;

public partial class MBomMaterial
{
    public string ModelNo { get; set; } = null!;

    public string MaterialNo { get; set; } = null!;

    public decimal? MaterialNum { get; set; }

    public string? BomTxt { get; set; }

    public string? BomTxt2 { get; set; }

    public string? BomTxt3 { get; set; }

    public string? BomTxt4 { get; set; }

    public string? BomTxt5 { get; set; }

    public string? BomTxt6 { get; set; }

    public string? BomTxt7 { get; set; }

    public string? BomTxt8 { get; set; }

    public string? BomTxt9 { get; set; }

    public string? BomTxt10 { get; set; }

    public string? BomTxt11 { get; set; }

    public DateOnly? CreateDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateOnly? UpdateDate { get; set; }

    public string? UpdatedBy { get; set; }

    public string? MakeBuyDiv { get; set; }
}
