# Phone Book Service

This project is a micro-service that serves a RESTful API made with [.NET 6](https://docs.microsoft.com/es-es/aspnet/core/?view=aspnetcore-6.0) that will let the user add, find and remove people from a phonebook.

Using [Domain Driven Design](https://en.wikipedia.org/wiki/Domain-driven_design) architecture and following [SOLID](https://en.wikipedia.org/wiki/SOLID) principles.

Also using:
- Entity Framework
- Fluent Validation

The API can be served using [Docker](https://docs.docker.com/get-started/overview/) containers.

##  Clone, Build and Run 
* `git clone https://github.com/blacksmith94/PhoneBookService.git`
* `cd PhoneBookService`
* `docker-compose up`

The API will be served at http://localhost:7628


## OpenAPI Specification

The project includes API documentation with [Swagger](https://swagger.io/).

Served at http://localhost:7628/swagger