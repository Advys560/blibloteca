using System;

internal static class Program
{
    private static void Main()
    {
        new Menu().MostrarMenu();
    }
}

public class Menu
{
    public void MostrarMenu()
    {
        while (true)
        {
            MostrarOpciones();

            string? opcion = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(opcion))
            {
                Console.WriteLine("Debe ingresar una opción válida.");
                continue;
            }

            if (!ProcesarOpcion(opcion))
            {
                break;
            }
        }
    }

    private static void MostrarOpciones()
    {
        Console.WriteLine("\n--- MENÚ DE OPCIONES ---");
        Console.WriteLine("1. Libros disponibles");
        Console.WriteLine("2. Usuarios registrados");
        Console.WriteLine("3. Préstamos");
        Console.WriteLine("4. Búsquedas y reportes");
        Console.WriteLine("5. Guardar / Cargar datos");
        Console.WriteLine("6. Salir");
        Console.Write("Seleccione una opción: ");
    }

    private static bool ProcesarOpcion(string opcion)
    {
        switch (opcion)
        {
            case "1":
                new Books().MostrarLibros();
                return true;
            case "2":
                new Users().MostrarUsuarios();
                return true;
            case "3":
                new Loans().MostrarPrestamos();
                return true;
            case "4":
                new SearchAndReports().MostrarBusquedaYReportes();
                return true;
            case "5":
                Console.WriteLine("Funcionalidad de guardar/cargar datos aún no implementada.");
                return true;
            case "6":
                Console.WriteLine("Saliendo del programa...");
                return false;
            default:
                Console.WriteLine("Opción inválida. Por favor, seleccione una opción válida.");
                break;
        }

        return true;
    }
}

public class Books
{
    public void MostrarLibros()
    {
        while (true)
        {
            if (!Console.IsOutputRedirected)
            {
                Console.Clear();
            }

            Console.WriteLine("========================================");
            Console.WriteLine("       GESTIÓN DE LIBROS");
            Console.WriteLine("========================================");
            Console.WriteLine("1. Consultar Libros");
            Console.WriteLine("2. Consultar Libros disponibles");
            Console.WriteLine("3. Listar prestados");
            Console.WriteLine("4. Agregar libro");
            Console.WriteLine("5. Volver al menú principal");
            Console.WriteLine("========================================");
            Console.Write("Seleccione una opción: ");

            string? opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    CatalogoLibros.MostrarLibros();
                    break;
                case "2":
                    CatalogoLibros.MostrarLibros();
                    break;
                case "3":
                    Console.WriteLine("La consulta de libros prestados aún no está implementada.");
                    break;
                case "4":
                    CatalogoLibros.AgregarLibro();
                    break;
                case "5":
                    Console.WriteLine("Volviendo al menú principal...");
                    return;
                default:
                    Console.WriteLine("Opción inválida. Por favor, seleccione una opción válida.");
                    break;
            }

            Console.WriteLine("\nPresione Enter para volver al menú de libros.");
            Console.ReadLine();
        }
    }
}

public static class CatalogoLibros
{
    public static List<string> Libros { get; } = new()
    {
        "El principio de la eternidad"
    };

    public static void MostrarLibros()
    {
        Console.WriteLine("\nLibros registrados:");

        if (Libros.Count == 0)
        {
            Console.WriteLine("No hay libros registrados.");
            return;
        }

        for (int i = 0; i < Libros.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {Libros[i]}");
        }
    }

