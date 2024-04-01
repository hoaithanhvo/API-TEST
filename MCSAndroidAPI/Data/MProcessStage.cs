using System;
using System.Collections.Generic;

namespace MCSAndroidAPI.Data;

public partial class MProcessStage
{
    public string? ProcessCd { get; set; }

    public int Id { get; set; }

    public string? StageCd { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public string? DeletedFlg { get; set; }
}
