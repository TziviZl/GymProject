using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models;

public partial class GlobalStudioClass
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    public int Price { get; set; }

    public int MaxParticipantsNumber { get; set; }

    [StringLength(9)]
    public string TrainerId { get; set; } = null!;

    [InverseProperty("Global")]
    public virtual ICollection<StudioClass> StudioClasses { get; set; } = new List<StudioClass>();

    [ForeignKey("TrainerId")]
    [InverseProperty("GlobalStudioClasses")]
    public virtual Trainer Trainer { get; set; } = null!;
}
