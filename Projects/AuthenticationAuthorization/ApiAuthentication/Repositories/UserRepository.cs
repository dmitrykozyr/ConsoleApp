using ApiAuthentication.Models;

namespace ApiAuthentication.Repositories;

// Класс эмуляции БД
public class UserRepository
{
    public static List<User> Users = new()
    {
        new() 
        { 
            UserName = "name1", 
            EmailAddress = "email1@email.com", 
            Password = "password1",
            GivenName = "gname1",
            Surname = "sname1",
            Role = "Administrator"
        },
        new()
        {
            UserName = "name2",
            EmailAddress = "email2@email.com",
            Password = "password2",
            GivenName = "gname2",
            Surname = "sname2",
            Role = "Standard"
        },
    };
}
