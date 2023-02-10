using System;
using System.Collections.Generic;

namespace API.Models;

public partial class OrderHeader
{
    public int OrderId { get; set; }

    public int UserId { get; set; }

    public int? BillToAddressId { get; set; }

    public int? ShipToAddressId { get; set; }

    public int? CarrierId { get; set; }

    public string? ShipToName { get; set; }

    public string? ShipToAddress1 { get; set; }

    public string? ShipToAddress2 { get; set; }

    public string? ShipToCity { get; set; }

    public string? ShipToState { get; set; }

    public string? ShipToCountryId { get; set; }

    public string? ShipToZipcode { get; set; }

    public string? UseDropShip { get; set; }

    public string? Note { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public DateTime? RequestedShipDate { get; set; }

    public string? OrderStatus { get; set; }

    public DateTime? CreatedDate { get; set; }

    public bool? FinishedFlag { get; set; }

    public int ErpCustomerId { get; set; }

    public string? ErpCustomerPo { get; set; }

    public virtual ICollection<OrderHeaderLine> OrderHeaderLines { get; } = new List<OrderHeaderLine>();
}
