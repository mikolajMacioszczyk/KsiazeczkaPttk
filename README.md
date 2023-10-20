# KsiazeczkaPttk

## Project Description

KsiazeczkaPttk to projekt web API stworzony przy użyciu .NET 5. API to służy jako backend do aplikacji umożliwiającej planowanie wycieczek górskich. 
Projekt został zbudowany z wykorzystaniem architektury warstwowej.

## Requirements

- docker up and running
- .NET 5 (local debug)

## Running the Web API

To run this API, we use Docker containers. A `Dockerfile` and a `docker-compose.yml` file have been prepared for this purpose. Please follow the steps below:

1. make sure you have Docker installed on your system.

2. navigate to the project directory where the `docker-compose.yml` file is located.

3. run the following command in terminal:

```bash
docker-compose up --build
```

4. After the startup process is complete, the API will be accessible at: http://localhost:8080

5. You can access the interactive API documentation using Swagger by opening your web browser and navigating to the following URL: http://localhost:8080/swagger/index.html
