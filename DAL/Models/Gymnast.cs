using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models;

[Table("Gymnast")]
public partial class Gymnast
{
    [Key]
    [StringLength(9)]
    public string Id { get; set; } = null!;

    [StringLength(10)]
    public string FirstName { get; set; } = null!;

    [StringLength(10)]
    public string LastName { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? BirthDate { get; set; }

    [StringLength(50)]
    public string? MedicalInsurance { get; set; }

    [StringLength(1)]
    public string? Level { get; set; }

    [StringLength(50)]
    public string? MemberShipType { get; set; }

    [StringLength(50)]
    public string? PaymentType { get; set; }

    [StringLength(50)]
    public string Email { get; set; } = null!;

    [StringLength(10)]
    public string Cell { get; set; } = null!;

    public int WeeklyCounter { get; set; }

    public DateOnly? EntryDate { get; set; }

    [InverseProperty("Gymnast")]
    public virtual ICollection<GymnastClass> GymnastClasses { get; set; } = new List<GymnastClass>();
}
