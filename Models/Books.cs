public class Books
{
    private readonly Loans _loans;

    public Books(Loans loans)
    {
        _loans = loans;
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
            Console.WriteLine("1. Catalogo");
            Console.WriteLine("2. Consultar Libros disponibles");
            Console.WriteLine("3. Libros prestados");
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
                    MostrarLibrosDisponibles();
                    break;
                case "3":
                    MostrarLibrosPrestados();
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

    private void MostrarLibrosDisponibles()
    {
        IReadOnlyList<string> libros = _loans.ObtenerLibrosDisponibles();
        if (libros.Count == 0)
        {
            Console.WriteLine("No hay libros disponibles.");
            return;
        }

        Console.WriteLine("\nLibros disponibles:");
        for (int i = 0; i < libros.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {libros[i]}");
        }
    }

    private void MostrarLibrosPrestados()
    {
        IReadOnlyList<Prestamo> prestamos = _loans.ObtenerPrestamosActivos();
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
}