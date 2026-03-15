using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Education.General.Db;

public class TablesRelations
{
    #region Один к одному

        public class User
        {
            public int Id { get; init; }

            public string? Name { get; init; }

            public int Age { get; init; }

            public decimal MoneyAmount { get; init; }

            // Для Lazy Loading пометить это навигационное св-во как public virtual
            public UserProfile? UserProfile { get; init; }
        }

        public class UserProfile
        {
            [Key]
            [ForeignKey("User")]
            public int Id { get; init; }

            public string? Login { get; init; }

            public string? Password { get; init; }

            public User? User { get; init; }
        }

    #endregion

    #region Один ко многим

        public class Player
        {
            public int Id { get; init; }

            public string? Name { get; init; }

            // Если написать int?, объект Player сможет существовать самостоятельно
            public int TeamId { get; init; }

            // Для Lazy Loading пометить это навигационное св-во как public virtual
            public Team? Team { get; init; }
        }

        public class Team
        {
            public int Id { get; init; }

            public string TeamName { get; init; }

            public ICollection<Player> Players { get; init; }

            public Team()
            {
                // new List защищает от NullReferenceException при обращении к коллекции
                Players = new List<Player>();
            }
        }

    #endregion

    #region Многие ко многим

        // В EF Core данный код создаст скрытую таблицу для составных ключей автоматически

        public class Team_
        {
            public int Id { get; init; }

            public string Name { get; init; }

            // Для Lazy Loading пометить это навигационное св-во как public virtual
            public ICollection<Player_> Players { get; init; }

            public Team_()
            {
                Players = new List<Player_>();
            }
        }

        public class Player_
        {
            public int Id { get; init; }

            public string Name { get; init; }

            public ICollection<Team_> Teams { get; init; }

            public Player_()
            {
                // new List защищает от NullReferenceException при обращении к коллекции
                Teams = new List<Team_>();
            }
        }

    #endregion
}
