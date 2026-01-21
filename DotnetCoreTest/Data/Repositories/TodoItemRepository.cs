using Microsoft.EntityFrameworkCore;
using DotnetCoreTest.Data;
using DotnetCoreTest.Models.Entities;

namespace DotnetCoreTest.Data.Repositories;

public interface ITodoItemRepository : IRepository<TodoItem>
{
    Task<IEnumerable<TodoItem>> GetPendingItemsAsync();
    Task<IEnumerable<TodoItem>> GetCompletedItemsAsync();
}

public class TodoItemRepository : Repository<TodoItem>, ITodoItemRepository
{
    public TodoItemRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<TodoItem>> GetPendingItemsAsync()
    {
        return await _context.TodoItems
            .Where(x => !x.IsCompleted)
            .OrderBy(x => x.DueDate)
            .ThenBy(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<TodoItem>> GetCompletedItemsAsync()
    {
        return await _context.TodoItems
            .Where(x => x.IsCompleted)
            .OrderByDescending(x => x.CompletedAt)
            .ToListAsync();
    }

    public override async Task<TodoItem> AddAsync(TodoItem entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        return await base.AddAsync(entity);
    }

    public override async Task<TodoItem> UpdateAsync(TodoItem entity)
    {
        if (entity.IsCompleted && !entity.CompletedAt.HasValue)
        {
            entity.CompletedAt = DateTime.UtcNow;
        }
        else if (!entity.IsCompleted && entity.CompletedAt.HasValue)
        {
            entity.CompletedAt = null;
        }

        return await base.UpdateAsync(entity);
    }
}