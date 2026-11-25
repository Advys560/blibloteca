namespace Proyecto_Bliblotecta.Models;

public sealed class Prestamo
{
    public const int DuracionPredeterminadaDias = 14;

    public Prestamo(int id, Usuario usuario, string libro)
        : this(id, usuario, libro, DateTime.Now, null)
    {
    }

    public Prestamo(int id, Usuario usuario, Libro libro)
        : this(id, usuario, libro?.Titulo ?? throw new ArgumentNullException(nameof(libro)), DateTime.Now, null, libro)
    {
    }

    internal Prestamo(
        int id,
        Usuario usuario,
        string libro,
        DateTime fechaPrestamo,
        DateTime? fechaDevolucion,
        Libro? libroAsociado = null)
    {
        Id = id;
        Usuario = usuario;
        Libro = libro;
        FechaPrestamo = fechaPrestamo;
        FechaDevolucion = fechaDevolucion;
        FechaLimite = fechaPrestamo.AddDays(DuracionPredeterminadaDias);
        LibroAsociado = libroAsociado;
    }

    public int Id { get; }
    public Usuario Usuario { get; }
    public string Libro { get; }
    public DateTime FechaPrestamo { get; }
    public DateTime FechaLimite { get; }
    public DateTime? FechaDevolucion { get; private set; }
    public bool EstaDevuelto => FechaDevolucion.HasValue;
    public bool EstaVencido() => !EstaDevuelto && DateTime.Now > FechaLimite;
    public EstadoPrestamo Estado => EstaDevuelto
        ? EstadoPrestamo.Devuelto
        : EstaVencido()
            ? EstadoPrestamo.Vencido
            : EstadoPrestamo.Pendiente;

    public Libro? LibroAsociado { get; private set; }

    public int DiasTranscurridos()
    {
        DateTime fechaFinal = FechaDevolucion ?? DateTime.Now;
        return (fechaFinal.Date - FechaPrestamo.Date).Days;
    }

    internal void AsociarLibro(Libro libro)
    {
        LibroAsociado = libro;
    }

    public void RegistrarDevolucion()
    {
        if (EstaDevuelto)
        {
            throw new InvalidOperationException("El préstamo ya fue devuelto.");
        }

        FechaDevolucion = DateTime.Now;
    }

    public string ObtenerDetalle()
    {
        string estado = EstaDevuelto
            ? $"Devuelto el {FechaDevolucion:dd/MM/yyyy}"
            : "Pendiente";

        return $"Préstamo {Id} | Libro: {Libro} | Usuario: {Usuario.Nombre} | " +
               $"Fecha: {FechaPrestamo:dd/MM/yyyy} | Estado: {estado}";
    }
}
