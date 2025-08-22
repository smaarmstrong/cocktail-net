import { useState, useEffect } from 'react'
import type { Cocktail } from '../types/cocktail'
import { CocktailApiService } from '../services/cocktailApi'

export const useCocktail = () => {
  const [cocktail, setCocktail] = useState<Cocktail | null>(null)
  const [loading, setLoading] = useState<boolean>(false)
  const [error, setError] = useState<string | null>(null)

  const fetchRandomCocktail = async (): Promise<void> => {
    setLoading(true)
    setError(null)
    
    try {
      const cocktailData = await CocktailApiService.fetchRandomCocktail()
      setCocktail(cocktailData)
    } catch (err) {
      const errorMessage = err instanceof Error ? err.message : 'Failed to fetch cocktail'
      setError(errorMessage)
      console.error('Error fetching cocktail:', err)
    } finally {
      setLoading(false)
    }
  }

  useEffect(() => {
    fetchRandomCocktail()
  }, [])

  return {
    cocktail,
    loading,
    error,
    fetchRandomCocktail
  }
} 