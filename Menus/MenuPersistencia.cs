using System.Text.Json;
using Proyecto_Bliblotecta.Servicios;
using Proyecto_Bliblotecta.Utilidades;

namespace Proyecto_Bliblotecta.Menus;

internal sealed class MenuPersistencia
{
    public void Mostrar()
    {
        while (true)
        {
            Consola.MostrarEncabezado("MENÚ DE PERSISTENCIA");
            Console.WriteLine("1. Información para guardar datos");
            Console.WriteLine("2. Cargar datos guardados");
            Console.WriteLine("3. Reiniciar datos");
            Console.WriteLine("4. Volver al menú principal");
            string? opcion = Consola.LeerOpcion();

            switch (opcion)
            {
                case "1":
                    Console.WriteLine("Los datos se guardan al confirmar la salida desde el menú principal.");
                    Consola.Pausar("persistencia");
                    break;
                case "2":
                    CargarDatos();
                    break;
                case "3":
                    ReiniciarDatos();
                    break;
                case "4":
                    if (Consola.Confirmar("¿Está seguro de que desea volver al menú principal?"))
                    {
                        return;
                    }

                    Console.WriteLine("Se canceló la salida del menú.");
                    Consola.Pausar("persistencia");
                    break;
                case null:
                    return;
                default:
                    Console.WriteLine("Opción inválida. Seleccione una opción del menú.");
                    Consola.Pausar("persistencia");
                    break;
            }
        }
    }

    private static void CargarDatos()
    {
        try
        {
            DatosBiblioteca.Cargar();
            Console.WriteLine("Se cargaron los datos guardados.");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("No hay datos guardados. Salga del programa y confirme guardar para crear el archivo.");
        }
        catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException || ex is JsonException)
        {
            Console.WriteLine($"No se pudieron cargar los datos: {ex.Message}");
        }

        Consola.Pausar("persistencia");
    }

    private static void ReiniciarDatos()
    {
        if (Consola.Confirmar("¿Está seguro de que desea vaciar los libros y usuarios?"))
        {
            DatosBiblioteca.Reiniciar();
            Console.WriteLine("Se vaciaron los libros y usuarios. Los cambios se guardarán al salir y confirmar.");
        }
        else
        {
            Console.WriteLine("Se canceló el reinicio de los datos.");
        }

        Consola.Pausar("persistencia");
    }
}
