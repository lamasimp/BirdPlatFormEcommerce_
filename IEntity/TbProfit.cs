using System;
using System.Collections.Generic;

namespace BirdPlatFormEcommerce.IEntity;

public partial class TbProfit
{
    public int Id { get; set; }

    public int ShopId { get; set; }

    public DateTime Orderdate { get; set; }

    public decimal? Total { get; set; }

    public int? OrderDetailId { get; set; }

    public virtual TbOrderDetail? OrderDetail { get; set; }

    public virtual TbShop Shop { get; set; } = null!;
}
