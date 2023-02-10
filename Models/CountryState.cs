using System;
using System.Collections.Generic;

namespace API.Models;

public partial class CountryState
{
    public int Id { get; set; }

    public int CountryStateId { get; set; }

    public string Name { get; set; } = null!;

    public string? Abbreviation { get; set; }

    public int Rno { get; set; }

    public int ActiveFlag { get; set; }
}
