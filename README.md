**Setup**

After you clone this project, you need to restore the packages on each project.

**Front-end**

Navigate to the angular folder path and do the following: To restore

To run the app:
ng start

**Web API Project**

Please update appsettings.json file in Api project:
- ConnectionString
- RedisConnectionString

Update the database:
dotnet ef database update
