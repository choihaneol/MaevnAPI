using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Locator
{
    public int LocatorId { get; set; }

    public string? CountryName { get; set; }

    public string? LoginId { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Address1 { get; set; }

    public string? Address2 { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }

    public string? ZipCode { get; set; }
}
