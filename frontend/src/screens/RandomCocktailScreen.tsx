import React from 'react'
import { useCocktail } from '../hooks/useCocktail'
import { CocktailDisplay } from '../components/CocktailDisplay'
import { RandomCocktailButton } from '../components/RandomCocktailButton'
import { ErrorMessage } from '../components/ErrorMessage'

// This is a SCREEN COMPONENT - it represents a full page
// Screens are made up of smaller components and use hooks for data
// Think of screens as "pages" in your app
export const RandomCocktailScreen: React.FC = () => {
  // This custom hook gives us all the cocktail data and functions we need
  // It's like a "data manager" for this screen
  const { cocktail, loading, error, fetchRandomCocktail } = useCocktail()

  // 🎨 This is the JSX that describes what the screen looks like
  // It's like HTML but with JavaScript superpowers
  return (
    // 🎨 Tailwind CSS classes for styling:
    // min-h-screen = full height, bg-gradient-to-br = diagonal gradient background
    // from-red-900 via-red-800 to-red-700 = dark red colors
    // flex flex-col = vertical layout, items-center = center horizontally
    <div className="min-h-screen bg-gradient-to-br from-red-900 via-red-800 to-red-700 flex flex-col items-center justify-center p-8">
      <div className="text-center space-y-8 max-w-2xl">
        {/* Title - this is a comment in JSX */}
        <h1 className="text-5xl font-bold text-white mb-8">
          Random Cocktail
        </h1>

        {/* Error Message - only shows when there's an error */}
        <ErrorMessage error={error} />

        {/* 🍸 Cocktail Display - shows the image and name */}
        <CocktailDisplay cocktail={cocktail} loading={loading} />

        {/* 🔘 Random Cocktail Button - fetches new cocktails */}
        <RandomCocktailButton onClick={fetchRandomCocktail} loading={loading} />
      </div>
    </div>
  )
} 