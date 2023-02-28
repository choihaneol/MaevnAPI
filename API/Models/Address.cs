using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Address
{
    public int Id { get; set; }

    public int TypeId { get; set; }

    public int UserId { get; set; }

    public string CompanyName { get; set; } = null!;

    public string Address1 { get; set; } = null!;

    public string? Address2 { get; set; }

    public string City { get; set; } = null!;

    public int CountryId { get; set; }

    public int? StateId { get; set; }

    public string? ZipCode { get; set; }

    public bool? DefualtTypeId { get; set; }

    public int ErpCustomerId { get; set; }

    public int ErpAddressId { get; set; }

    public virtual User User { get; set; } = null!;
}
