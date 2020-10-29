
## Setting Up Database (Storage Layer)

In Package Manager Console (PM):

- `Update-Database` - to update database.
- `Add-Migration "Migration Name" -O Migrations` - to create new migration.

In Console (.NET Core Tools should be installed: `dotnet tool install --global dotnet-ef`):
Ensure you're in the folder `cd Music-Quiz/src/MusicalQuiz/ClientLayer`

- `dotnet ef database update` - to update database.
- `dotnet ef migrations add "Migration Name" -O ../MusicalQuiz/StorageLayer/Migrations` - to create new migration.
