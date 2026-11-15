using System;

internal static class Program
{
    private static void Main()
    {
        Persistence persistence = new();
        Loans loans = new(persistence);
        persistence.CargarDatos(loans, mostrarMensaje: false);
        new Menu(loans, persistence).MostrarMenu();
    }
}

public class Menu
{
    private readonly Loans _loans;
    private readonly Persistence _persistence;

    public Menu(Loans loans, Persistence persistence)
    {
        _loans = loans;
        _persistence = persistence;
    }

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
        Console.WriteLine("5. Persistencia");
        Console.WriteLine("6. Salir");
        Console.Write("Seleccione una opción: ");
    }

    private bool ProcesarOpcion(string opcion)
    {
        switch (opcion)
        {
            case "1":
                new Books(_loans, _persistence).MostrarLibros();
                return true;
            case "2":
                new Users(_persistence).MostrarUsuarios();
                return true;
            case "3":
                _loans.MostrarPrestamos();
                return true;
            case "4":
                new SearchAndReports().MostrarBusquedaYReportes();
                return true;
            case "5":
                _persistence.MostrarMenu();
                return true;
            case "6":
                if (!Confirmar("¿Está seguro de que desea salir? (S/N): "))
                {
                    Console.WriteLine("Salida cancelada.");
                    return true;
                }

                Console.WriteLine("Saliendo del programa...");
                return false;
            default:
                Console.WriteLine("Opción inválida. Por favor, seleccione una opción válida.");
                break;
        }

        return true;
    }

    private static bool Confirmar(string mensaje)
    {
        while (true)
        {
            Console.Write(mensaje);
            string? respuesta = Console.ReadLine()?.Trim();

            if (string.Equals(respuesta, "S", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (string.Equals(respuesta, "N", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            Console.WriteLine("Respuesta inválida. Ingrese S para confirmar o N para cancelar.");
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

    public static bool AgregarLibro()
    {
        Console.Write("Ingrese el título del libro: ");
        string? nuevoLibro = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(nuevoLibro))
        {
            Console.WriteLine("El título no puede estar vacío.");
            return false;
        }

        Libros.Add(nuevoLibro.Trim());
        Console.WriteLine("Libro agregado correctamente.");
        return true;
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