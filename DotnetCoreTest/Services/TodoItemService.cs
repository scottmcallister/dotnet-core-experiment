using AutoMapper;
using DotnetCoreTest.Models.DTOs;
using DotnetCoreTest.Models.Entities;
using DotnetCoreTest.Data.Repositories;

namespace DotnetCoreTest.Services;

public class TodoItemService : ITodoItemService
{
    private readonly ITodoItemRepository _repository;
    private readonly IMapper _mapper;

    public TodoItemService(ITodoItemRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TodoItemDto>> GetAllAsync()
    {
        var items = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<TodoItemDto>>(items);
    }

    public async Task<IEnumerable<TodoItemDto>> GetPendingAsync()
    {
        var items = await _repository.GetPendingItemsAsync();
        return _mapper.Map<IEnumerable<TodoItemDto>>(items);
    }

    public async Task<IEnumerable<TodoItemDto>> GetCompletedAsync()
    {
        var items = await _repository.GetCompletedItemsAsync();
        return _mapper.Map<IEnumerable<TodoItemDto>>(items);
    }

    public async Task<TodoItemDto?> GetByIdAsync(int id)
    {
        var item = await _repository.GetByIdAsync(id);
        return item != null ? _mapper.Map<TodoItemDto>(item) : null;
    }

    public async Task<TodoItemDto> CreateAsync(CreateTodoItemDto createDto)
    {
        var todoItem = _mapper.Map<TodoItem>(createDto);
        var createdItem = await _repository.AddAsync(todoItem);
        await _repository.SaveChangesAsync();
        return _mapper.Map<TodoItemDto>(createdItem);
    }

    public async Task<TodoItemDto?> UpdateAsync(int id, UpdateTodoItemDto updateDto)
    {
        var existingItem = await _repository.GetByIdAsync(id);
        if (existingItem == null)
            return null;

        _mapper.Map(updateDto, existingItem);
        var updatedItem = await _repository.UpdateAsync(existingItem);
        await _repository.SaveChangesAsync();
        return _mapper.Map<TodoItemDto>(updatedItem);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existingItem = await _repository.GetByIdAsync(id);
        if (existingItem == null)
            return false;

        await _repository.DeleteAsync(id);
        await _repository.SaveChangesAsync();
        return true;
    }

    public async Task<TodoItemDto?> ToggleCompleteAsync(int id)
    {
        var existingItem = await _repository.GetByIdAsync(id);
        if (existingItem == null)
            return null;

        existingItem.IsCompleted = !existingItem.IsCompleted;
        var updatedItem = await _repository.UpdateAsync(existingItem);
        await _repository.SaveChangesAsync();
        return _mapper.Map<TodoItemDto>(updatedItem);
    }
}