# SecureWebApi

SecureWebApi is a modern ASP.NET Core sample API built to showcase secure endpoint protection with OpenID Connect (OIDC) and JWT-based authentication.

## Why this project matters

This repository demonstrates a practical, developer-friendly approach to securing a .NET API with industry-standard authentication patterns. It is designed for learning, prototyping, and experimentation—ideal for teams and developers exploring how to protect APIs with identity-driven security.

## What the project includes

- JWT bearer authentication setup
- Scope-based authorization policies
- Protected API routes using `[Authorize]`
- Swagger UI for local development and endpoint exploration
- MediatR-driven application flow for cleaner command/query separation
- EF Core-backed data access and seed initialization
- Examples for testing endpoints using `.http` files

## Solution structure

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

## High-level architecture

SecureWebApi follows a simple layered design:

- API layer: ASP.NET Core controllers handle incoming HTTP requests
- Security layer: authentication and scope-based authorization are configured centrally
- Data layer: MediatR and EF Core help organize business logic and persistence cleanly

This separation keeps the application easy to understand while demonstrating common .NET patterns used in secure API development.

## Security model

The sample includes policy-based authorization for scoped access control:

- `EditPolicy` requires the `edit:products` scope
- `DeletePolicy` requires the `delete:products` scope

This gives a clear example of how JWT claims can be translated into real security decisions in an API.

## Configuration

Authentication and database settings are defined in `MyMicroService/appsettings.json`.

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

These values are intended for sample/local development and should be replaced with your own identity provider and environment configuration in production.

## Getting started

### Prerequisites

- .NET 9 SDK
- LocalDB or another compatible database backend
- Access to a trusted identity provider such as Auth0

### Run locally

```bash
dotnet restore MyMicroservice.sln
dotnet build MyMicroservice.sln
dotnet run --project MyMicroService
```

Swagger is enabled in development, making it easy to explore and test the API locally.

## Example endpoints

### Products

```http
GET /api/products
GET /api/products/{id}
POST /api/products
PUT /api/products/{id}
DELETE /api/products/{id}
```

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

Sample request payloads are included in:

- `OrdersApi.http`
- `ParkingLot.http`

## Why developers use this repo

SecureWebApi is a compact, approachable example of a secure API built with ASP.NET Core. It combines authentication, authorization, service layering, and modern .NET patterns into a project that is easy to understand and easy to extend.

It is particularly useful for:

- learning OIDC/JWT security in .NET
- testing authorization flows locally
- exploring layered API design
- building a base for a more production-ready secure service

## Notes

This is a demonstration project, not a production security blueprint. It is designed to educate and accelerate understanding of secure API patterns in a .NET environment.

## Summary

SecureWebApi brings together authentication, authorization, and clean API design in one focused sample. It is a strong starting point for anyone looking to understand how secure ASP.NET Core services are structured in practice.
