using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Product
{
    public int Pid { get; set; }

    public int ErpProgramId { get; set; }

    public int ErpProductId { get; set; }

    public string? StyleNumber { get; set; }

    public string? ColorCode { get; set; }

    public string? ColorName { get; set; }

    public string? ShortDescription { get; set; }

    public string? LongDescription { get; set; }

    public string? Fit { get; set; }

    public string? Sizes { get; set; }

    public string? Gender { get; set; }

    public string? GarmentType { get; set; }

    public string? FabricContent { get; set; }

    public string? ItemWeight { get; set; }

    public string? InseamLength { get; set; }

    public decimal? UnitPrice { get; set; }

    public string? ProductLine { get; set; }

    public int QtyAvailable { get; set; }

    public string? NextAvailDate { get; set; }

    public bool? ErpActiveFlag { get; set; }

    public bool? B2bActiveFlag { get; set; }

    public string? ProductUrlId { get; set; }
}
