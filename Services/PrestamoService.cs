using Proyecto_Bliblotecta.Models;

namespace Proyecto_Bliblotecta.Services;

/// <summary>
/// Administra préstamos y disponibilidad de libros en memoria.
/// </summary>
public class PrestamoService
{
    public const int PlazoPrestamoDias = Prestamo.DuracionPredeterminadaDias;

    private readonly List<Prestamo> prestamos = new();
    private readonly LibroService? libroService;

    public PrestamoService(LibroService? libroService = null)
    {
        this.libroService = libroService;
    }

    internal void ReemplazarTodos(IEnumerable<Prestamo> nuevosPrestamos)
    {
        ArgumentNullException.ThrowIfNull(nuevosPrestamos);

        List<Libro> libros = libroService?.ObtenerTodos() ?? new List<Libro>();
        foreach (Libro libro in libros)
        {
            libro.Disponible = true;
        }

        List<Prestamo> reemplazo = nuevosPrestamos.ToList();
        if (reemplazo.Select(item => item.Id).Distinct().Count() != reemplazo.Count)
        {
            throw new InvalidDataException("Hay préstamos con identificadores duplicados.");
        }

        foreach (Prestamo prestamo in reemplazo)
        {
            Libro? libro = ObtenerLibro(prestamo, requiereDisponible: !prestamo.EstaDevuelto);
            if (!prestamo.EstaDevuelto && libro is null)
            {
                throw new InvalidDataException(
                    $"No se encontró un ejemplar disponible para el préstamo {prestamo.Id}.");
            }

            if (libro is not null)
            {
                prestamo.AsociarLibro(libro);
                if (!prestamo.EstaDevuelto)
                {
                    libro.Disponible = false;
                }
            }
        }

        prestamos.Clear();
        prestamos.AddRange(reemplazo);
    }

    /// <summary>
    /// Registra un préstamo solo si el usuario está activo y el libro disponible.
    /// </summary>
    public bool AgregarPrestamo(Prestamo? prestamo)
    {
        if (prestamo is null ||
            prestamo.EstaDevuelto ||
            prestamos.Any(item => item.Id == prestamo.Id) ||
            prestamo.Usuario is null ||
            !prestamo.Usuario.Activo)
        {
            return false;
        }

        Libro? libro = ObtenerLibro(prestamo, requiereDisponible: true);
        if (libro is null || !libro.Disponible)
        {
            return false;
        }

        prestamo.AsociarLibro(libro);
        libro.Disponible = false;
        prestamos.Add(prestamo);
        return true;
    }

    /// <summary>
    /// Registra la devolución de un préstamo activo y libera su libro.
    /// </summary>
    public bool RegistrarDevolucion(int idPrestamo)
    {
        Prestamo? prestamo = BuscarPorId(idPrestamo);
        if (prestamo is null || prestamo.EstaDevuelto)
        {
            return false;
        }

        Libro? libro = ObtenerLibro(prestamo);
        if (libro is null)
        {
            return false;
        }

        prestamo.RegistrarDevolucion();
        libro.Disponible = true;
        return true;
    }

    /// <summary>
    /// Elimina únicamente préstamos que ya fueron devueltos.
    /// </summary>
    public bool EliminarPrestamo(int idPrestamo)
    {
        Prestamo? prestamo = BuscarPorId(idPrestamo);
        return prestamo is not null &&
            prestamo.EstaDevuelto &&
            prestamos.Remove(prestamo);
    }

    /// <summary>
    /// Retorna una copia independiente de los préstamos registrados.
    /// </summary>
    public List<Prestamo> ObtenerTodos()
    {
        return prestamos.ToList();
    }

    public Prestamo? BuscarPorId(int id)
    {
        return prestamos.FirstOrDefault(item => item.Id == id);
    }

    public List<Prestamo> BuscarPorEstado(EstadoPrestamo estado)
    {
        return prestamos
            .Where(item => item.Estado == estado)
            .ToList();
    }

    /// <summary>
    /// Ordena los préstamos por fecha límite, del vencimiento más próximo al más lejano.
    /// </summary>
    public List<Prestamo> OrdenarPorFechaLimite()
    {
        return prestamos
            .OrderBy(item => item.FechaLimite)
            .ThenBy(item => item.Id)
            .ToList();
    }

    public int ObtenerTotalPrestamos()
    {
        return prestamos.Count;
    }

    public int ObtenerPrestamosActivos()
    {
        return prestamos.Count(item => !item.EstaDevuelto);
    }

    public int ObtenerPrestamosVencidos()
    {
        return prestamos.Count(item => item.EstaVencido());
    }

    public int ObtenerPrestamosDevueltos()
    {
        return prestamos.Count(item => item.EstaDevuelto);
    }

    public double ObtenerPromedioDiasPrestamo()
    {
        return prestamos.Count == 0
            ? 0
            : prestamos.Average(item => item.DiasTranscurridos());
    }

    /// <summary>
    /// Retorna los indicadores principales de préstamos en formato legible.
    /// </summary>
    public string ObtenerResumenKPIs()
    {
        return $"Total: {ObtenerTotalPrestamos()} | Activos: {ObtenerPrestamosActivos()} | " +
               $"Vencidos: {ObtenerPrestamosVencidos()} | Devueltos: {ObtenerPrestamosDevueltos()} | " +
               $"Promedio de días: {ObtenerPromedioDiasPrestamo():F1}";
    }

    private Libro? ObtenerLibro(Prestamo prestamo, bool requiereDisponible = false)
    {
        if (prestamo.LibroAsociado is not null)
        {
            return prestamo.LibroAsociado;
        }

        if (libroService is null)
        {
            return null;
        }

        List<Libro> libros = libroService.ObtenerTodos();
        IEnumerable<Libro> porIsbn = libros.Where(item =>
            string.Equals(item.Isbn.Trim(), prestamo.Libro.Trim(), StringComparison.OrdinalIgnoreCase));
        IEnumerable<Libro> porTitulo = libros.Where(item =>
            string.Equals(item.Titulo.Trim(), prestamo.Libro.Trim(), StringComparison.OrdinalIgnoreCase));

        Libro? libro = (requiereDisponible ? porIsbn.Where(item => item.Disponible) : porIsbn).FirstOrDefault();
        libro ??= (requiereDisponible ? porTitulo.Where(item => item.Disponible) : porTitulo).FirstOrDefault();

        if (libro is not null)
        {
            prestamo.AsociarLibro(libro);
        }

        return libro;
    }
}
