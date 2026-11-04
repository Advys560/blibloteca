using System;

public class Program
{
    public static void Main()
    {
        Menu miMenu = new Menu();
    }
}

public class Menu
{
    public void MostrarMenu()
    {
        bool ejecutando = true;

        while (ejecutando)
        {
            Console.WriteLine("\n--- MENÚ DE OPCIONES ---");
            Console.WriteLine("1. Libros disponibles");
            Console.WriteLine("2. Usuarios registrados");
            Console.WriteLine("3. Préstamos");
            Console.WriteLine("4. Búsquedas y reportes");
            Console.WriteLine("5. Guardar / Cargar datos");
            Console.WriteLine("6. Salir");
            Console.Write("Seleccione una opción: ");

            string? opcion = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(opcion))
            {
                Console.WriteLine("Debe ingresar una opción válida.");
                continue;
            }

            if (opcion == "1")
            {
                Console.WriteLine("Funcionalidad de libros disponibles aún no implementada.");
            }
            else if (opcion == "2")
            {
                Console.WriteLine("Funcionalidad de usuarios registrados aún no implementada.");
            }
            else if (opcion == "3")
            {
                Console.WriteLine("Funcionalidad de préstamos aún no implementada.");
            }
            else if (opcion == "4")
            {
                Console.WriteLine("Funcionalidad de búsquedas y reportes aún no implementada.");
            }
            else if (opcion == "5")
            {
                Console.WriteLine("Funcionalidad de guardar/cargar datos aún no implementada.");
            }
            else if (opcion == "6")
            {
                ejecutando = false;
                Console.WriteLine("Saliendo del programa...");
            }
            else
            {
                Console.WriteLine("Opción inválida. Por favor, seleccione una opción válida.");
            }
        }
    }
}