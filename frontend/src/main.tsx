/// <reference types="vite/client" />
import React from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App'

// This is the ENTRY POINT of our React app
// createRoot() creates a React root container that will hold our entire app
// document.getElementById('root') finds the HTML element with id="root" in index.html
// The ! tells TypeScript "trust me, this element exists"
const root = createRoot(document.getElementById('root')!)

// root.render() puts our React app into the HTML page
// React.StrictMode is a development tool that helps catch potential problems
root.render(
  <React.StrictMode>
    <App />
  </React.StrictMode>
) 