namespace Education.General.Db;

public class SQLInjection
{
    /*
        Код имеет уязвимость к SQL-инъекциям, потому что значение dealId
        вставляется непосредственно в строку SQL-запроса без какой-либо обработки или параметризации

        Для защиты от SQL-инъекций следует использовать параметризованные запросы


        private async Task<Client> F1_Error(string dealId)
        {
            using var connection = _connectionFactory.Create();

            // Неправильно
            //var selectCommandText = @"SELECT c.FirstName, c.SecondName, c.*
            //                          FROM clients as c   
            //                          INNER JOIN client_deal as cd on cd.ClientId = c.Id   
            //                          WHERE cd.DealId = @DealId";

            var selectCommandText = $@"SELECT c.FirstName, c.SecondName, c.*
                                       FROM clients as c   
                                       INNER JOIN client_deal as cd on cd.ClientId = c.Id   
                                       WHERE cd.DealId = {dealId}";

            var selectSqlCommand = new SqlCommand(selectCommandText, connection);

            // Неправильно
            //var result = await connection.ExecuteAsync(readQuery);

            // Правильно
            selectSqlCommand.Parameters.AddWithValue("@DealId", dealId);
            var result = await connection.ExecuteAsync(selectSqlCommand);

            return result;
        }
    */
}
