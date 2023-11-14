# ICT-Module 295 - Jetstream Skiservice WebAPI

## Overview
The Jetstream Ski Service API is a robust backend service designed to manage the [Website](https://github.com/mahgoe/ICT_Modul294_Praxisarbeit) we created in the past. It provides endpoints for registration, status updates, prioriry and a JWT authentification.

## Installation

### Prerequisites
- .NET 7.0 SDK
- SQL Server

### Steps
1. Clone the repository to your local machine.
2. Restore the necessary packages by running `dotnet restore` within the project directory.
3. Ensure your SQL Server instance is running.
4. Execute the SQL scripts located in `SQLScript_JetstreamSkiserviceDB` to set up the database.
5. Update the database connection string in the application settings.

## Usage
After installation and database setup, you can start the API with `dotnet run`. The API is now ready to accept requests at the designated endpoints.

Refer to the Postman collection `JetstreamSkiserviceAPI.postman_collection.json` for examples of requests that can be made to the API.

## Database Setup
The SQL scripts provided in the `SQLScript_JetstreamSkiserviceDB` directory can be used to select all data after you used Add-Migration and Update-Database in your Packet Manager. The nessecary data will be inserted in the migration provided by `RegistrationDbContext.cs`

tbt
