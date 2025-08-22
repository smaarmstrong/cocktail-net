# Cocktail API

A full-stack cocktail application with a .NET Web API backend and React TypeScript frontend.

## 🏗️ Project Overview

This project consists of two main parts:

- **Backend**: .NET Web API for managing a cocktail database with SQL Server stored procedures and external API integration
- **Frontend**: React TypeScript application for fetching and displaying random cocktails

## 📁 Project Structure

```
cocktail-net/
├── backend/              # .NET Web API Backend
│   ├── Controllers/      # API endpoints
│   ├── Services/         # Business logic
│   ├── Data/             # Data access layer
│   ├── Models/           # Data models
│   ├── Database/         # SQL scripts and database setup
│   ├── appsettings.json  # Configuration
│   └── docker-compose.yml # Docker setup
├── cocktail-frontend/    # React TypeScript Frontend
│   ├── src/
│   │   ├── components/   # Reusable UI components
│   │   ├── hooks/        # Custom React hooks
│   │   ├── services/     # API and business logic
│   │   ├── types/        # TypeScript interfaces
│   │   └── utils/        # Utilities and constants
│   └── package.json      # Frontend dependencies
└── Makefile              # Development automation
```

## 🏗️ **Project Structure Explained (For .NET Beginners)**

This project follows the **Clean Architecture** pattern, which separates concerns into different layers. Here's what each file and folder does:

### **📁 Root Level Files**
- **`CocktailApi.csproj`** - The project file that defines what type of application this is, which .NET version to use, and which external packages (NuGet packages) to include
- **`Program.cs`** - The entry point of the application (like `main()` in other languages). This is where the web server is configured and started
- **`appsettings.json`** - Configuration file that stores settings like database connection strings, logging levels, etc.
- **`appsettings.Docker.json`** - Alternative configuration for when running with Docker
- **`docker-compose.yml`** - Defines how to run SQL Server in a Docker container
- **`Makefile`** - Contains shortcuts for common commands (build, run, docker operations, etc.)

### **📁 Models/ (Data Models)**
- **`Cocktail.cs`** - Defines what a Cocktail object looks like (properties like Name, Category, Ingredients, etc.)
- **`Ingredient.cs`** - Defines what an Ingredient object looks like

### **📁 Data/ (Database Layer)**
- **`IDatabaseService.cs`** - Interface (contract) that defines what database operations are available
- **`DatabaseService.cs`** - Implementation that actually talks to SQL Server using stored procedures

### **📁 Services/ (Business Logic Layer)**
- **`ICocktailService.cs`** - Interface that defines what cocktail operations are available
- **`CocktailService.cs`** - Implementation that combines database operations with external API calls

### **📁 Controllers/ (API Endpoints)**
- **`CocktailController.cs`** - Defines the actual HTTP endpoints (GET, POST, PUT, DELETE) that clients can call

### **📁 Database/ (SQL Scripts)**
- **`CreateDatabase.sql`** - SQL script that creates the database, tables, and stored procedures

### **📁 Properties/ (Project Configuration)**
- **`launchSettings.json`** - Defines how to run the application (which ports, which URLs to open, etc.)

### **🔧 How It All Works Together:**
1. **Client** makes an HTTP request to an endpoint (e.g., `GET /api/cocktail`)
2. **Controller** receives the request and calls the appropriate **Service**
3. **Service** contains business logic and calls the **Data** layer
4. **Data** layer executes stored procedures against **SQL Server**
5. **Response** flows back through the same layers to the client

## Features

- **Full CRUD Operations**: Create, Read, Update, and Delete cocktails
- **Stored Procedures**: All database operations use SQL Server stored procedures
- **External API Integration**: Fetch random cocktails from TheCocktailDB
- **Search Functionality**: Search cocktails by name, category, or instructions
- **Swagger Documentation**: Built-in API documentation
- **CORS Support**: Cross-origin resource sharing enabled

## Prerequisites

- .NET 8.0 SDK
- SQL Server (LocalDB, Express, or full version)
- SQL Server Management Studio (SSMS) or Azure Data Studio

## Setup

### 1. Database Setup

1. Open SQL Server Management Studio or Azure Data Studio
2. Connect to your SQL Server instance
3. Open and execute the `Database/CreateDatabase.sql` script
4. This will create:
   - `CocktailDB` database
   - `Cocktails` and `Ingredients` tables
   - All required stored procedures
   - Sample data (Mojito and Old Fashioned)

### 2. Connection String

Update the connection string in `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=CocktailDB;Trusted_Connection=true;TrustServerCertificate=true;"
}
```

