using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecuringRestApiAspNetCore.Models;
using SecuringRestApiAspNetCore.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecuringRestApiAspNetCore.Controllers
{
    // Authentication and authorization
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        //[Authorize]
        [ProducesResponseType(401)]
        [HttpGet("{userId}", Name = nameof(GetUserById))]
        public Task<IActionResult> GetUserById(Guid userId)
        {
            //throw new NotImplementedException();
            return null;
        }

        [HttpGet(Name = nameof(GetVisibleUsers))]
        public async Task<IEnumerable<User>> GetVisibleUsers()
        {
            var users = await _userService.GetUsersAsync();
            return users;
        }
    }
}
