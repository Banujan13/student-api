using Npgsql;
using StudentManagement.Api.DTOs;
using StudentManagement.Api.Interfaces;
using StudentManagement.Api.Utils;

namespace StudentManagement.Api.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly string _connectionString;

    public StudentRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void InitializeDatabase()
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        const string sql = "CREATE TABLE IF NOT EXISTS students (id UUID PRIMARY KEY, name VARCHAR(100) NOT NULL, date_of_birth DATE NOT NULL)";
        using var command = new NpgsqlCommand(sql, connection);
        command.ExecuteNonQuery();
    }

    public List<StudentResponseDto> GetAllStudents()
    {
        var students = new List<StudentResponseDto>();
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        const string sql = "SELECT id, name, date_of_birth FROM students";
        using var command = new NpgsqlCommand(sql, connection);
        using var reader = command.ExecuteReader();
        while (reader.Read()) students.Add(reader.MapToResponse());
        return students;
    }

    public StudentResponseDto? GetStudentById(Guid id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        const string sql = "SELECT id, name, date_of_birth FROM students WHERE id = @id";
        using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("id", id);
        using var reader = command.ExecuteReader();
        return reader.Read() ? reader.MapToResponse() : null;
    }

    public StudentResponseDto CreateStudent(StudentRequestDto request)
    {
        var id = Guid.NewGuid();
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        const string sql = "INSERT INTO students (id, name, date_of_birth) VALUES (@id, @name, @dateOfBirth)";
        using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("id", id);
        command.Parameters.AddWithValue("name", request.Name);
        command.Parameters.AddWithValue("dateOfBirth", request.DateOfBirth.Date);
        command.ExecuteNonQuery();
        return new StudentResponseDto { Id = id, Name = request.Name, DateOfBirth = request.DateOfBirth };
    }

    public bool UpdateStudent(Guid id, StudentRequestDto request)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        const string sql = "UPDATE students SET name = @name, date_of_birth = @dateOfBirth WHERE id = @id";
        using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("id", id);
        command.Parameters.AddWithValue("name", request.Name);
        command.Parameters.AddWithValue("dateOfBirth", request.DateOfBirth.Date);
        return command.ExecuteNonQuery() > 0;
    }

    public bool DeleteStudent(Guid id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        const string sql = "DELETE FROM students WHERE id = @id";
        using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("id", id);
        return command.ExecuteNonQuery() > 0;
    }
}
