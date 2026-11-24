using Proyecto_Bliblotecta.Services;

namespace Proyecto_Bliblotecta.ConsoleUI;

public class BooksMenu
{
    private readonly LibroService _libros;
    private readonly PrestamoService _prestamos;
    private readonly Persistence _persistence;

    public BooksMenu(LibroService libros, PrestamoService prestamos, Persistence persistence)
    {
        _libros = libros;
        _prestamos = prestamos;
        _persistence = persistence;
    }

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
            Console.WriteLine("1. Catálogo");
            Console.WriteLine("2. Consultar libros disponibles");
            Console.WriteLine("3. Libros prestados");
            Console.WriteLine("4. Agregar libro");
            Console.WriteLine("5. Volver al menú principal");
            Console.WriteLine("========================================");
            Console.Write("Seleccione una opción: ");

            switch (Console.ReadLine())
            {
                case "1":
                    MostrarCatalogo();
                    break;
                case "2":
                    MostrarLibrosDisponibles();
                    break;
                case "3":
                    MostrarLibrosPrestados();
                    break;
                case "4":
                    AgregarLibro();
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

    private void MostrarCatalogo()
    {
        List<Libro> libros = _libros.ObtenerTodos();
        if (libros.Count == 0)
        {
            Console.WriteLine("No hay libros registrados.");
            return;
        }

        Console.WriteLine("\nLibros registrados:");
        foreach (Libro libro in libros)
        {
            Console.WriteLine($"{libro.Titulo} | Autor: {libro.Autor} | ISBN: {libro.Isbn} | " +
                              $"Estado: {(libro.Disponible ? "Disponible" : "Prestado")}");
        }
    }

    private void MostrarLibrosDisponibles()
    {
        List<Libro> libros = _libros.ObtenerTodos().Where(libro => libro.Disponible).ToList();
        if (libros.Count == 0)
        {
            Console.WriteLine("No hay libros disponibles.");
            return;
        }

        Console.WriteLine("\nLibros disponibles:");
        for (int i = 0; i < libros.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {libros[i].Titulo}");
        }
    }

    private void MostrarLibrosPrestados()
    {
        List<Prestamo> prestamos = _prestamos.ObtenerTodos()
            .Where(prestamo => !prestamo.EstaDevuelto)
            .ToList();
        if (prestamos.Count == 0)
        {
            Console.WriteLine("No hay libros prestados.");
            return;
        }

        Console.WriteLine("\nLibros prestados:");
        foreach (Prestamo prestamo in prestamos)
        {
            Console.WriteLine(prestamo.ObtenerDetalle());
        }
    }

    private void AgregarLibro()
    {
        Console.Write("Ingrese el título del libro: ");
        string? titulo = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(titulo))
        {
            Console.WriteLine("El título no puede estar vacío.");
            return;
        }

        int id = _libros.ObtenerTodos().Select(libro => libro.Id).DefaultIfEmpty(0).Max() + 1;
        Libro libro = new(id, $"LOCAL-{id}", titulo.Trim(), "Desconocido", 0);
        if (!_libros.AgregarLibro(libro))
        {
            Console.WriteLine("No se pudo agregar el libro.");
            return;
        }

        _persistence.GuardarDatos();
        Console.WriteLine("Libro agregado correctamente.");
    }
}
