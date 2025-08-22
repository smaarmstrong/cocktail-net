# Cocktail API Makefile
# Usage: make <target>

.PHONY: help build run clean test restore docker-up docker-down docker-logs db-setup db-reset api-test

# Default target
help:
	@echo "🍸 Cocktail API - Available Commands:"
	@echo ""
	@echo "Development:"
	@echo "  build        - Build the project"
	@echo "  run          - Run the API locally"
	@echo "  restore      - Restore NuGet packages"
	@echo "  clean        - Clean build artifacts"
	@echo ""
	@echo "Docker:"
	@echo "  docker-up    - Start SQL Server with Docker"
	@echo "  docker-down  - Stop and remove Docker containers"
	@echo "  docker-logs  - View Docker container logs"
	@echo "  docker-shell - Open shell in SQL Server container"
	@echo ""
	@echo "Database:"
	@echo "  db-setup     - Set up database (requires Docker or SQL Server)"
	@echo "  db-reset     - Reset database (drop and recreate)"
	@echo ""
	@echo "Testing:"
	@echo "  api-test     - Test API endpoints"
	@echo "  test         - Run unit tests (if available)"
	@echo ""
	@echo "Utilities:"
	@echo "  format       - Format code with dotnet format"
	@echo "  lint         - Check code with dotnet format --verify-no-changes"
	@echo "  watch        - Run with file watching (hot reload)"

# Development Commands
build:
	@echo "🔨 Building Cocktail API..."
	cd backend && dotnet build

run: build
	@echo "🚀 Starting Cocktail API..."
	@echo "📍 API will be available at: http://localhost:5000"
	@echo "📚 Swagger UI: http://localhost:5000/swagger"
	@echo "⏹️  Press Ctrl+C to stop"
	cd backend && dotnet run

restore:
	@echo "📦 Restoring NuGet packages..."
	cd backend && dotnet restore

clean:
	@echo "🧹 Cleaning build artifacts..."
	cd backend && dotnet clean
	cd backend && rm -rf bin/ obj/

# Docker Commands
docker-up:
	@echo "🐳 Starting SQL Server with Docker..."
	@echo "📊 Database: CocktailDB"
	@echo "🔑 SA Password: YourStrong@Passw0rd"
	@echo "🌐 Port: localhost:1433"
	cd backend && docker compose up -d
	@echo "⏳ Waiting for SQL Server to be ready..."
	@sleep 10
	@echo "✅ SQL Server is ready!"

docker-down:
	@echo "🛑 Stopping Docker containers..."
	cd backend && docker compose down
	@echo "✅ Containers stopped"

docker-logs:
	@echo "📋 Docker container logs:"
	cd backend && docker compose logs -f

docker-shell:
	@echo "🐚 Opening shell in SQL Server container..."
	docker exec -it cocktail-sqlserver /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P YourStrong@Passw0rd

# Database Commands
db-setup: docker-up
	@echo "🗄️  Setting up database..."
	@echo "📝 Running database creation script..."
	docker exec -i cocktail-sqlserver /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P YourStrong@Passw0rd -i /database/CreateDatabase.sql
	@echo "✅ Database setup complete!"

db-reset: docker-down
	@echo "🔄 Resetting database..."
	docker volume rm cocktail-net_sqlserver_data 2>/dev/null || true
	@echo "✅ Database reset complete. Run 'make db-setup' to recreate."

# Testing Commands
api-test:
	@echo "🧪 Testing API endpoints..."
	@echo "⏳ Waiting for API to be ready..."
	@sleep 5
	@echo "📍 Testing base endpoint..."
	@curl -s -o /dev/null -w "Base endpoint: %{http_code}\n" http://localhost:5000/api/cocktail || echo "❌ API not responding"
	@echo "📚 Testing Swagger endpoint..."
	@curl -s -o /dev/null -w "Swagger: %{http_code}\n" http://localhost:5000/swagger || echo "❌ Swagger not responding"
	@echo "🌐 Testing external API fetch..."
	@curl -s -o /dev/null -w "External API: %{http_code}\n" http://localhost:5000/api/cocktail/fetch-external || echo "❌ External API not responding"
	@echo "🔍 Testing search endpoint..."
	@curl -s -o /dev/null -w "Search endpoint: %{http_code}\n" "http://localhost:5000/api/cocktail/search?q=mint" || echo "❌ Search not responding"
	@echo ""
	@echo "✅ API test completed!"
	@echo "🌐 Visit http://localhost:5000/swagger for interactive documentation"

test:
	@echo "🧪 Running unit tests..."
	cd backend && dotnet test

# Code Quality Commands
format:
	@echo "✨ Formatting code..."
	cd backend && dotnet format

lint:
	@echo "🔍 Checking code format..."
	cd backend && dotnet format --verify-no-changes

watch:
	@echo "👀 Running with file watching (hot reload)..."
	cd backend && dotnet watch run

# Full Development Setup
dev-setup: restore build docker-up db-setup
	@echo ""
	@echo "🎉 Development environment is ready!"
	@echo "🚀 Run 'make run' to start the API"
	@echo "📚 Visit http://localhost:5000/swagger for API docs"
	@echo "🐳 SQL Server is running on localhost:1433"

# Production-like setup (without Docker)
prod-setup: restore build
	@echo ""
	@echo "🏭 Production build ready!"
	@echo "📝 Remember to:"
	@echo "  1. Set up SQL Server manually"
	@echo "  2. Run Database/CreateDatabase.sql"
	@echo "  3. Update connection string in appsettings.json"
	@echo "  4. Run 'make run' to start the API"

# Quick start for development
dev: dev-setup run

# Show current status
status:
	@echo "📊 Current Status:"
	@echo "🐳 Docker containers:"
	@docker-compose ps 2>/dev/null || echo "   Docker Compose not available"
	@echo "🔌 API status:"
	@curl -s -o /dev/null -w "   HTTP: %{http_code}\n" http://localhost:5000/api/cocktail 2>/dev/null || echo "   ❌ Not running"
	@echo "🗄️  Database connection:"
	@docker exec cocktail-sqlserver /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P YourStrong@Passw0rd -Q "SELECT 'Connected' as Status" 2>/dev/null | grep "Connected" || echo "   ❌ Not connected"

# Clean everything
clean-all: clean docker-down
	@echo "🧹 Cleaned everything including Docker containers" 