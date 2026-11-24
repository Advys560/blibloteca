using Proyecto_Bliblotecta.Services;

internal static class Program
{
    private static void Main()
    {
        LibroService libros = new();
        libros.AgregarLibro(new Libro(1, "LOCAL-1", "El principio de la eternidad", "Desconocido", 0));

        UsuarioService usuarios = new();
        usuarios.AgregarUsuario(new Usuario { Id = 1, Nombre = "Juan Pérez", Correo = "juan@email.com", Activo = true });
        usuarios.AgregarUsuario(new Usuario { Id = 2, Nombre = "María Gómez", Correo = "maria@email.com", Activo = true });
        usuarios.AgregarUsuario(new Usuario { Id = 3, Nombre = "Carlos López", Correo = "carlos@email.com", Activo = true });

        PrestamoService prestamos = new(libros);
        Persistence persistence = new(libros, usuarios, prestamos);
        persistence.CargarDatos(mostrarMensaje: false);

        Menu menu = new(libros, usuarios, prestamos, persistence);
        menu.MostrarMenu();
    }
}
