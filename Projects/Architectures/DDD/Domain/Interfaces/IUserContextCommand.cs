using System.Data;

namespace Domain.Interfaces;

public interface IUserContextCommand : IDisposable
{
    IDataReader ExecuteReader(string login);
}
