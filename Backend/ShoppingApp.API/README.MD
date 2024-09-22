This repository contains the backend API for the Shopping App, built using .NET 8. The API is designed to provide robust and efficient access to the application's data and services.
Features
RESTful API: The backend is structured as a RESTful service, allowing easy integration with various frontend applications.
SQLite Database: Utilizes SQLite for lightweight data storage, making it easy to set up and manage.
Automated Mapping: Employs AutoMapper for efficient object-to-object mapping, simplifying data transfer between layers.
Data Access Layer: Implements Dapper for fast and efficient data access, providing a simple interface for interacting with the database.
Validation: Uses FluentValidation to ensure that incoming data meets defined validation rules before processing.
OpenAPI Documentation: Integrated with Microsoft.AspNetCore.OpenApi to automatically generate API documentation, making it easier for developers to understand and use the API.
Logging: Incorporates Serilog for structured logging, allowing for better monitoring and debugging of the application.
Swagger UI: Utilizes Swashbuckle.AspNetCore to provide an interactive API documentation interface.


Getting Started

Prerequisites

.NET 8 SDK
SQLite (if not included in your .NET installation)

Installation

Clone the repository:
bash
git clone https://github.com/zhentos/ShoppingApp.git

cd ShoppingApp/Backend

Restore the NuGet packages:
bash
dotnet restore

Run the application:
bash
dotnet run

API Documentation
Once the application is running, you can access the Swagger UI at https://localhost:7090 (or your configured port) to explore the available endpoints and their documentation.

Contributing
Contributions are welcome! Please feel free to submit a pull request or open an issue for any enhancements or bug fixes.

License
This project is licensed under the MIT License - see the LICENSE file for details. Feel free to customize any sections according to your specific project details or preferences! This format provides clear information about your backend API, its features, and how to get started with it, making it easy for users and contributors to understand your project.