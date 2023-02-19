# .NET 7 & C# 11 Sample Banking API

<p align="center">
	<img src="https://badgen.net/badge/API version/1.0/red">
	<img src="https://badgen.net/badge/XUnit/2.4/cyan">
	<img src="https://badgen.net/badge/icon/Docker?icon=docker&label">

</p>
<p align="center">
	<img src="https://dev.azure.com/arminsmajlagic/banking/_apis/build/status/banking?branchName=master">
	<img src="https://badgen.net/badge/OpenAPI/3.0.3/cyan">
</p>

This is sample banking API that uses .NET 7 & C# 11.
It includes most popular technologies, concepts and patterns. 
To insure quality I performed unit tests & integration tests.
And finnaly everything is dockerized and built with aid of Azure DevOps CI/CD & Docker.

## Diagrams
<p align="center">
	<img  src="https://user-images.githubusercontent.com/45321513/219958829-fc1d746a-1973-4d83-a4dc-87cf0804949f.png">
</p>

<p align="center">
	<img width="80%"  src="https://user-images.githubusercontent.com/45321513/219960017-e5d988e9-8c8d-465c-96f6-4c866ce3f2a2.png">
</p>

## Technologies
- .NET 7 & C# 11
- PostgreSQL with Dapper
- FluentMigrator for migrations
- Swagger UI with OpenAPI 3.1
- Redis for caching
- XUnit for testing
- Docker & Kubernetes for deployment

## C# & .NET Features
- Custom middleware for logging, API versioning, request throttling and migrations
- Custom Attributes & Filters to reduce boiler plate code in controllers (BadRequests, InternalServerErrors, NotFound, Caching)
- Extension methodes to extract service registry and middleware registry from Program.cs
- Using IDisposable & Garbage Collector to free unused resources
- Asynchronous programming
- Func<,> , Spans, Tuplets & Deconstructing, Collections, Locks, params, ... 

## Design patterns
- Unit Of Work pattern to perform transactions on cash transfers from accounts
- Repository pattern to abstract repositoires, services from consumers 
- Factory pattern to create database connections
- Singleton, Transient and Scoped for DI

## Testing
- Unit tests for repositories, controllers, caching
- Integration testing of PostgreSQL database connection and transaction

## DevOps 
- Dockerization and orchestration of API, PostgreSQL database, Redis Caching and other tools
- Azure CI/CD Piplines


Author: Armin Smajlagic
