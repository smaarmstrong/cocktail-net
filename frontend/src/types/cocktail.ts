export interface Cocktail {
  strDrink: string
  strDrinkThumb: string
  strCategory?: string
  strAlcoholic?: string
  strGlass?: string
  strInstructions?: string
}

export interface CocktailApiResponse {
  drinks: Cocktail[]
} 