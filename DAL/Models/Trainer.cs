﻿using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Trainer
{
    public string Id { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime BirthDate { get; set; }

    public virtual ICollection<StudioClass> StudioClasses { get; set; } = new List<StudioClass>();

    public string Specialization { get; set; } = null!;

    public string? Email { get; set; } = null;
    public string? Cell { get; set; } = null!;


}
