using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models;

#region Один к одному

    public class User
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public int Age { get; set; }

        public decimal MoneyAmount { get; set; }

        public UserProfile? UserProfile { get; set; }
    }

    public class UserProfile
    {
        [Key]
        [ForeignKey("User")]
        public int Id { get; set; }

        public string? Login { get; set; }

        public string? Password { get; set; }

        public User? User { get; set; }
    }

#endregion

#region Один ко многим

    public class Player
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public int TeamId { get; set; }

        public Team? Team { get; set; }
    }

    public class Team
    {
        public int Id { get; set; }

        public string TeamName { get; set; }

        public ICollection<Player> Players { get; set; }

        public Team()
        {
            Players = new List<Player>();
        }
    }

#endregion

#region Многие ко многим

    public class Team_
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Player_> Players { get; set; }

        public Team_()
        {
            Players = new List<Player_>();
        }
    }

    public class Player_
    {
        public int Id { get; set; }

        public string TeamName { get; set; }

        public ICollection<Team_> Teams { get; set; }

        public Player_()
        {
            Teams = new List<Team_>();
        }
    }

#endregion
