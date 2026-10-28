using System;

public class Program
{
    public static void Main()
    {
        Menu miMenu = new Menu();
        miMenu.MostrarMenu();
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

            string opcion = Console.ReadLine();

        }
    }
}