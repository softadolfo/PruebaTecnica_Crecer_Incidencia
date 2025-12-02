using CrecerIncidencia.Domain.Interfaces;
using CrecerIncidencia.Domain.Entities;
using CrecerIncidencia.Infrastructure.Database;
using Dapper;
using System.Data;
namespace CrecerIncidencia.Infrastructure.Repositories
{
    public class IncidenciaRepository : IIncidenciaRepository
    {
        private readonly DapperContext _context;

        public IncidenciaRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> InsertarAsync(Incidencia incidencia)
        {
            using var conn = _context.CreateConnection();
            const string sql = @"
            INSERT INTO Incidencias (Titulo, Descripcion, Categoria, Severidad, Estado, FechaRegistro, BitacoraInicial)
            OUTPUT INSERTED.Id
            VALUES (@Titulo, @Descripcion, @Categoria, @Severidad, @Estado, @FechaRegistro, @BitacoraInicial);
        ";

            var id = await conn.ExecuteScalarAsync<int>(sql, new
            {
                incidencia.Titulo,
                incidencia.Descripcion,
                incidencia.Categoria,
                Severidad = incidencia.Severidad.ToString(),
                Estado = incidencia.Estado.ToString(),
                incidencia.FechaRegistro,
                incidencia.BitacoraInicial
            });

            if (!string.IsNullOrEmpty(incidencia.BitacoraInicial))
            {
                const string sqlBit = @"
                INSERT INTO IncidenciaBitacora (IncidenciaId, Fecha, Accion, Usuario)
                VALUES (@IncidenciaId, @Fecha, @Accion, @Usuario);
            ";
                await conn.ExecuteAsync(sqlBit, new { IncidenciaId = id, Fecha = DateTime.UtcNow, Accion = incidencia.BitacoraInicial, Usuario = (string?)null });
            }

            return id;
        }

        public async Task<Incidencia?> ObtenerPorIdAsync(int id)
        {
            using var conn = _context.CreateConnection();
            const string sql = @"
            SELECT * FROM Incidencias WHERE Id = @Id;
            SELECT * FROM IncidenciaBitacora WHERE IncidenciaId = @Id ORDER BY Fecha;
        ";

            using var multi = await conn.QueryMultipleAsync(sql, new { Id = id });
            var inc = (await multi.ReadAsync<Incidencia>()).FirstOrDefault();
            if (inc == null) return null;

            var bit = (await multi.ReadAsync<IncidenciaBitacora>()).ToList();
            inc.Bitacoras = bit;
            return inc;
        }

        public async Task<IEnumerable<Incidencia>> ObtenerTodosAsync()
        {
            using var conn = _context.CreateConnection();
            const string sql = "SELECT * FROM Incidencias ORDER BY FechaRegistro DESC";
            return await conn.QueryAsync<Incidencia>(sql);
        }

        public async Task<bool> ActualizarEstadoAsync(int id, int nuevoEstado, string? comentario, string? usuario)
        {
            using var conn = _context.CreateConnection();

            var estadoText = Enum.GetName(typeof(Domain.Enums.EstadoIncidencia), nuevoEstado)
                              ?? nuevoEstado.ToString();

            var result = await conn.QueryFirstOrDefaultAsync<int>(
                "sp_ActualizarEstado",
                new
                {
                    Id = id,
                    NuevoEstado = estadoText,
                    Comentario = comentario,
                    Usuario = usuario
                },
                commandType: CommandType.StoredProcedure
            );

            return result > 0;
        }

    }
}