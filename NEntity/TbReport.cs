using System;
using System.Collections.Generic;

namespace BirdPlatFormEcommerce.NEntity;

public partial class TbReport
{
    public int ReportId { get; set; }

    public string? Detail { get; set; }

    public bool? Status { get; set; }

    public int? UserId { get; set; }

    public int? ShopId { get; set; }

    

    public int CateRpId { get; set; }

    public virtual TbCategoryReport CateRp { get; set; } = null!;

    public virtual TbShop? Shop { get; set; }

    public virtual TbUser? User { get; set; }
}
