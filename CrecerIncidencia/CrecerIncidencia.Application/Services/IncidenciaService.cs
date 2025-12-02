using CrecerIncidencia.Application.DTOs;
using CrecerIncidencia.Domain.Entities;
using CrecerIncidencia.Domain.Exceptions;
using CrecerIncidencia.Domain.Interfaces;
using FluentValidation;

namespace CrecerIncidencia.Application.Services
{
    public class IncidenciaService : IIncidenciaService
    {
        private readonly IIncidenciaRepository _repo;
        private readonly IValidator<IncidenciaRequestDTO> _validator;

        public IncidenciaService(IIncidenciaRepository repo, IValidator<IncidenciaRequestDTO> validator)
        {
            _repo = repo;
            _validator = validator;
        }

        public async Task<int> CrearIncidenciaAsync(IncidenciaRequestDTO request)
        {
            var validation = await _validator.ValidateAsync(request);
            if (!validation.IsValid)
                throw new DomainException(string.Join("; ", validation.Errors));

            var entity = new Incidencia
            {
                Titulo = request.Titulo,
                Descripcion = request.Descripcion,
                Categoria = request.Categoria,
                Severidad = request.Severidad,
                BitacoraInicial = request.BitacoraInicial
            };

            return await _repo.InsertarAsync(entity);
        }

        public async Task<IncidenciaResponseDTO?> ObtenerPorIdAsync(int id)
        {
            var inc = await _repo.ObtenerPorIdAsync(id);
            if (inc == null) return null;

            return new IncidenciaResponseDTO
            {
                Id = inc.Id,
                Titulo = inc.Titulo,
                Descripcion = inc.Descripcion,
                Categoria = inc.Categoria,
                Estado = inc.Estado.ToString(),
                FechaRegistro = inc.FechaRegistro
            };
        }

        public async Task<bool> ActualizarEstadoAsync(int id, ActualizarEstadoRequestDTO request)
        {
            if (request.Estado < 0) throw new DomainException("Estado inválido");

            return await _repo.ActualizarEstadoAsync(id, request.Estado, request.Comentario, request.Usuario);
        }

        public async Task<IEnumerable<IncidenciaResponseDTO>> ObtenerTodosAsync()
        {
            var incidencias = await _repo.ObtenerTodosAsync();

            return incidencias.Select(inc => new IncidenciaResponseDTO
            {
                Id = inc.Id,
                Titulo = inc.Titulo,
                Descripcion = inc.Descripcion,
                Categoria = inc.Categoria,
                Estado = inc.Estado.ToString(),
                FechaRegistro = inc.FechaRegistro
            });
        }
      
    }
}