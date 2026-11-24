using Proyecto_Bliblotecta.Services;

public class SearchAndReports
{
    private readonly LibroService _libros;
    private readonly PrestamoService _prestamos;

    public SearchAndReports(LibroService libros, PrestamoService prestamos)
    {
        _libros = libros;
        _prestamos = prestamos;
    }

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
            if (opcion == null)
            {
                return;
            }

            switch (opcion)
            {
                case "1":
                    BuscarLibro();
                    break;
                case "2":
                    MostrarReportePrestamos();
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

    private void BuscarLibro()
    {
        Console.Write("Ingrese el título o parte del título que desea buscar: ");
        string? termino = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(termino))
        {
            Console.WriteLine("El término de búsqueda no puede estar vacío.");
            return;
        }

        List<Libro> resultados = _libros.BuscarPorTitulo(termino.Trim());
        if (resultados.Count == 0)
        {
            Console.WriteLine("No se encontraron libros con ese título.");
            return;
        }

        Console.WriteLine("\nLibros encontrados:");
        foreach (Libro libro in resultados)
        {
            Console.WriteLine($"- {libro.Titulo}");
        }
    }

    private void MostrarReportePrestamos()
    {
        List<Prestamo> prestamos = _prestamos.ObtenerTodos();
        if (prestamos.Count == 0)
        {
            Console.WriteLine("No hay préstamos registrados para generar el reporte.");
            return;
        }

        Console.WriteLine("\nReporte de préstamos:");
        foreach (Prestamo prestamo in prestamos)
        {
            Console.WriteLine(prestamo.ObtenerDetalle());
        }
    }
}