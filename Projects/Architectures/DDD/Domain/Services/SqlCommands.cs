using CommunityToolkit.Diagnostics;
using System.Data;
using System.Data.SqlClient;

namespace Domain.Services;

public static class SqlCommands
{
    private static long OpenedConnections = 0;

    public static SqlCommand CreateCommand(string commandText, SqlParameter[] parameters, CommandType commandType)
    {
        Guard.IsNotNull(commandText);
        Guard.IsNotNull(parameters);

        var sqlCommand = new SqlCommand(commandText)
        {
            CommandType = commandType
        };

        foreach (SqlParameter param in parameters)
        {
            sqlCommand.Parameters.Add(param);
        }

        return sqlCommand;
    }

    public static SqlCommand CreateCommand(SqlConnection conn, string commandText, SqlParameter[]? parameters = default)
    {
        SqlCommand sqlCommand;

        if (parameters == default)
        {
            sqlCommand = new SqlCommand(commandText)
            {
                CommandType = CommandType.Text,
                Connection = conn ?? throw new ArgumentNullException(nameof(conn))
            };
        }
        else
        {
            sqlCommand = CreateCommand(commandText, parameters, CommandType.StoredProcedure);
            sqlCommand.Connection = conn ?? throw new ArgumentNullException(nameof(conn));
        }

        return sqlCommand;
    }

    public static SqlConnection Connect(string connStr)
    {
        SqlConnection? sqlConnection = null;

        try
        {
            sqlConnection = new SqlConnection(connStr);
            sqlConnection.StateChange += new StateChangeEventHandler(ConnStateChange);
            sqlConnection.Open();
            return sqlConnection;
        }
        catch (Exception)
        {
            sqlConnection?.Close();
            throw;
        }
    }

    public static SqlParameter CreateParameter(string name, object val)
    {
        return new SqlParameter(name, val);
    }

    public static void ExecuteNonQuery(SqlCommand sqlCommand)
    {
        Guard.IsNotNull(sqlCommand);

        try
        {
#if DEBUG
            Console.WriteLine(sqlCommand.CommandText);

            foreach (SqlParameter sqlParameter in sqlCommand.Parameters)
            {
                Console.Write("{0} = {1}, ",
                    sqlParameter.ParameterName,
                    (sqlParameter.Value is not null && sqlParameter.Value.ToString()?.Length > 0)
                        ? "'" + sqlParameter.Value + "'"
                        : "NULL");
            }

            Console.WriteLine();
            Console.WriteLine();
#endif
            sqlCommand.ExecuteNonQuery();
        }
        catch
        {
            throw;
        }
    }


    private static void ConnStateChange(object sender, StateChangeEventArgs e)
    {
        if (e.CurrentState == ConnectionState.Open)
        {
            Interlocked.Increment(ref OpenedConnections);
        }
        else if (e.CurrentState == ConnectionState.Closed)
        {
            Interlocked.Decrement(ref OpenedConnections);
        }
    }
}
