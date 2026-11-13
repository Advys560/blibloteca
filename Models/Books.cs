public class Books
{
    public void MostrarLibros()
    {
        while (true)
        {
            if (!Console.IsOutputRedirected)
            {
                Console.Clear();
            }

            Console.WriteLine("========================================");
            Console.WriteLine("       GESTIÓN DE LIBROS");
            Console.WriteLine("========================================");
            Console.WriteLine("1. Consultar Libros");
            Console.WriteLine("2. Consultar Libros disponibles");
            Console.WriteLine("3. Listar prestados");
            Console.WriteLine("4. Agregar libro");
            Console.WriteLine("5. Volver al menú principal");
            Console.WriteLine("========================================");
            Console.Write("Seleccione una opción: ");

            string? opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    CatalogoLibros.MostrarLibros();
                    break;
                case "2":
                    CatalogoLibros.MostrarLibros();
                    break;
                case "3":
                    Console.WriteLine("La consulta de libros prestados aún no está implementada.");
                    break;
                case "4":
                    CatalogoLibros.AgregarLibro();
                    break;
                case "5":
                    Console.WriteLine("Volviendo al menú principal...");
                    return;
                default:
                    Console.WriteLine("Opción inválida. Por favor, seleccione una opción válida.");
                    break;
            }

            Console.WriteLine("\nPresione Enter para volver al menú de libros.");
            Console.ReadLine();
        }
    }
}