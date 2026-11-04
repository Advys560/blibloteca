# Proyecto Biblioteca

Aplicación de consola para gestionar libros y usuarios, desarrollada con .NET 10.

## Trabajo realizado

- Se creó un menú principal para acceder a las secciones y salir del programa.
- Se agregó un submenú de libros que permanece abierto después de cada consulta hasta elegir «Volver al menú principal».
- Se agregó un submenú de usuarios para registrar nombres y mostrar una lista inicial de ejemplo.
- Se validan las opciones del menú y los nombres vacíos.
- El código de la aplicación está en `Program.cs`, organizado en las clases `Program`, `Menu`, `Books` y `Users`.

## Estado actual

Las consultas del submenú de libros muestran el mismo libro de ejemplo. Los usuarios nuevos se guardan en memoria mientras se usa el submenú; todavía no se almacenan al cerrar la aplicación. Las funciones de préstamos, búsquedas, reportes y guardar o cargar datos están pendientes.

## Requisitos

- SDK de .NET 10.

## Compilar y ejecutar

Desde la carpeta del proyecto:

```bash
dotnet build
dotnet run
```
