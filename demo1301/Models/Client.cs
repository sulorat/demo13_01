﻿using System;
using System.Collections.Generic;

namespace demo1301.Models;

public partial class Client
{
    public int Id { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string? Patronymic { get; set; }

    public DateOnly? Birthday { get; set; }

    public DateOnly Registrationdate { get; set; }

    public string? Email { get; set; }

    public string Phone { get; set; } = null!;

    public char Gendercode { get; set; }

    public string? Photopath { get; set; }

    public virtual ICollection<Clientservice> Clientservices { get; set; } = new List<Clientservice>();

    public virtual Gender GendercodeNavigation { get; set; } = null!;
}
