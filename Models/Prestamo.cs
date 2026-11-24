public sealed class Prestamo
{
    public Prestamo(int id, Usuario usuario, string libro)
        : this(id, usuario, libro, DateTime.Now, null)
    {
    }

    internal Prestamo(int id, Usuario usuario, string libro, DateTime fechaPrestamo, DateTime? fechaDevolucion)
    {
        Id = id;
        Usuario = usuario;
        Libro = libro;
        FechaPrestamo = fechaPrestamo;
        FechaDevolucion = fechaDevolucion;
    }

    public int Id { get; }
    public Usuario Usuario { get; }
    public string Libro { get; }
    public DateTime FechaPrestamo { get; }
    public DateTime? FechaDevolucion { get; private set; }
    public bool EstaDevuelto => FechaDevolucion.HasValue;

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
