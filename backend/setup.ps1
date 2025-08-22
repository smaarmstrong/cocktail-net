# Cocktail API Setup Script for Windows
Write-Host "Cocktail API Setup Script" -ForegroundColor Green
Write-Host "=========================" -ForegroundColor Green

# Check if .NET is available
try {
    $dotnetVersion = dotnet --version
    Write-Host "✓ .NET SDK found: $dotnetVersion" -ForegroundColor Green
} catch {
    Write-Host "Error: .NET SDK is not installed or not in PATH" -ForegroundColor Red
    Write-Host "Please install .NET 8.0 SDK first:" -ForegroundColor Yellow
    Write-Host "  - Visit: https://dotnet.microsoft.com/download" -ForegroundColor Yellow
    Write-Host "  - Or use winget: winget install Microsoft.DotNet.SDK.8" -ForegroundColor Yellow
    exit 1
}

# Restore packages
Write-Host "Restoring NuGet packages..." -ForegroundColor Yellow
dotnet restore

if ($LASTEXITCODE -ne 0) {
    Write-Host "Error: Failed to restore packages" -ForegroundColor Red
    exit 1
}

# Build the project
Write-Host "Building the project..." -ForegroundColor Yellow
dotnet build

if ($LASTEXITCODE -ne 0) {
    Write-Host "Error: Failed to build the project" -ForegroundColor Red
    exit 1
}

Write-Host ""
Write-Host "✓ Project built successfully!" -ForegroundColor Green
Write-Host ""
Write-Host "Next steps:" -ForegroundColor Yellow
Write-Host "1. Set up SQL Server database using Database/CreateDatabase.sql" -ForegroundColor White
Write-Host "2. Update connection string in appsettings.json" -ForegroundColor White
Write-Host "3. Run the API: dotnet run" -ForegroundColor White
Write-Host "4. Visit https://localhost:7000/swagger for API documentation" -ForegroundColor White
Write-Host ""
Write-Host "For database setup help, see README.md" -ForegroundColor White
Write-Host ""
Write-Host "Alternative: Use Docker Compose for SQL Server:" -ForegroundColor Cyan
Write-Host "  docker-compose up -d" -ForegroundColor White 