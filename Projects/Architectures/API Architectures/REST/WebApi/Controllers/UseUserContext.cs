using WebApi.ApplicationDbContext;
using WebApi.Models;

namespace WebApi.Controllers;

public class UseUserContext
{
    static void AddUser()
    {
        using (UserContext db = new UserContext())
        {
            var user1 = new User { Name = "Tom", Age = 33 };
            var user2 = new User { Name = "Sam", Age = 26 };

            db.Users.Add(user1);
            db.Users.Add(user2);

            db.SaveChanges();

            var users = db.Users;

            foreach (User u in users)
            {
                Console.WriteLine("{0}.{1} - {2}", u.Id, u.Name, u.Age);
            }
        }
    }
}
