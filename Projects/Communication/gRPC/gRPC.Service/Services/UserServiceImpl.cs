using Grpc.Core;

namespace gRPC.Service.Services;

// UserService - имя из файла user.proto
public class UserServiceImpl : UserService.UserServiceBase
{
    private static readonly Dictionary<int, User> Users = new();

    private static int _nextId = 1;

    // Метод из user.proto
    public override Task<User> GetUser(GetUserRequest request, ServerCallContext context)
    {
        if (Users.TryGetValue(request.Id, out var user))
        {
            return Task.FromResult(user);
        }

        var result = Task.FromResult(new User
        {
            // В файле user.proto своства с маленькой буквы, а тут с большой
            Id      = 0,
            Name    = "",
            Email   = ""
        });

        return result;
    }

    // Метод из user.proto
    public override Task<User> CreateUser(CreateUserRequest request, ServerCallContext context)
    {
        var user = new User
        {
            Id      = _nextId++,
            Name    = request.Name,
            Email   = request.Email
        };

        Users[user.Id] = user;

        var result =  Task.FromResult(user);

        return result;
    }
}
