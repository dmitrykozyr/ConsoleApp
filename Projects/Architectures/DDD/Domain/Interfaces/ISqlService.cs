using Domain.DomainServices.Login;
using System.Data;
using System.Data.SqlClient;

namespace Domain.Interfaces;

public interface ISqlService
{
    SqlConnection? CreateConnection();

    UserContextCommand CreateCommand(string commandText, CommandType commandType);
}
