using Proyecto_Bliblotecta.Servicios;
using Proyecto_Bliblotecta.Utilidades;

namespace Proyecto_Bliblotecta.Menus;

internal sealed class MenuBusquedasYReportes
{
    public void Mostrar()
    {
        while (true)
        {
            Consola.MostrarEncabezado("BÚSQUEDAS Y REPORTES");
            Console.WriteLine("1. Buscar libro por título");
            Console.WriteLine("2. Generar reporte de préstamos");
            Console.WriteLine("3. Volver al menú principal");
            string? opcion = Consola.LeerOpcion();

            switch (opcion)
            {
                case "1":
                    BuscarLibro();
                    Consola.Pausar("búsquedas y reportes");
                    break;
                case "2":
                    Console.WriteLine("La generación de reportes aún no está implementada.");
                    Consola.Pausar("búsquedas y reportes");
                    break;
                case "3":
                    if (Consola.Confirmar("¿Está seguro de que desea volver al menú principal?"))
                    {
                        return;
                    }

                    Console.WriteLine("Se canceló la salida del menú.");
                    Consola.Pausar("búsquedas y reportes");
                    break;
                case null:
                    return;
                default:
                    Console.WriteLine("Opción inválida. Seleccione una opción del menú.");
                    Consola.Pausar("búsquedas y reportes");
                    break;
            }
        }
    }

    private static void BuscarLibro()
    {
        Console.Write("Ingrese el título o parte del título que desea buscar: ");
        string? termino = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(termino))
        {
            Console.WriteLine("El término de búsqueda no puede estar vacío.");
            return;
        }

        CatalogoLibros.Buscar(termino);
    }
}
