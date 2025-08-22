import React from 'react'
import { RandomCocktailScreen } from './screens/RandomCocktailScreen'

// This is the MAIN APP COMPONENT
// Think of it as the "container" that holds everything else
// Right now it just renders the RandomCocktailScreen, but later you could add:
// - Navigation between different pages
// - A header/footer
// - Multiple screens
function App() {
  return <RandomCocktailScreen />
}

export default App 