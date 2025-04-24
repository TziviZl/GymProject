using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class StudioClass
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Level { get; set; } = null!;

    public int Price { get; set; }

    public DateTime Date { get; set; }

    public int ParticipantsNumber { get; set; }

    public string TrainerId { get; set; } = null!;

    public virtual Trainer Trainer { get; set; } = null!;
}
