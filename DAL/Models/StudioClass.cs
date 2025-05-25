using System;

namespace DAL.Models;

public partial class StudioClass
{
    public int GlobalId { get; set; }

    public int Id { get; set; } 

    public DateTime Date { get; set; }

    public string Level { get; set; } = null!; 

    public int CurrentNum { get; set; } = 20;

    public virtual GlobalStudioClasses GlobalStudioClass { get; set; } = null!; 
}