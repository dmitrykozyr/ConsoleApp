using System.Data.Entity;

namespace EntityFrameworkEdu
{
    class UserContext : DbContext
    {
        // DbConnection - имя строки подключения к БД
        // Если БД с именем DbConnection нет, она будет создана
        public UserContext() : base("DbConnection") { }

        public DbSet<User> Users { get; set; }
    }
}
