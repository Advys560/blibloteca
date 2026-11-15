public class SearchAndReports
{
    private readonly Loans _loans;

    public SearchAndReports(Loans loans)
    {
        _loans = loans;
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
                    BuscarLibroPorTitulo();
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

    public void BuscarLibroPorTitulo()
    {
        Console.Write("Ingrese el título o parte del título que desea buscar: ");
        string? termino = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(termino))
        {
            Console.WriteLine("El término de búsqueda no puede estar vacío.");
            return;
        }

        CatalogoLibros.BuscarLibros(termino.Trim());
    }

    public void MostrarReportePrestamos()
    {
        IReadOnlyList<Prestamo> prestamos = _loans.ObtenerTodosLosPrestamos();
        if (prestamos.Count == 0)
        {
            Console.WriteLine("No hay préstamos registrados para generar el reporte.");
            return;
        }

        int pendientes = prestamos.Count(prestamo => !prestamo.EstaDevuelto);
        int devueltos = prestamos.Count - pendientes;

        Console.WriteLine("\nREPORTE DE PRÉSTAMOS");
        Console.WriteLine($"Total registrados: {prestamos.Count}");
        Console.WriteLine($"Pendientes: {pendientes}");
        Console.WriteLine($"Devueltos: {devueltos}");

        foreach (Prestamo prestamo in prestamos)
        {
            Console.WriteLine(prestamo.ObtenerDetalle());
        }
    }
}
