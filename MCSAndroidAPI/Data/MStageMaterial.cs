using System;
using System.Collections.Generic;

namespace MCSAndroidAPI.Data;

public partial class MStageMaterial
{
    public string ModelCd { get; set; } = null!;

    public int ProcessStageId { get; set; }

    public string MaterialCd { get; set; } = null!;

    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public string? DeletedFlg { get; set; }
}
