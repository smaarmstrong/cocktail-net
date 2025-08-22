import { useState, useEffect } from 'react'
import type { Cocktail } from '../types/cocktail'
import { CocktailApiService } from '../services/cocktailApi'

// This is a CUSTOM HOOK - it's like a reusable function that manages state
// Hooks always start with "use" and can only be used inside React components
// Think of hooks as "data managers" that components can use
export const useCocktail = () => {
  // STATE VARIABLES - these hold data that can change over time
  // useState() creates a variable and a function to update it
  // The <Cocktail | null> means "this can be a Cocktail object OR nothing"
  const [cocktail, setCocktail] = useState<Cocktail | null>(null)
  const [loading, setLoading] = useState<boolean>(false)  // true/false for loading state
  const [error, setError] = useState<string | null>(null) // error message or nothing

  // This function fetches a new random cocktail from the API
  // async/await means "wait for the API to respond before continuing"
  const fetchRandomCocktail = async (): Promise<void> => {
    setLoading(true)    // Show loading spinner
    setError(null)      // Clear any previous errors
    
    try {
      // Call the API service to get a random cocktail
      const cocktailData = await CocktailApiService.fetchRandomCocktail()
      setCocktail(cocktailData)  // Save the cocktail data
    } catch (err) {
      // If something goes wrong, show an error message
      const errorMessage = err instanceof Error ? err.message : 'Failed to fetch cocktail'
      setError(errorMessage)
      console.error('Error fetching cocktail:', err)
    } finally {
      // Always hide the loading spinner, whether it succeeded or failed
      setLoading(false)
    }
  }

  // useEffect runs code when the component first loads
  // The empty array [] means "only run once when the component starts"
  // This automatically fetches a cocktail when the page loads
  useEffect(() => {
    fetchRandomCocktail()
  }, [])

  // Return the data and functions so components can use them
  // This is like "exporting" what the hook provides
  return {
    cocktail,              // The current cocktail data
    loading,               // Whether we're loading
    error,                 // Any error message
    fetchRandomCocktail    // Function to get a new cocktail
  }
} 