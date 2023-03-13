using System;
using System.Collections.Generic;

namespace API.Models;

public partial class ProductCategory
{
    public int Id { get; set; }

    public int ErpProgramId { get; set; }

    public string? ProductLine { get; set; }

    public string? StyleNumber { get; set; }

    public string DefaultColorCode { get; set; } = null!;

    public string Colors { get; set; } = null!;

    public string? ShortDescription { get; set; }

    public string? LongDescription { get; set; }

    public string? Fits { get; set; }

    public string? Gender { get; set; }

    public string? GarmentType { get; set; }

    public string? FabricContent { get; set; }

    public string? ItemWeight { get; set; }

    public string? InseamLengths { get; set; }

    public string? Sizes { get; set; }

    public decimal? PriceMin { get; set; }

    public decimal? PriceMax { get; set; }

    public bool? B2bActiveFlag { get; set; }

    public int IsPreorder { get; set; }

    public int IsNew { get; set; }

    public int DiscountRate { get; set; }

    public string ImageLinks { get; set; } = null!;
}
