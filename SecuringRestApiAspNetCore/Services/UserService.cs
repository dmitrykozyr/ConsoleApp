using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SecuringRestApiAspNetCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecuringRestApiAspNetCore.Services
{
    // Authentication and authorization
    public class UserService : IUserService
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IConfigurationProvider _mappingConfiguration;

        public UserService(
            UserManager<UserEntity> userManager,
            IConfigurationProvider mappingConfiguration)
        {
            _userManager = userManager;
            _mappingConfiguration = mappingConfiguration;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            IEnumerable<UserEntity> users =  _userManager.Users;

            // Маппим UserEntity и User
            return await _userManager.Users.ProjectTo<User>(_mappingConfiguration).ToArrayAsync();
        }
    }
}
