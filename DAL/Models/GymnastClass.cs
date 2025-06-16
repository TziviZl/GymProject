using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models;

public partial class GymnastClass
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(9)]
    public string GymnastId { get; set; } = null!;
    [Required]
    public int ClassId { get; set; }

    [ForeignKey(nameof(GymnastId))]
    public virtual Gymnast Gymnast { get; set; } = null!;

    [ForeignKey(nameof(ClassId))]
    public virtual StudioClass Class { get; set; } = null!;
}
