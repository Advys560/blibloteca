# Proyecto Biblioteca

Aplicación de consola para gestionar un catálogo de biblioteca. El proyecto utiliza .NET 10.

## Descripción del trabajo realizado

Se preparó la estructura inicial de la aplicación y se organizó el código del menú principal y la gestión de libros. El menú permite navegar por las opciones disponibles, valida entradas vacías o no reconocidas y finaliza correctamente al seleccionar «Salir». El submenú de libros permanece abierto después de cada consulta y solo regresa al menú principal cuando se elige esa opción. También se documentaron las funciones que todavía están pendientes.

## Funcionalidad actual

- Menú principal con opciones para libros, usuarios, préstamos, búsquedas y reportes, guardar o cargar datos y salir.
- Submenú de libros con opciones para consultar el catálogo, consultar libros disponibles, listar libros prestados y volver al menú principal.
- Después de elegir una opción del submenú de libros, se muestra el resultado y se espera que se presione Enter para continuar en ese submenú.
- Las opciones de usuarios, préstamos, búsquedas, reportes y almacenamiento todavía no están implementadas.

Las opciones del catálogo muestran actualmente un libro de ejemplo: «El principio de la eternidad».

## Requisitos

- SDK de .NET 10.

## Compilar y ejecutar

Desde la carpeta del proyecto:

```bash
dotnet build
dotnet run
```