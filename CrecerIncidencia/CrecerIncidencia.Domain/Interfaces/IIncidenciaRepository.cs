using CrecerIncidencia.Domain.Entities;

namespace CrecerIncidencia.Domain.Interfaces
{
    public interface IIncidenciaRepository
    {
        Task<int> InsertarAsync(Incidencia incidencia);
        Task<Incidencia?> ObtenerPorIdAsync(int id);
        Task<IEnumerable<Incidencia>> ObtenerTodosAsync();
        Task<bool> ActualizarEstadoAsync(int id, int nuevoEstado, string? comentario, string? usuario);
    }
}
