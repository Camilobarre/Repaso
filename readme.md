# Pet Management API

## Descripción
Esta API permite gestionar información sobre mascotas. Proporciona funcionalidades para agregar, eliminar y obtener mascotas.

## Tecnologías
- C#
- ASP.NET Core
- Entity Framework
- JWT para autenticación

## Endpoints

### Nugets
- `Dotnetenv` 
- `JwtBearer` 
- `EntityFramework` 
- `EntityFramework.Design`
- `Pomelo.Mysql`
- `Swashbuckle`
- `Swashbuckle.Swagger`

## Pasos a seguir
- Crea un proyecto usando `dotnet new webapi -n ELNOMBRE --use-controllers` - `cd` - `dotnet new gitignore` y después `git init` - `code .`
- Crea una base datos en Clever Cloud y un archivo .env
- Crea los modelos y sus respectivas data annotations
- Conecta la base de datos en program.cs
- Haz las migraciones `dotnet ef migrations add ELNOMBREQUEQUIERASPONER` y despues `dotnet ef database update`
- Configura la carpeta Data (Crea las tablas desde los modelos)
- Crea la carpeta Service
- Crea la carpeta Repositories
- Crea la carpeta de Controladores
- Crea la carpeta Config con todo el tema de seguirad y validación