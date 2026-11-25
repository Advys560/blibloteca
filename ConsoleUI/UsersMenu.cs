using Proyecto_Bliblotecta.Infrastructure;
using Proyecto_Bliblotecta.Models;
using Proyecto_Bliblotecta.Services;

namespace Proyecto_Bliblotecta.ConsoleUI;

public class UsersMenu
{
    private readonly UsuarioService _usuarios;
    private readonly Persistence _persistence;

    public UsersMenu(UsuarioService usuarios, Persistence persistence)
    {
        _usuarios = usuarios;
        _persistence = persistence;
    }

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
            Console.WriteLine("1. Registrar usuario");
            Console.WriteLine("2. Listar usuarios");
            Console.WriteLine("3. Volver al menú principal");
            Console.WriteLine("========================================");
            Console.Write("Seleccione una opción: ");

            switch (Console.ReadLine())
            {
                case "1":
                    AgregarUsuario();
                    break;
                case "2":
                    MostrarListaUsuarios();
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

    private void AgregarUsuario()
    {
        Console.Write("Ingrese el nombre del usuario: ");
        string? nombre = Console.ReadLine();
        Console.Write("Ingrese el correo del usuario: ");
        string? correoIngresado = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(correoIngresado))
        {
            Console.WriteLine("Debe ingresar un nombre y un correo válidos.");
            return;
        }

        string correo = correoIngresado.Trim();
        if (!correo.Contains('@'))
        {
            correo += "@gmail.com";
        }

        int id = _usuarios.ObtenerTodos().Select(usuario => usuario.Id).DefaultIfEmpty(0).Max() + 1;
        Usuario usuario = new()
        {
            Id = id,
            Nombre = nombre.Trim(),
            Correo = correo,
            Activo = true
        };

        if (!_usuarios.AgregarUsuario(usuario))
        {
            Console.WriteLine("No se pudo registrar el usuario; revise que el correo no esté duplicado.");
            return;
        }

        _persistence.GuardarDatos();
        Console.WriteLine("¡Usuario agregado correctamente!");
    }

    private void MostrarListaUsuarios()
    {
        List<Usuario> usuarios = _usuarios.ObtenerTodos();
        if (usuarios.Count == 0)
        {
            Console.WriteLine("No hay usuarios registrados.");
            return;
        }

        foreach (Usuario usuario in usuarios)
        {
            Console.WriteLine(usuario.DetalleCompleto());
        }
    }
}
