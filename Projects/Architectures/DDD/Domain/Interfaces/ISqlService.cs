namespace Domain.Interfaces;

public interface ISqlService
{
    SqlDataReader ExecuteReader(string login);
}
