using System.Data;
using System.Data.SqlClient;
using CocktailApi.Models;

namespace CocktailApi.Data;

public class DatabaseService : IDatabaseService
{
    private readonly string _connectionString;

    public DatabaseService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection") 
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    }

    public async Task<List<Cocktail>> GetAllCocktailsAsync()
    {
        var cocktails = new List<Cocktail>();
        
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("sp_GetAllCocktails", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();
        
        while (await reader.ReadAsync())
        {
            var cocktail = new Cocktail
            {
                Id = reader.GetInt32("Id"),
                Name = reader.GetString("Name"),
                Category = reader.GetString("Category"),
                Alcoholic = reader.GetString("Alcoholic"),
                Glass = reader.GetString("Glass"),
                Instructions = reader.GetString("Instructions"),
                ImageUrl = reader.GetString("ImageUrl"),
                CreatedAt = reader.GetDateTime("CreatedAt"),
                UpdatedAt = reader.IsDBNull("UpdatedAt") ? null : reader.GetDateTime("UpdatedAt")
            };
            cocktails.Add(cocktail);
        }

        return cocktails;
    }

    public async Task<Cocktail?> GetCocktailByIdAsync(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("sp_GetCocktailById", connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        
        command.Parameters.AddWithValue("@Id", id);

        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();
        
        if (await reader.ReadAsync())
        {
            var cocktail = new Cocktail
            {
                Id = reader.GetInt32("Id"),
                Name = reader.GetString("Name"),
                Category = reader.GetString("Category"),
                Alcoholic = reader.GetString("Alcoholic"),
                Glass = reader.GetString("Glass"),
                Instructions = reader.GetString("Instructions"),
                ImageUrl = reader.GetString("ImageUrl"),
                CreatedAt = reader.GetDateTime("CreatedAt"),
                UpdatedAt = reader.IsDBNull("UpdatedAt") ? null : reader.GetDateTime("UpdatedAt")
            };

            // Get ingredients
            await reader.NextResultAsync();
            while (await reader.ReadAsync())
            {
                cocktail.Ingredients.Add(new Ingredient
                {
                    Id = reader.GetInt32("Id"),
                    Name = reader.GetString("Name"),
                    Measure = reader.GetString("Measure"),
                    CocktailId = cocktail.Id
                });
            }

            return cocktail;
        }

        return null;
    }

    public async Task<Cocktail> CreateCocktailAsync(Cocktail cocktail)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("sp_CreateCocktail", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.AddWithValue("@Name", cocktail.Name);
        command.Parameters.AddWithValue("@Category", cocktail.Category);
        command.Parameters.AddWithValue("@Alcoholic", cocktail.Alcoholic);
        command.Parameters.AddWithValue("@Glass", cocktail.Glass);
        command.Parameters.AddWithValue("@Instructions", cocktail.Instructions);
        command.Parameters.AddWithValue("@ImageUrl", cocktail.ImageUrl);

        await connection.OpenAsync();
        var result = await command.ExecuteScalarAsync();
        
        if (result != null)
        {
            cocktail.Id = Convert.ToInt32(result);
            cocktail.CreatedAt = DateTime.UtcNow;
        }

        return cocktail;
    }

    public async Task<Cocktail> UpdateCocktailAsync(Cocktail cocktail)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("sp_UpdateCocktail", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.AddWithValue("@Id", cocktail.Id);
        command.Parameters.AddWithValue("@Name", cocktail.Name);
        command.Parameters.AddWithValue("@Category", cocktail.Category);
        command.Parameters.AddWithValue("@Alcoholic", cocktail.Alcoholic);
        command.Parameters.AddWithValue("@Glass", cocktail.Glass);
        command.Parameters.AddWithValue("@Instructions", cocktail.Instructions);
        command.Parameters.AddWithValue("@ImageUrl", cocktail.ImageUrl);

        await connection.OpenAsync();
        await command.ExecuteNonQueryAsync();
        
        cocktail.UpdatedAt = DateTime.UtcNow;
        return cocktail;
    }

    public async Task<bool> DeleteCocktailAsync(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("sp_DeleteCocktail", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.AddWithValue("@Id", id);

        await connection.OpenAsync();
        var rowsAffected = await command.ExecuteNonQueryAsync();
        
        return rowsAffected > 0;
    }

    public async Task<List<Cocktail>> SearchCocktailsAsync(string searchTerm)
    {
        var cocktails = new List<Cocktail>();
        
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("sp_SearchCocktails", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.AddWithValue("@SearchTerm", searchTerm);

        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();
        
        while (await reader.ReadAsync())
        {
            var cocktail = new Cocktail
            {
                Id = reader.GetInt32("Id"),
                Name = reader.GetString("Name"),
                Category = reader.GetString("Category"),
                Alcoholic = reader.GetString("Alcoholic"),
                Glass = reader.GetString("Glass"),
                Instructions = reader.GetString("Instructions"),
                ImageUrl = reader.GetString("ImageUrl"),
                CreatedAt = reader.GetDateTime("CreatedAt"),
                UpdatedAt = reader.IsDBNull("UpdatedAt") ? null : reader.GetDateTime("UpdatedAt")
            };
            cocktails.Add(cocktail);
        }

        return cocktails;
    }
} 