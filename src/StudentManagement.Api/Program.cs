using Microsoft.OpenApi.Models;
using StudentManagement.Api.Interfaces;
using StudentManagement.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Student Management CRUD Web API", Version = "v1" });
    var xmlPath = Path.Combine(AppContext.BaseDirectory, $"{typeof(Program).Assembly.GetName().Name}.xml");
    if (File.Exists(xmlPath)) c.IncludeXmlComments(xmlPath);
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
builder.Services.AddSingleton<IStudentRepository>(new StudentRepository(connectionString));

var app = builder.Build();

app.Services.GetRequiredService<IStudentRepository>().InitializeDatabase();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();
