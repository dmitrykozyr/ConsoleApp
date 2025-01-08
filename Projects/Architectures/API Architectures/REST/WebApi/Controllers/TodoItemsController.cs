using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Controllers;

// action нужен, когда есть два метода одного типа, например, два Post, тогда в URL указываем название метода GET api/users/get
//[Route("api/[controller]/[action]")]

// POST https://localhost:5001/api/todoitems
/*{
    "name":"walk dog",
    "isComplete":true
}*/

// GET https://localhost:5001/api/todoitems/1 
[Route("api/[controller]")]
[ApiController] // Контроллер отвечает на запросы веб API
public class TodoItemsController : ControllerBase
{
    private readonly DbContextSetUp _context;
    public TodoItemsController(DbContextSetUp context) { _context = context; }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
    {
        return await _context.TodoItems.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
    {
        var todoItem = await _context.TodoItems.FindAsync(id);
        if (todoItem == null)
            return NotFound();
        return todoItem;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutTodoItem(long id, TodoItem todoItem)
    {
        if (id != todoItem.Id)
            return BadRequest();
        _context.Entry(todoItem).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TodoItemExists(id))
                return NotFound();
            else throw;
        }

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
    {
        _context.TodoItems.Add(todoItem);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<TodoItem>> DeleteTodoItem(long id)
    {
        var todoItem = await _context.TodoItems.FindAsync(id);
        if (todoItem == null)
            return NotFound();
        _context.TodoItems.Remove(todoItem);
        await _context.SaveChangesAsync();
        return todoItem;
    }

    private bool TodoItemExists(long id)
    {
        return _context.TodoItems.Any(e => e.Id == id);
    }
}
