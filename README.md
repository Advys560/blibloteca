# Proyecto Biblioteca

Aplicación de consola para administrar un catálogo básico de libros y usuarios, desarrollada con .NET 10.

## Funcionalidades

- **Libros:** consultar el catálogo, ver los libros disponibles y agregar títulos. El catálogo incluye inicialmente «El principio de la eternidad».
- **Usuarios:** registrar y listar usuarios. Cada vez que se abre la sección se cargan tres usuarios de ejemplo: Juan Pérez, María Gómez y Carlos López.
- **Búsqueda:** buscar libros por título o por una parte del título, sin distinguir entre mayúsculas y minúsculas.
- **Préstamos:** consultar el catálogo desde la sección de préstamos.
- **Menús interactivos:** navegar por las secciones y volver al menú principal; las opciones inválidas y las entradas vacías se informan en pantalla.

## Funcionalidades pendientes

El registro, listado y devolución de préstamos, la consulta de libros prestados y la generación de reportes todavía no están implementados. La opción de guardar y cargar datos también está pendiente.

Los libros agregados se conservan en memoria mientras se ejecuta la aplicación. Los usuarios registrados se conservan mientras se permanece en la sección de usuarios, pero se reinician al salir de ella. Al cerrar el programa, los cambios se pierden.

## Requisitos

- SDK de .NET 10.

## Compilar y ejecutar

Desde la carpeta del proyecto:

```bash
dotnet build
dotnet run
```

El código de la aplicación está en [`Program.cs`](./Program.cs).
