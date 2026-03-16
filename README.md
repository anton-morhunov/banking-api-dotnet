# Banking API

RESTful API built with ASP.NET Core for managing banking clients and accounts with JWT authentication and role-based authorization.

## Technologies
Backend
- C#
- ASP.NET Core
- Entity Framework Core
- PostgreSQL / SQL

Authentication & API
- JWT Authentication
- REST API
- Swagger

DevOps
- Docker
- GitHub Actions
- Render

Testing
- xUnit

## Features
- JWT authentication and role-based authorization
- secure login endpoint
- protected API endpoints
- client management
- bank account management
- update account status
- RESTful API design
- Swagger API documentation


## Deployment
The project is deployed using Render.

Live API(Swagger): https://banking-api-dotnet-2.onrender.com/swagger

## How to Use

The API is deployed and available online.

Base URL: 

https://banking-api-dotnet-2.onrender.com/swagger

You can test all endpoints directly using Swagger UI.

---

## Authentication Flow

### 1. Log in using the test credentials

Use the following credentials to obtain a JWT token.

Email: test@gmail.com  
Password: test  

Endpoint: `POST /api/auth/login`

Example request:

```json
{
  "email": "test@gmail.com",
  "password": "test"
}
```

Example response:

```json
{
  "token": "your_jwt_token"
}
```

Save the token returned by the API.

---

### 2. Authorize requests

To access protected endpoints:

1. Open Swagger UI  
2. Click **Authorize**  
3. Enter the token in the following format:

```
Bearer YOUR_TOKEN
```

---

### 3. Example request to a protected endpoint

Create a new client.

Endpoint: `POST /api/clients`

Example request:

```json
{
  "name": "Milinda Robinson",
  "email": "testClient@gmail.com",
  "phoneNumber": "134547895"
}
```

Example response:

```json
{
  "id": 1,
  "name": "Jack",
  "email": "jack@income.com",
  "phoneNumber": "3456553464",
  "created": "created-time",
  "status": 0
}
```


## Project Structure
- Controllers – API endpoints
- Data – database context
- Configuration – application configuration and settings
- DTOs – request/response models
- Middleware – custom request/response pipeline components
- Migrations – Entity Framework database migrations
- Models – entities
- Repositories – data access layer
- Services – business logic    
- Validators – request validation logic

## Architecture

```mermaid
flowchart TD

    Client[Client Application]

    subgraph API Layer
        Middleware[Middleware]
        Controllers[Controllers]
        Validators[Validators]
    end

    subgraph Application Layer
        Services[Services]
        DTOs[DTOs]
    end

    subgraph Infrastructure Layer
        Repositories[Repositories]
        EFCore[Entity Framework Core]
    end

    subgraph Database
        DB[(SQL Database)]
    end

    Client --> Middleware
    Middleware --> Controllers
    Controllers --> Validators
    Controllers --> Services

    Services --> DTOs
    Services --> Repositories

    Repositories --> EFCore
    EFCore --> DB

    


