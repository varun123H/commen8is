#!/bin/bash

# Setup script for LearnAnomalyDetect (Linux/Mac)

echo "========================================"
echo "LearnAnomalyDetect - Setup Script"
echo "========================================"

# Check .NET installation
echo ""
echo "Checking .NET installation..."
if ! command -v dotnet &> /dev/null; then
    echo "ERROR: .NET SDK not found. Please install .NET 6+ SDK"
    exit 1
fi
dotnet --version

# Restore backend dependencies
echo ""
echo "Installing backend dependencies..."
cd Backend/LearnAnomalyAPI || exit 1
dotnet restore
if [ $? -ne 0 ]; then
    echo "ERROR: Failed to restore NuGet packages"
    exit 1
fi
cd ../../

# Check MongoDB
echo ""
echo "Checking MongoDB connection..."
echo "MongoDB setup: Please ensure MongoDB is running on localhost:27017"
echo "Or update connection string in appsettings.json"

echo ""
echo "========================================"
echo "Setup Complete!"
echo "========================================"
echo ""
echo "Next steps:"
echo "1. Start MongoDB: mongod"
echo "2. Run backend: cd Backend/LearnAnomalyAPI && dotnet run"
echo "3. Run frontend: cd Frontend && http-server -p 8080"
echo ""
echo "API: https://localhost:5000"
echo "Frontend: http://localhost:8080"
echo ""
