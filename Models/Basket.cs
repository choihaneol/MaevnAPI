using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Basket
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int ProductId { get; set; }

    public int Qty { get; set; }

    public int IsPreorder { get; set; }

    public int SubAccount { get; set; }

    public int StatusId { get; set; }

    public DateTime DateCreated { get; set; }
}
