using DAL.Api;
using DAL.Models;

public class UserTypeDal : IUserTypeDal
{
    private readonly DB_Manager _db;

    public UserTypeDal(DB_Manager db)
    {
        _db = db;
    }

    public string? GetUserTypeById(string id)
    {
        if (_db.Secretaries.Any(s => s.Id == id))
            return "Secretary";

        if (_db.Trainers.Any(t => t.Id == id))
            return "Trainer";

        if (_db.Gymnasts.Any(g => g.Id == id))
            return "Gymnast";

        return null;
    }
}
