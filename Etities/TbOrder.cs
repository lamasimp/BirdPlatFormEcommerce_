using System;
using System.Collections.Generic;

namespace BirdPlatFormEcommerce.Etities;

public partial class TbOrder
{
    public int OrderId { get; set; }

    public bool? Status { get; set; }

    public int UserId { get; set; }

    public string? Note { get; set; }

    public decimal TotalPrice { get; set; }

    public DateTime? OrderDate { get; set; }

    public int? PaymentId { get; set; }

    public virtual TbPayment? Payment { get; set; }

    public virtual ICollection<TbOrderDetail> TbOrderDetails { get; set; } = new List<TbOrderDetail>();

    public virtual ICollection<TbProfit> TbProfits { get; set; } = new List<TbProfit>();

    public virtual TbUser User { get; set; } = null!;
}
