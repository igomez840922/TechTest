# Registro de productos
![Registro de Productos](https://i.imgur.com/lXhqEmt.png)
## Como correr en local
- Clonar la rama.
- Usando el dotnet CLI ejecutar el siguiente comando: `dotnet restore`
- En el appsettings del proyecto "Test.Server" reemplazar el `ConnectionString` con el suyo.
- Ejecutar el proyecto "Test.Server"

## Características

 - Autenticación con JWT.
 - Autorización basada en roles.
 - UI con MudBlazor.
 - Contraseñas ofuscadas.
 - Guardado de imagenes en base64.
 - CRUD completo.

## Características faltantes
- Capacidad de exportar a PDF o Excel.

### Problemas de seguridad conocidos
- La llave para generar los JWT esta en el appsettings por motivos de practicidad.