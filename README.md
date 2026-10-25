# SecureWebApi

A simple ASP.NET Core Web API designed to demonstrate how to secure endpoints with OpenID Connect (OIDC) and JWT-based authentication.

## Overview

This repository is a lightweight learning/demo project for implementing authentication and authorization in a .NET API. It shows how to:

- configure JWT bearer authentication
- validate issuer and audience claims
- define authorization policies based on scopes
- protect controller actions with `[Authorize]`
- expose Swagger in development
- use MediatR for a clean command/query pattern
- work with EF Core-backed models and simple seeded data

The project is intentionally small and focused on demonstrating secure API patterns rather than production-ready enterprise architecture.

## What this project contains

The solution includes two main projects:

- `MyMicroService` - ASP.NET Core Web API
- `DataAccess` - shared MediatR, handlers, commands, services, and model logic

## Architecture

### API layer
The API layer is built with ASP.NET Core and contains controllers for:

- `ProductsController` - basic product CRUD endpoints
- `ParkingLotController` - parking lot and vehicle lifecycle endpoints
- `OrdersController` - order operations through MediatR
- `WeatherForecastController` - default ASP.NET template example

### Security layer
The security configuration is implemented in:

- `MyMicroService/Security/JwtAuthenticationService.cs`
- `MyMicroService/Security/SecurityConstants.cs`

It configures:

- JWT bearer authentication
- issuer and audience validation
- authorization policies such as `EditPolicy` and `DeletePolicy`
- scope-based checks such as `edit:products` and `delete:products`

### Data access layer
The `DataAccess` project uses MediatR and organizes logic into:

- `Commands`
- `Handlers`
- `Models`
- `Services`

This keeps the API controllers thin and promotes separation of concerns.

## Features

- JWT/OIDC-style authentication setup
- Role/scope-based authorization policies
- Swagger UI enabled in development
- CORS configuration for local frontend access
- EF Core database contexts and startup data seeding
- REST-style endpoints for products, parking, and orders
- Example API calls via `.http` files

## Project structure

```text
SecureWebApi/
├── DataAccess/
│   ├── Commands/
│   ├── Handlers/
│   ├── Models/
│   ├── Services/
│   ├── MediatRServiceRegistration.cs
│   └── DataAccess.csproj
├── MyMicroService/
│   ├── Controllers/
│   ├── Data/
│   ├── Exceptions/
│   ├── Interfaces/
│   ├── Migrations/
│   ├── Models/
│   ├── Security/
│   ├── Services/
│   ├── appsettings.json
│   ├── appsettings.Development.json
│   ├── Dockerfile
│   ├── MyMicroservice.csproj
│   └── Program.cs
├── MyMicroservice.sln
├── OrdersApi.http
├── ParkingLot.http
├── README.md
├── .gitignore
├── .dockerignore
├── .gitattributes
└── .github/
```

## Prerequisites

- .NET 9 SDK
- Access to a compatible authentication provider (for example, Auth0)
- LocalDB or another configured database backend for development

## Configuration

The application reads authentication and connection settings from `MyMicroService/appsettings.json`.

Example configuration:

```json
{
  "ConnectionStrings": {
    "EFContext": "Server=(localdb)\\mssqllocaldb;Database=MyMicroservice.Data;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "Jwt": {
    "Issuer": "https://dev-rset4xdu1aiy6rep.us.auth0.com",
    "Audience": "http://localhost:5262/"
  }
}
```

Important notes:

- `Jwt:Issuer` should be replaced with your own identity provider URL
- `Jwt:Audience` should match the API audience expected by your tokens
- The sample is configured for local development and uses a local Auth0 issuer URL in the default config

## Running the application

From the repository root:

```bash
dotnet restore MyMicroservice.sln
dotnet build MyMicroservice.sln
dotnet run --project MyMicroService
```

Once running, Swagger should be available in development at:

```text
https://localhost:PORT/swagger
```

## Example API endpoints

### Products

```http
GET /api/products
GET /api/products/{id}
POST /api/products
PUT /api/products/{id}
DELETE /api/products/{id}
```

The `PUT` and `DELETE` actions are protected with authorization.

### Parking lot

```http
GET /api/ParkingLot
GET /api/ParkingLot/{vehicleId}
POST /api/ParkingLot/register
POST /api/ParkingLot/{vehicleId}/park
POST /api/ParkingLot/{vehicleId}/unpark
```

### Orders

```http
GET /api/orders
GET /api/orders/{id}
POST /api/orders
PUT /api/orders/{id}
DELETE /api/orders/{id}
```

The repo includes ready-to-use request examples in:

- `OrdersApi.http`
- `ParkingLot.http`

## Security model

The sample defines two authorization policies:

- `EditPolicy` -> requires claim `scope=edit:products`
- `DeletePolicy` -> requires claim `scope=delete:products`

This is a simple way to demonstrate scoped authorization in a JWT-based API.

## Notes and limitations

- This is a demo project intended for learning secure API development
- Some configuration values are sample values and should be updated for real environments
- CORS is configured for a local frontend origin and may need to be adjusted for your setup
- The app is set up for local development; production hardening would include secret management, stronger validation, and environment-specific configuration

## Summary

`SecureWebApi` is a practical example of how to secure an ASP.NET Core API using JWT/OIDC concepts, custom authorization policies, and a layered .NET architecture. It is a useful starting point for learning, experimenting, or extending secure API patterns in a .NET environment.
