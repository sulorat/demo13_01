using System;
using System.Collections.Generic;

namespace demo1301.Models;

public partial class Manufacturer
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateOnly? Startdate { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
