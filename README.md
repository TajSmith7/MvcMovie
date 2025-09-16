# Movie List App

This is a simple **ASP.NET Core MVC web app** I built by following Microsoft’s learning tutorial. The goal of the project was to learn how to build a CRUD application using ASP.NET Core and Entity Framework Core.

---

## What the app does
- Lets you **add movies** with details like title, release date, genre, price, and rating.  
- Shows a **list of movies** in a table.  
- Allows you to **edit** or **delete** existing movies.  
- Includes a **search and filter option** so you can find movies by title or genre.  

---

## Tech stack
- **ASP.NET Core MVC** 
- **Entity Framework Core**  
- **SQLServer**   

---

## How to run it
1. Clone the repo to your machine.  
2. Open the project in **Visual Studio**.  
3. Run the app

---
## Design Notes

To improve separation of concerns, I moved the CRUD logic out of the MVC controller and into a dedicated service class (MvcMovieService). This keeps the controller focused on handling HTTP requests/responses while the service encapsulates the business logic and data access coordination.

I also registered MvcMovieService with the built-in Dependency Injection (DI) container, which makes it easier to maintain, test, and swap implementations in the future if needed. This design choice aligns with clean architecture principles by decoupling the controller from direct data operations.

I added routing so the url can be manipulated to search movies(Movies/Details/5), filter by genre(/movies/bygenre/comedy), or filter by year/month(/movies/released/2010/5).

---
## Testing

A unit test project was added to validate the service layer. The tests use an in-memory database to simulate CRUD operations without requiring a real SQL Server instance. This allows the tests to run quickly and ensures that the MvcMovieService behaves correctly when adding, updating, retrieving, and deleting movies.
