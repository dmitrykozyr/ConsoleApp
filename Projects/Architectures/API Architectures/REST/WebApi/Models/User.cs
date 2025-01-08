using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models;

// EF при работе с Code First требует определения ключа для создания первичного ключа в таблице в БД
// По умолчанию EF в качестве первичных ключей рассматривает св-ва с именами Id или [Имя_класса]Id

// Атрибуты
// [Key]                    первичный ключ
// [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] установить ключ в качестве идентификатора
// [Required]               обязательное св-во, в БД будет помечено как NOT NULL
// [MinLength(10)]
// [MaxLength(20)]
// [NotMapped]              не добавлять столбец в БД
// [Table("Mobiles")]       если имя модели и таблицы разные, можем явно указать, с какой таблицей сопоставлять модель
// [Column("ModelName")]    аналогично для свойства
// [ForeignKey("CompId")]   установить внешний ключ для связи с другой сущностью
// [Index]                  установить индекс для столбца
// [ConcurrencyCheck]       для св-ва решаем проблему параллелизма, когда с одной же записью могут работать одновременно несколько пользователей

#region Один к одному
public class User
{
    public int Id { get; set; }
    // Атрибуты валидации свойств
    // [Required]
    // [Required(ErrorMessage = "Не указан электронный адрес")]
    // [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
    // [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
    // [Range(1, 110, ErrorMessage = "Недопустимый возраст")]
    // [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    public string Name { get; set; }
    public int Age { get; set; }
    public decimal MoneyAmount { get; set; }
    public UserProfile UserProfile { get; set; }
}

public class UserProfile
{
    [Key]
    [ForeignKey("User")]
    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public User User { get; set; }
}
#endregion

#region Один ко многим
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
#endregion

#region Многие ко многим
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
#endregion
