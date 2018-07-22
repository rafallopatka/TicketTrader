# TicketTrader
Sample project for selling tickets for events, presenting implementation of common architecture styles and technologies

Repository contains three versions of the app representing stages of creation and evolving project between different styles

## 1 n-layer crud
Basic implementation of n-tier architecture as crud, with REST API and single page application frontend.
Technologies used:
* .NET Core for API and web apps
* Entity Framework Core with PostgreSQL for persistence
* Swagger for api code and documentation generation
* Docker - to make many web apps hosting easy to manage
* OpenId Connect with IdentityServer4 - for authorization and authentication in all apps
* Angular for single page application
* NWebsec - for security

## 2 ports & adapters cqrs
Split app into smaller microservices corresponding to DDD bounded contexts of system domain.
Most of microservices uses ports and adapters architecture to allow increase modularity of app, and split domain logic from technical aspects.
Technologies used (same as previous plus):
* MongoDb for aggregates persistance
* Reactive Extensions - for asynchronous in process communications and events handling
* RabbitMq + RawRabbit for communication between microservices and queueing 

## 3 microservices actor model
Reimplementation of main microservice to model and implement domain logic with actor model.

Technologies used (same as previous plus):
* Akka.NET - for actor model


