# Expense Tracker API

A RESTful API for tracking expenses and categories built with .NET 8.

## Features

- Category management (CRUD operations)
- Expense management (CRUD operations)
- Filter expenses by category
- RESTful API with proper HTTP status codes
- Swagger documentation
- AutoMapper for DTO mappings
- Repository pattern
- Service layer
- Entity Framework Core with SQL Server
- MM/DD/YYYY date format for expenses

## Technologies

- .NET 8
- Entity Framework Core 8
- AutoMapper
- SQL Server
- Swagger / OpenAPI

## API Endpoints

### Categories

- `GET /api/Category` - Get all categories
- `GET /api/Category/{id}` - Get a specific category
- `POST /api/Category` - Create a new category
- `PUT /api/Category/{id}` - Update a category
- `DELETE /api/Category/{id}` - Delete a category

### Expenses

- `GET /api/Expense` - Get all expenses
- `GET /api/Expense/{id}` - Get a specific expense
- `GET /api/Expense/category/{categoryId}` - Get expenses by category
- `POST /api/Expense` - Create a new expense
- `PUT /api/Expense/{id}` - Update an expense
- `DELETE /api/Expense/{id}` - Delete an expense

## Getting Started

### Prerequisites

- .NET 8 SDK
- SQL Server (Local or Express)

### Setup

1. Clone the repository
2. Update the connection string in `appsettings.json` if needed
3. Run the application:
   ```
   dotnet run
   ```
4. Access the Swagger UI at: `https://localhost:5001/swagger/index.html`

## Database

The database is automatically created if it doesn't exist. Initial migrations are applied when the application starts.

## Date Format

Expense dates are formatted as MM/DD/YYYY (no time component) for better readability and consistency.

## Project Structure

The project follows a clean architecture with these components:

- **Models**: Data models representing the domain entities
- **DTOs**: Data Transfer Objects for API requests and responses
- **Data**: Repository interfaces and implementations for data access
- **Controllers**: API endpoints implementing CRUD operations

## License

MIT 