import React from 'react'

// 🔘 This is a BUTTON COMPONENT - it handles user clicks
// Components can receive "props" (properties) to customize their behavior
// This button can be reused anywhere you need a "fetch cocktail" button

interface RandomCocktailButtonProps {
  onClick: () => void    // Function to call when button is clicked
  loading: boolean       // Whether to show loading state
}

export const RandomCocktailButton: React.FC<RandomCocktailButtonProps> = ({ onClick, loading }) => {
  return (
    <button
      onClick={onClick}                    // Call the function when clicked
      disabled={loading}                   // Disable button while loading
      // 🎨 Tailwind CSS classes for styling:
      // px-8 py-4 = padding, bg-orange-500 = orange background
      // hover:bg-yellow-400 = yellow background on hover
      // transition-all duration-300 = smooth color change
      // hover:scale-105 = slightly bigger on hover
      // disabled:opacity-50 = half transparent when disabled
      className="px-8 py-4 bg-orange-500 hover:bg-yellow-400 text-black font-bold text-xl rounded-lg shadow-lg transition-all duration-300 transform hover:scale-105 disabled:opacity-50 disabled:cursor-not-allowed"
    >
              {/* Show different text based on loading state */}
      {loading ? 'Loading...' : 'Random Cocktail'}
    </button>
  )
} 