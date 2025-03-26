using System;
using System.Collections.Generic;

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

    public string StudioClasses { get; set; } = null!;

    public string PaymentType { get; set; } = null!;
}
