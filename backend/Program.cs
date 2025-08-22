// This is the entry point of your .NET Web API application
// Think of this as the "main()" function that starts everything up
// In .NET, this file configures the web server, registers services, and starts the application

using CocktailApi.Services;
using CocktailApi.Data;

// Create a "builder" object that will help us configure our web application
// This is the modern .NET way to set up a web API
var builder = WebApplication.CreateBuilder(args);

// ============================================================================
// SERVICE REGISTRATION SECTION
// ============================================================================
// This is where we tell .NET what services our application needs
// Services are like "tools" that different parts of our app can use

// Add the basic services that every .NET Web API needs
builder.Services.AddControllers();        // Enables API controllers (handles HTTP requests)
builder.Services.AddEndpointsApiExplorer(); // Enables endpoint discovery
builder.Services.AddSwaggerGen();         // Generates Swagger documentation (API docs)

// ============================================================================
// CUSTOM SERVICE REGISTRATION
// ============================================================================
// Register our own custom services that we created for this cocktail app

// Add SQL Server connection services
// "Scoped" means a new instance is created for each HTTP request
builder.Services.AddScoped<ICocktailService, CocktailService>();  // Our cocktail business logic
builder.Services.AddScoped<IDatabaseService, DatabaseService>();  // Our database operations

// Add HttpClient for external API calls
// This allows our app to make HTTP requests to TheCocktailDB API
builder.Services.AddHttpClient();

// ============================================================================
// CORS CONFIGURATION
// ============================================================================
// CORS = Cross-Origin Resource Sharing
// This allows web pages from other domains to call our API
// Important for frontend applications that run on different ports/domains

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()      // Allow requests from any website
                   .AllowAnyMethod()      // Allow GET, POST, PUT, DELETE, etc.
                   .AllowAnyHeader();     // Allow any HTTP headers
        });
});

// ============================================================================
// BUILD THE APPLICATION
// ============================================================================
// Now we create the actual web application from our configuration
var app = builder.Build();

// ============================================================================
// MIDDLEWARE PIPELINE CONFIGURATION
// ============================================================================
// Middleware are like "filters" that process each HTTP request
// They run in order, so the order matters!

// Only show Swagger in development (not in production)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();        // Enable Swagger JSON endpoint
    app.UseSwaggerUI();      // Enable Swagger web interface
}

// Redirect HTTP requests to HTTPS for security
app.UseHttpsRedirection();

// Enable CORS (must come before routing)
app.UseCors("AllowAll");

// Enable authorization (for future use - not implemented yet)
app.UseAuthorization();

// Map our API controllers to URL routes
// This tells .NET which URLs should go to which controller methods
app.MapControllers();

// ============================================================================
// START THE APPLICATION
// ============================================================================
// This starts the web server and begins listening for HTTP requests
// The app will keep running until you stop it (Ctrl+C)
app.Run(); 