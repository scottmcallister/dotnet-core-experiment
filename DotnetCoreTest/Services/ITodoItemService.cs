using DotnetCoreTest.Models.DTOs;

namespace DotnetCoreTest.Services;

public interface ITodoItemService
{
    Task<IEnumerable<TodoItemDto>> GetAllAsync();
    Task<IEnumerable<TodoItemDto>> GetPendingAsync();
    Task<IEnumerable<TodoItemDto>> GetCompletedAsync();
    Task<TodoItemDto?> GetByIdAsync(int id);
    Task<TodoItemDto> CreateAsync(CreateTodoItemDto createDto);
    Task<TodoItemDto?> UpdateAsync(int id, UpdateTodoItemDto updateDto);
    Task<bool> DeleteAsync(int id);
    Task<TodoItemDto?> ToggleCompleteAsync(int id);
}