using Proyecto_Bliblotecta.Infrastructure;
using Proyecto_Bliblotecta.Models;
using Proyecto_Bliblotecta.Services;

namespace Proyecto_Bliblotecta.ConsoleUI;

public class LoansMenu
{
    private readonly PrestamoService _prestamos;
    private readonly LibroService _libros;
    private readonly UsuarioService _usuarios;
    private readonly Persistence _persistence;

    public LoansMenu(
        PrestamoService prestamos,
        LibroService libros,
        UsuarioService usuarios,
        Persistence persistence)
    {
        _prestamos = prestamos;
        _libros = libros;
        _usuarios = usuarios;
        _persistence = persistence;
    }

    public void MostrarPrestamos()
    {
        while (true)
        {
            if (!Console.IsOutputRedirected)
            {
                Console.Clear();
            }

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

            switch (Console.ReadLine())
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
                    MostrarCatalogo();
                    break;
                case "5":
                    Console.WriteLine("Volviendo al menú principal...");
                    return;
                default:
                    Console.WriteLine("Opción inválida. Por favor, seleccione una opción válida.");
                    break;
            }

            Console.WriteLine("\nPresione Enter para volver al menú de préstamos.");
            Console.ReadLine();
        }
    }

    private void RegistrarPrestamo()
    {
        List<Usuario> usuarios = _usuarios.ObtenerTodos()
            .Where(usuario => usuario.Activo)
            .ToList();
        if (usuarios.Count == 0)
        {
            Console.WriteLine("No hay usuarios activos para registrar un préstamo.");
            return;
        }

        List<Libro> libros = _libros.ObtenerTodos()
            .Where(libro => libro.Disponible)
            .ToList();
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
            Console.WriteLine($"{i + 1}. {libros[i].Titulo}");
        }

        int? indiceLibro = LeerSeleccion(libros.Count, "libro");
        if (!indiceLibro.HasValue)
        {
            return;
        }

        int id = _prestamos.ObtenerTodos().Select(prestamo => prestamo.Id).DefaultIfEmpty(0).Max() + 1;
        Prestamo prestamo = new(id, usuarios[indiceUsuario.Value - 1], libros[indiceLibro.Value - 1]);
        if (!_prestamos.AgregarPrestamo(prestamo))
        {
            Console.WriteLine("No se pudo registrar el préstamo; verifique el estado del usuario y del libro.");
            return;
        }

        _persistence.GuardarDatos();
        Console.WriteLine("Préstamo registrado correctamente.");
        Console.WriteLine(prestamo.ObtenerDetalle());
    }

    private void MostrarPrestamosRegistrados()
    {
        List<Prestamo> prestamos = _prestamos.ObtenerTodos();
        if (prestamos.Count == 0)
        {
            Console.WriteLine("No hay préstamos registrados.");
            return;
        }

        Console.WriteLine("\nPréstamos registrados:");
        foreach (Prestamo prestamo in prestamos)
        {
            Console.WriteLine(prestamo.ObtenerDetalle());
        }
    }

    private void RegistrarDevolucion()
    {
        List<Prestamo> pendientes = _prestamos.ObtenerTodos()
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
        if (!_prestamos.RegistrarDevolucion(prestamo.Id))
        {
            Console.WriteLine("No se pudo registrar la devolución.");
            return;
        }

        _persistence.GuardarDatos();
        Console.WriteLine("Devolución registrada correctamente.");
        Console.WriteLine(prestamo.ObtenerDetalle());
    }

    private void MostrarCatalogo()
    {
        List<Libro> libros = _libros.ObtenerTodos();
        if (libros.Count == 0)
        {
            Console.WriteLine("No hay libros registrados.");
            return;
        }

        Console.WriteLine("\nLibros registrados:");
        foreach (Libro libro in libros)
        {
            Console.WriteLine($"- {libro.Titulo}");
        }
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
