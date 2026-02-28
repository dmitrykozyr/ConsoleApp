using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.Interfaces;

public interface ISqlService
{
    SqlConnection? CreateConnection();

    IUserContextCommand CreateCommand(string commandText, CommandType commandType);
}
