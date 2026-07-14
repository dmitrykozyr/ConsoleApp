using Dapper_.Interfaces;
using Dapper_.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dapper_.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IUsersRepository _repository;

    public UsersController(IUsersRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IEnumerable<User>> Get()
    {
        return await _repository.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<User?> Get(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    [HttpPost]
    public async Task<int> Create(User user)
    {
        return await _repository.AddAsync(user);
    }

    [HttpPut]
    public async Task Update(User user)
    {
        await _repository.UpdateAsync(user);
    }

    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
        await _repository.DeleteAsync(id);
    }
}
