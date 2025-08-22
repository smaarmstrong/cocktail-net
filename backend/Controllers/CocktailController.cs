// ============================================================================
// CONTROLLER - HTTP Request Handler
// ============================================================================
// Controllers are the "traffic cops" of your API - they receive HTTP requests
// and decide what to do with them. Think of them as the "front desk" of your application.
//
// Each controller method handles a specific type of HTTP request (GET, POST, PUT, DELETE)
// and returns the appropriate response to the client.
// ============================================================================

using Microsoft.AspNetCore.Mvc;
using CocktailApi.Models;
using CocktailApi.Services;

namespace CocktailApi.Controllers;

// This attribute tells .NET this is an API controller
// It enables automatic model validation, JSON serialization, and HTTP status codes
[ApiController]

// This attribute defines the base URL route for all methods in this controller
// So all endpoints will start with /api/cocktail
[Route("api/[controller]")]

// ControllerBase gives us access to HTTP-specific features like:
// - Ok() for 200 responses
// - NotFound() for 404 responses
// - BadRequest() for 400 responses
// - StatusCode() for custom status codes
public class CocktailController : ControllerBase
{
    // ============================================================================
    // DEPENDENCY INJECTION
    // ============================================================================
    // These are services that this controller needs to do its job
    // .NET will automatically provide these when the controller is created
    // This is called "Dependency Injection" - a way to get the tools you need
    
    private readonly ICocktailService _cocktailService;  // Handles business logic
    private readonly ILogger<CocktailController> _logger; // Logs errors and info

    // Constructor - this runs when a new controller is created
    // The parameters are automatically provided by .NET's dependency injection system
    public CocktailController(ICocktailService cocktailService, ILogger<CocktailController> logger)
    {
        _cocktailService = cocktailService;
        _logger = logger;
    }

    // ============================================================================
    // GET ALL COCKTAILS
    // ============================================================================
    // HTTP GET /api/cocktail
    // Returns a list of all cocktails in the database
    
    [HttpGet]  // This method handles GET requests to /api/cocktail
    public async Task<ActionResult<List<Cocktail>>> GetAllCocktails()
    {
        try
        {
            // Call the service to get all cocktails
            var cocktails = await _cocktailService.GetAllCocktailsAsync();
            
            // Return 200 OK with the list of cocktails
            return Ok(cocktails);
        }
        catch (Exception ex)
        {
            // Log the error for debugging
            _logger.LogError(ex, "Error getting all cocktails");
            
            // Return 500 Internal Server Error
            return StatusCode(500, "Internal server error");
        }
    }

    // ============================================================================
    // GET COCKTAIL BY ID
    // ============================================================================
    // HTTP GET /api/cocktail/{id}
    // Returns a specific cocktail by its ID number
    
    [HttpGet("{id}")]  // The {id} part is a route parameter - it gets the value from the URL
    public async Task<ActionResult<Cocktail>> GetCocktailById(int id)
    {
        try
        {
            // Try to find the cocktail with the given ID
            var cocktail = await _cocktailService.GetCocktailByIdAsync(id);
            
            // If no cocktail was found, return 404 Not Found
            if (cocktail == null)
                return NotFound($"Cocktail with ID {id} not found");

            // Return 200 OK with the cocktail data
            return Ok(cocktail);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting cocktail by ID {Id}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    // ============================================================================
    // CREATE NEW COCKTAIL
    // ============================================================================
    // HTTP POST /api/cocktail
    // Creates a new cocktail in the database
    
    [HttpPost]  // This method handles POST requests to /api/cocktail
    public async Task<ActionResult<Cocktail>> CreateCocktail([FromBody] Cocktail cocktail)
    {
        try
        {
            // Check if the data sent by the client is valid
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Call the service to create the cocktail
            var createdCocktail = await _cocktailService.CreateCocktailAsync(cocktail);
            
            // Return 201 Created with the new cocktail data
            // The CreatedAtAction tells the client where to find the new resource
            return CreatedAtAction(nameof(GetCocktailById), new { id = createdCocktail.Id }, createdCocktail);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating cocktail");
            return StatusCode(500, "Internal server error");
        }
    }

    // ============================================================================
    // UPDATE EXISTING COCKTAIL
    // ============================================================================
    // HTTP PUT /api/cocktail/{id}
    // Updates an existing cocktail in the database
    
    [HttpPut("{id}")]  // PUT requests are typically used for updates
    public async Task<ActionResult<Cocktail>> UpdateCocktail(int id, [FromBody] Cocktail cocktail)
    {
        try
        {
            // Make sure the ID in the URL matches the ID in the request body
            if (id != cocktail.Id)
                return BadRequest("ID mismatch");

            // Validate the data
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Call the service to update the cocktail
            var updatedCocktail = await _cocktailService.UpdateCocktailAsync(cocktail);
            
            // Return 200 OK with the updated data
            return Ok(updatedCocktail);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating cocktail with ID {Id}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    // ============================================================================
    // DELETE COCKTAIL
    // ============================================================================
    // HTTP DELETE /api/cocktail/{id}
    // Removes a cocktail from the database
    
    [HttpDelete("{id}")]  // DELETE requests remove resources
    public async Task<ActionResult> DeleteCocktail(int id)
    {
        try
        {
            // Try to delete the cocktail
            var deleted = await _cocktailService.DeleteCocktailAsync(id);
            
            // If no cocktail was found to delete, return 404
            if (!deleted)
                return NotFound($"Cocktail with ID {id} not found");

            // Return 204 No Content (successful deletion, no data to return)
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting cocktail with ID {Id}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    // ============================================================================
    // SEARCH COCKTAILS
    // ============================================================================
    // HTTP GET /api/cocktail/search?q={searchTerm}
    // Searches for cocktails by name, category, or instructions
    
    [HttpGet("search")]  // This creates the route /api/cocktail/search
    public async Task<ActionResult<List<Cocktail>>> SearchCocktails([FromQuery] string q)
    {
        try
        {
            // Make sure the search term is provided
            if (string.IsNullOrWhiteSpace(q))
                return BadRequest("Search term is required");

            // Call the service to search for cocktails
            var cocktails = await _cocktailService.SearchCocktailsAsync(q);
            
            // Return 200 OK with the search results
            return Ok(cocktails);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching cocktails with term {SearchTerm}", q);
            return StatusCode(500, "Internal server error");
        }
    }

    // ============================================================================
    // FETCH FROM EXTERNAL API
    // ============================================================================
    // HTTP GET /api/cocktail/fetch-external
    // Gets random cocktails from TheCocktailDB external API
    
    [HttpGet("fetch-external")]  // This creates the route /api/cocktail/fetch-external
    public async Task<ActionResult<List<Cocktail>>> FetchFromExternalApi()
    {
        try
        {
            // Call the service to fetch from external API
            var cocktails = await _cocktailService.FetchFromExternalApiAsync();
            
            // Return 200 OK with the external data
            return Ok(cocktails);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching from external API");
            return StatusCode(500, "Internal server error");
        }
    }
} 