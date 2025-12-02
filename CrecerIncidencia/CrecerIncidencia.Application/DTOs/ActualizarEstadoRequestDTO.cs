using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrecerIncidencia.Application.DTOs
{
    public class ActualizarEstadoRequestDTO
    {
        public int Estado { get; set; }
        public string? Comentario { get; set; }
        public string? Usuario { get; set; }
    }
}
