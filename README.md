# Finance & Expense Management System

A full-stack finance and expense management application built with **C#, ASP.NET Core Web API, Entity Framework Core, SQL Server, and Angular**.

The application is designed to help users manage personal expenses through a secure, user-specific finance management platform.

## Tech Stack

### Backend
- C#
- .NET 9
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- RESTful APIs
- Swagger / OpenAPI
- JWT Authentication
- Password Hashing

### Frontend
- Angular (planned / under development)
- TypeScript
- HTML5
- CSS3

## Implemented Features

### Authentication & Authorization
- User registration
- Secure password hashing
- User login with JWT authentication
- Protected API endpoints
- User-specific expense access
- Authorization to prevent users from accessing or modifying other users' expenses

### Expense Management
- Create expenses
- Retrieve expenses
- Update expenses
- Delete expenses
- Associate expenses with authenticated users

### Validation & Error Handling
- Request model validation
- Required-field validation
- Expense amount validation
- Invalid date handling
- Standardized API response structure
- Exception handling middleware
- Logging

## Project Structure

```text
Finance-Expense-Management-System/
│
├── backend/
│   ├── FinanceApi/
│   │   ├── Controllers/
│   │   ├── Models/
│   │   ├── DTOs/
│   │   ├── Services/
│   │   ├── Middleware/
│   │   └── Data/
│   │
│   └── FinanceSystem.sln
│
├── frontend/
│   └── Angular application (under development)
│
└── README.md
```

## Getting Started

### Prerequisites
- .NET 9 SDK
- SQL Server / SQL Server LocalDB
- Visual Studio 2022 or Visual Studio Code
- Node.js and npm (for the Angular frontend)

### Backend Setup

1. Clone the repository:

   ```bash
   git clone https://github.com/CharmiBShah/Finance-Expense-Management-System.git
   ```

2. Navigate to the backend project:

   ```bash
   cd backend/FinanceApi
   ```

3. Configure your SQL Server connection string.

4. Configure the JWT signing key securely using .NET User Secrets or environment variables. Do not commit secrets to source control.

5. Apply database migrations:

   ```bash
   dotnet ef database update
   ```

6. Run the API:

   ```bash
   dotnet run
   ```

7. Open the Swagger UI to explore and test the API endpoints.

## Development Roadmap

- [x] ASP.NET Core Web API setup
- [x] SQL Server and Entity Framework Core integration
- [x] User entity and expense relationship
- [x] User registration and password hashing
- [x] JWT authentication
- [x] User-specific expense authorization
- [x] Expense CRUD operations
- [x] Input validation and error handling
- [ ] Angular frontend setup
- [ ] Angular authentication and route protection
- [ ] Expense dashboard
- [ ] Expense management interface
- [ ] Expense filtering and reporting
- [ ] Frontend and backend integration

## Project Status

**Backend:** Core functionality implemented and tested.

**Frontend:** Angular development planned.

---

This project is being developed as a practical full-stack application to strengthen skills in modern .NET development, Angular, REST APIs, database management, and secure application architecture.