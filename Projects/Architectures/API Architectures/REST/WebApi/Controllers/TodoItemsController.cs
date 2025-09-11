using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoItemsController
{
    private readonly DbContextSetUp _context;

    public TodoItemsController(DbContextSetUp context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IResult> GetTodoItems()
    {
        var result = await _context.TodoItems.ToListAsync();

        return Results.Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IResult> GetTodoItem(long id)
    {
        var todoItem = await _context.TodoItems.FindAsync(id);

        if (todoItem is null)
        {
            return Results.NotFound();
        }

        return Results.Ok(todoItem);
    }

    [HttpPut("{id}")]
    public async Task<IResult> PutTodoItem(long id, TodoItem todoItem)
    {
        if (id != todoItem.Id)
        {
            return Results.BadRequest();
        }

        _context.Entry(todoItem).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            var todoItemExists = TodoItemExists(id);

            if (!todoItemExists)
            {
                return Results.NotFound();
            }
            else
            {
                throw;
            }
        }

        return Results.NoContent();
    }

    [HttpPost]
    public async Task<IResult> PostTodoItem(TodoItem todoItem)
    {
        _context.TodoItems.Add(todoItem);

        await _context.SaveChangesAsync();

        return Results.CreatedAtRoute(
            nameof(GetTodoItem),
            new { id = todoItem.Id },
            todoItem);
    }

    [HttpDelete("{id}")]
    public async Task<IResult> DeleteTodoItem(long id)
    {
        var todoItem = await _context.TodoItems.FindAsync(id);

        if (todoItem is null)
        {
            return Results.NotFound();
        }

        _context.TodoItems.Remove(todoItem);

        await _context.SaveChangesAsync();

        return Results.Ok(todoItem);
    }

    private bool TodoItemExists(long id)
    {
        var result = _context.TodoItems.Any(e => e.Id == id);

        return result;
    }
}
