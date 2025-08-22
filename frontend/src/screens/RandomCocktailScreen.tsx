import React from 'react'
import { useCocktail } from '../hooks/useCocktail'
import { CocktailDisplay } from '../components/CocktailDisplay'
import { RandomCocktailButton } from '../components/RandomCocktailButton'
import { ErrorMessage } from '../components/ErrorMessage'

export const RandomCocktailScreen: React.FC = () => {
  const { cocktail, loading, error, fetchRandomCocktail } = useCocktail()

  return (
    <div className="min-h-screen bg-gradient-to-br from-red-900 via-red-800 to-red-700 flex flex-col items-center justify-center p-8">
      <div className="text-center space-y-8 max-w-2xl">
        {/* Title */}
        <h1 className="text-5xl font-bold text-white mb-8">
          Random Cocktail
        </h1>

        {/* Error Message */}
        <ErrorMessage error={error} />

        {/* Cocktail Display */}
        <CocktailDisplay cocktail={cocktail} loading={loading} />

        {/* Random Cocktail Button */}
        <RandomCocktailButton onClick={fetchRandomCocktail} loading={loading} />
      </div>
    </div>
  )
} 