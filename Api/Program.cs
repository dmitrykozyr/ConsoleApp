using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Api
{
    /*
        Entity Framework позволяет абстрагироваться от БД и работать с данными независимо от типа хранилища
        Если на физическом уровне оперируем таблицами, то на концептуальном - объектами
    
        EF предполагает три возможных способа взаимодействия с базой данных:
        - DB first: EF создает классы, отражающие модель БД
        - Model first: сначала создается модель БД, по которой сначала создает реальную БД
        - Code first: сначала создается класс модели данных, которые будут храниться в БД, затем EF генерирует БД и таблицы

        В Entity Framework есть три способа загрузки данных:
        - eager loading - жадная загрузка
          Подгружает связанные по внешнему ключу данные через метод Include
          var players = db.Players.Include(p => p.Team).ToList()

        - explicit loading - явная загрузка
          Для загрузки данных в контекст применяем метод Load()
          var p = db.Players.FirstOrDefault()
          db.Entry(p).Reference("Team").Load()

        - lazy loading - ленивая загрузка
          Данные подгружаются при первом обращении к навигационному св-ву, а до этого не подргужаются
          Классы, использующие ленивую загрузку, должны быть публичными, а свойства иметь модификаторы public и virtual
          В этом случае не потребуется использовать Include и Load
     */
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
