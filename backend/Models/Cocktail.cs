// ============================================================================
// MODELS - Data Structure Definitions
// ============================================================================
// Models define what our data looks like - think of them as "templates" or "blueprints"
// for the objects we'll work with in our application.
//
// In .NET, models are typically simple classes that just hold data (properties)
// They don't contain business logic - that goes in Services
// ============================================================================

namespace CocktailApi.Models;

// This class represents a Cocktail in our system
// It's like a "recipe card" that contains all the information about a drink
public class Cocktail
{
    // ============================================================================
    // PROPERTIES - The data that makes up a cocktail
    // ============================================================================
    // Properties are like "fields" or "attributes" of an object
    // They define what information we can store about each cocktail
    
    // Primary key - unique identifier for each cocktail
    // In SQL Server, this will be an auto-incrementing number
    public int Id { get; set; }
    
    // The name of the cocktail (e.g., "Mojito", "Old Fashioned")
    // string.Empty is a safe default value (empty string)
    public string Name { get; set; } = string.Empty;
    
    // What type of drink this is (e.g., "Cocktail", "Shot", "Mocktail")
    public string Category { get; set; } = string.Empty;
    
    // Whether it contains alcohol ("Alcoholic", "Non-alcoholic", "Optional alcohol")
    public string Alcoholic { get; set; } = string.Empty;
    
    // What type of glass to serve it in (e.g., "Highball glass", "Martini glass")
    public string Glass { get; set; } = string.Empty;
    
    // Step-by-step instructions for making the drink
    // NVARCHAR(MAX) in SQL Server can hold very long text
    public string Instructions { get; set; } = string.Empty;
    
    // URL to an image of the cocktail
    public string ImageUrl { get; set; } = string.Empty;
    
    // List of ingredients needed for this cocktail
    // This creates a relationship: one cocktail can have many ingredients
    // The "new()" creates an empty list by default
    public List<Ingredient> Ingredients { get; set; } = new();
    
    // When this cocktail was first added to our database
    // DateTime.UtcNow will be set when we create the cocktail
    public DateTime CreatedAt { get; set; }
    
    // When this cocktail was last modified (nullable means it can be null)
    // Will be null until someone updates the cocktail
    public DateTime? UpdatedAt { get; set; }
}

// This class represents an individual ingredient in a cocktail
// Think of it as one line item on a recipe (e.g., "2 oz White Rum")
public class Ingredient
{
    // Unique identifier for each ingredient
    public int Id { get; set; }
    
    // Name of the ingredient (e.g., "White Rum", "Lime Juice", "Mint Leaves")
    public string Name { get; set; } = string.Empty;
    
    // How much of this ingredient to use (e.g., "2 oz", "1 tsp", "6-8 leaves")
    public string Measure { get; set; } = string.Empty;
    
    // Which cocktail this ingredient belongs to
    // This is a "foreign key" - it links to the Cocktails table
    // When we delete a cocktail, all its ingredients will be deleted too (cascade)
    public int CocktailId { get; set; }
} 