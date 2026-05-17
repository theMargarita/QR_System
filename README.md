# QR System

## Overview
QR_System is a modular backend system for managing QR-based digital contexts, such as restaurants, cafes, bars, or other venues. It provides APIs for context creation, QR code management, user and transaction handling, and more. The system is built with ASP.NET Core, Entity Framework Core, and follows a clean architecture with separation of concerns.

## Features
- Create and manage digital contexts (e.g., venues, tables)
- Generate and manage QR tokens for each context
- User and owner management
- Product and transaction management
- Modular service and DTO structure
- RESTful API endpoints
- Swagger/OpenAPI documentation

## Project Structure
```
QR_System.sln
Application/        # Application logic, DTOs, services, interfaces
Domain/             # Domain models/entities
Infrastructure/     # Database context, migrations, extensions
QR_System/          # API project (controllers, startup, config)
```

## Getting Started
### Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- PostgreSQL or SQL Server (update connection string in `appsettings.json`)

### Setup
1. Clone the repository:
	```sh
	git clone <repo-url>
	cd QR_System
	```
2. Update the connection string in `QR_System/appsettings.json`:
	```json
	"ConnectionStrings": {
	  "DefaultConnection": "Host=localhost;Database=qr_db;Username=postgres;Password=yourpassword"
	}
	```
3. Apply database migrations:
	```sh
	dotnet ef database update --project Infrastructure --startup-project QR_System
	```
4. Run the API:
	```sh
	dotnet run --project QR_System
	```

### API Documentation
Swagger UI is available at `/swagger` when running the project.

## Example Endpoints
- `POST /api/Context/CreateContext` — Create a new context
- `GET /api/Context/{id}/fetchContextById` — Get context by ID
- `GET /api/Context/{id}/qr` — Get QR token for a context
- `DELETE /api/Context/{id}` — Delete a context

## Main Entities
- **Context**: Represents a digital venue (restaurant, bar, etc.)
- **Owner**: The owner of a context
- **User**: End user interacting with the context
- **Product**: Items available in a context
- **Transaction**: Records of user actions or purchases

## Contributing
Pull requests are welcome! For major changes, please open an issue first to discuss what you would like to change.

## License
MIT