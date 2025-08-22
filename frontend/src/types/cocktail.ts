// This file defines the SHAPE of our data using TypeScript
// Think of interfaces as "blueprints" that describe what data looks like
// This helps catch errors early and makes your code more reliable

// 🍸 This describes what a cocktail object looks like
export interface Cocktail {
  strDrink: string        // The name of the cocktail (required)
  strDrinkThumb: string   // URL to the cocktail image (required)
  strCategory?: string    // Category (optional - the ? means it might not exist)
  strAlcoholic?: string   // Whether it's alcoholic (optional)
  strGlass?: string       // What glass to serve it in (optional)
  strInstructions?: string // How to make it (optional)
}

// This describes what the API response looks like
export interface CocktailApiResponse {
  drinks: Cocktail[]      // An array of cocktails (the API always returns an array)
} 