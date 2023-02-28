using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Role
{
    public int UserId { get; set; }

    public bool? RolePlaceOrder { get; set; }

    public bool? RolePayment { get; set; }

    public bool? RoleInvoice { get; set; }

    public bool? RoleStatement { get; set; }

    public bool? RoleCancelation { get; set; }

    public bool? RoleMapPrice { get; set; }

    public bool? RoleProductMaster { get; set; }
}
