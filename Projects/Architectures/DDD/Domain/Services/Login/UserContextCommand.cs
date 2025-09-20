using CommunityToolkit.Diagnostics;
using Domain.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace Domain.Services.Login;

public class UserContextCommand : IDisposable
{
    private byte[]? cookie;
    private readonly bool internalConnection = false;
    private readonly SqlCommand command;
    private readonly SqlConnection? connection;
    private readonly SqlTransaction? transaction;
    private readonly ISqlService _sqlService;

    internal UserContextCommand(string commandText, CommandType commandType, int timeout, ISqlService sqlService)
    {
        internalConnection = true;
        _sqlService = sqlService;

        Guard.IsNotNull(_sqlService);
        connection = _sqlService.CreateConnection();

        Guard.IsNotNull(connection);
        command = connection.CreateCommand();

        command.CommandText = commandText;
        command.CommandType = commandType;
        command.CommandTimeout = timeout;
    }

    public SqlDataReader ExecuteReader(string login)
    {
        SqlDataReader reader;

        try
        {
            SwitchSqlExecuteContext(login);
            reader = command.ExecuteReader(CommandBehavior.Default);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return reader;
    }

    private void SwitchSqlExecuteContext(string login)
    {
        if (cookie is null)
        {
            Guard.IsNotNull(connection);

            using (SqlCommand sqlCommand = connection.CreateCommand())
            {
                sqlCommand.CommandText =
                    string.Format(
                        @"DECLARE @cookie VARBINARY(100); EXECUTE AS LOGIN = '{0}' WITH COOKIE INTO @cookie; SELECT @cookie;",
                        login);

                sqlCommand.CommandType = CommandType.Text;

                if (transaction is not null)
                {
                    sqlCommand.Transaction = transaction;
                }

                cookie = (byte[])sqlCommand.ExecuteScalar();
            }
        }
    }

    public void Dispose()
    {
        Guard.IsNotNull(connection);

        // Revert SQL execute context
        if (cookie is not null)
        {

            using (SqlCommand sqlCommand = connection.CreateCommand())
            {
                sqlCommand.CommandText =
                    string.Format(
                        "DECLARE @cookie VARBINARY(100); SET @cookie = 0x{0}; REVERT WITH COOKIE = @cookie;",
                        BitConverter.ToString(cookie).Replace("-", ""));

                sqlCommand.CommandType = CommandType.Text;

                if (transaction is not null)
                {
                    sqlCommand.Transaction = transaction;
                }

                sqlCommand.ExecuteNonQuery();
            }

            cookie = null;
        }

        if (internalConnection)
        {
            connection.Close();
        }
    }
}
