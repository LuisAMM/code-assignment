<#
.SYNOPSIS
    Runs the Currency Converter locally: backend with `dotnet run`, frontend with `ng serve`.

.DESCRIPTION
    Starts the .NET backend in a separate PowerShell window, then installs the
    frontend packages (if needed) and starts the Angular dev server in this window.
    Press Ctrl+C to stop the frontend; close the extra window to stop the backend.
#>

$ErrorActionPreference = 'Stop'

# Start the backend in its own window
Write-Host "Starting backend (http://localhost:5241)..." -ForegroundColor Cyan
Start-Process powershell -ArgumentList '-NoExit', '-Command', "dotnet run --project `"$PSScriptRoot\backend\Backend.Api`""

# Install frontend dependencies and start the dev server
Push-Location "$PSScriptRoot\frontend"
try {
    Write-Host "Installing frontend packages..." -ForegroundColor Cyan
    npm install

    Write-Host "Starting frontend (http://localhost:4200)..." -ForegroundColor Green
    npm start
}
finally {
    Pop-Location
}
