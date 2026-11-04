namespace Proyecto_Bliblotecta.Servicios;

internal static class CatalogoLibros
{
    public static void Mostrar()
    {
        Console.WriteLine("\nLibros registrados:");

        if (DatosBiblioteca.Libros.Count == 0)
        {
            Console.WriteLine("No hay libros registrados.");
            return;
        }

        for (int i = 0; i < DatosBiblioteca.Libros.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {DatosBiblioteca.Libros[i]}");
        }
    }

    public static void Buscar(string termino)
    {
        List<string> resultados = DatosBiblioteca.Libros
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

    public static void Agregar()
    {
        Console.Write("Ingrese el título del libro: ");
        string? titulo = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(titulo))
        {
            Console.WriteLine("El título no puede estar vacío.");
            return;
        }

        DatosBiblioteca.Libros.Add(titulo);
        Console.WriteLine("Libro agregado correctamente.");
    }
}
