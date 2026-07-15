# Currency Converter Application

## Overview

This repository contains a currency converter application with a frontend developed using Angular 21 (with SSR) and a backend written in .NET 8. The application converts a currency amount into its written-word representation (e.g. `12.34` → "twelve dollars and thirty-four cents"), with support for multiple languages (English and German).

## Folder Structure

- **frontend**: Angular 21 application (standalone components, Angular Material, server-side rendering via Express).
- **backend**: .NET 8 Web API solution containing:
  - `Backend.Api`: the API project (currency conversion endpoint at `GET /api/currency?amount=<decimal>&language=<En|De>`, Swagger UI in development).
  - `Backend.Test`: the unit test project.
- **run.ps1**: PowerShell script that starts both applications with a single command.

## Ways to run the application

### Option 1: PowerShell script (one command)

Prerequisites: .NET 8 SDK, Node.js and npm, Windows PowerShell.

From the repository root:

```powershell
.\run.ps1
```

The script starts the backend (`dotnet run`, http://localhost:5241) in a separate window, then runs `npm install` and starts the Angular dev server (http://localhost:4200) in the current one. Press `Ctrl+C` to stop the frontend and close the extra window to stop the backend.

### Option 2: Run each application manually

See [Frontend (local development)](#frontend-local-development) and [Backend (local development)](#backend-local-development) below.

## Frontend (local development)

The frontend application is built with Angular and consists of a single page that performs currency conversion.

### Prerequisites

- Node.js (v20+) and npm installed on your machine.

### Setup

1. Navigate to the `frontend` directory:
    ```sh
    cd frontend
    ```

2. Install the necessary packages:
    ```sh
    npm install
    ```

3. Run the application:
    ```sh
    npm start
    ```

4. Open your browser and navigate to `http://localhost:4200`.

> **Note:** The API base URL is configured in `src/environments/` and points to the backend at `http://localhost:5241/api/`, which is the fixed backend port.

### Running Frontend Tests

```sh
npm test
```

## Backend (local development)

The backend is developed using .NET 8 and includes the source code and tests for the backend logic.

### Prerequisites

- .NET 8 SDK installed on your machine.

### Setup

1. Navigate to the `backend` directory:
    ```sh
    cd backend
    ```

2. Restore the .NET packages:
    ```sh
    dotnet restore
    ```

3. Build the solution:
    ```sh
    dotnet build
    ```

4. Run the API project:
    ```sh
    dotnet run --project Backend.Api
    ```

The API always listens on `http://localhost:5241` (configured in both `Properties/launchSettings.json` and `appsettings.json`, so the port is the same whether you use `dotnet run`, an IDE, or run the published output). Swagger UI is available at `http://localhost:5241/swagger`.

### Running Backend Tests

To run the tests for the backend, from the `backend` directory:

```sh
dotnet test
```
