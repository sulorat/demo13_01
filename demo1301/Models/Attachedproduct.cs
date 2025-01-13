using System;
using System.Collections.Generic;

namespace demo1301.Models;

public partial class Attachedproduct
{
    public int Mainproductid { get; set; }

    public int Attachedproductid { get; set; }

    public virtual Product AttachedproductNavigation { get; set; } = null!;

    public virtual Product Mainproduct { get; set; } = null!;
}
