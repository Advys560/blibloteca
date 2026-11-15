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
- **Disponibilidad:** consulta de libros disponibles y préstamos activos.
- **Usuarios:** registro y listado de usuarios con datos de ejemplo iniciales.
- **Búsqueda y reportes:** búsqueda de libros por título y reporte de préstamos con estado.
- **Préstamos:** registro, consulta y devolución de libros.
- **Persistencia:** guardado de libros y usuarios en una carpeta local del sistema usando JSON.
- **Reinicio de datos:** vaciado completo de libros y usuarios tras confirmación.
- **Salida segura:** confirma antes de cerrar la aplicación.

## Funcionalidades pendientes

La carga automática de datos desde JSON aún no está implementada.

Los libros y usuarios agregados se mantienen en memoria durante la ejecución. Para conservarlos entre ejecuciones, use la opción de persistencia en el menú.

## Implementación de POO

La aplicación sigue un enfoque de programación orientada a objetos (POO), separando responsabilidades en clases y métodos reutilizables:

- `Program`: inicia la ejecución de la aplicación.
- `Menu`: controla el menú principal y delega las acciones según la opción seleccionada.
- `Books`: gestiona la interfaz y las operaciones relacionadas con libros.
- `Users`: gestiona la interfaz y registro de usuarios.
- `Loans`: registra préstamos, controla devoluciones y consulta libros disponibles y prestados.
- `SearchAndReports`: controla búsquedas y reportes y comparte sus operaciones con el menú de libros.
- `Persistence`: se encarga de guardar y reiniciar datos.
- `CatalogoLibros`: centraliza la colección de libros y las operaciones sobre ella.

Además, se usa encapsulación al agrupar la lógica por módulos. El catálogo, los usuarios y los préstamos mantienen su estado en memoria durante la ejecución y se comparten entre los menús que los utilizan.

## Requisitos

- .NET 10 SDK

## Compilar y ejecutar

Desde la carpeta del proyecto:

```bash
dotnet build
dotnet run
```

El punto de entrada principal está en [`Program.cs`](./Program.cs).
