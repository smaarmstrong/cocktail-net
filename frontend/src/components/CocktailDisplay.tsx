import React from 'react'
import type { Cocktail } from '../types/cocktail'

interface CocktailDisplayProps {
  cocktail: Cocktail | null
  loading: boolean
}

export const CocktailDisplay: React.FC<CocktailDisplayProps> = ({ cocktail, loading }) => {
  if (loading) {
    return (
      <div className="flex justify-center">
        <div className="w-80 h-80 bg-gray-300 rounded-lg animate-pulse flex items-center justify-center">
          <span className="text-gray-600">Loading...</span>
        </div>
      </div>
    )
  }

  if (!cocktail) {
    return (
      <div className="flex justify-center">
        <div className="w-80 h-80 bg-gray-300 rounded-lg flex items-center justify-center">
          <span className="text-gray-600">No cocktail loaded</span>
        </div>
      </div>
    )
  }

  return (
    <div className="flex justify-center">
      <div className="space-y-4">
        <img 
          src={cocktail.strDrinkThumb} 
          alt={cocktail.strDrink}
          className="w-80 h-80 object-cover rounded-lg shadow-2xl border-4 border-white"
        />
        <h2 className="text-2xl font-semibold text-white text-center">
          {cocktail.strDrink}
        </h2>
      </div>
    </div>
  )
} 