using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class GymnastClass

{
    public int Id { get; set; }

    public string GymnastId { get; set; } = null!;

    public int ClassId { get; set; }

    public virtual StudioClass Class { get; set; } = null!;

    public virtual Gymnast Gymnast { get; set; } = null!;
}
