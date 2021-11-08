using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkEdu
{
    // EF при работе с Code First требует определения ключа элемента для создания первичного ключа в таблице в БД
    // По умолчанию при генерации БД EF в качестве первичных ключей рассматривает свойства с именами Id или [Имя_класса]Id
    // Если хотим назвать ключевое свойство иначе - нужна дополнительная логика
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }

    // Связь один к одному
    // Есть класс User и UserProfile, где хранятся дополнительные данные об юзере
    public class UserProfile
    {
        [Key]
        [ForeignKey("User")]
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public User User { get; set; }
    }

    // Один ко многим
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? TeamId { get; set; }
        public Team Team { get; set; }
    }
    public class Team
    {
        public int Id { get; set; }
        public string TeamName { get; set; }
        public ICollection<Player> Players { get; set; }
        public Team() { Players = new List<Player>(); }
    }

    // Многие ко многим
    public class Team_
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Player_> Players { get; set; }
        public Team_() { Players = new List<Player_>(); }
    }
    public class Player_
    {
        public int Id { get; set; }
        public string TeamName { get; set; }
        public ICollection<Team_> Teams { get; set; }
        public Player_() { Teams = new List<Team_>(); }
    }
}
