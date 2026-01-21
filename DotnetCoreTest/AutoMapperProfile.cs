using AutoMapper;
using DotnetCoreTest.Models.DTOs;
using DotnetCoreTest.Models.Entities;

namespace DotnetCoreTest;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<TodoItem, TodoItemDto>();
        CreateMap<CreateTodoItemDto, TodoItem>();
        CreateMap<UpdateTodoItemDto, TodoItem>();
    }
}