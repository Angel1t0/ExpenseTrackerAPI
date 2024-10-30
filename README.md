# ExpenseTrackerAPI

API de seguimiento de gastos desarrollada con ASP.NET Core, que permite a los usuarios gestionar sus gastos de forma segura con autenticación JWT.

## Descripción

Esta API permite a los usuarios:
- Registrarse e iniciar sesión para generar un token JWT.
- Acceder de forma segura a los endpoints mediante autenticación.
- CRUD completo para la gestión de gastos, permitiendo filtrar y clasificar por categorías.

Este proyecto está diseñado como parte de mi portafolio de habilidades en desarrollo backend con .NET.

## Tecnologías Utilizadas

- **ASP.NET Core 8.0**: Framework principal para el desarrollo de la API.
- **Entity Framework Core**: ORM para la gestión de la base de datos.
- **JWT (JSON Web Token)**: Autenticación y autorización basada en tokens.
- **Swagger**: Documentación interactiva de la API.
- **MS SQL Server**: Base de datos para almacenar los datos de usuarios y gastos.

## Funcionalidades Principales

1. **Autenticación y Autorización con JWT**
   - Registro de usuarios y generación de token JWT al iniciar sesión.
   - Acceso seguro a los endpoints mediante autenticación.

2. **Gestión de Gastos (CRUD)**
   - Crear, leer, actualizar y eliminar gastos.
   - Filtrado de gastos por fechas.

## Endpoints Principales

### Autenticación
- `POST /api/Cuenta/registrar`: Registro de usuario.
- `POST /api/Cuenta/login`: Inicio de sesión y generación de token JWT.

### Gestión de Gastos
- `GET /api/Gasto`: Obtener todos los gastos del usuario autenticado y opcionalmente filtrar por fechas.
- `GET /api/Gasto/{id}`: Obtener un gasto específico por ID.
- `POST /api/Gasto`: Crear un nuevo gasto.
- `PUT /api/Gasto/{id}`: Actualizar un gasto existente.
- `DELETE /api/Gasto/{id}`: Eliminar un gasto.

### Categorías
- `GET /api/Categoria`: Obtener todas las categorías disponibles para agregar un gasto.

### Endpoints
![image](https://github.com/user-attachments/assets/2590a235-1393-40c8-a3ff-1727ba4b0a42)

#### Referencia del Proyecto
RoadMap: https://roadmap.sh/projects/expense-tracker-api
