#!/bin/bash

echo "Cocktail API Test Script"
echo "========================"

# Wait for API to be ready
echo "Waiting for API to be ready..."
sleep 5

# Test base endpoint
echo "Testing base endpoint..."
curl -s -o /dev/null -w "Base endpoint: %{http_code}\n" http://localhost:5000/api/cocktail

# Test Swagger
echo "Testing Swagger endpoint..."
curl -s -o /dev/null -w "Swagger: %{http_code}\n" http://localhost:5000/swagger

# Test external API fetch
echo "Testing external API fetch..."
curl -s -o /dev/null -w "External API: %{http_code}\n" http://localhost:5000/api/cocktail/fetch-external

# Test search
echo "Testing search endpoint..."
curl -s -o /dev/null -w "Search endpoint: %{http_code}\n" "http://localhost:5000/api/cocktail/search?q=mint"

echo ""
echo "API test completed!"
echo "Visit http://localhost:5000/swagger for interactive documentation" 