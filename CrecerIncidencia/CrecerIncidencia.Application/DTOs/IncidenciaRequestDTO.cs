using CrecerIncidencia.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrecerIncidencia.Application.DTOs
{
    public class IncidenciaRequestDTO
    {
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public Severidad Severidad { get; set; }
        public string? BitacoraInicial { get; set; }
    }
}
