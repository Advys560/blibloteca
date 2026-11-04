using Proyecto_Bliblotecta.Servicios;
using Proyecto_Bliblotecta.Utilidades;

namespace Proyecto_Bliblotecta.Menus;

internal sealed class MenuUsuarios
{
    public void Mostrar()
    {
        while (true)
        {
            Consola.MostrarEncabezado("GESTIÓN DE USUARIOS");
            Console.WriteLine("1. Registrar usuario");
            Console.WriteLine("2. Listar usuarios");
            Console.WriteLine("3. Volver al menú principal");
            string? opcion = Consola.LeerOpcion();

            switch (opcion)
            {
                case "1":
                    RegistrarUsuario();
                    Consola.Pausar("usuarios");
                    break;
                case "2":
                    MostrarUsuarios();
                    Consola.Pausar("usuarios");
                    break;
                case "3":
                    if (Consola.Confirmar("¿Está seguro de que desea volver al menú principal?"))
                    {
                        return;
                    }

                    Console.WriteLine("Se canceló la salida del menú.");
                    Consola.Pausar("usuarios");
                    break;
                case null:
                    return;
                default:
                    Console.WriteLine("Opción inválida. Seleccione una opción del menú.");
                    Consola.Pausar("usuarios");
                    break;
            }
        }
    }

    private static void RegistrarUsuario()
    {
        Console.Write("Ingrese el nombre del usuario: ");
        string? nombre = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(nombre))
        {
            Console.WriteLine("El nombre no puede estar vacío.");
            return;
        }

        DatosBiblioteca.Usuarios.Add($"Usuario {DatosBiblioteca.Usuarios.Count + 1}: {nombre}");
        Console.WriteLine("Usuario agregado correctamente.");
    }

    private static void MostrarUsuarios()
    {
        Console.WriteLine("\nUsuarios registrados:");

        if (DatosBiblioteca.Usuarios.Count == 0)
        {
            Console.WriteLine("No hay usuarios registrados.");
            return;
        }

        foreach (string usuario in DatosBiblioteca.Usuarios)
        {
            Console.WriteLine(usuario);
        }
    }
}
