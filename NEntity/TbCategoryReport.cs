using System;
using System.Collections.Generic;

namespace BirdPlatFormEcommerce.NEntity;

public partial class TbCategoryReport
{
    public int CateRpId { get; set; }

    public string? Detail { get; set; }

    public virtual ICollection<TbReport> TbReports { get; set; } = new List<TbReport>();
}
