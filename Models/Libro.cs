namespace Proyecto_Bliblotecta.Models;

/// <summary>
/// Representa un libro registrado en la biblioteca.
/// </summary>
public class Libro
{
    public Libro()
    {
    }

    public Libro(string isbn, string titulo, string autor, int anioPublicacion, bool disponible = true)
    {
        Isbn = isbn;
        Titulo = titulo;
        Autor = autor;
        AnioPublicacion = anioPublicacion;
        Disponible = disponible;
    }

    public Libro(int id, string isbn, string titulo, string autor, int anioPublicacion, bool disponible = true)
        : this(isbn, titulo, autor, anioPublicacion, disponible)
    {
        Id = id;
    }

    public int Id { get; set; }
    public string Isbn { get; set; } = string.Empty;
    public string Titulo { get; set; } = string.Empty;
    public string Autor { get; set; } = string.Empty;
    public int AnioPublicacion { get; set; }
    public bool Disponible { get; set; } = true;

    // Alias en mayusculas para mantener compatibilidad con codigo que usa ISBN.
    public string ISBN
    {
        get => Isbn;
        set => Isbn = value;
    }

    // Alias alternativo para nomenclatura en espanol.
    public int Anio
    {
        get => AnioPublicacion;
        set => AnioPublicacion = value;
    }
}
