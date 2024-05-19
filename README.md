# Microservices Project README

This project is developed using ASP.NET Web API, SignalR, Docker, RabbitMQ, MassTransit, Ocelot API Gateway, MongoDB, Redis, PostgreSQL, SQL Server, Dapper, Entity Framework Core technologies to develop microservices. This project is built following the principles of CQRS (Command Query Responsibility Segregation) and Clean Architecture, aiming to provide optimal performance and user experience for project clients by incorporating immediate consistency, validation, and eventual consistency.

## Technologies and Components

This project uses the following technologies and components:

- **ASP.NET Web API**: For developing RESTful APIs.
- **SignalR**: For providing real-time chat functionality.
- **Docker**: To containerize microservices and deploy applications easily.
- **RabbitMQ**: For asynchronous messaging and validation.
- **MassTransit**: An integrated asynchronous messaging infrastructure with RabbitMQ.
- **Ocelot API Gateway**: Functions as an entry point to manage microservices.
- **MongoDB**: As a NoSQL database.
- **Redis**: For storing cached data.
- **PostgreSQL**: As a management system.
- **SQL Server**: As a database.
- **Dapper**: An ORM (Object-Relational Mapping) tool optimized for performance.
- **Entity Framework Core**: ORM tools for database operations.
- **CQRS (Command Query Responsibility Segregation)**: The principle of separating modifying user operations from reading operations.
- **Clean Architecture**: Ensures logical organization of the project and enables components to be changed without modification.

## Project Structure

The project follows the following structure:

- **src**: Contains all source code.
  - **Microservice1**: First microservice.
  - **Microservice2**: Second microservice.
  - **Shared**: Contains source code shared by both microservices.
- **tests**: Contains user and automated tests.

## Installation

To run the project in your local environment, follow these steps:

1. Docker Desktop and Visual Studio (if available) are required.
2. Clone the project to your environment.
3. Run the `docker-compose up` command.
4. The applications are now ready. You can test the application by opening a new terminal and using tools such as curl or Postman.

## Assistance on Topics

If you need more information or assistance, please refer to the help files on the topic or contact me.

