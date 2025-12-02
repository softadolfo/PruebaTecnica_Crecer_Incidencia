using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrecerIncidencia.Domain.Entities
{
    public class IncidenciaBitacora
    {
        public int Id { get; set; }
        public int IncidenciaId { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string Accion { get; set; } = string.Empty;
        public string? Usuario { get; set; }
    }
}
