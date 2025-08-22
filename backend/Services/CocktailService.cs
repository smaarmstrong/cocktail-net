using CocktailApi.Data;
using CocktailApi.Models;
using System.Text.Json;

namespace CocktailApi.Services;

public class CocktailService : ICocktailService
{
    private readonly IDatabaseService _databaseService;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<CocktailService> _logger;

    public CocktailService(IDatabaseService databaseService, IHttpClientFactory httpClientFactory, ILogger<CocktailService> logger)
    {
        _databaseService = databaseService;
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task<List<Cocktail>> GetAllCocktailsAsync()
    {
        return await _databaseService.GetAllCocktailsAsync();
    }

    public async Task<Cocktail?> GetCocktailByIdAsync(int id)
    {
        return await _databaseService.GetCocktailByIdAsync(id);
    }

    public async Task<Cocktail> CreateCocktailAsync(Cocktail cocktail)
    {
        return await _databaseService.CreateCocktailAsync(cocktail);
    }

    public async Task<Cocktail> UpdateCocktailAsync(Cocktail cocktail)
    {
        return await _databaseService.UpdateCocktailAsync(cocktail);
    }

    public async Task<bool> DeleteCocktailAsync(int id)
    {
        return await _databaseService.DeleteCocktailAsync(id);
    }

    public async Task<List<Cocktail>> SearchCocktailsAsync(string searchTerm)
    {
        return await _databaseService.SearchCocktailsAsync(searchTerm);
    }

    public async Task<List<Cocktail>> FetchFromExternalApiAsync()
    {
        try
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync("https://www.thecocktaildb.com/api/json/v1/1/random.php");
            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            var externalData = JsonSerializer.Deserialize<ExternalCocktailResponse>(content);
            
            var cocktails = new List<Cocktail>();
            
            if (externalData?.Drinks != null)
            {
                foreach (var drink in externalData.Drinks)
                {
                    var cocktail = new Cocktail
                    {
                        Name = drink.StrDrink ?? "",
                        Category = drink.StrCategory ?? "",
                        Alcoholic = drink.StrAlcoholic ?? "",
                        Glass = drink.StrGlass ?? "",
                        Instructions = drink.StrInstructions ?? "",
                        ImageUrl = drink.StrDrinkThumb ?? "",
                        CreatedAt = DateTime.UtcNow
                    };

                    // Add ingredients
                    for (int i = 1; i <= 15; i++)
                    {
                        var ingredientName = GetPropertyValue(drink, $"StrIngredient{i}");
                        var measure = GetPropertyValue(drink, $"StrMeasure{i}");
                        
                        if (!string.IsNullOrEmpty(ingredientName) && ingredientName != "null")
                        {
                            cocktail.Ingredients.Add(new Ingredient
                            {
                                Name = ingredientName,
                                Measure = measure ?? "",
                                CocktailId = 0
                            });
                        }
                    }

                    cocktails.Add(cocktail);
                }
            }

            return cocktails;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching from external API");
            return new List<Cocktail>();
        }
    }

    private static string? GetPropertyValue(object obj, string propertyName)
    {
        var property = obj.GetType().GetProperty(propertyName);
        return property?.GetValue(obj)?.ToString();
    }
}

// External API response models
public class ExternalCocktailResponse
{
    public List<ExternalDrink>? Drinks { get; set; }
}

public class ExternalDrink
{
    public string? StrDrink { get; set; }
    public string? StrCategory { get; set; }
    public string? StrAlcoholic { get; set; }
    public string? StrGlass { get; set; }
    public string? StrInstructions { get; set; }
    public string? StrDrinkThumb { get; set; }
    public string? StrIngredient1 { get; set; }
    public string? StrIngredient2 { get; set; }
    public string? StrIngredient3 { get; set; }
    public string? StrIngredient4 { get; set; }
    public string? StrIngredient5 { get; set; }
    public string? StrIngredient6 { get; set; }
    public string? StrIngredient7 { get; set; }
    public string? StrIngredient8 { get; set; }
    public string? StrIngredient9 { get; set; }
    public string? StrIngredient10 { get; set; }
    public string? StrIngredient11 { get; set; }
    public string? StrIngredient12 { get; set; }
    public string? StrIngredient13 { get; set; }
    public string? StrIngredient14 { get; set; }
    public string? StrIngredient15 { get; set; }
    public string? StrMeasure1 { get; set; }
    public string? StrMeasure2 { get; set; }
    public string? StrMeasure3 { get; set; }
    public string? StrMeasure4 { get; set; }
    public string? StrMeasure5 { get; set; }
    public string? StrMeasure6 { get; set; }
    public string? StrMeasure7 { get; set; }
    public string? StrMeasure8 { get; set; }
    public string? StrMeasure9 { get; set; }
    public string? StrMeasure10 { get; set; }
    public string? StrMeasure11 { get; set; }
    public string? StrMeasure12 { get; set; }
    public string? StrMeasure13 { get; set; }
    public string? StrMeasure14 { get; set; }
    public string? StrMeasure15 { get; set; }
} 