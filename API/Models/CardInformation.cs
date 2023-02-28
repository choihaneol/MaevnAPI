using System;
using System.Collections.Generic;

namespace API.Models;

public partial class CardInformation
{
    public int CardId { get; set; }

    public int UserId { get; set; }

    public string? LoginId { get; set; }

    public int? ErpCustomerId { get; set; }

    public int? CardNumber { get; set; }

    public string? CardHolderName { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public int? CardCode { get; set; }

    public virtual User User { get; set; } = null!;
}