For SQL Server Express:
```
Server=localhost\\SQLEXPRESS;Database=CocktailDB;Trusted_Connection=true;TrustServerCertificate=true;
```

### 3. Build and Run

```bash
# Using Makefile (recommended)
make restore
make build
make run

# Or manually
cd backend
dotnet restore
dotnet build
dotnet run
```

The API will be available at `https://localhost:7000` (or the port shown in the console).

### 4. Frontend Setup

```bash
cd cocktail-frontend
bun install
bun run dev
```

The frontend will be available at `http://localhost:5173`.

## API Endpoints

### Cocktails

- `GET /api/cocktail` - Get all cocktails
- `GET /api/cocktail/{id}` - Get cocktail by ID
- `POST /api/cocktail` - Create new cocktail
- `PUT /api/cocktail/{id}` - Update existing cocktail
- `DELETE /api/cocktail/{id}` - Delete cocktail
- `GET /api/cocktail/search?q={term}` - Search cocktails
- `GET /api/cocktail/fetch-external` - Fetch random cocktail from TheCocktailDB

### Swagger Documentation

Visit `https://localhost:7000/swagger` to see the interactive API documentation.

## Database Schema

### Cocktails Table
- `Id` (INT, Primary Key)
- `Name` (NVARCHAR(255))
- `Category` (NVARCHAR(100))
- `Alcoholic` (NVARCHAR(50))
- `Glass` (NVARCHAR(100))
- `Instructions` (NVARCHAR(MAX))
- `ImageUrl` (NVARCHAR(500))
- `CreatedAt` (DATETIME2)
- `UpdatedAt` (DATETIME2, nullable)

### Ingredients Table
- `Id` (INT, Primary Key)
- `Name` (NVARCHAR(255))
- `Measure` (NVARCHAR(100))
- `CocktailId` (INT, Foreign Key to Cocktails)

## Stored Procedures

- `sp_GetAllCocktails` - Retrieves all cocktails
- `sp_GetCocktailById` - Gets cocktail with ingredients by ID
- `sp_CreateCocktail` - Creates new cocktail
- `sp_UpdateCocktail` - Updates existing cocktail
- `sp_DeleteCocktail` - Deletes cocktail (cascades to ingredients)
- `sp_SearchCocktails` - Searches cocktails by term

## Example Usage

### Create a Cocktail

```json
POST /api/cocktail
{
  "name": "Margarita",
  "category": "Cocktail",
  "alcoholic": "Alcoholic",
  "glass": "Margarita glass",
  "instructions": "Rub the rim of the glass with the lime slice...",
  "imageUrl": "https://example.com/margarita.jpg"
}
```

### Search Cocktails

```
GET /api/cocktail/search?q=rum
```

### Fetch from External API

```
GET /api/cocktail/fetch-external
```

## Project Structure

```
cocktail-net/
├── backend/              # .NET Web API Backend
│   ├── Controllers/
│   │   └── CocktailController.cs
│   ├── Data/
│   │   ├── IDatabaseService.cs
│   │   └── DatabaseService.cs
│   ├── Models/
│   │   └── Cocktail.cs
│   ├── Services/
│   │   ├── ICocktailService.cs
│   │   └── CocktailService.cs
│   ├── Database/
│   │   └── CreateDatabase.sql
│   ├── appsettings.json
│   ├── Program.cs
│   └── CocktailApi.csproj
├── cocktail-frontend/    # React TypeScript Frontend
│   ├── src/
│   │   ├── components/   # Reusable UI components
│   │   ├── hooks/        # Custom React hooks
│   │   ├── services/     # API and business logic
│   │   ├── types/        # TypeScript interfaces
│   │   └── utils/        # Utilities and constants
│   └── package.json      # Frontend dependencies
├── Makefile              # Development automation
└── README.md             # This file
```

## Future Enhancements

- User authentication and authorization
- Favorite cocktails functionality
- Rating and review system
- Advanced filtering and sorting
- Bulk import from external API
- Image upload and storage
- Recipe scaling and unit conversion

## Troubleshooting

### Common Issues

1. **Connection String**: Ensure SQL Server is running and the connection string is correct
2. **Database**: Run the SQL script to create the database and stored procedures
3. **Port Conflicts**: The API runs on HTTPS by default, check for port conflicts
4. **CORS**: The API allows all origins for development; restrict this for production

### Logs

Check the console output for detailed error messages and logs.

## License

This project is for demonstration purposes. TheCocktailDB API is free to use for non-commercial purposes.

