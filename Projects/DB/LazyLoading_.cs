namespace Education.General.Db;

public class LazyLoading_
{
    // Используется при работе с большими данными
    // Данные загружаются только по мере необходимости
    // Если объект не используется - он не будет загружен в память

    // Если данные могут измениться, стандартный Lazy<T> не подходит,
    // т.к. он спроектирован для однократной инициализации
    // Как только значение попало в Value, оно там и остается
    public class DataLoader
    {
        private Lazy<string> _lazyData;

        public string LazyData
        {
            get { return _lazyData.Value; }
        }

        public DataLoader()
        {
            _lazyData = new Lazy<string>(LoadData);
        }

        private string LoadData()
        {
            Thread.Sleep(5000);

            return "Данные загружены";
        }

        public void Main_()
        {
            // Данные не загрузятся до первого обращения к свойствуву Data
            var dataLoader = new DataLoader();

            // Метод LoadData выполнится сейчас - в момент обращения к свойству dataLoader.Data
            // Результат сохраняется после первого вызова, все последующие обращения к свойству вернут строку мгновенно
            Console.WriteLine($"{DateTime.Now} {dataLoader.LazyData}, первый запуск");

            // Повторный доступ к данным не вызывает повторной загрузки
            Console.WriteLine($"{DateTime.Now} {dataLoader.LazyData}, второй запуск");
        }
    }
}
