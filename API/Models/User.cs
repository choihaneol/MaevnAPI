using System;
using System.Collections.Generic;

namespace API.Models;

public partial class User 
{
    public int UserId { get; set; }

    public string LoginId { get; set; } = null!;

    public string? Password { get; set; }

    public int ErpCustomerId { get; set; }

    public string AccountNumber { get; set; } = null!;

    public string CompanyName { get; set; } = null!;

    public string? ContactEmail { get; set; }

    public string? ContactPhone { get; set; }

    public string PaymentTerm { get; set; } = null!;

    public decimal Credit { get; set; }

    public decimal TotalCreditBalance { get; set; }

    public decimal TotalOpenBalance { get; set; }

    public bool IsAdmin { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<Address> Addresses { get; } = new List<Address>();

    public virtual ICollection<CardInformation> CardInformations { get; } = new List<CardInformation>();
}
