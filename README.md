# IDNoValidator ASP.NET Core Application

## Prerequisites

- .NET Core SDK installed

## Getting Started

1. Clone this repository.
2. Navigate to the project directory.
3. Run `dotnet restore` to restore project dependencies.
4. Run `dotnet build` to build the application.
5. Run `dotnet run` to start the application.

## Architectural Choices

I created my application to follow the MVC architectural pattern, which is these days a common approach in web development. 

I used dependency injection to inject an instance of ValidatorDBContext into my controller, this is considered good practice because it allows me to manage the database context's lifecycle and makes the code more testable and maintainable.

In my model IDNumberModel, I applied data annotations to enforce data integrity and database contraints

For response handiling I include data in the response body to make it easier for the client to understand the outcome of the request.

I used Razor to generate HTML view making it easier to integrate server-side and clientside code.

my overall code structure and architectural choices follow best practices for building web applications with ASP.NET Core, combining server-side and client-side validation, database interaction, and providing user feedback. I believe it's a well-structured and maintainable approach to implementing an ID number validation system, but as always, there's room for improvement.


