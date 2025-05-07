using System.Data.Entity;
using WebApi.Models;

namespace WebApi.ApplicationDbContext;

public class UserContext : DbContext
{
    //!
    // DbConnection - имя строки подключения к БД
    // Если БД с именем DbConnection нет - она будет создана
    public UserContext() : base("DbConnection") { }

    public DbSet<User> Users { get; set; }

    public DbSet<UserProfile> UserProfiles { get; set; }

    // При Code First модели сопоставляются с таблицами,
    // но иногда необходимо изменить правила сопоставления через Fluent API и аннотации данных
    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        // По умолчанию EF сопоставляет модель с одноименной таблицей, но можно переопределить это в методе ToTable()
        // Таблицы называем по имени моделей во множественном числе
        modelBuilder.Entity<User>().ToTable("Users");

        // Если по сущности не надо создавать таблицу - можем ее проигнорировать
        modelBuilder.Ignore<User>();

        // Первичный ключ должен представлять св-во модели с именем Id или [Имя_класса]Id
        // Переопределить можно через HasKey(), а внешний через .HasForeignKey
        modelBuilder.Entity<User>().HasKey(p => p.Name);

        // Чтобы настроить составной первичный ключ, указываем два свойства
        modelBuilder.Entity<User>().HasKey(p => new { p.Age, p.Name });

        // Если не хотим, чтобы с каким-то свойством шло сопоставление
        modelBuilder.Entity<User>().Ignore(p => p.Age);

        modelBuilder.Entity<User>().Property(p => p.Name)
                                        .IsRequired()               // Поле обязательное
                                                                    //.IsOptional()             // Необязательное
                                        .HasColumnType("varchar")   // EF сам определяет тип, но можем его явно указать
                                        .HasColumnName("PhoneName") // Сопоставдяем св-во с определенным столбцом
                                        .HasMaxLength(50);          // Длина строки

        // Настройка точности для decimal, по умолчанию 18, 2
        modelBuilder.Entity<User>().Property(p => p.MoneyAmount).HasPrecision(15, 2);

        // Можем поместить св-ва модели в одну таблицу, а другие св-ва связать со столбцами из другой
        modelBuilder.Entity<User>().Map(m =>
        {
            m.Properties(p => new { p.Age, p.Name });
            m.ToTable("Mobiles");
        }).Map(m =>
        {
            m.Properties(p => new { p.Age, p.MoneyAmount, p.Name });
            m.ToTable("MobilesInfo");
        });

        // ============================== СВЯЗИ ==============================

        // Один к одному
        // WithRequiredPrincipal настраивает обязательную связь и делает сущность User основной
        // Таблица, на которую отображается модель UserProfile, будет содержать внешний ключ к таблице User
        modelBuilder.Entity<User>()
            .HasRequired(c => c.UserProfile)
            .WithRequiredPrincipal(c => c.User);

        // Один ко многим
        modelBuilder.Entity<Team>()
            .HasMany(p => p.Players)
            .WithRequired(p => p.Team);

        // Многие ко многим
        modelBuilder.Entity<Team_>()
            .HasMany(p => p.Players)
            .WithMany(c => c.Teams);

        base.OnModelCreating(modelBuilder);
    }
}
