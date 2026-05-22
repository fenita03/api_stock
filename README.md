# MiApp - API de stock

API Web en .NET 8 para administrar stock de productos tecnologicos. El ejemplo usa productos, categorias y usuarios con autenticacion JWT.

## Capas de Clean Architecture

- `MiApp.Domain`: entidades, reglas de negocio y `DomainException`. No depende de EF Core, ASP.NET ni MediatR.
- `MiApp.Application`: casos de uso con CQRS y MediatR. Define interfaces de repositorios y DTOs/responses.
- `MiApp.Infrastructure`: EF Core, SQLite, repositorios concretos, `ApplicationDbContext`, seed y JWT.
- `MiApp.WebApi`: controllers, Swagger, autenticacion/autorizacion y manejo global de errores.

## Flujo correcto de una request

HTTP Request -> Controller -> `MediatR.Send()` -> Command o Query -> Handler -> Repository Interface -> Repository Implementation -> DbContext -> Base de datos.

## Command y Query

Un Command representa una accion que modifica datos. Ejemplo: `CreateProductCommand` crea un producto.

Un Query representa una consulta que solo lee datos. Ejemplo: `GetProductsQuery` lista los productos.

## MediatR

MediatR recibe el Command o Query desde el controller y lo envia a su Handler. Asi el controller no conoce repositorios ni logica de negocio.

## Repository Pattern

Los Handlers usan interfaces como `IProductRepository`. Infrastructure implementa esas interfaces con EF Core y `ApplicationDbContext`.

## Seguridad y Swagger

Login:

- Usuario: `admin@test.com`
- Contrasena: `123456`

En Swagger:

1. Ejecutar `POST /api/Auth/login`.
2. Copiar el valor `token`.
3. Presionar `Authorize`.
4. Pegar el token JWT.

## Endpoints para probar

- `POST /api/Auth/login`
- `GET /api/Products`
- `GET /api/Products/{id}`
- `POST /api/Products`
- `PUT /api/Products/{id}`
- `DELETE /api/Products/{id}`
- `GET /api/Categories`

## Comandos

```bash
dotnet restore
dotnet build
dotnet ef migrations add InitialCreate --project src/MiApp.Infrastructure --startup-project src/MiApp.WebApi
dotnet ef database update --project src/MiApp.Infrastructure --startup-project src/MiApp.WebApi
dotnet run --project src/MiApp.WebApi
```
