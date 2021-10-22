using System;

namespace EntityFrameworkEdu
{
    // Позволяет абстрагироваться от БД и работать с данными независимо от типа хранилища
    // Если на физическом уровне оперируем таблицами, то на концептуальном - объектами

    // EF предполагает три возможных способа взаимодействия с базой данных:
    // - DB first: EF создает классы, отражающие модель БД
    // - Model first: сначала создается модель БД, по которой сначала создает реальную БД
    // - Code first: сначала создается класс модели данных, которые будут храниться в БД, затем EF генерирует БД и таблицы
    class Program
    {
        static void Main()
        {
            using (UserContext db = new UserContext())
            {
                // Создаем два объекта User
                User user1 = new User { Name = "Tom", Age = 33 };
                User user2 = new User { Name = "Sam", Age = 26 };

                // Добавляем их в БД
                db.Users.Add(user1);
                db.Users.Add(user2);
                db.SaveChanges();
                Console.WriteLine("Объекты успешно сохранены");

                // Получаем объекты из БД и выводим на консоль
                var users = db.Users;
                Console.WriteLine("Список объектов:");
                foreach (User u in users)
                    Console.WriteLine("{0}.{1} - {2}", u.Id, u.Name, u.Age);
            }
            Console.Read();
        }
    }
}
