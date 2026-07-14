using Dapper_.Models;

namespace Dapper_.Interfaces;

public interface IUsersRepository
{
    Task<IEnumerable<User>> GetAllAsync();

    Task<User?> GetByIdAsync(int id);

    Task<int> AddAsync(User user);

    Task UpdateAsync(User user);

    Task DeleteAsync(int id);
}
