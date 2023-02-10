using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Basket
{
    public int ProductId { get; set; }

    public int UserId { get; set; }

    public int? ItemId { get; set; }

    public int? Quantity { get; set; }
}
