using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models;

public partial class StudioClass
{
    public int GlobalId { get; set; }

    [Key]
    public int Id { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Date { get; set; }

    [StringLength(1)]
    public string Level { get; set; } = null!;

    public int CurrentNum { get; set; }
    public bool IsCancelled { get; set; } = false;


    [ForeignKey("GlobalId")]
    [InverseProperty("StudioClasses")]
    public virtual GlobalStudioClass Global { get; set; } = null!;

    [InverseProperty("Class")]
    public virtual ICollection<GymnastClass> GymnastClasses { get; set; } = new List<GymnastClass>();
}
