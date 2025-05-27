public class Gymnast
{
    public string Id { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime? BirthDate { get; set; } 
    public string? MedicalInsurance { get; set; }
    public string? Level { get; set; } 
    public string? MemberShipType { get; set; } 
    public string? PaymentType { get; set; } 
    public string Email { get; set; } = null!;
    public string Cell { get; set; } = null!;
    public int WeeklyCounter { get; set; } = 2;
    public DateOnly? EntryDate { get; set; } 

    public Gymnast() { }
}