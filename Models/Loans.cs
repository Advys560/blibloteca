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

public class Loans
{
    private readonly List<Prestamo> _prestamos = new();
    private readonly Persistence _persistence;

    public Loans(Persistence persistence)
    {
        _persistence = persistence;
    }

    internal IReadOnlyList<Prestamo> ObtenerTodosLosPrestamos()
    {
        return _prestamos.ToList();
    }

    internal void ReemplazarPrestamos(IEnumerable<Prestamo> prestamos)
    {
        _prestamos.Clear();
        _prestamos.AddRange(prestamos);
    }

    public IReadOnlyList<string> ObtenerLibrosDisponibles()
    {
        Dictionary<string, int> cantidadesPrestadas = _prestamos
            .Where(prestamo => !prestamo.EstaDevuelto)
            .GroupBy(prestamo => prestamo.Libro, StringComparer.OrdinalIgnoreCase)
            .ToDictionary(grupo => grupo.Key, grupo => grupo.Count(), StringComparer.OrdinalIgnoreCase);

        List<string> librosDisponibles = new();
        foreach (string libro in CatalogoLibros.Libros)
        {
            if (cantidadesPrestadas.TryGetValue(libro, out int cantidadPrestada) && cantidadPrestada > 0)
            {
                cantidadesPrestadas[libro] = cantidadPrestada - 1;
            }
            else
            {
                librosDisponibles.Add(libro);
            }
        }

        return librosDisponibles;
    }

    public IReadOnlyList<Prestamo> ObtenerPrestamosActivos()
    {
        return _prestamos.Where(prestamo => !prestamo.EstaDevuelto).ToList();
    }

    public void MostrarPrestamos()
    {
        while (true)
        {
            if (!Console.IsOutputRedirected)
            {
                Console.Clear();
            }

            MostrarOpciones();
            string? opcion = Console.ReadLine();

            if (opcion == null || !ProcesarOpcion(opcion))
            {
                return;
            }

            Console.WriteLine("\nPresione Enter para volver al menú de préstamos.");
            Console.ReadLine();
        }
    }

    private static void MostrarOpciones()
    {
        Console.WriteLine("========================================");
        Console.WriteLine("       GESTIÓN DE PRÉSTAMOS");
        Console.WriteLine("========================================");
        Console.WriteLine("1. Registrar préstamo");
        Console.WriteLine("2. Listar préstamos");
        Console.WriteLine("3. Registrar devolución");
        Console.WriteLine("4. Consultar catálogo de libros");
        Console.WriteLine("5. Volver al menú principal");
        Console.WriteLine("========================================");
        Console.Write("Seleccione una opción: ");
    }

    private bool ProcesarOpcion(string opcion)
    {
        switch (opcion)
        {
            case "1":
                RegistrarPrestamo();
                break;
            case "2":
                MostrarPrestamosRegistrados();
                break;
            case "3":
                RegistrarDevolucion();
                break;
            case "4":
                CatalogoLibros.MostrarLibros();
                break;
            case "5":
                Console.WriteLine("Volviendo al menú principal...");
                return false;
            default:
                Console.WriteLine("Opción inválida. Por favor, seleccione una opción válida.");
                break;
        }

        return true;
    }

    private void RegistrarPrestamo()
    {
        List<Usuario> usuarios = Users.Usuarios.Where(usuario => usuario.Activo).ToList();
        if (usuarios.Count == 0)
        {
            Console.WriteLine("No hay usuarios activos para registrar un préstamo.");
            return;
        }

        List<string> libros = ObtenerLibrosDisponibles().ToList();
        if (libros.Count == 0)
        {
            Console.WriteLine("No hay libros disponibles para prestar.");
            return;
        }

        Console.WriteLine("\nUsuarios disponibles:");
        for (int i = 0; i < usuarios.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {usuarios[i].Nombre} ({usuarios[i].Correo})");
        }

        int? indiceUsuario = LeerSeleccion(usuarios.Count, "usuario");
        if (!indiceUsuario.HasValue)
        {
            return;
        }

        Console.WriteLine("\nLibros disponibles:");
        for (int i = 0; i < libros.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {libros[i]}");
        }

        int? indiceLibro = LeerSeleccion(libros.Count, "libro");
        if (!indiceLibro.HasValue)
        {
            return;
        }

        int nuevoId = _prestamos.Count == 0 ? 1 : _prestamos.Max(prestamo => prestamo.Id) + 1;
        Prestamo prestamo = new(nuevoId, usuarios[indiceUsuario.Value - 1], libros[indiceLibro.Value - 1]);
        _prestamos.Add(prestamo);
        _persistence.GuardarDatos();
        Console.WriteLine("Préstamo registrado correctamente.");
        Console.WriteLine(prestamo.ObtenerDetalle());
    }

    private void MostrarPrestamosRegistrados()
    {
        if (_prestamos.Count == 0)
        {
            Console.WriteLine("No hay préstamos registrados.");
            return;
        }

        Console.WriteLine("\nPréstamos registrados:");
        foreach (Prestamo prestamo in _prestamos)
        {
            Console.WriteLine(prestamo.ObtenerDetalle());
        }
    }

    private void RegistrarDevolucion()
    {
        List<Prestamo> pendientes = _prestamos
            .Where(prestamo => !prestamo.EstaDevuelto)
            .ToList();

        if (pendientes.Count == 0)
        {
            Console.WriteLine("No hay préstamos pendientes de devolución.");
            return;
        }

        Console.WriteLine("\nPréstamos pendientes:");
        for (int i = 0; i < pendientes.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {pendientes[i].ObtenerDetalle()}");
        }

        int? indicePrestamo = LeerSeleccion(pendientes.Count, "préstamo");
        if (!indicePrestamo.HasValue)
        {
            return;
        }

        Prestamo prestamo = pendientes[indicePrestamo.Value - 1];
        prestamo.RegistrarDevolucion();
        _persistence.GuardarDatos();
        Console.WriteLine("Devolución registrada correctamente.");
        Console.WriteLine(prestamo.ObtenerDetalle());
    }

    private static int? LeerSeleccion(int cantidadOpciones, string elemento)
    {
        Console.Write($"Seleccione el número del {elemento}: ");
        if (!int.TryParse(Console.ReadLine(), out int seleccion) ||
            seleccion < 1 ||
            seleccion > cantidadOpciones)
        {
            Console.WriteLine($"La selección del {elemento} no es válida.");
            return null;
        }

        return seleccion;
    }
}
