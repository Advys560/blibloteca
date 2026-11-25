using Proyecto_Bliblotecta.Models;
using Proyecto_Bliblotecta.Services;

namespace Proyecto_Bliblotecta.ConsoleUI;

public class SearchAndReports
{
    private readonly LibroService _libros;
    private readonly UsuarioService _usuarios;
    private readonly PrestamoService _prestamos;

    public SearchAndReports(LibroService libros, UsuarioService usuarios, PrestamoService prestamos)
    {
        _libros = libros;
        _usuarios = usuarios;
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
            Console.WriteLine("3. Demostración de servicios, ordenamientos y estadísticas");
            Console.WriteLine("4. Volver al menú principal");
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
                    MostrarDemostracionServicios();
                    break;
                case "4":
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

    private void MostrarDemostracionServicios()
    {
        List<Libro> libros = _libros.ObtenerTodos();
        List<Usuario> usuarios = _usuarios.ObtenerTodos();
        List<Prestamo> prestamos = _prestamos.ObtenerTodos();

        Console.WriteLine("\n--- Búsqueda y ordenamiento de libros ---");
        if (libros.Count > 0)
        {
            Console.WriteLine($"Coincidencias por título \"{libros[0].Titulo}\":");
            foreach (Libro libro in _libros.BuscarPorTitulo(libros[0].Titulo))
            {
                Console.WriteLine($"- {libro.Titulo} | ISBN: {libro.Isbn}");
            }

            Console.WriteLine("Ordenados por título:");
            foreach (Libro libro in _libros.OrdenarPorTitulo())
            {
                Console.WriteLine($"- {libro.Titulo}");
            }
        }
        else
        {
            Console.WriteLine("No hay libros para buscar ni ordenar.");
        }

        Console.WriteLine("\n--- Búsqueda y ordenamiento de usuarios ---");
        if (usuarios.Count > 0)
        {
            Console.WriteLine($"Coincidencias por nombre \"{usuarios[0].Nombre}\":");
            foreach (Usuario usuario in _usuarios.BuscarPorNombre(usuarios[0].Nombre))
            {
                Console.WriteLine($"- {usuario.Nombre} | {usuario.Correo}");
            }

            Console.WriteLine("Ordenados por nombre:");
            foreach (Usuario usuario in _usuarios.OrdenarPorNombre())
            {
                Console.WriteLine($"- {usuario.Nombre}");
            }
        }
        else
        {
            Console.WriteLine("No hay usuarios para buscar ni ordenar.");
        }

        Console.WriteLine("\n--- Búsqueda y ordenamiento de préstamos ---");
        List<Prestamo> prestamosActivos = _prestamos.BuscarPorEstado(EstadoPrestamo.Pendiente);
        Console.WriteLine($"Préstamos activos/en espera: {prestamosActivos.Count}");
        foreach (Prestamo prestamo in prestamosActivos)
        {
            Console.WriteLine($"- {prestamo.ObtenerDetalle()}");
        }

        Console.WriteLine("Ordenados por fecha límite:");
        foreach (Prestamo prestamo in _prestamos.OrdenarPorFechaLimite())
        {
            Console.WriteLine($"- {prestamo.ObtenerDetalle()} | Límite: {prestamo.FechaLimite:dd/MM/yyyy}");
        }

        Console.WriteLine("\n--- KPIs ---");
        Console.WriteLine($"Libros: {_libros.ObtenerResumenKPIs()}");
        Console.WriteLine($"Usuarios: {_usuarios.ObtenerResumenKPIs()}");
        Console.WriteLine($"Préstamos: {_prestamos.ObtenerResumenKPIs()}");
        Console.WriteLine($"  Activos: {_prestamos.ObtenerPrestamosActivos()}");
        Console.WriteLine($"  Vencidos: {_prestamos.ObtenerPrestamosVencidos()}");
        Console.WriteLine($"  Devueltos: {_prestamos.ObtenerPrestamosDevueltos()}");
        Console.WriteLine($"  Promedio de días: {_prestamos.ObtenerPromedioDiasPrestamo():F1}");

        MostrarComparacionArrayYLista(libros);
    }

    private static void MostrarComparacionArrayYLista(List<Libro> libros)
    {
        Console.WriteLine("\n--- La misma operación con Array y List ---");

        // Ambas colecciones cuentan los libros disponibles; el array tiene tamaño fijo y List puede crecer.
        Libro[] arreglo = libros.ToArray();
        List<Libro> lista = libros.ToList();
        int disponiblesEnArreglo = arreglo.Count(libro => libro.Disponible);
        int disponiblesEnLista = lista.Count(libro => libro.Disponible);

        Console.WriteLine($"Array<Libro>: {disponiblesEnArreglo} disponibles (tamaño fijo: {arreglo.Length}).");
        Console.WriteLine($"List<Libro>: {disponiblesEnLista} disponibles (cantidad actual: {lista.Count}; permite agregar o quitar elementos).");
    }
}