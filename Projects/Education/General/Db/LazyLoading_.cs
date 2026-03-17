namespace Education.General.Db;

public class LazyLoading_
{
    /*
        Используется для оптимизации производительности при работе с большими данными
        Объекты и данные загружаются только по мере необходимости, что снижает время загрузки
        Если объект не используется, он не будет загружен в память
        Можно избежать ненужной инициализации объектов, что упрощает управление зависимостями
    */

    public class DataLoader
    {
        // Используем Lazy<string> для хранения данных
        private Lazy<string> _data;

        public string Data => _data.Value;

        public DataLoader()
        {
            // Объект Lazy<string> инициализируется функцией, загружающей данные
            _data = new Lazy<string>(() => LoadData());
        }

        private string LoadData()
        {
            Console.WriteLine("Загрузка данных...");

            // Симуляция долгой операции чтения из БД
            Thread.Sleep(2000);

            return "Данные загружены";
        }

        static void Main_()
        {
            // Создаем экземпляр DataLoader, но данные еще не загружаются до первого обращения к св-ву Data
            var dataLoader = new DataLoader();

            Console.WriteLine("Объект DataLoader создан.");

            Console.WriteLine("Данные еще не загружены.");

            // При первом обращении к св - ву Data вызывается _data.Value, что приводит к выполнению функции загрузки данных
            // Если данные уже были загружены, св-во возвращает уже загруженные данные без повторной загрузки
            Console.WriteLine(dataLoader.Data);

            // Повторный доступ к данным не вызывает повторной загрузки
            Console.WriteLine(dataLoader.Data);
        }
    }
}
