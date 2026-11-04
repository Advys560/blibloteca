namespace Proyecto_Bliblotecta.Utilidades;

internal static class Consola
{
    public static void MostrarEncabezado(string titulo)
    {
        if (!Console.IsOutputRedirected)
        {
            Console.Clear();
        }

        Console.WriteLine("========================================");
        Console.WriteLine($"       {titulo}");
        Console.WriteLine("========================================");
        Console.Write("Seleccione una opción: ");
    }

    public static string? LeerOpcion()
    {
        return Console.ReadLine()?.Trim();
    }

    public static bool Confirmar(string mensaje)
    {
        Console.Write($"{mensaje} (S/N): ");
        string? respuesta = Console.ReadLine()?.Trim();
        return string.Equals(respuesta, "S", StringComparison.OrdinalIgnoreCase)
            || string.Equals(respuesta, "SI", StringComparison.OrdinalIgnoreCase);
    }

    public static void Pausar(string menu)
    {
        Console.WriteLine($"\nPresione Enter para volver al menú de {menu}.");
        Console.ReadLine();
    }
}
