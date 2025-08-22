# Configuration Files Explained

## 📁 appsettings.json

This is your main configuration file. Think of it as a "settings panel" that you can change without recompiling your application.

### What It Contains:

**Logging Configuration:**
```json
"Logging": {
  "LogLevel": {
    "Default": "Information",           // General application logs
    "Microsoft.AspNetCore": "Warning"   // ASP.NET Core framework logs (reduce noise)
  }
}
```

**Security Settings:**
```json
"AllowedHosts": "*"  // Controls which hosts can access your API
                     // "*" means allow all hosts (good for development, restrict for production)
```

**Database Connection String:**
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=CocktailDB;Trusted_Connection=true;TrustServerCertificate=true;"
}
```

### Connection String Explained:
- **Server**: Where SQL Server is running (`localhost` = same machine)
- **Database**: Which database to use (`CocktailDB`)
- **Trusted_Connection**: Use Windows authentication (your login)
- **TrustServerCertificate**: Skip SSL certificate validation (development only)

## 📁 appsettings.Docker.json

This is an alternative configuration file specifically for when running with Docker. It contains the connection string for the containerized SQL Server.

## 📁 Properties/launchSettings.json

This file defines how to run your application:
- Which ports to use
- Which URLs to open automatically
- Environment variables
- Launch profiles (HTTP vs HTTPS)

## 🔧 How to Use Configuration in Code:

```csharp
// In your service or controller:
public class MyService
{
    private readonly IConfiguration _configuration;
    
    public MyService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public void DoSomething()
    {
        // Get connection string
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        
        // Get other settings
        var logLevel = _configuration["Logging:LogLevel:Default"];
    }
}
```

## Environment-Specific Configuration:

You can have different configuration files for different environments:
- `appsettings.Development.json` - Development settings
- `appsettings.Production.json` - Production settings
- `appsettings.Staging.json` - Staging settings

The system automatically picks the right one based on your environment. 