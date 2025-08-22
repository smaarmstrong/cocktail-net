using CocktailApi.Models;

namespace CocktailApi.Data;

public interface IDatabaseService
{
    Task<List<Cocktail>> GetAllCocktailsAsync();
    Task<Cocktail?> GetCocktailByIdAsync(int id);
    Task<Cocktail> CreateCocktailAsync(Cocktail cocktail);
    Task<Cocktail> UpdateCocktailAsync(Cocktail cocktail);
    Task<bool> DeleteCocktailAsync(int id);
    Task<List<Cocktail>> SearchCocktailsAsync(string searchTerm);
} 