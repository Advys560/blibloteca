public static class CatalogoLibros
{
    public static List<string> Libros { get; } = new()
    {
        "El principio de la eternidad"
    };

    public static void MostrarLibros()
    {
        Console.WriteLine("\nLibros registrados:");

        if (Libros.Count == 0)
        {
            Console.WriteLine("No hay libros registrados.");
            return;
        }

        for (int i = 0; i < Libros.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {Libros[i]}");
        }
    }

    public static void BuscarLibros(string termino)
    {
        List<string> resultados = Libros
            .Where(libro => libro.Contains(termino, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (resultados.Count == 0)
        {
            Console.WriteLine("No se encontraron libros con ese título.");
            return;
        }

        Console.WriteLine("\nLibros encontrados:");
        foreach (string libro in resultados)
        {
            Console.WriteLine($"- {libro}");
        }
    }

    public static bool AgregarLibro()
    {
        Console.Write("Ingrese el título del libro: ");
        string? nuevoLibro = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(nuevoLibro))
        {
            Console.WriteLine("El título no puede estar vacío.");
            return false;
        }

        Libros.Add(nuevoLibro.Trim());
        Console.WriteLine("Libro agregado correctamente.");
        return true;
    }
}
