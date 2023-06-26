using System;
using System.Collections.Generic;

namespace BirdPlatFormEcommerce.IEntity;

public partial class TbFeedback
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int UserId { get; set; }

    public int? Rate { get; set; }

    public string? Detail { get; set; }

    public string? Image { get; set; }

    public virtual TbProduct Product { get; set; } = null!;

    public virtual TbUser User { get; set; } = null!;
}
