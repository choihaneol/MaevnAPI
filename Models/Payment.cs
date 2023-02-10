using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int UserId { get; set; }

    public int ErpCustomerId { get; set; }

    public int CardId { get; set; }

    public string? AccountNumber { get; set; }

    public int? CardNumber { get; set; }

    public string? CardHolderName { get; set; }

    public int? InvoiceId { get; set; }

    public string? ErpCustomerPo { get; set; }

    public decimal? BalanceToUse { get; set; }

    public decimal? PaymentAmount { get; set; }
}
