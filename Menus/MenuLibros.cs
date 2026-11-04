using Proyecto_Bliblotecta.Servicios;
using Proyecto_Bliblotecta.Utilidades;

namespace Proyecto_Bliblotecta.Menus;

internal sealed class MenuLibros
{
    public void Mostrar()
    {
        while (true)
        {
            Consola.MostrarEncabezado("GESTIÓN DE LIBROS");
            Console.WriteLine("1. Consultar libros");
            Console.WriteLine("2. Consultar libros disponibles");
            Console.WriteLine("3. Listar libros prestados");
            Console.WriteLine("4. Agregar libro");
            Console.WriteLine("5. Volver al menú principal");
            string? opcion = Consola.LeerOpcion();

            switch (opcion)
            {
                case "1":
                case "2":
                    CatalogoLibros.Mostrar();
                    Consola.Pausar("libros");
                    break;
                case "3":
                    Console.WriteLine("La consulta de libros prestados aún no está implementada.");
                    Consola.Pausar("libros");
                    break;
                case "4":
                    CatalogoLibros.Agregar();
                    Consola.Pausar("libros");
                    break;
                case "5":
                    if (Consola.Confirmar("¿Está seguro de que desea volver al menú principal?"))
                    {
                        return;
                    }

                    Console.WriteLine("Se canceló la salida del menú.");
                    Consola.Pausar("libros");
                    break;
                case null:
                    return;
                default:
                    Console.WriteLine("Opción inválida. Seleccione una opción del menú.");
                    Consola.Pausar("libros");
                    break;
            }
        }
    }
}