    public static void BuscarLibros(string termino)
    {
        List<string> resultados = Libros
            .Where(libro => libro.Contains(termino, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (resultados.Count == 0)
        {
            Console.WriteLine("No se encontraron libros con ese título.");
            return;
        }

        Console.WriteLine("\nLibros encontrados:");
        foreach (string libro in resultados)
        {
            Console.WriteLine($"- {libro}");
        }
    }

    public static void AgregarLibro()
    {
        Console.Write("Ingrese el título del libro: ");
        string? nuevoLibro = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(nuevoLibro))
        {
            Console.WriteLine("El título no puede estar vacío.");
            return;
        }

        Libros.Add(nuevoLibro.Trim());
        Console.WriteLine("Libro agregado correctamente.");
    }
}

public class Users
{
    public void MostrarUsuarios()
    {
        List<string> usuarios = new List<string>
        {
            "Usuario 1: Juan Pérez",
            "Usuario 2: María Gómez",
            "Usuario 3: Carlos López"
        };
        while (true)
        {
            if (!Console.IsOutputRedirected)
            {
                Console.Clear();
            }

            Console.WriteLine("========================================");
            Console.WriteLine("       GESTIÓN DE USUARIOS");
            Console.WriteLine("========================================");
            Console.WriteLine("1. Registrar Usuarios");
            Console.WriteLine("2. Listar usuarios");
            Console.WriteLine("3. Volver al menú principal");
            Console.WriteLine("========================================");
            Console.Write("Seleccione una opción: ");

            string? opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    Console.Write("Ingrese el nombre del usuario: ");
                    string? nuevoUsuario = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(nuevoUsuario))
                    {
                        usuarios.Add($"Usuario {usuarios.Count + 1}: {nuevoUsuario}");
                        Console.WriteLine("¡Usuario agregado correctamente!");
                    }
                    else
                    {
                        Console.WriteLine("No ingresó ningún texto válido.");
                    }

                    break;
                case "2":
                    Console.WriteLine("Usuarios registrados:");
                    foreach (string usuario in usuarios)
                    {
                        Console.WriteLine(usuario);
                    }
                    break;
                case "3":
                    Console.WriteLine("Volviendo al menú principal...");
                    return;
                default:
                    Console.WriteLine("Opción inválida. Por favor, seleccione una opción válida.");
                    break;
            }

            Console.WriteLine("\nPresione Enter para volver al menú de usuarios.");
            Console.ReadLine();
        }
    }
}

public class Loans
{
    public void MostrarPrestamos()
    {
        while (true)
        {
            if (!Console.IsOutputRedirected)
            {
                Console.Clear();
            }

            Console.WriteLine("========================================");
            Console.WriteLine("       GESTIÓN DE PRÉSTAMOS");
            Console.WriteLine("========================================");
            Console.WriteLine("1. Registrar Préstamo");
            Console.WriteLine("2. Listar préstamos");
            Console.WriteLine("3. Libros devueltos");
            Console.WriteLine("4. Consultar catálogo de libros");
            Console.WriteLine("5. Volver al menú principal");
            Console.WriteLine("========================================");
            Console.Write("Seleccione una opción: ");

            string? opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    Console.WriteLine("La función de registrar préstamos aún no está implementada.");
                    break;
                case "2":
                    Console.WriteLine("La función de listar préstamos aún no está implementada.");
                    break;
                case "3":
                    Console.WriteLine("La función de registrar devoluciones aún no está implementada.");
                    break;
                case "4":
                    CatalogoLibros.MostrarLibros();
                    break;
                case "5":
                    Console.WriteLine("Volviendo al menú principal...");
                    return;
                default:
                    Console.WriteLine("Opción inválida. Por favor, seleccione una opción válida.");
                    break;
            }

            Console.WriteLine("\nPresione Enter para volver al menú de préstamos.");
            Console.ReadLine();
        }
    }
}

public class SearchAndReports
{
    public void MostrarBusquedaYReportes()
    {
        while (true)
        {
            if (!Console.IsOutputRedirected)
            {
                Console.Clear();
            }

            Console.WriteLine("========================================");
            Console.WriteLine("       BÚSQUEDAS Y REPORTES");
            Console.WriteLine("========================================");
            Console.WriteLine("1. Buscar libro por título");
            Console.WriteLine("2. Generar reporte de préstamos");
            Console.WriteLine("3. Volver al menú principal");
            Console.WriteLine("========================================");
            Console.Write("Seleccione una opción: ");

            string? opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    Console.Write("Ingrese el título o parte del título que desea buscar: ");
                    string? termino = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(termino))
                    {
                        Console.WriteLine("El término de búsqueda no puede estar vacío.");
                    }
                    else
                    {
                        CatalogoLibros.BuscarLibros(termino.Trim());
                    }

                    break;
                case "2":
                    Console.WriteLine("La función de generación de reportes aún no está implementada.");
                    break;
                case "3":
                    Console.WriteLine("Volviendo al menú principal...");
                    return;
                default:
                    Console.WriteLine("Opción inválida. Por favor, seleccione una opción válida.");
                    break;
            }

            Console.WriteLine("\nPresione Enter para volver al menú de búsquedas y reportes.");
            Console.ReadLine();
        }
    }
}