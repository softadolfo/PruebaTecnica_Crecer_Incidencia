using CrecerIncidencia.Domain.Entities;
using CrecerIncidencia.Domain.Enums;
using FluentAssertions;
using Xunit;

namespace CrecerIncidencia.UnitTests.Domain.Entities
{
    public class IncidenciaTests
    {
        [Fact]
        public void Incidencia_WhenInstantiated_ShouldHaveDefaultValues()
        {
            // Arrange & Act
            var incidencia = new Incidencia();

            // Assert
            incidencia.Id.Should().Be(0);
            incidencia.Titulo.Should().BeEmpty();
            incidencia.Descripcion.Should().BeEmpty();
            incidencia.Categoria.Should().BeEmpty();
            incidencia.Severidad.Should().Be(Severidad.Baja);
            incidencia.Estado.Should().Be(EstadoIncidencia.Pendiente);
            incidencia.BitacoraInicial.Should().BeNull();
            incidencia.Bitacoras.Should().NotBeNull();
            incidencia.Bitacoras.Should().BeEmpty();
            incidencia.FechaRegistro.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(2));
        }

        [Fact]
        public void Incidencia_WhenSettingProperties_ShouldRetainValues()
        {
            // Arrange
            var fecha = new DateTime(2025, 1, 15, 10, 30, 0);

            // Act
            var incidencia = new Incidencia
            {
                Id = 1,
                Titulo = "Error en servidor",
                Descripcion = "El servidor de base de datos no responde a peticiones",
                Categoria = "Infraestructura",
                Severidad = Severidad.Critica,
                Estado = EstadoIncidencia.EnProceso,
                FechaRegistro = fecha,
                BitacoraInicial = "Se reporta caída de nodo"
            };

            // Assert
            incidencia.Id.Should().Be(1);
            incidencia.Titulo.Should().Be("Error en servidor");
            incidencia.Descripcion.Should().Be("El servidor de base de datos no responde a peticiones");
            incidencia.Categoria.Should().Be("Infraestructura");
            incidencia.Severidad.Should().Be(Severidad.Critica);
            incidencia.Estado.Should().Be(EstadoIncidencia.EnProceso);
            incidencia.FechaRegistro.Should().Be(fecha);
            incidencia.BitacoraInicial.Should().Be("Se reporta caída de nodo");
        }

        [Fact]
        public void IncidenciaBitacora_WhenInstantiated_ShouldHaveDefaultValuesAndRetainProperties()
        {
            // Arrange & Act
            var bitacora = new IncidenciaBitacora
            {
                Id = 10,
                IncidenciaId = 1,
                Accion = "Cambio a EnProceso",
                Usuario = "admin@correo.com"
            };

            // Assert
            bitacora.Id.Should().Be(10);
            bitacora.IncidenciaId.Should().Be(1);
            bitacora.Accion.Should().Be("Cambio a EnProceso");
            bitacora.Usuario.Should().Be("admin@correo.com");
            bitacora.Fecha.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(2));
        }

        [Fact]
        public void Incidencia_WhenAddingBitacora_ShouldContainItemInCollection()
        {
            // Arrange
            var incidencia = new Incidencia();
            var bitacora = new IncidenciaBitacora
            {
                Id = 1,
                IncidenciaId = incidencia.Id,
                Accion = "Creación de incidencia",
                Usuario = "operador"
            };

            // Act
            incidencia.Bitacoras.Add(bitacora);

            // Assert
            incidencia.Bitacoras.Should().HaveCount(1);
            incidencia.Bitacoras.First().Should().BeEquivalentTo(bitacora);
        }
    }
}
