# Student Management CRUD Web API

A clean, minimal, and high-performance Student Management API built with **ASP.NET Core (.NET 9)** and **PostgreSQL**. This project demonstrates best practices in API design, clean directory structure, and basic CRUD operations using Docker for database management.

## Key Features
- **Clean Architecture**: Organized directory structure (`src/`, `tests/`).
- **Minimal Code**: Focused implementation with simple SQL statements.
- **DTO Support**: Separate Request and Response DTOs for data encapsulation.
- **OpenAPI / Swagger**: Fully documented API with descriptions and examples.
- **Unit Testing**: 100% logic coverage with xUnit and Moq.
- **Docker Integration**: Easy database setup with PostgreSQL in Docker.

---

## Requirements

To run this project, you need the following installed:
- [Docker & Docker Compose](https://www.docker.com/products/docker-desktop/)
- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0) (Optional if using Docker to run/test)

---

## Installation & Verification

### 1. Verification of Requirements
Check if Docker and .NET are installed:
```bash
docker --version
docker-compose --version
dotnet --version # If installed locally
```

### 2. Cloned Repository Structure
```text
baanu/
├── src/
│   └── StudentManagement.Api/
│       ├── Controllers/      # API Endpoints
│       ├── DTOs/             # Request/Response payloads (Flattened)
│       ├── Interfaces/       # Contracts (IStudentRepository)
│       ├── Models/           # Database Entities (Student)
│       ├── Repositories/     # DB Logic (Implements Interfaces)
│       ├── Utils/            # Mapping and helper extensions
│       ├── Program.cs        # DI Registration (Interface based)
│       └── appsettings.json
└── tests/
    └── StudentManagement.Api.Tests/
        └── UnitTests/        # Mocks interfaces for clean testing
```

---

## How to Run the Project

### 1. Start the Database
Runs PostgreSQL 16 in a detached container:
```bash
docker-compose up -d
```

### 2. Run the API
You can run the API locally using the .NET CLI:
```bash
dotnet run --project src/StudentManagement.Api/StudentManagement.Api.csproj
```
Wait for the application to start. The database will be automatically initialized on startup.

### 3. Access the API
- **Swagger UI (Documentation)**: [http://localhost:5000/swagger](http://localhost:5000/swagger)
- **API Endpoint**: [http://localhost:5000/api/students](http://localhost:5000/api/students)

### Execution Details
- **Database**: PostgreSQL is running on port **5433**.
- **API Server**: Running at **http://localhost:5000**.

---

## How to Run Tests

### Run via .NET CLI (Local)
```bash
dotnet test
```

### Run via Docker (No local .NET needed)
```bash
docker run --rm -v "$(pwd):/app" -w /app mcr.microsoft.com/dotnet/sdk:9.0 dotnet test
```

---

## Cleanup & Maintenance
To stop the project and remove containers:
```bash
# Stop the API container
docker stop student_api

# Stop the database and remove volumes
docker-compose down -v
```
