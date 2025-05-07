using ApiAuthentication.Models;

namespace ApiAuthentication.Repositories;

// Класс эмуляции БД
public class MovieRepository
{
    public static List<Movie> Movies = new()
    {
        new()
        {
            Id = 1,
            Title = "title1",
            Description = "description1",
            Rating = 1.1
        },
        new()
        {
            Id = 2,
            Title = "title2",
            Description = "description2",
            Rating = 2.2
        },
        new()
        {
            Id = 3,
            Title = "title3",
            Description = "description3",
            Rating = 3.3
        },
        new()
        {
            Id = 4,
            Title = "title4",
            Description = "description4",
            Rating = 4.4
        },
        new()
        {
            Id = 5,
            Title = "title5",
            Description = "description5",
            Rating = 5.5
        },
    };
}
