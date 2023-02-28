using System;
using System.Collections.Generic;

namespace API.Models;

public partial class InvoiceLine
{
    public int Id { get; set; }

    public string LoginId { get; set; } = null!;

    public string CustomerPo { get; set; } = null!;

    public string? Sku { get; set; }

    public string? ShortDescription { get; set; }

    public int PoQty { get; set; }

    public int ShipQty { get; set; }

    public int BoQty { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal? DcRate { get; set; }

    public decimal? DcAmount { get; set; }

    public decimal? FinalAmount { get; set; }

    public string? Memo { get; set; }

    public string Num { get; set; } = null!;
}
