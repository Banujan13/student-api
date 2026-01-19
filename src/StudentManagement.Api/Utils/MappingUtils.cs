using Npgsql;
using StudentManagement.Api.DTOs;

namespace StudentManagement.Api.Utils;

public static class MappingUtils
{
    public static StudentResponseDto MapToResponse(this NpgsqlDataReader reader)
    {
        return new StudentResponseDto
        {
            Id = reader.GetGuid(0),
            Name = reader.GetString(1),
            DateOfBirth = reader.GetDateTime(2)
        };
    }
}
