using Proyecto_Bliblotecta.ConsoleUI;
using Proyecto_Bliblotecta.Infrastructure;
using Proyecto_Bliblotecta.Models;
using Proyecto_Bliblotecta.Services;

internal static class Program
{
    private static void Main()
    {
        LibroService libros = new();
        UsuarioService usuarios = new();
        PrestamoService prestamos = new(libros);
        Persistence persistence = new(libros, usuarios, prestamos);
        persistence.CargarDatos(mostrarMensaje: false);

        if (libros.ObtenerTotalLibros() == 0 &&
            usuarios.ObtenerTotalUsuarios() == 0 &&
            prestamos.ObtenerTotalPrestamos() == 0)
        {
            CrearDatosIniciales(libros, usuarios, prestamos);
        }

        Menu menu = new(libros, usuarios, prestamos, persistence);
        menu.MostrarMenu();
    }

    private static void CrearDatosIniciales(
        LibroService libros,
        UsuarioService usuarios,
        PrestamoService prestamos)
    {
        libros.AgregarLibro(new Libro(1, "LOCAL-1", "El principio de la eternidad", "Desconocido", 2020));
        libros.AgregarLibro(new Libro(2, "LOCAL-2", "Cien años de soledad", "Gabriel García Márquez", 1967));
        libros.AgregarLibro(new Libro(3, "LOCAL-3", "La ciudad y los perros", "Mario Vargas Llosa", 1963));

        usuarios.AgregarUsuario(new Usuario { Id = 1, Nombre = "Juan Pérez", Correo = "juan@email.com", Activo = true });
        usuarios.AgregarUsuario(new Usuario { Id = 2, Nombre = "María Gómez", Correo = "maria@email.com", Activo = true });
        usuarios.AgregarUsuario(new Usuario { Id = 3, Nombre = "Carlos López", Correo = "carlos@email.com", Activo = true });

        prestamos.AgregarPrestamo(new Prestamo(1, usuarios.ObtenerTodos()[0], libros.ObtenerTodos()[0]));
    }
}
