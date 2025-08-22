import type { Cocktail, CocktailApiResponse } from '../types/cocktail'

// This is a SERVICE - it handles communication with external APIs
// Services are like "messengers" that talk to other websites/services
// They keep the API logic separate from your React components

const API_BASE_URL = 'https://www.thecocktaildb.com/api/json/v1/1'

export class CocktailApiService {
  // This method fetches a random cocktail from TheCocktailDB API
  // static means you can call it without creating an instance: CocktailApiService.fetchRandomCocktail()
  // async means "this might take time, wait for it to finish"
  // Promise<Cocktail> means "this will eventually return a Cocktail object"
  static async fetchRandomCocktail(): Promise<Cocktail> {
    // Make an HTTP request to the API
    const response = await fetch(`${API_BASE_URL}/random.php`)
    
    // Check if the request failed (like 404, 500 errors)
    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`)
    }
    
    // 📦 Convert the response from JSON text to JavaScript objects
    // as CocktailApiResponse tells TypeScript "trust me, this is the right shape"
    const data = await response.json() as CocktailApiResponse
    
    // 🚫 Check if we got valid data
    if (!data.drinks || data.drinks.length === 0) {
      throw new Error('No cocktail data received')
    }
    
    // 🍸 Get the first (and only) cocktail from the array
    const cocktail = data.drinks[0]
    if (!cocktail) {
      throw new Error('No cocktail data received')
    }
    
    // Return the cocktail data
    return cocktail
  }
} 