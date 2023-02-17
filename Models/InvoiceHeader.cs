using System;
using System.Collections.Generic;

namespace API.Models;

public partial class InvoiceHeader
{
    public int Id { get; set; }

    public string LoginId { get; set; } = null!;

    public string Num { get; set; } = null!;

    public DateTime InvoiceDate { get; set; }

    public string Sonum { get; set; } = null!;

    public string CustomerPo { get; set; } = null!;

    public string AccountNum { get; set; } = null!;

    public string CustomerName { get; set; } = null!;

    public string PaymentTerms { get; set; } = null!;

    public DateTime DueDate { get; set; }

    public string ShipVia { get; set; } = null!;

    public string BillTransportationTo { get; set; } = null!;

    public string? TrackingNumber { get; set; }

    public DateTime ShipDate { get; set; }

    public string ShipNum { get; set; } = null!;

    public string SalesPerson { get; set; } = null!;

    public string IssuedBy { get; set; } = null!;

    public string PickedBy { get; set; } = null!;

    public string ScannedBy { get; set; } = null!;

    public DateTime IssuedDate { get; set; }

    public string BillToName { get; set; } = null!;

    public string BillToCity { get; set; } = null!;

    public string BillToCountry { get; set; } = null!;

    public string? BillToCountryAbb { get; set; }

    public string? BillToState { get; set; }

    public string? BillToStateAbb { get; set; }

    public string BillToAddress { get; set; } = null!;

    public string? BillToZip { get; set; }

    public string ShipToName { get; set; } = null!;

    public string ShipToCity { get; set; } = null!;

    public string ShipToCountry { get; set; } = null!;

    public string? ShipToCountryAbb { get; set; }

    public string? ShipToState { get; set; }

    public string? ShipToStateAbb { get; set; }

    public string ShipToAddress { get; set; } = null!;

    public string? ShipToZip { get; set; }

    public string? Note { get; set; }

    public decimal? SubTotalPrice { get; set; }

    public decimal? ShipHandlingCost { get; set; }

    public decimal? DropShipCost { get; set; }

    public decimal? WireTransCost { get; set; }

    public decimal? AdjustCost { get; set; }

    public decimal? TotalAmount { get; set; }

    public decimal? CreditAmount { get; set; }

    public decimal? BalanceDue { get; set; }

    public int? StatusId { get; set; }
}
