namespace Education.Patterns.Behavioral.Memento;

public class DataToRemember
{
    public string? PhoneNumber { get; private init; }

    public DataToRemember(string? phoneNumber)
    {
        PhoneNumber = phoneNumber;
    }
}
