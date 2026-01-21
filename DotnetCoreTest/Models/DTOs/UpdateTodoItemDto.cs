using System.ComponentModel.DataAnnotations;

namespace DotnetCoreTest.Models.DTOs;

public class UpdateTodoItemDto
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Description { get; set; }

    public bool IsCompleted { get; set; }

    public DateTime? DueDate { get; set; }
}