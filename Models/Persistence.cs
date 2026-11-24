using System.Text.Json;
using Proyecto_Bliblotecta.Services;

public class Persistence
{
    private static readonly string CarpetaDatos = ObtenerCarpetaDatos();
    private static readonly string RutaArchivo = Path.Combine(CarpetaDatos, "biblioteca.json");
    private readonly JsonSerializerOptions _opcionesJson = new() { WriteIndented = true };
    private readonly LibroService _libroService;
    private readonly UsuarioService _usuarioService;
    private readonly PrestamoService _prestamoService;

    public Persistence(
        LibroService libroService,
        UsuarioService usuarioService,
        PrestamoService prestamoService)
    {
        _libroService = libroService;
        _usuarioService = usuarioService;
        _prestamoService = prestamoService;
    }

    public void MostrarMenu()
    {
        while (true)
        {
            if (!Console.IsOutputRedirected)
            {
                Console.Clear();
            }

            Console.WriteLine("\n--- PERSISTENCIA ---");
            Console.WriteLine("1. Guardar datos");
            Console.WriteLine("2. Cargar datos");
            Console.WriteLine("3. Reiniciar datos (vaciar todo)");
            Console.WriteLine("4. Volver al menú principal");
            Console.Write("Seleccione una opción: ");

            string? opcion = Console.ReadLine();
            if (opcion == null)
            {
                return;
            }

            switch (opcion)
            {
                case "1":
                    GuardarDatos();
                    break;
                case "2":
                    CargarDatos();
                    break;
                case "3":
                    if (Confirmar("¿Está seguro de que desea vaciar todos los libros, usuarios y préstamos? (S/N): "))
                    {
                        ReiniciarDatos();
                    }
                    else
                    {
                        Console.WriteLine("Reinicio cancelado.");
                    }

                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Opción inválida. Seleccione una opción del menú.");
                    break;
            }

            Console.WriteLine("\nPresione Enter para volver al menú de persistencia.");
            Console.ReadLine();
        }
    }

