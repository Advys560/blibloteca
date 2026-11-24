# Proyecto Bliblotecta

Aplicación de consola para administrar un catálogo básico de libros y usuarios, desarrollada en C# con .NET 10.

## Descripción

Este proyecto simula una pequeña biblioteca digital donde el usuario puede:

- consultar y agregar libros,
- registrar y listar usuarios,
- buscar libros por título,
- guardar datos de la aplicación en un archivo JSON,
- reiniciar la información de la biblioteca,
- salir del sistema de forma segura con confirmación.

## Funcionalidades implementadas

- **Menú principal interactivo:** navegación por secciones con validación de opciones.
- **Libros:** consulta del catálogo, listado inicial y agregado de nuevos títulos.
- **Usuarios:** registro y listado de usuarios con datos de ejemplo iniciales.
- **Préstamos:** registro, consulta y devolución de libros.
- **Búsqueda:** búsqueda de libros por nombre o por parte del título sin distinguir mayúsculas/minúsculas.
- **Reportes:** listado de préstamos registrados con su estado.
- **Persistencia:** carga al iniciar y guardado de libros, usuarios y préstamos usando JSON.
- **Reinicio de datos:** vaciado de libros, usuarios y préstamos tras confirmación.
- **Salida segura:** confirma antes de cerrar la aplicación.

## Implementación de POO

La aplicación sigue un enfoque de programación orientada a objetos (POO), separando responsabilidades en clases y métodos reutilizables:

- `Program`: inicia la persistencia, carga los datos y abre la aplicación.
- `Menu`: controla el menú principal y delega las acciones según la opción seleccionada.
- `BooksMenu`, `UsersMenu` y `LoansMenu`: mantienen la interacción de consola en `ConsoleUI/`.
- `LibroService`, `UsuarioService` y `PrestamoService`: gestionan en memoria las entidades y reglas de negocio en `Services/`.
- `Prestamo`: representa cada préstamo y su estado.
- `SearchAndReports`: controla búsquedas y reportes.
- `Persistence`: guarda y carga los datos de los servicios, manteniendo compatibilidad con los archivos JSON anteriores.

Los modelos están en `Models/`, la lógica de negocio en `Services/` y las interfaces de consola en `ConsoleUI/`. `Program.cs` crea y comparte las mismas instancias de los servicios y la persistencia para mantener sincronizado el estado de la aplicación.

## Requisitos

- .NET 10 SDK

## Compilar y ejecutar

Desde la carpeta del proyecto:

```bash
dotnet build
dotnet run
```

El punto de entrada principal está en [`Program.cs`](./Program.cs).
