# 🏗️ System Architecture

This document explains in-depth the **.NET 10** architecture used in the LegendsTeamVN.BadmintonClub project, including Clean Architecture, directory structure, and the CQRS/MediatR flow.

*[Đọc bằng Tiếng Việt](ARCHITECTURE.vi.md)*

---

## 1. Clean Architecture & Dependency Rule

The project strictly follows **Clean Architecture**. The fundamental rule of this architecture is the **Dependency Rule**:
> *Source code dependencies must point only inward. Nothing in an inner circle can know anything at all about something in an outer circle.*

### System Layers:
1. **Domain Layer (`Core.Domain`, `BadmintonClub.Domain`)**
   - **Role**: The heart of the system. Contains Entities, Value Objects, Enums, and Domain Exceptions.
   - **Constraint**: MUST NOT depend on any external libraries or frameworks (no Entity Framework, no ASP.NET Core).

2. **Application Layer (`Core.Application`, `BadmintonClub.Application`)**
   - **Role**: Contains the business logic (Use Cases) in the form of CQRS Commands/Queries, DTOs, Validators (FluentValidation), and abstract Interfaces (e.g., `IUserRepository`).
   - **Constraint**: Depends ONLY on the Domain layer.

3. **Infrastructure / Persistence Layer (`Core.Infrastructure`, `BadmintonClub.Persistence`)**
   - **Role**: Implements the Interfaces defined in the Application layer. Contains `DbContext`, Repositories, RabbitMQ configurations, Redis caching, and external API integrations.
   - **Constraint**: Depends on the Application layer.

4. **Presentation Layer (`Core.Presentation`, `Hosts/API`)**
   - **Role**: The entry point for Client communication (RESTful APIs). Contains Minimal API Endpoints, Error handling Middleware, and Authentication.
   - **Constraint**: Depends on both Application and Infrastructure layers.

---

## 2. Directory Structure

The repository is divided into 3 main areas:

```text
src/
├── BuildingBlocks/                 # Contains shared code and utilities across the system
│   ├── LegendsTeamVN.Core.Domain
│   ├── LegendsTeamVN.Core.Application
│   ├── LegendsTeamVN.Core.Persistence
│   └── ...
├── Hosts/                          # Contains application entry points
│   ├── LegendsTeamVN.BadmintonClub.API       # The main API host project
│   └── LegendsTeamVN.BadmintonClub.Migrator  # A dedicated worker for EF Core Migrations
└── LegendsTeamVN.BadmintonClub.*   # Contains business logic specific to the Badminton Club domain
    ├── .Domain
    ├── .Application
    ├── .Infrastructure
    └── .Presentation
```

---

## 3. CQRS & MediatR Flow

The system implements **CQRS (Command Query Responsibility Segregation)** using the **MediatR** library to separate Read operations (Queries) from Write operations (Commands).

### Request Processing Flow:
1. **Client Sends Request**: The client calls an API endpoint (e.g., `POST /users`).
2. **Endpoint Receives Request**: The Presentation layer receives the request and initializes a `Command` (e.g., `CreateUserCommand`).
3. **MediatR Dispatch**: The endpoint calls `sender.Send(command)`. MediatR automatically locates the corresponding `CreateUserCommandHandler`.
4. **Validation Pipeline**: Before entering the Handler, the request passes through the `ValidationBehavior` (automatically triggering FluentValidation). If validation fails, it returns an HTTP 400 Bad Request immediately.
5. **Logic Execution**: The Handler receives the Command, manipulates Domain Entities, and calls the Repository (in the Persistence layer) to save changes to the Database.
6. **Result Returned**: The Handler returns a `Result<T>` object to the Endpoint. The Endpoint uses the `.Match()` extension method to map it into an HTTP 200 OK or an HTTP 400/500 with standardized Problem Details.
