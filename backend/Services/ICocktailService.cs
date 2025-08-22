using CocktailApi.Models;

namespace CocktailApi.Services;

public interface ICocktailService
{
    Task<List<Cocktail>> GetAllCocktailsAsync();
    Task<Cocktail?> GetCocktailByIdAsync(int id);
    Task<Cocktail> CreateCocktailAsync(Cocktail cocktail);
    Task<Cocktail> UpdateCocktailAsync(Cocktail cocktail);
    Task<bool> DeleteCocktailAsync(int id);
    Task<List<Cocktail>> SearchCocktailsAsync(string searchTerm);
    Task<List<Cocktail>> FetchFromExternalApiAsync();
} 