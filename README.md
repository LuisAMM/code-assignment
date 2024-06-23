# Project Title

Currency Converter Application

## Overview

This repository contains a simple currency converter application with a frontend developed using Angular and a backend written in .NET 8. The application allows users to convert currency values and display the results as text.

## Folder Structure

- **frontend**: Contains the Angular application for the frontend.
- **backend**: Contains the source code and tests for the backend logic written in .NET 8.

## Frontend

The frontend application is built with Angular and consists of a single page that performs currency conversion.

### Prerequisites

- Node.js and npm installed on your machine.

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
    ng serve
    ```

4. Open your browser and navigate to `http://localhost:4200`.

## Backend

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

3. Build the application:
    ```sh
    dotnet build
    ```

4. Run the application:
    ```sh
    dotnet run
    ```

### Running Tests

To run the tests for the backend, use the following command:
```sh
dotnet test
