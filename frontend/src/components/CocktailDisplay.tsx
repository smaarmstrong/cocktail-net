import React from 'react'
import type { Cocktail } from '../types/cocktail'

// 🧩 This is a COMPONENT - a reusable piece of UI
// Components are like LEGO blocks that you can combine to build your app
// This component handles displaying the cocktail image and name

// INTERFACE - defines what data this component expects
// Think of it as a "contract" - the component promises to work with this data structure
interface CocktailDisplayProps {
  cocktail: Cocktail | null  // Can be a cocktail OR nothing
  loading: boolean           // true = show loading, false = show content
}

// React.FC<Props> means "this is a React Function Component that takes these props"
// Props are like "parameters" that get passed to the component
export const CocktailDisplay: React.FC<CocktailDisplayProps> = ({ cocktail, loading }) => {
  // CONDITIONAL RENDERING - show different things based on the state
  
  // 1. LOADING STATE - show a loading spinner
  if (loading) {
    return (
      <div className="flex justify-center">
        {/* 🎨 Tailwind classes: w-80 h-80 = size, bg-gray-300 = gray background */}
        {/* animate-pulse = loading animation, flex = center content */}
        <div className="w-80 h-80 bg-gray-300 rounded-lg animate-pulse flex items-center justify-center">
          <span className="text-gray-600">Loading...</span>
        </div>
      </div>
    )
  }

  // 2. NO COCKTAIL STATE - show a placeholder
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
        <h2 className="text-2xl font-semibold text-black text-center">
          {cocktail.strDrink}
        </h2>
      </div>
    </div>
  )
} 