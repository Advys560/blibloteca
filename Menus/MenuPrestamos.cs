using Proyecto_Bliblotecta.Servicios;
using Proyecto_Bliblotecta.Utilidades;

namespace Proyecto_Bliblotecta.Menus;

internal sealed class MenuPrestamos
{
    public void Mostrar()
    {
        while (true)
        {
            Consola.MostrarEncabezado("GESTIÓN DE PRÉSTAMOS");
            Console.WriteLine("1. Registrar préstamo");
            Console.WriteLine("2. Listar préstamos");
            Console.WriteLine("3. Registrar devolución");
            Console.WriteLine("4. Consultar catálogo de libros");
            Console.WriteLine("5. Volver al menú principal");
            string? opcion = Consola.LeerOpcion();

            switch (opcion)
            {
                case "1":
                    Console.WriteLine("El registro de préstamos aún no está implementado.");
                    Consola.Pausar("préstamos");
                    break;
                case "2":
                    Console.WriteLine("El listado de préstamos aún no está implementado.");
                    Consola.Pausar("préstamos");
                    break;
                case "3":
                    Console.WriteLine("El registro de devoluciones aún no está implementado.");
                    Consola.Pausar("préstamos");
                    break;
                case "4":
                    CatalogoLibros.Mostrar();
                    Consola.Pausar("préstamos");
                    break;
                case "5":
                    if (Consola.Confirmar("¿Está seguro de que desea volver al menú principal?"))
                    {
                        return;
                    }

                    Console.WriteLine("Se canceló la salida del menú.");
                    Consola.Pausar("préstamos");
                    break;
                case null:
                    return;
                default:
                    Console.WriteLine("Opción inválida. Seleccione una opción del menú.");
                    Consola.Pausar("préstamos");
                    break;
            }
        }
    }
}
