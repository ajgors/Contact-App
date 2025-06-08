# Contact App Documentation

## Launching application

1. Docker compose of entire application
   in the main folder there is a compose.yaml file containing definitions of containers for the database, backend and frontend

To launch enter:

```
docker compose up --build
```

the application will be launched at localhost:4040

2. Docker compose for backend with database and frontend run locally
   in the backend folder there is a compose.yaml file containing definition of containers for backend and database

To launch enter:

```
docker compose up --build
```

To run the frontend, go to the fontend folder and type

```
npm install
npm run dev
```

the application will be launched at localhost:5173

## Used libraries

1. Frontend

- Default project configuration for Vue that contains vite, eslint, prettier
- Bootstrap

2. Backend

- Default project configuration for ASP.NET Rest API
- ORM Entity Framework with PostgreSQL database

## Description of classes and methods

1. Entity framework Models

- Contact model contains contact data
- ContactDetails model contains additional contact data
- Enumeration definition for ContactCategory and ContactSubcategory
- Definicje enumów dla kategorii kontaktów
- DTO classes for Login and Contact Patch method
- AccountController Controller responsible for managing user authentication, checking whether the user is logged in, logging out
- ContactController responsible for managing contacts
