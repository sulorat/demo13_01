﻿using System;
using System.Collections.Generic;

namespace demo1301.Models;

public partial class Tagofclient
{
    public int Clientid { get; set; }

    public int Tagid { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual Tag Tag { get; set; } = null!;
}
