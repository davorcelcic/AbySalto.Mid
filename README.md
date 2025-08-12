# mid.net-main
README.txt

 
# AbySalto.Mid - InMemory Product & User Management Application with Basket and Favorites Functionality

## Overview
This application:

demonstrates 
	user 
		registration, 
		login, 
		logout,
	product 
		viewing, 
		basket functionality, 
		favorites functionality,
		data population from https://dummyjson.org/products, 
using 
	EF Core InMemory provider (RAM database) for data storage.

Authentication is handled via cookies, and the MVC pattern is used for organization structure of code.

## Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- IDE (Visual Studio, VS Code)

## Setup Instructions

1. Clone or download the repository
2. Open a terminal in the project directory
3. Restore dependencies:
	dotnet restore
4. Build the project:
	dotnet build
5. Run the application:
	dotnet run

6. Open your browser and navigate to:	
	https://localhost:7221; http://localhost:5269


## Usage
- Register a new user via the registration page
- Login with your credentials
- Browse products (paging, sorting[asc, desc], filtering[id, title, category, price, discountPercentage, rating, stock, weight]), add/remove to basket, add/remove to favorites
- View and manage your basket/favorites
- Logout when done

## Notes
- Passwords are stored as SHA256 hashes
- Products are populated from https://dummyjson.org/products 
- In-memory database means data is lost when the app stops
- For testing purposes two users are seeded in database 
		username: korisnik1, password: korisnik1, email: korisnik1@gmail.com
		username: korisnik2, password: korisnik2, email: korisnik2@gmail.com

## Dependencies
- Microsoft.EntityFrameworkCore.InMemory
- Microsoft.AspNetCore.Authentication.Cookies
