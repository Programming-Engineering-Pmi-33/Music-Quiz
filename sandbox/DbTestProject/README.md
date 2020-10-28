
## Setting Up Database

In Package Manager Console (PM):

- `Update-Database` - to update database.
- `Add-Migration "Migration Name"` - to create new migration.

In Console (.NET Core Tools should be installed: `dotnet tool install --global dotnet-ef`):
Ensure you're in the folder `cd Music-Quiz/sandbox/DbTestProject/`


- `dotnet ef database update` - to update database.
- `dotnet ef migrations add "Migration Name"` - to create new migration.

Script which updates db located in `Music-Quiz/sandbox/DbTestProject/update-database.sh`