# CourtesyBanking

Transaction Ledger is a full-stack banking application designed to simulate core financial operations such as user registration, account creation, secure login, and transaction management.

The backend is built using ASP.NET Core (MVC pattern) with Entity Framework Core for data access and SQLite for local persistence.

## HOW TO RUN LOCALLY

In order to get this application running locally you'll first need to clone the application
and then open it up as a folder.

Open up two command line terminals pointing to the project's root directory

One terminal will be used for running the frontend and the other will be used for the backend

On terminal one enter:
  "cd backend"
  "dotnet run"
  This will fully start up the backend application

On the other terminal enter:
  "cd frontend"
  npm run dev
  This will fully start up the frontend application.
  (You may need to run npm install beforehand)

Ordinarily this would be enough to get things running but you'll also need to create a .env file within the frontend directory.
This should tell the application where to point its URLs. The contents should look something like this

VITE_API_BASE_URL = http://localhost:5000
VITE_FRONTEND_URL = http://localhost:5173;


Once you do that, attempt to access the frontend to run the application as intended!
