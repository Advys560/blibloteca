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
- **Demostración de servicios:** búsquedas y ordenamientos de libros, usuarios y préstamos; KPIs y comparación de `Libro[]` con `List<Libro>`.
- **Persistencia:** carga al iniciar y guardado de libros, usuarios y préstamos usando JSON.
- **Reinicio de datos:** vaciado de libros, usuarios y préstamos tras confirmación.
- **Salida segura:** confirma antes de cerrar la aplicación.

## Implementación de POO

La aplicación sigue un enfoque de programación orientada a objetos (POO), separando responsabilidades en clases y métodos reutilizables:

- `Program`: inicia la persistencia, carga los datos y abre la aplicación.
- `Menu`, `BooksMenu`, `UsersMenu`, `LoansMenu` y `SearchAndReports`: mantienen la interacción de consola en `ConsoleUI/`.
- `LibroService`, `UsuarioService` y `PrestamoService`: gestionan en memoria las entidades y reglas de negocio en `Services/`.
- `Libro`, `Usuario`, `Prestamo` y `EstadoPrestamo`: definen las entidades y sus estados en `Models/`.
- `Persistence`: guarda y carga los datos de los servicios en `Infrastructure/`, manteniendo compatibilidad con los archivos JSON anteriores.

La estructura principal del proyecto es:

```text
Proyecto-Bliblotecta/
├── ConsoleUI/
│   ├── BooksMenu.cs
│   ├── LoansMenu.cs
│   ├── Menu.cs
│   ├── SearchAndReports.cs
│   └── UsersMenu.cs
├── Infrastructure/
│   └── Persistence.cs
├── Models/
│   ├── EstadoPrestamo.cs
│   ├── Libro.cs
│   ├── Prestamo.cs
│   └── Usuario.cs
├── Services/
│   ├── LibroService.cs
│   ├── PrestamoService.cs
│   └── UsuarioService.cs
├── Program.cs
└── Proyecto-Bliblotecta.csproj
```

`Program.cs` construye una sola instancia de cada servicio, prepara datos de prueba, carga los datos guardados si existen y comparte esas instancias con los menús. Models contiene solo entidades; Services administra las colecciones y reglas de negocio; ConsoleUI gestiona la interacción, e Infrastructure la persistencia.

## Requisitos

- .NET 10 SDK

## Compilar y ejecutar

Desde la carpeta del proyecto:

```bash
dotnet build
dotnet run
```

El punto de entrada principal está en [`Program.cs`](./Program.cs).
