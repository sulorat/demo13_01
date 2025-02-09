﻿using System;
using System.Collections.Generic;

namespace demo1301.Models;

public partial class Service
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public decimal Cost { get; set; }

    public int Durationinseconds { get; set; }

    public string? Description { get; set; }

    public float? Discount { get; set; }

    public string? Mainimagepath { get; set; }

    public virtual ICollection<Clientservice> Clientservices { get; set; } = new List<Clientservice>();

    public virtual ICollection<Servicephoto> Servicephotos { get; set; } = new List<Servicephoto>();
}
