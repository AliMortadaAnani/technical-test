# technical-test
This repository includes the files and codebase of the technical evaluation test. (Task 1)

For Task 2: I provided the answer in the submitted form, and the same answer is also in the Task-2_Debugging_System-Thinking file.

For Task 3: I provided the video in the submitted form, and a link to Drive for the same video is here: ###

## Task 1:
Full stack Application:
I built a web application that serves as an internal dashboard for a company to manage the job applications of candidates applying for roles at this company.
The main features of the app are to show on the dashboard page a list of all job applications registered, update the status of each application, and create new applications.

Stack:
- Backend: .NET 8 Web API
- Database: SQL Server (Express)
- Frontend: React JS + TypeScript

Setup instructions:
- .NET 8 SDK required
- Node.js required
- SQL Server 16 (Developer or Express edition)
- Editor like Visual Studio and VS Code
- DBMS like MSSQL is useful
- Browser like Chrome

### Frontend setup:
1. Download the GitHub repo as a zip (for simplicity) and extract it.
2. Navigate to the frontend folder and open it in VS Code.
3. Open a terminal inside VS Code at `frontend/dashboard-frontend`.
4. Run the command:
   ```
   npm install
   ```
5. If all successful, create a `.env` file in root folder (dashboard-frontend) with:
   ```
   VITE_API_BASE_URL=https://localhost:7000/api/Dashboard
   ```
   (or your server URL)
6. Run the command:
   ```
   npm run dev
   ```
7. The app should be launched at a port like 5173.

### Backend setup:
1. Navigate to `backend/Dashboard.Api`, then open `Dashboard.API.slnx`.
2. The project should open in Visual Studio, and packages in NuGet Package Manager should auto-download.
3. Edit `appsettings.Development.json` and enter your connection string for the database.
4. Edit the `startupExtension/configureServicesExtensions` file and enter the frontend URL in the CORS configuration:
   ```csharp
   policyBuilder.WithOrigins("http://localhost:5173") // Your React Frontend URL
   ```
5. In Package Manager Console (Visual Studio), run the commands:
   ```powershell
   Add-Migration InitialCreate
   Update-Database
   ```
   Alternatively, using the .NET CLI:
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```
6. Run the project.

Now the backend and frontend servers are running.
Explore the dashboard and log in with username: "admin" password: "1234".
Explore the job application list, update statuses, create new job applications, and check the new list.

