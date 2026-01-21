using System.ComponentModel.DataAnnotations;

namespace DotnetCoreTest.Models.DTOs;

public class CreateTodoItemDto
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Description { get; set; }

    public DateTime? DueDate { get; set; }
}