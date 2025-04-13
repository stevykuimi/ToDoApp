# TodoApp

TodoApp is a full-featured ASP.NET Core MVC web application that allows users to manage a to-do list with full CRUD operations, filtering, sorting, and task completion logic. The project uses a clean, modular architecture with separate handler classes, Entity Framework Core for persistence, and is containerized with Docker. Unit tests are written using xUnit, and the project includes GitHub Actions for CI.

## Features

- Create, Read, Update, Delete to-do items
- Mark items as completed
- Prevent editing or deleting completed items
- Sort by due date or title
- Filter by completion status
- SQLite persistence using EF Core (Code-First)
- Razor views (MVC)
- Unit testing with xUnit
- Docker containerization
- GitHub Actions CI pipeline

## Technology Stack

- ASP.NET Core MVC (.NET 6 or .NET 7)
- Entity Framework Core with SQLite
- xUnit for testing
- Docker for containerization
- GitHub Actions for CI/CD

## Getting Started

### Prerequisites

- .NET SDK 6.0 or 7.0
- Docker (Desktop or CLI)
- Git

### Run the App Locally

```bash
git clone https://github.com/your-username/TodoApp.git
cd TodoApp
dotnet ef database update
dotnet run --project ToDoApp
