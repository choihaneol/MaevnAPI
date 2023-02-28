using System;
using System.Collections.Generic;

namespace API.Models;

public partial class OrderHeaderLine
{
    public int OrderLineId { get; set; }

    public int OrderId { get; set; }

    public int UserId { get; set; }

    public int ProductId { get; set; }

    public int ProgramId { get; set; }

    public decimal WholeSalePrice { get; set; }

    public string StyleNumber { get; set; } = null!;

    public string ColorName { get; set; } = null!;

    public string Size { get; set; } = null!;

    public int Quantity { get; set; }

    public int Scannedquantity { get; set; }

    public decimal TotalScannedPrice { get; set; }

    public decimal TotalPrice { get; set; }
}
