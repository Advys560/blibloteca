using System.Text.Json;

namespace Proyecto_Bliblotecta.Servicios;

internal static class DatosBiblioteca
{
    private static readonly string[] LibrosIniciales =
    {
        "El principio de la eternidad"
    };

    private static readonly string[] UsuariosIniciales =
    {
        "Usuario 1: Juan Pérez",
        "Usuario 2: María Gómez",
        "Usuario 3: Carlos López"
    };

    private static readonly JsonSerializerOptions OpcionesJson = new()
    {
        WriteIndented = true
    };

    private static readonly string RutaArchivo = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "ProyectoBiblioteca",
        "datos.json");

    static DatosBiblioteca()
    {
        Libros = new List<string>(LibrosIniciales);
        Usuarios = new List<string>(UsuariosIniciales);
    }

    public static List<string> Libros { get; }
    public static List<string> Usuarios { get; }

    public static void Guardar()
    {
        string? directorio = Path.GetDirectoryName(RutaArchivo);
        if (string.IsNullOrEmpty(directorio))
        {
            throw new IOException("No se pudo determinar la carpeta para guardar los datos.");
        }

        Directory.CreateDirectory(directorio);
        string json = JsonSerializer.Serialize(
            new DatosGuardados(Libros, Usuarios),
            OpcionesJson);
        File.WriteAllText(RutaArchivo, json);
    }

    public static void Cargar()
    {
        string json = File.ReadAllText(RutaArchivo);
        DatosGuardados? datos = JsonSerializer.Deserialize<DatosGuardados>(json);

        if (datos?.Libros is null || datos.Usuarios is null)
        {
            throw new JsonException("El archivo no contiene listas válidas de libros y usuarios.");
        }

        Libros.Clear();
        Libros.AddRange(datos.Libros);
        Usuarios.Clear();
        Usuarios.AddRange(datos.Usuarios);
    }

    public static void Reiniciar()
    {
        Libros.Clear();
        Usuarios.Clear();
    }

    private sealed record DatosGuardados(List<string> Libros, List<string> Usuarios);
}
