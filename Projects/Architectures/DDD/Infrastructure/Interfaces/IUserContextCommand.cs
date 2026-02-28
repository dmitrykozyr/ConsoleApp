using System.Data;

namespace Infrastructure.Interfaces;

public interface IUserContextCommand : IDisposable
{
    IDataReader ExecuteReader(string login);
}
