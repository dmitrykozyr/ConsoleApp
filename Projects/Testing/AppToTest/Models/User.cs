namespace AppToTest.Models;

public class User
{
    public int Id { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public string? Phone { get; set; }

    public bool VerifiedEmail { get; set; } = false;
}
