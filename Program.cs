internal static class Program
{
    private static void Main()
    {
        Persistence persistence = new();
        Loans loans = new(persistence);
        persistence.CargarDatos(loans, mostrarMensaje: false);

        Menu menu = new(loans, persistence);
        menu.MostrarMenu();
    }
}
