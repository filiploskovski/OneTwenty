# OneTwenty Project Overview

**OneTwenty.Shared**
Used for all shared models and extensions across the solution.

**OneTwenty.Infrastructure**
Responsible for accessing and configuring the database (SQL).

### Used NuGet Packages:
- EntityFrameworkCore
- EntityFrameworkCore.Relational
- EntityFrameworkCore.SqlServer
- EntityFrameworkCore.Tools

Missing due to time constraints: Repository Pattern and Transaction handling, hence not used in other features.
No project references in Infrastructure.

**OneTwenty.Jobs**
Contains necessary background jobs for the system.

### Job Management:
- Utilizes Hangfire for job management.
- External NuGet packages used:
  - REFIT for RestApi job
  - CSVHelper for CSV job

Both jobs use the same data validation service (DataValidationService) and data store service (DataStoreService).
Project references: Infrastructure and Shared.

**OneTwenty.Services**
Implements CRUD operations and features required for the task, such as DuplicateUserService and UserService for fetching users for the last month.

Project references: Infrastructure and Shared.

**OneTwenty.WebApi**
Entry point of the application. Automatically creates the database and applies necessary migrations on startup. First run the WebApiMock without debug and after run this application.

### Endpoints:
- Swagger Endpoint: `/swagger/index.html`
- Hangfire Endpoint: `/hangfire`

**OneTwenty.WebApiMock**
A mock API used for the RestAPI job.

One thing missing in this task is **TESTS**. The codebase is prepared for test scenarios with all the necessary patterns.
