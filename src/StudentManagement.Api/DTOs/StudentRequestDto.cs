using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Api.DTOs;

public class StudentRequestDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public DateTime DateOfBirth { get; set; }
}
