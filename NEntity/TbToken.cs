using System;
using System.Collections.Generic;

namespace BirdPlatFormEcommerce.NEntity;

public partial class TbToken
{
    public int Id { get; set; }

    public string? Token { get; set; }

    public int UserId { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public virtual TbUser User { get; set; } = null!;
}
