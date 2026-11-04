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
                Console.WriteLine("Funcionalidad de usuarios registrados aún no implementada.");
                return true;
            case "3":
                Console.WriteLine("Funcionalidad de préstamos aún no implementada.");
                return true;
            case "4":
                Console.WriteLine("Funcionalidad de búsquedas y reportes aún no implementada.");
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
            Console.WriteLine("4. Volver al menú principal");
            Console.WriteLine("========================================");
            Console.Write("Seleccione una opción: ");

            string? opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    Console.WriteLine("1. el principio de la eternidad");
                    break;
                case "2":
                    Console.WriteLine("1. el principio de la eternidad");
                    break;
                case "3":
                    Console.WriteLine("1. el principio de la eternidad");
                    break;
                case "4":
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