    public void GuardarDatos()
    {
        try
        {
            Directory.CreateDirectory(CarpetaDatos);
            DatosBiblioteca datos = new()
            {
                Libros = _libroService.ObtenerTodos().Select(libro => libro.Titulo).ToList(),
                LibrosCompletos = _libroService.ObtenerTodos(),
                Usuarios = _usuarioService.ObtenerTodos(),
                Prestamos = _prestamoService.ObtenerTodos()
                    .Select(prestamo => new PrestamoGuardado
                    {
                        Id = prestamo.Id,
                        UsuarioId = prestamo.Usuario.Id,
                        Libro = prestamo.Libro,
                        FechaPrestamo = prestamo.FechaPrestamo,
                        FechaDevolucion = prestamo.FechaDevolucion
                    })
                    .ToList()
            };

            string contenido = JsonSerializer.Serialize(datos, _opcionesJson);
            File.WriteAllText(RutaArchivo, contenido);
            Console.WriteLine($"Datos guardados correctamente en: {RutaArchivo}");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"No se pudieron guardar los datos: {ex.Message}");
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine($"No hay permisos para guardar los datos: {ex.Message}");
        }
    }

    public void CargarDatos(bool mostrarMensaje = true)
    {
        if (!File.Exists(RutaArchivo))
        {
            if (mostrarMensaje)
            {
                Console.WriteLine("No existe un archivo guardado para cargar.");
            }

            return;
        }

        try
        {
            string contenido = File.ReadAllText(RutaArchivo);
            DatosBiblioteca? datos = JsonSerializer.Deserialize<DatosBiblioteca>(contenido);
            if (datos == null)
            {
                throw new InvalidDataException("El archivo no contiene datos válidos.");
            }

            List<string> titulosLibros = datos.Libros
                ?? throw new InvalidDataException("El archivo no contiene una lista válida de libros.");
            List<Usuario> usuarios = datos.Usuarios
                ?? throw new InvalidDataException("El archivo no contiene una lista válida de usuarios.");
            List<PrestamoGuardado> prestamosGuardados = datos.Prestamos ?? new List<PrestamoGuardado>();
            List<Libro> libros = datos.LibrosCompletos
                ?? titulosLibros
                    .Select((titulo, indice) => new Libro(
                        indice + 1,
                        $"LEGACY-{indice + 1}",
                        titulo,
                        string.Empty,
                        0))
                    .ToList();

            if (libros.Any(libro =>
                    libro is null ||
                    libro.Id <= 0 ||
                    string.IsNullOrWhiteSpace(libro.Isbn) ||
                    string.IsNullOrWhiteSpace(libro.Titulo)))
            {
                throw new InvalidDataException("La lista de libros contiene datos inválidos.");
            }

            titulosLibros = libros.Select(libro => libro.Titulo).ToList();
            ValidarDatos(titulosLibros, usuarios, prestamosGuardados);
            Dictionary<int, Usuario> usuariosPorId = usuarios.ToDictionary(usuario => usuario.Id);
            List<Prestamo> prestamos = prestamosGuardados
                .Select(prestamo => new Prestamo(
                    prestamo.Id,
                    usuariosPorId[prestamo.UsuarioId],
                    prestamo.Libro,
                    prestamo.FechaPrestamo,
                    prestamo.FechaDevolucion))
                .ToList();

            _libroService.ReemplazarTodos(libros);
            _usuarioService.ReemplazarTodos(usuarios);
            _prestamoService.ReemplazarTodos(prestamos);

            if (mostrarMensaje)
            {
                Console.WriteLine("Datos cargados correctamente.");
            }
        }
        catch (IOException ex)
        {
            Console.WriteLine($"No se pudieron leer los datos: {ex.Message}");
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine($"No hay permisos para leer los datos: {ex.Message}");
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"El archivo de datos no contiene JSON válido: {ex.Message}");
        }
        catch (InvalidDataException ex)
        {
            Console.WriteLine($"No se pudieron cargar los datos: {ex.Message}");
        }
    }

    public void ReiniciarDatos()
    {
        _libroService.ReemplazarTodos(Array.Empty<Libro>());
        _usuarioService.ReemplazarTodos(Array.Empty<Usuario>());
        _prestamoService.ReemplazarTodos(Array.Empty<Prestamo>());
        GuardarDatos();

        Console.WriteLine("Se vaciaron todos los libros, usuarios y préstamos.");
    }

    private static void ValidarDatos(
        IReadOnlyCollection<string> libros,
        IReadOnlyCollection<Usuario> usuarios,
        IReadOnlyCollection<PrestamoGuardado> prestamos)
    {
        if (libros.Any(string.IsNullOrWhiteSpace))
        {
            throw new InvalidDataException("La lista de libros contiene títulos vacíos.");
        }

        if (usuarios.Any(usuario =>
                usuario == null ||
                usuario.Id <= 0 ||
                string.IsNullOrWhiteSpace(usuario.Nombre) ||
                string.IsNullOrWhiteSpace(usuario.Correo)))
        {
            throw new InvalidDataException("La lista de usuarios contiene datos inválidos.");
        }

        if (usuarios.Select(usuario => usuario.Id).Distinct().Count() != usuarios.Count)
        {
            throw new InvalidDataException("Hay usuarios con identificadores duplicados.");
        }

        HashSet<int> idsPrestamos = new();
        HashSet<int> idsUsuarios = usuarios.Select(usuario => usuario.Id).ToHashSet();
        foreach (PrestamoGuardado? prestamo in prestamos)
        {
            if (prestamo == null ||
                prestamo.Id <= 0 ||
                !idsPrestamos.Add(prestamo.Id) ||
                !idsUsuarios.Contains(prestamo.UsuarioId) ||
                string.IsNullOrWhiteSpace(prestamo.Libro) ||
                !libros.Contains(prestamo.Libro, StringComparer.OrdinalIgnoreCase) ||
                prestamo.FechaPrestamo == default ||
                (prestamo.FechaDevolucion.HasValue && prestamo.FechaDevolucion < prestamo.FechaPrestamo))
            {
                throw new InvalidDataException("La lista de préstamos contiene datos inválidos.");
            }
        }
    }

    private static bool Confirmar(string mensaje)
    {
        while (true)
        {
            Console.Write(mensaje);
            string? respuesta = Console.ReadLine()?.Trim();

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

    private static string ObtenerCarpetaDatos()
    {
        string carpetaLocal = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        return string.IsNullOrWhiteSpace(carpetaLocal)
            ? Path.Combine(AppContext.BaseDirectory, "ProyectoBiblioteca")
            : Path.Combine(carpetaLocal, "ProyectoBiblioteca");
    }

    private sealed class DatosBiblioteca
    {
        public DatosBiblioteca()
        {
        }

        public List<string>? Libros { get; set; }
        public List<Libro>? LibrosCompletos { get; set; }
        public List<Usuario>? Usuarios { get; set; }
        public List<PrestamoGuardado>? Prestamos { get; set; }
    }

    private sealed class PrestamoGuardado
    {
        public PrestamoGuardado()
        {
        }

        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Libro { get; set; } = string.Empty;
        public DateTime FechaPrestamo { get; set; }
        public DateTime? FechaDevolucion { get; set; }
    }
}
