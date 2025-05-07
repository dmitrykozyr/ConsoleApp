namespace xUnit_.FunctionalityToTest.User_;

public record User(string firstName, string lastName)
{
    public int Id { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public string? Phone { get; set; }

    public bool VerifiedEmail { get; set; } = false;
}
