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
- `Books`: gestiona la interfaz y las operaciones relacionadas con libros.
- `Users`: gestiona la interfaz y registro de usuarios.
- `Loans`: gestiona los préstamos y la disponibilidad de los libros.
- `Prestamo`: representa cada préstamo y su estado.
- `SearchAndReports`: controla búsquedas y reportes.
- `Persistence`: se encarga de cargar, guardar y reiniciar datos.
- `CatalogoLibros`: centraliza la colección de libros y las operaciones sobre ella.

Las clases se encuentran en `Models/` excepto el punto de entrada, que permanece en `Program.cs`. Los menús comparten las mismas instancias de préstamos y persistencia para mantener sincronizado el estado de la aplicación.

## Requisitos

- .NET 10 SDK

## Compilar y ejecutar

Desde la carpeta del proyecto:

```bash
dotnet build
dotnet run
```

El punto de entrada principal está en [`Program.cs`](./Program.cs).
