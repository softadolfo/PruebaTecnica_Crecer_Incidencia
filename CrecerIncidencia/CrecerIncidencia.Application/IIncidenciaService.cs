using CrecerIncidencia.Application.DTOs;
namespace CrecerIncidencia.Application
{
    public interface IIncidenciaService
    {
        Task<int> CrearIncidenciaAsync(IncidenciaRequestDTO request);
        Task<IncidenciaResponseDTO?> ObtenerPorIdAsync(int id);
        Task<IEnumerable<IncidenciaResponseDTO>> ObtenerTodosAsync(); 
        Task<bool> ActualizarEstadoAsync(int id, ActualizarEstadoRequestDTO request);
    }
}