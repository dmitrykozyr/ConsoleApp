using EmptyApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmptyApi.Controllers
{
    [ApiController]
    // action нужен, когда есть два метода одного типа, например, два Post, тогда в урле указываем название метода GET api/users/get
    //[Route("api/[controller]/[action]")]

    // GET api/users/5
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        readonly UsersContext db;
        public UsersController(UsersContext context)
        {
            db = context;
            if (db.Users.Any()) return;
            db.Users.Add(new User { Name = "Tom", Age = 26 });
            db.Users.Add(new User { Name = "Alice", Age = 31 });
            db.SaveChanges();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            return await db.Users.ToListAsync();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get2()
        {
            return await db.Users.Where(z => z.Age <= 30).ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            var user = await db.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
                return NotFound();

            return new ObjectResult(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> Post(User user)
        {
            if (user == null)
                return BadRequest();

            db.Users.Add(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }

        [HttpPut]
        public async Task<ActionResult<User>> Put(User user)
        {
            if (user == null)
                return BadRequest();

            if (!db.Users.Any(x => x.Id == user.Id))
                return NotFound();

            db.Update(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<User>> Delete(int id)
        {
            var user = db.Users.FirstOrDefault(x => x.Id == id);

            if (user == null)
                return NotFound();

            db.Users.Remove(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }
    }
}
