using System;
using System.Collections.Generic;

namespace MCSAndroidAPI.Data;

public partial class MUser
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string? UserCode { get; set; }

    public string? PassWord { get; set; }

    public string? FullName { get; set; }

    public int? RoleId { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public string? Department { get; set; }

    public bool? FiFoFlg { get; set; }
}
