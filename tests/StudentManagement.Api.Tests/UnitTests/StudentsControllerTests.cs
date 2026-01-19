using Microsoft.AspNetCore.Mvc;
using Moq;
using StudentManagement.Api.Controllers;
using StudentManagement.Api.DTOs;
using StudentManagement.Api.Interfaces;
using Xunit;

namespace StudentManagement.Api.Tests.UnitTests;

public class StudentsControllerTests
{
    private readonly Mock<IStudentRepository> _mockRepo;
    private readonly StudentsController _controller;

    public StudentsControllerTests()
    {
        _mockRepo = new Mock<IStudentRepository>();
        _controller = new StudentsController(_mockRepo.Object);
    }

    [Fact]
    public void GetAll_ReturnsOk_WithStudents()
    {
        _mockRepo.Setup(repo => repo.GetAllStudents()).Returns(new List<StudentResponseDto> { new() });
        var result = _controller.GetAll();
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void GetById_ReturnsOk_WhenExists()
    {
        var id = Guid.NewGuid();
        _mockRepo.Setup(repo => repo.GetStudentById(id)).Returns(new StudentResponseDto { Id = id });
        var result = _controller.GetById(id);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void Create_ReturnsCreatedAtAction()
    {
        var req = new StudentRequestDto { Name = "Test" };
        _mockRepo.Setup(repo => repo.CreateStudent(req)).Returns(new StudentResponseDto { Id = Guid.NewGuid() });
        var result = _controller.Create(req);
        Assert.IsType<CreatedAtActionResult>(result);
    }
}
