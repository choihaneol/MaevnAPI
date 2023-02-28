using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Carrier
{
    public int Id { get; set; }

    public int CarrierId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Code { get; set; }

    public int Rno { get; set; }

    public int ActiveFlag { get; set; }
}
