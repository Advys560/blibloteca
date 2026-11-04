using Proyecto_Bliblotecta.Servicios;
using Proyecto_Bliblotecta.Utilidades;

namespace Proyecto_Bliblotecta.Menus;

internal sealed class MenuPrincipal
{
    public void Mostrar()
    {
        while (true)
        {
            Consola.MostrarEncabezado("MENÚ PRINCIPAL");
            Console.WriteLine("1. Libros");
            Console.WriteLine("2. Usuarios");
            Console.WriteLine("3. Préstamos");
            Console.WriteLine("4. Búsquedas y reportes");
            Console.WriteLine("5. Persistencia");
            Console.WriteLine("6. Salir y guardar");
            string? opcion = Consola.LeerOpcion();

            switch (opcion)
            {
                case "1":
                    new MenuLibros().Mostrar();
                    break;
                case "2":
                    new MenuUsuarios().Mostrar();
                    break;
                case "3":
                    new MenuPrestamos().Mostrar();
                    break;
                case "4":
                    new MenuBusquedasYReportes().Mostrar();
                    break;
                case "5":
                    new MenuPersistencia().Mostrar();
                    break;
                case "6":
                    if (!Consola.Confirmar("¿Está seguro de que desea salir y guardar los datos?"))
                    {
                        Console.WriteLine("Se canceló la salida.");
                        break;
                    }

                    try
                    {
                        DatosBiblioteca.Guardar();
                        Console.WriteLine("Datos guardados. Saliendo del programa...");
                        return;
                    }
                    catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException)
                    {
                        Console.WriteLine($"No se pudieron guardar los datos: {ex.Message}");
                    }

                    break;
                case null:
                    Console.WriteLine("Fin de la entrada. Saliendo sin guardar.");
                    return;
                default:
                    Console.WriteLine("Opción inválida. Seleccione una opción del menú.");
                    break;
            }
        }
    }
}
