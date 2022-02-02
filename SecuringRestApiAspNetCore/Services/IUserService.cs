using SecuringRestApiAspNetCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecuringRestApiAspNetCore.Services
{
    // Authentication and authorization
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsersAsync();
    }
}
