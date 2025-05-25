using System;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DAL.Models;

public partial class Gymnast
{
    public string Id { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime BirthDate { get; set; }

    public string MedicalInsurance { get; set; } = null!;

    public string Level { get; set; } = null!;

    public string MemberShipType { get; set; } = null!;

    public string PaymentType { get; set; } = null!;

    public string Email { get; set; } = null;
    public string Cell { get; set; } = null!;
    public int WeeklyCounter { get; set; }
    public DateOnly EntryDate { get; set; }


    public Gymnast()
    { }
}
