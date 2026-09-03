using CrecerIncidencia.Domain.Exceptions;
using FluentAssertions;
using Xunit;

namespace CrecerIncidencia.UnitTests.Domain.Exceptions
{
    public class DomainExceptionTests
    {
        [Fact]
        public void DomainException_WhenCreatedWithMessage_ShouldStoreMessage()
        {
            // Arrange
            const string errorMessage = "La incidencia se encuentra en un estado inválido";

            // Act
            var exception = new DomainException(errorMessage);

            // Assert
            exception.Message.Should().Be(errorMessage);
        }

        [Fact]
        public void DomainException_ShouldInheritFromException()
        {
            // Arrange & Act
            var exception = new DomainException("Error");

            // Assert
            exception.Should().BeAssignableTo<Exception>();
        }
    }
}
