using CrecerIncidencia.Domain.Enums;

namespace CrecerIncidencia.Domain.Entities
{
    public class Incidencia
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public Severidad Severidad { get; set; }
        public EstadoIncidencia Estado { get; set; } = EstadoIncidencia.Pendiente;
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public string? BitacoraInicial { get; set; }

        public List<IncidenciaBitacora> Bitacoras { get; set; } = new();
    }
}
