namespace Education.Patterns.Behavioral.Memento;

public class Man
{
    public string? PhoneNumber { get; set; }

    public DataToRemember Save() => new DataToRemember(PhoneNumber);

    public void Restore(DataToRemember dataToRemember) => PhoneNumber = dataToRemember.PhoneNumber;
}
