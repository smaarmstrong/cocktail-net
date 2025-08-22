# Cocktail Frontend

A React TypeScript application for fetching and displaying random cocktails from TheCocktailDB API.

## 🏗️ Project Structure

```
src/
├── components/          # 🧩 Reusable UI components
│   ├── CocktailDisplay.tsx      # Cocktail image & name display
│   ├── RandomCocktailButton.tsx # Orange→yellow hover button
│   ├── ErrorMessage.tsx         # Error display component
│   └── index.ts                 # Component exports
├── screens/             # 🖥️ Page-level components
│   └── RandomCocktailScreen.tsx # Main cocktail display screen
├── hooks/               # 🪝 Custom React hooks
│   └── useCocktail.ts           # State management logic
├── services/            # 🔌 API & business logic
│   └── cocktailApi.ts           # External API calls
├── types/               # 📝 TypeScript definitions
│   └── cocktail.ts              # Cocktail interface
├── utils/               # 🛠️ Utilities & constants
├── App.tsx              # 🎯 Main app component
└── main.tsx             # 🚀 Application entry point
```

## 🎨 Features

- **Random Cocktail Display**: Fetches and shows random cocktails from TheCocktailDB
- **Beautiful UI**: Dark red gradient background with modern styling
- **Interactive Button**: Orange button that turns yellow on hover
- **Loading States**: Smooth loading animations
- **Error Handling**: User-friendly error messages
- **Responsive Design**: Works on all screen sizes

## 🚀 Development

```bash
# Install dependencies
bun install

# Start development server
bun run dev

# Build for production
bun run build

# Preview production build
bun run preview
```

## 🛠️ Tech Stack

- **React 19** - Modern React with latest features
- **TypeScript** - Type-safe development
- **Vite** - Fast build tool and dev server
- **Tailwind CSS** - Utility-first CSS framework
- **Bun** - Fast JavaScript runtime and package manager

## 📱 Usage

1. The app automatically loads a random cocktail on startup
2. Click the "Random Cocktail" button to fetch a new cocktail
3. Enjoy beautiful cocktail images and names from around the world!

## 🔧 Architecture Benefits

- **Separation of Concerns**: Each component has a single responsibility
- **Reusability**: Components can be easily reused across the application
- **Maintainability**: Code is organized and easy to navigate
- **Type Safety**: Full TypeScript support with proper interfaces
- **Scalability**: Easy to add new features and components
