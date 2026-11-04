# Proyecto Biblioteca

Aplicación de consola para administrar un catálogo básico de libros y usuarios, desarrollada con .NET 10.

## Funcionalidades

- **Libros:** consultar el catálogo, ver los libros disponibles y agregar títulos. El catálogo incluye inicialmente «El principio de la eternidad».
- **Usuarios:** registrar y listar usuarios. La lista inicia con tres usuarios de ejemplo: Juan Pérez, María Gómez y Carlos López.
- **Búsqueda:** buscar libros por título o por una parte del título, sin distinguir entre mayúsculas y minúsculas.
- **Préstamos:** consultar el catálogo desde la sección de préstamos.
- **Persistencia:** guardar libros y usuarios en un archivo JSON ubicado en la carpeta local de datos de la aplicación. La carga aún no está implementada.
- **Reinicio:** vaciar todos los libros y usuarios después de confirmar la acción.
- **Salida segura:** confirmar antes de salir del programa.
- **Menús interactivos:** navegar por las secciones y volver al menú principal; las opciones inválidas y las entradas vacías se informan en pantalla.

## Funcionalidades pendientes

El registro, listado y devolución de préstamos, la consulta de libros prestados y la generación de reportes todavía no están implementados. Los datos guardados no se cargan automáticamente; la opción para cargarlos está pendiente.

Los libros y usuarios agregados se conservan en memoria durante la ejecución. Para conservarlos entre ejecuciones, use **Persistencia > Guardar datos**. Reiniciar los datos vacía las listas en memoria; no elimina el archivo guardado.

## Requisitos

- SDK de .NET 10.

## Compilar y ejecutar

Desde la carpeta del proyecto:

```bash
dotnet build
dotnet run
```

El código de la aplicación está en [`Program.cs`](./Program.cs).
