using Dapper;
using System.Data.SqlClient;

namespace Education.General.Db;

public class SQLInjection
{
    public interface IConnectionFactory
    {
        SqlConnection Create();
    }

    // Код имеет уязвимость к SQL-инъекциям, потому что значение dealId
    // вставляется непосредственно в строку SQL-запроса без какой-либо обработки или параметризации
    // Для защиты от SQL-инъекций следует использовать параметризованные запросы
    public class SqlInjectionExamples
    {
        private readonly IConnectionFactory _connectionFactory;

        public async Task<IEnumerable<dynamic>> F1_Error(string dealId)
        {
            using var connection = _connectionFactory.Create();

            // Код уязвим к SQL-инъекции
            // Злоумышленник может передать в dealId строку:
            // "1; DROP TABLE clients; --"
            string sql =
                $@"SELECT c.FirstName, c.SecondName
                FROM clients as c
                INNER JOIN client_deal as cd on cd.ClientId = c.Id
                WHERE cd.DealId = '{dealId}'";

            //! Установить Dapper
            return await connection.QueryAsync(sql);
        }

        // Правильный пример с Dapper
        public async Task<IEnumerable<dynamic>> F1_Correct_Dapper(string dealId)
        {
            using var connection = _connectionFactory.Create();

            // Безопасно - значение передается отдельно от команды
            var sql =
                @"SELECT c.FirstName, c.SecondName
                FROM clients as c
                INNER JOIN client_deal as cd on cd.ClientId = c.Id
                WHERE cd.DealId = @DealId";

            // Dapper превратит DealId в безопасный параметр
            return await connection.QueryAsync(sql, new { DealId = dealId });
        }

        // Правильный пример с SqlCommand
        public async Task F1_Correct_SqlCommand(string dealId)
        {
            using var connection = (SqlConnection)_connectionFactory.Create();

            await connection.OpenAsync();

            var sql = "SELECT FirstName FROM clients WHERE Id = @Id";

            using var command = new SqlCommand(sql, connection);

            // Явно добавляем параметр - это защищает от инъекций
            command.Parameters.AddWithValue("@Id", dealId);

            // Чтение данных
            using var reader = await command.ExecuteReaderAsync();
        }
    }
}
