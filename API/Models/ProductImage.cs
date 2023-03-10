using System;
using System.Collections.Generic;

namespace API.Models;

public partial class ProductImage
{
    public int Id { get; set; }

    public int ProductCategoryId { get; set; }

    public int TypeId { get; set; }

    public string StyleNumber { get; set; } = null!;

    public string ColorCode { get; set; } = null!;

    public string? ColorName { get; set; }

    public string? ImageUrl { get; set; }

    public bool? ActiveFlag { get; set; }
}
