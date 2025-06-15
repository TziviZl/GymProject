using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models;

public partial class GymnastClass
{
    [Key]
    public int Id { get; set; }

    [StringLength(9)]
    public string GymnastId { get; set; } = null!;

    public int ClassId { get; set; }

    [ForeignKey("ClassId")]
    [InverseProperty("GymnastClasses")]
    public virtual StudioClass Class { get; set; } = null!;

    [ForeignKey("GymnastId")]
    [InverseProperty("GymnastClasses")]
    public virtual Gymnast Gymnast { get; set; } = null!;
}
