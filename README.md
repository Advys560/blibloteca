# Proyecto Biblioteca

Aplicación de consola en C# para administrar un catálogo básico de libros y usuarios. El proyecto utiliza .NET 10 y guarda los datos localmente en formato JSON.

## Requisitos

- Windows
- SDK de .NET 10

## Ejecución

Abre PowerShell o una terminal en la carpeta del proyecto y ejecuta:

```powershell
dotnet restore
dotnet build
dotnet run
```

## Funcionalidades

### Libros

- Consultar y buscar títulos por nombre completo o parcial, sin distinguir entre mayúsculas y minúsculas.
- Agregar libros al catálogo durante la ejecución.
- El catálogo inicial incluye «El principio de la eternidad».

### Usuarios

- Registrar y listar usuarios.
- La aplicación inicia con tres usuarios de ejemplo: Juan Pérez, María Gómez y Carlos López.

### Préstamos y reportes

Los menús están disponibles, pero el registro y listado de préstamos, las devoluciones, la consulta de libros prestados y la generación de reportes todavía no están implementados.

### Guardado y carga

- Al elegir **Salir y guardar** en el menú principal, la aplicación pide confirmación y guarda los libros y usuarios en:

  ```text
  %LOCALAPPDATA%\ProyectoBiblioteca\datos.json
  ```

- Desde **Persistencia** se pueden cargar los datos del archivo guardado.
- La opción **Reiniciar datos** vacía los libros y usuarios actuales después de pedir confirmación. Para guardar ese cambio, confirma también la salida desde el menú principal.
- Cancelar la salida mantiene la aplicación abierta y no guarda los cambios pendientes.
- Al iniciar, se cargan los datos de ejemplo. Para restaurar un archivo existente, usa **Persistencia → Cargar datos guardados**.

## Navegación

El menú principal da acceso a Libros, Usuarios, Préstamos, Búsquedas y reportes, Persistencia y Salir y guardar. Cada submenú pide confirmación antes de volver al menú principal. Responde `S` o `SI` para confirmar; cualquier otra respuesta cancela la acción.

## Estructura del proyecto

```text
.
├── Menus/
│   ├── MenuBusquedasYReportes.cs
│   ├── MenuLibros.cs
│   ├── MenuPersistencia.cs
│   ├── MenuPrestamos.cs
│   ├── MenuPrincipal.cs
│   └── MenuUsuarios.cs
├── Servicios/
│   ├── CatalogoLibros.cs
│   └── DatosBiblioteca.cs
├── Utilidades/
│   └── Consola.cs
├── Program.cs
└── Proyecto-Bliblotecta.csproj
```
