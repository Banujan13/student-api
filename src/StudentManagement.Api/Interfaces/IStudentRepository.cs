using StudentManagement.Api.DTOs;

namespace StudentManagement.Api.Interfaces;

/// <summary>
/// Contract for student database operations.
/// </summary>
public interface IStudentRepository
{
    void InitializeDatabase();
    List<StudentResponseDto> GetAllStudents();
    StudentResponseDto? GetStudentById(Guid id);
    StudentResponseDto CreateStudent(StudentRequestDto request);
    bool UpdateStudent(Guid id, StudentRequestDto request);
    bool DeleteStudent(Guid id);
}
