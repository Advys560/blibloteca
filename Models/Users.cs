public class Users
{
    private readonly Persistence _persistence;

    public Users(Persistence persistence)
    {
        _persistence = persistence;
    }

    public static List<Usuario> Usuarios { get; } = new()
    {
        new Usuario { Id = 1, Nombre = "Juan Pérez", Correo = "juan@email.com", Activo = true },
        new Usuario { Id = 2, Nombre = "María Gómez", Correo = "maria@email.com", Activo = true },
        new Usuario { Id = 3, Nombre = "Carlos López", Correo = "carlos@email.com", Activo = true }
    };

    public void MostrarUsuarios()
    {
        while (true)
        {
            if (!Console.IsOutputRedirected)
            {
                Console.Clear();
            }

            Console.WriteLine("========================================");
            Console.WriteLine("       GESTIÓN DE USUARIOS");
            Console.WriteLine("========================================");
            Console.WriteLine("1. Registrar Usuario");
            Console.WriteLine("2. Listar usuarios");
            Console.WriteLine("3. Volver al menú principal");
            Console.WriteLine("========================================");
            Console.Write("Seleccione una opción: ");

            string? opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    Console.Write("Ingrese el nombre del usuario: ");
                    string? nuevoNombre = Console.ReadLine();
                    Console.Write("Ingrese el correo del usuario: ");
                    string? nuevoCorreo = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(nuevoNombre) || string.IsNullOrWhiteSpace(nuevoCorreo))
                    {
                        Console.WriteLine("Debe ingresar un nombre y un correo válidos.");
                        break;
                    }

                    string correo = nuevoCorreo.Trim();
                    if (!correo.Contains('@'))
                    {
                        correo += "@gmail.com";
                    }

                    Usuario nuevoUsuario = new()
                    {
                        Id = Usuarios.Count == 0 ? 1 : Usuarios.Max(usuario => usuario.Id) + 1,
                        Nombre = nuevoNombre.Trim(),
                        Correo = correo,
                        Activo = true
                    };

                    Usuarios.Add(nuevoUsuario);
                    _persistence.GuardarDatos();
                    Console.WriteLine("¡Usuario agregado correctamente!");
                    break;

                case "2":
                    MostrarListaUsuarios(Usuarios, "No hay usuarios registrados.");
                    break;

                case "3":
                    Console.WriteLine("Volviendo al menú principal...");
                    return;

                default:
                    Console.WriteLine("Opción inválida. Por favor, seleccione una opción válida.");
                    break;
            }

            Console.WriteLine("\nPresione Enter para volver al menú de usuarios.");
            Console.ReadLine();
        }
    }

    private static void MostrarListaUsuarios(IEnumerable<Usuario> usuarios, string mensajeVacio)
    {
        List<Usuario> resultados = usuarios.ToList();

        if (resultados.Count == 0)
        {
            Console.WriteLine(mensajeVacio);
            return;
        }

        foreach (Usuario usuario in resultados)
        {
            Console.WriteLine(usuario.DetalleCompleto());
        }
    }
}