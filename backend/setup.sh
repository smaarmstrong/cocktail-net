#!/bin/bash

echo "Cocktail API Setup Script"
echo "========================="

# Check if .NET is available
if ! command -v dotnet &> /dev/null; then
    echo "Error: .NET SDK is not installed or not in PATH"
    echo "Please install .NET 8.0 SDK first:"
    echo "  - Visit: https://dotnet.microsoft.com/download"
    echo "  - Or use your package manager"
    exit 1
fi

echo "✓ .NET SDK found: $(dotnet --version)"

# Restore packages
echo "Restoring NuGet packages..."
dotnet restore

if [ $? -ne 0 ]; then
    echo "Error: Failed to restore packages"
    exit 1
fi

# Build the project
echo "Building the project..."
dotnet build

if [ $? -ne 0 ]; then
    echo "Error: Failed to build the project"
    exit 1
fi

echo ""
echo "✓ Project built successfully!"
echo ""
echo "Next steps:"
echo "1. Set up SQL Server database using Database/CreateDatabase.sql"
echo "2. Update connection string in appsettings.json"
echo "3. Run the API: dotnet run"
echo "4. Visit https://localhost:5000/swagger for API documentation"
echo ""
echo "For database setup help, see README.md" 