using System.Data;
using System.Data.SqlClient;

namespace Domain.Interfaces;

public interface ISqlService
{
    SqlConnection? CreateConnection();

    IUserContextCommand CreateCommand(string commandText, CommandType commandType);
}
