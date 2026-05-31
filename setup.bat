@echo off
REM Setup script for LearnAnomalyDetect

echo ========================================
echo LearnAnomalyDetect - Setup Script
echo ========================================

REM Check .NET installation
echo.
echo Checking .NET installation...
dotnet --version
if errorlevel 1 (
    echo ERROR: .NET SDK not found. Please install .NET 6+ SDK
    exit /b 1
)

REM Restore backend dependencies
echo.
echo Installing backend dependencies...
cd Backend\LearnAnomalyAPI
dotnet restore
if errorlevel 1 (
    echo ERROR: Failed to restore NuGet packages
    exit /b 1
)
cd ..\..

REM Check MongoDB
echo.
echo Checking MongoDB connection...
REM MongoDB check would require mongosh or direct connection attempt
echo MongoDB setup: Please ensure MongoDB is running on localhost:27017
echo Or update connection string in appsettings.json

echo.
echo ========================================
echo Setup Complete!
echo ========================================
echo.
echo Next steps:
echo 1. Start MongoDB: mongod
echo 2. Run backend: cd Backend\LearnAnomalyAPI && dotnet run
echo 3. Run frontend: cd Frontend && http-server -p 8080
echo.
echo API: https://localhost:5000
echo Frontend: http://localhost:8080
echo.
pause
