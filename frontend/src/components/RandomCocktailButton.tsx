import React from 'react'

interface RandomCocktailButtonProps {
  onClick: () => void
  loading: boolean
}

export const RandomCocktailButton: React.FC<RandomCocktailButtonProps> = ({ onClick, loading }) => {
  return (
    <button
      onClick={onClick}
      disabled={loading}
      className="px-8 py-4 bg-orange-500 hover:bg-yellow-400 text-white font-bold text-xl rounded-lg shadow-lg transition-all duration-300 transform hover:scale-105 disabled:opacity-50 disabled:cursor-not-allowed"
    >
      {loading ? 'Loading...' : 'Random Cocktail'}
    </button>
  )
} 