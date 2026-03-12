using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace UnitOfWork
{
    /*     
        Объединяет несколько операций над данными в одну единицу работы,
        чтобы выполнить несколько изменений (добавление, обновление, удаление) в БД одновременно
        
        Управляет транзакциями, чтобы все изменения применились только при успешном завершении всех операций
        Если одна из операций не удалась, все изменения будут отменены
    */

    public interface IRepository<TEntity>
    {
        public void Add(TEntity entity);
    }

    public class MyDbContext : DbContext
    {
    }

    public class User
    {
        public string? Name { get; init; }
    }

    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> Repository<TEntity>()
            where TEntity : class;

        void Save();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        public IRepository<TEntity> Repository<TEntity>()
            where TEntity : class
        {
            return new Repository<TEntity>(_context);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

    // Создаем экземпляр UnitOfWork
    // Выполняем операции с репозиториями
    // Сохраняем все изменения методом Save
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }
    }

    class Program
    {
        static void Main_()
        {
            using (var unitOfWork = new UnitOfWork(new MyDbContext()))
            {
                var userRepository = unitOfWork.Repository<User>();

                var newUser = new User
                {
                    Name = "John Doe"
                };

                userRepository.Add(newUser);

                unitOfWork.Save();
            }
        }
    }    
}

namespace HttpClientFactory
{
    /*
        Упрощает создание и управление экземплярами HttpClient

        Помогает избежать проблем, связанных с использованием HttpClient
        - истощение сокетов
        - неправильное управление временем жизни экземпляров

        Преимущества HttpClientFactory:
        - HttpClient - дорогостоящий объект для создания, его следует использовать повторно
          HttpClientFactory управляет временем жизни экземпляров, что позволяет избежать проблем с исчерпанием сокетов
        - можно централизованно настраивать параметры HttpClient:
            - базовый адрес
            - заголовки
            - обработчики
        - можно добавлять общие обработчики (например, для аутентификации или логирования) для всех запросов
        - можно создавать несколько настроек HttpClient для различных сценариев использования

        Как использовать HttpClientFactory:
        - установка пакета Microsoft.Extensions.Http
        - настройка Sминимального хостинга в Startup.cs
        - использование IHttpClientFactory для получения экземпляров HttpClient в классах

        Пример:
        - сервис MyService использует IHttpClientFactory для создания экземпляра HttpClient,
          настроенного для работы с API GitHub
        - добавим заголовок User-Agent, чтобы соответствовать требованиям GitHub API
    */

    class Program
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Регистрация HttpClientFactory
            services.AddHttpClient();

            // Регистрация именованного клиента
            services.AddHttpClient("GitHub", client =>
            {
                client.BaseAddress = new Uri("https://api.github.com/");
                client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("MyApp", "1.0"));
            });
        }

        public class MyService
        {
            private readonly IHttpClientFactory _httpClientFactory;

            public MyService(IHttpClientFactory httpClientFactory)
            {
                _httpClientFactory = httpClientFactory;
            }

            public async Task<string> GetGitHubDataAsync()
            {
                var client = _httpClientFactory.CreateClient("GitHub");
                var response = await client.GetAsync("repos/dotnet/aspnetcore");

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
