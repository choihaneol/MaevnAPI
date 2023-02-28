using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Wishlist
{
    public int WishItemId { get; set; }

    public int UserId { get; set; }

    public int? ItemId { get; set; }
}
