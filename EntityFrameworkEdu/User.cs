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
}
