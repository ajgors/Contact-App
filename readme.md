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

Frontend

- Default project configuration for Vue that contains vite, eslint, prettier
- Bootstrap

Backend

- Default project configuration for ASP.NET Rest API
- ORM Entity Framework with PostgreSQL database

## Description of classes and methods

Backend

- Contact model contains contact data
- ContactDetails model contains additional contact data
- Enumeration definition for ContactCategory and ContactSubcategory
- DTO classes for Login and Contact Patch method
- AccountController Controller responsible for managing user authentication, checking whether the user is logged in, logging out
  - api/account/login POST endpoint for user login
  - api/account/register POST endpoint for user account creation
  - api/account/logout POST endpoint for user logout
  - api/account/isLogged GET endpointd to check if user is logged in
- ContactController responsible for managing contacts
  - api/contact GET endpoint for fetching contact
  - api/contact/id GET for fetching contact with id
  - api/contact POST endpoint to create new contact
  - api/contact/id PATCH endpoint to update existing contact
  - api/contact/id DELETE endpoint to delete contact with id
  - api/contact/id/details GET endpoint to get details for contact with id
