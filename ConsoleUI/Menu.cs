using Proyecto_Bliblotecta.Infrastructure;
using Proyecto_Bliblotecta.Models;
using Proyecto_Bliblotecta.ConsoleUI;
using Proyecto_Bliblotecta.Services;

namespace Proyecto_Bliblotecta.ConsoleUI;

public class Menu
{
    private readonly BooksMenu _books;
    private readonly UsersMenu _users;
    private readonly LoansMenu _loans;
    private readonly SearchAndReports _searchAndReports;
    private readonly Persistence _persistence;

    public Menu(
        LibroService libros,
        UsuarioService usuarios,
        PrestamoService prestamos,
        Persistence persistence)
    {
        _books = new BooksMenu(libros, prestamos, persistence);
        _users = new UsersMenu(usuarios, persistence);
        _loans = new LoansMenu(prestamos, libros, usuarios, persistence);
        _searchAndReports = new SearchAndReports(libros, usuarios, prestamos);
        _persistence = persistence;
    }

    public void MostrarMenu()
    {
        while (true)
        {
            MostrarOpciones();
            string? opcion = Console.ReadLine();

            if (opcion == null)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(opcion))
            {
                Console.WriteLine("Debe ingresar una opción válida.");
                continue;
            }

            if (!ProcesarOpcion(opcion))
            {
                return;
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
                _books.MostrarLibros();
                return true;
            case "2":
                _users.MostrarUsuarios();
                return true;
            case "3":
                _loans.MostrarPrestamos();
                return true;
            case "4":
                _searchAndReports.MostrarBusquedaYReportes();
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
                return true;
        }
    }

    private static bool Confirmar(string mensaje)
    {
        while (true)
        {
            Console.Write(mensaje);
            string? respuesta = Console.ReadLine()?.Trim();

            if (respuesta == null)
            {
                return false;
            }

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
