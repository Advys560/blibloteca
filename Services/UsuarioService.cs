namespace Proyecto_Bliblotecta.Services;

/// <summary>
/// Administra los usuarios registrados en memoria.
/// </summary>
public class UsuarioService
{
    private readonly List<Usuario> usuarios = new();

    /// <summary>
    /// Agrega un usuario si no es nulo y no tiene un Id o correo duplicado.
    /// </summary>
    public bool AgregarUsuario(Usuario? usuario)
    {
        if (usuario is null)
        {
            return false;
        }

        bool idDuplicado = usuario.Id != 0 &&
            usuarios.Any(item => item.Id == usuario.Id);

        bool correoDuplicado = !string.IsNullOrWhiteSpace(usuario.Correo) &&
            usuarios.Any(item =>
                !string.IsNullOrWhiteSpace(item.Correo) &&
                string.Equals(item.Correo.Trim(), usuario.Correo.Trim(), StringComparison.OrdinalIgnoreCase));

        if (idDuplicado || correoDuplicado)
        {
            return false;
        }

        usuarios.Add(usuario);
        return true;
    }

    internal void ReemplazarTodos(IEnumerable<Usuario> nuevosUsuarios)
    {
        ArgumentNullException.ThrowIfNull(nuevosUsuarios);
        usuarios.Clear();
        usuarios.AddRange(nuevosUsuarios);
    }

    /// <summary>
    /// Elimina un usuario por Id o correo. No elimina usuarios con prestamos activos.
    /// </summary>
    public bool EliminarUsuario(string? documento, bool tienePrestamosActivos = false)
    {
        if (tienePrestamosActivos || string.IsNullOrWhiteSpace(documento))
        {
            return false;
        }

        Usuario? usuario = BuscarPorDocumento(documento);
        return usuario is not null && usuarios.Remove(usuario);
    }

    /// <summary>
    /// Elimina un usuario por Id.
    /// </summary>
    public bool EliminarUsuario(int id, bool tienePrestamosActivos = false)
    {
        if (tienePrestamosActivos)
        {
            return false;
        }

        Usuario? usuario = usuarios.FirstOrDefault(item => item.Id == id);
        return usuario is not null && usuarios.Remove(usuario);
    }

    /// <summary>
    /// Retorna una copia independiente de todos los usuarios registrados.
    /// </summary>
    public List<Usuario> ObtenerTodos()
    {
        return usuarios.ToList();
    }

    /// <summary>
    /// Cambia el estado activo de un usuario identificado por Id o correo.
    /// </summary>
    public bool CambiarEstadoActivo(string? documento, bool activo)
    {
        Usuario? usuario = BuscarPorDocumento(documento);
        if (usuario is null)
        {
            return false;
        }

        usuario.Activo = activo;
        return true;
    }

    /// <summary>
    /// Cambia el estado activo de un usuario identificado por Id.
    /// </summary>
    public bool CambiarEstadoActivo(int id, bool activo)
    {
        Usuario? usuario = usuarios.FirstOrDefault(item => item.Id == id);
        if (usuario is null)
        {
            return false;
        }

        usuario.Activo = activo;
        return true;
    }

    /// <summary>
    /// Busca un usuario por coincidencia exacta de Id o correo.
    /// </summary>
    public Usuario? BuscarPorDocumento(string? documento)
    {
        if (string.IsNullOrWhiteSpace(documento))
        {
            return null;
        }

        string documentoLimpio = documento.Trim();

        if (int.TryParse(documentoLimpio, out int id))
        {
            Usuario? usuarioPorId = usuarios.FirstOrDefault(item => item.Id == id);
            if (usuarioPorId is not null)
            {
                return usuarioPorId;
            }
        }

        return usuarios.FirstOrDefault(item =>
            string.Equals(item.Correo.Trim(), documentoLimpio, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Busca usuarios cuyo nombre contenga el texto indicado.
    /// </summary>
    public List<Usuario> BuscarPorNombre(string? nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
        {
            return new List<Usuario>();
        }

        string nombreBuscado = nombre.Trim();
        return usuarios
            .Where(item => item.Nombre.Contains(nombreBuscado, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    /// <summary>
    /// Retorna una copia ordenada por nombre de forma alfabetica.
    /// </summary>
    public List<Usuario> OrdenarPorNombre()
    {
        return usuarios
            .OrderBy(item => item.Nombre, StringComparer.OrdinalIgnoreCase)
            .ThenBy(item => item.Id)
            .ToList();
    }

    public int ObtenerTotalUsuarios()
    {
        return usuarios.Count;
    }

    public int ObtenerUsuariosActivos()
    {
        return usuarios.Count(item => item.Activo);
    }

    public int ObtenerUsuariosInactivos()
    {
        return usuarios.Count(item => !item.Activo);
    }

    /// <summary>
    /// Retorna las cantidades principales en formato legible.
    /// </summary>
    public string ObtenerResumenKPIs()
    {
        return $"Total: {ObtenerTotalUsuarios()} | Activos: {ObtenerUsuariosActivos()} | Inactivos: {ObtenerUsuariosInactivos()}";
    }
}
