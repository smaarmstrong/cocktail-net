import React from 'react'

// This is an ERROR MESSAGE COMPONENT
// It only shows when there's an error, otherwise it shows nothing
// This is a common pattern in React - "conditional rendering"

interface ErrorMessageProps {
  error: string | null  // The error message to display, or null if no error
}

export const ErrorMessage: React.FC<ErrorMessageProps> = ({ error }) => {
  // 🚫 If there's no error, don't show anything (return null)
  // This is called "early return" - a common React pattern
  if (!error) return null

      // Show the error message with red styling
  return (
    <div className="bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded mb-4">
      <strong className="font-bold">Error: </strong>
      <span className="block sm:inline">{error}</span>
    </div>
  )
} 