# Proyecto Biblioteca

Aplicación de consola para administrar un catálogo básico de libros y usuarios, desarrollada con .NET 10.

## Funcionalidad

- **Menú principal:** permite acceder a las secciones de libros, usuarios y préstamos, además de mostrar las opciones aún pendientes y salir.
- **Libros:** permite consultar el catálogo, agregar títulos y ver los libros desde el menú de préstamos. El catálogo parte con «El principio de la eternidad».
- **Usuarios:** permite registrar nombres y listar usuarios, incluidos tres registros de ejemplo.
- **Préstamos:** permite consultar el catálogo compartido. El registro y listado de préstamos y devoluciones todavía no están implementados.
- Los submenús permanecen abiertos hasta elegir la opción para volver al menú principal. Se validan las opciones y se rechazan títulos o nombres vacíos.

Los libros y usuarios se guardan en memoria durante la ejecución; los cambios se pierden al cerrar la aplicación. La persistencia, las búsquedas y los reportes están pendientes. El código se encuentra en `Program.cs`, organizado en las clases `Program`, `Menu`, `Books`, `CatalogoLibros`, `Users` y `Loans`.

## Requisitos

- SDK de .NET 10.

## Compilar y ejecutar

Desde la carpeta del proyecto:

```bash
dotnet build
dotnet run
```
