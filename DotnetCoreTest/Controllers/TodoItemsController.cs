using Microsoft.AspNetCore.Mvc;
using DotnetCoreTest.Models.DTOs;
using DotnetCoreTest.Services;

namespace DotnetCoreTest.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoItemsController : ControllerBase
{
    private readonly ITodoItemService _todoItemService;

    public TodoItemsController(ITodoItemService todoItemService)
    {
        _todoItemService = todoItemService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetAll()
    {
        var items = await _todoItemService.GetAllAsync();
        return Ok(items);
    }

    [HttpGet("pending")]
    public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetPending()
    {
        var items = await _todoItemService.GetPendingAsync();
        return Ok(items);
    }

    [HttpGet("completed")]
    public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetCompleted()
    {
        var items = await _todoItemService.GetCompletedAsync();
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TodoItemDto>> GetById(int id)
    {
        var item = await _todoItemService.GetByIdAsync(id);
        if (item == null)
            return NotFound();

        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<TodoItemDto>> Create([FromBody] CreateTodoItemDto createDto)
    {
        var createdItem = await _todoItemService.CreateAsync(createDto);
        return CreatedAtAction(nameof(GetById), new { id = createdItem.Id }, createdItem);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TodoItemDto>> Update(int id, [FromBody] UpdateTodoItemDto updateDto)
    {
        var updatedItem = await _todoItemService.UpdateAsync(id, updateDto);
        if (updatedItem == null)
            return NotFound();

        return Ok(updatedItem);
    }

    [HttpPatch("{id}/toggle")]
    public async Task<ActionResult<TodoItemDto>> ToggleComplete(int id)
    {
        var updatedItem = await _todoItemService.ToggleCompleteAsync(id);
        if (updatedItem == null)
            return NotFound();

        return Ok(updatedItem);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _todoItemService.DeleteAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}