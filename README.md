# Notification Connector POC

## Project Overview

This repository contains the internship project developed during a 20-day software engineering internship.

The objective of the project was to design and implement a flexible, provider-independent notification system using a connector architecture. The application can receive notifications from multiple communication providers without changing the core business logic by following the Adapter Pattern and dependency injection principles.

Throughout the internship, the project was gradually expanded by integrating multiple messaging providers, improving reliability, adding monitoring capabilities, and validating the complete workflow inside a Docker environment.

---

# Technologies

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL
- RabbitMQ
- Redis
- Docker & Docker Compose
- Swagger / OpenAPI
- StackExchange.Redis

---

# Project Architecture

The application follows a modular connector architecture.

```
                +----------------+
                |     Client     |
                +--------+-------+
                         |
                         v
               ASP.NET Web API
                         |
                         v
                Connector Service
                         |
      -----------------------------------
      |        |        |        |      |
      v        v        v        v      v
 RabbitMQ   Redis   WebSocket Webhook Fake
      \        |        |        |      /
       \_______|________|________|_____/
                       |
                       v
            Notification Processor
                       |
                       v
            Notification Store
```

The architecture allows new communication providers to be added without modifying the notification processing logic.

---

# Features

During the internship, the following features were implemented:

- Connector-based notification architecture
- Adapter Pattern implementation
- RabbitMQ integration
- Redis Pub/Sub integration
- WebSocket adapter
- Webhook adapter
- Fake adapter for testing
- Configurable provider selection
- Notification processing pipeline
- Health monitoring endpoint
- Runtime logging
- Message validation
- Exception handling
- Automatic Redis reconnection
- Graceful shutdown
- In-memory notification storage
- Duplicate notification prevention
- Dockerized development environment

---

# API Endpoints

## Health Check

```
GET /api/Health
```

Returns the current status of the application and communication providers.

---

## Publish Redis Message

```
POST /api/Redis/publish
```

Example request:

```json
{
    "message": "Hello Redis"
}
```

---

## Notification History

```
GET /api/NotificationStore
```

Returns the notifications processed by the application.

---

# Running the Project

Clone the repository.

```bash
git clone https://github.com/dozgeakcayy/internship-poc.git
```

Navigate to the project directory.

```bash
cd docker101
```

Start Docker services.

```bash
docker compose up -d
```

Run the application.

```bash
cd InternshipAPI
dotnet run
```

Open Swagger.

```
http://localhost:5124/swagger
```

---

# Testing

The project was tested by publishing notifications through Swagger.

The verification process included:

- Publishing Redis messages
- Receiving messages through the Redis adapter
- Processing notifications
- Logging every processing step
- Monitoring application health
- Retrieving processed notifications
- Docker integration testing
- End-to-end workflow validation

---

# Internship Progress Summary

During the internship, the project evolved through several stages:

- Project setup and Docker environment configuration
- Database integration with PostgreSQL
- RabbitMQ messaging implementation
- Connector architecture development
- WebSocket and Webhook adapters
- Redis adapter implementation
- Health monitoring
- Runtime logging improvements
- Validation and exception handling
- Automatic reconnection support
- Message buffering
- Notification storage
- Final integration testing
- GitHub version control and project documentation

---

# Author

** Dilara Özge Akçay**

Computer Engineering Student

Turkish Aeronautical Association University (THK University)

Computer Engineering Internship Project
