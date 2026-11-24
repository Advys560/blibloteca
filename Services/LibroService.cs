namespace Proyecto_Bliblotecta.Services;

/// <summary>
/// Administra los libros registrados en memoria.
/// </summary>
public class LibroService
{
    private readonly List<Libro> libros = new();

    /// <summary>
    /// Agrega un libro si es valido y no tiene un Id o ISBN duplicado.
    /// </summary>
    public bool AgregarLibro(Libro? libro)
    {
        if (libro is null)
        {
            return false;
        }

        if (string.IsNullOrWhiteSpace(libro.Isbn))
        {
            return false;
        }

        bool idDuplicado = libro.Id != 0 &&
            libros.Any(item => item.Id == libro.Id);

        bool isbnDuplicado = libros.Any(item =>
            string.Equals(item.Isbn.Trim(), libro.Isbn.Trim(), StringComparison.OrdinalIgnoreCase));

        if (idDuplicado || isbnDuplicado)
        {
            return false;
        }

        libros.Add(libro);
        return true;
    }

    /// <summary>
    /// Elimina un libro disponible por su ISBN.
    /// </summary>
    public bool EliminarLibro(string? isbn)
    {
        if (string.IsNullOrWhiteSpace(isbn))
        {
            return false;
        }

        Libro? libro = BuscarPorIsbn(isbn);
        if (libro is null || !libro.Disponible)
        {
            return false;
        }

        return libros.Remove(libro);
    }

    /// <summary>
    /// Retorna una copia independiente de todos los libros registrados.
    /// </summary>
    public List<Libro> ObtenerTodos()
    {
        return libros.ToList();
    }

    /// <summary>
    /// Busca un libro por coincidencia exacta e insensible a mayusculas.
    /// </summary>
    public Libro? BuscarPorIsbn(string? isbn)
    {
        if (string.IsNullOrWhiteSpace(isbn))
        {
            return null;
        }

        return libros.FirstOrDefault(item =>
            string.Equals(item.Isbn.Trim(), isbn.Trim(), StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Busca libros cuyo titulo contenga el texto indicado.
    /// </summary>
    public List<Libro> BuscarPorTitulo(string? titulo)
    {
        if (string.IsNullOrWhiteSpace(titulo))
        {
            return new List<Libro>();
        }

        return libros
            .Where(item => item.Titulo.Contains(titulo.Trim(), StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    /// <summary>
    /// Busca libros cuyo autor contenga el texto indicado.
    /// </summary>
    public List<Libro> BuscarPorAutor(string? autor)
    {
        if (string.IsNullOrWhiteSpace(autor))
        {
            return new List<Libro>();
        }

        return libros
            .Where(item => item.Autor.Contains(autor.Trim(), StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    /// <summary>
    /// Retorna una copia ordenada por titulo de forma alfabetica.
    /// </summary>
    public List<Libro> OrdenarPorTitulo()
    {
        return libros
            .OrderBy(item => item.Titulo, StringComparer.OrdinalIgnoreCase)
            .ThenBy(item => item.Id)
            .ToList();
    }

    /// <summary>
    /// Retorna una copia ordenada por ano de publicacion ascendente.
    /// </summary>
    public List<Libro> OrdenarPorAnio()
    {
        return libros
            .OrderBy(item => item.AnioPublicacion)
            .ThenBy(item => item.Titulo, StringComparer.OrdinalIgnoreCase)
            .ToList();
    }

    public int ObtenerTotalLibros()
    {
        return libros.Count;
    }

    public int ObtenerLibrosDisponibles()
    {
        return libros.Count(item => item.Disponible);
    }

    public int ObtenerLibrosPrestados()
    {
        return libros.Count(item => !item.Disponible);
    }

    /// <summary>
    /// Retorna las cantidades principales en formato legible.
    /// </summary>
    public string ObtenerResumenKPIs()
    {
        return $"Total: {ObtenerTotalLibros()} | Disponibles: {ObtenerLibrosDisponibles()} | Prestados: {ObtenerLibrosPrestados()}";
    }
}
