using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models;

public partial class BackupTrainer
{
    [Key]
    [StringLength(9)]
    public string Id { get; set; } = null!;

    [StringLength(10)]
    public string FirstName { get; set; } = null!;

    [StringLength(10)]
    public string LastName { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime BirthDate { get; set; }

    [StringLength(50)]
    public string Specialization { get; set; } = null!;

    [StringLength(50)]
    public string? Email { get; set; }

    [StringLength(10)]
    public string Cell { get; set; } = null!;
}
