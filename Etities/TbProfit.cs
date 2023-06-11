using System;
using System.Collections.Generic;

namespace BirdPlatFormEcommerce.Etities;

public partial class TbProfit
{
    public int Id { get; set; }

    public int ShopId { get; set; }

    public int OrderId { get; set; }

    public DateTime? Orderdate { get; set; }

    public decimal? Total { get; set; }

    public virtual TbOrder Order { get; set; } = null!;

    public virtual TbShop Shop { get; set; } = null!;
}
