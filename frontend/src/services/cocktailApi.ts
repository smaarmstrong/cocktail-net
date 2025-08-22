import type { Cocktail, CocktailApiResponse } from '../types/cocktail'

const API_BASE_URL = 'https://www.thecocktaildb.com/api/json/v1/1'

export class CocktailApiService {
  static async fetchRandomCocktail(): Promise<Cocktail> {
    const response = await fetch(`${API_BASE_URL}/random.php`)
    
    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`)
    }
    
    const data = await response.json() as CocktailApiResponse
    
    if (!data.drinks || data.drinks.length === 0) {
      throw new Error('No cocktail data received')
    }
    
    const cocktail = data.drinks[0]
    if (!cocktail) {
      throw new Error('No cocktail data received')
    }
    
    return cocktail
  }
} 