﻿using System;
using System.Collections.Generic;

namespace demo1301.Models;

public partial class Gender
{
    public char Code { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();
}
