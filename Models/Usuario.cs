namespace Proyecto_Bliblotecta.Models;

public class Usuario
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
    public bool Activo { get; set; }

    public string DetalleCompleto()
    {
        return $"Id: {Id} | Nombre: {Nombre} | Correo: {Correo} | Estado: {(Activo ? "Activo" : "Inactivo")}";
    }
}
