using CrecerIncidencia.Application.DTOs;
using FluentValidation;

namespace CrecerIncidencia.Application.Validations
{
    public class IncidenciaRequestValidator : AbstractValidator<IncidenciaRequestDTO>
    {
        public IncidenciaRequestValidator()
        {
            RuleFor(x => x.Titulo).NotEmpty().WithMessage("El título no puede estar vacío")
                                  .MinimumLength(5).WithMessage("El título debe tener al menos 5 caracteres");
            RuleFor(x => x.Descripcion).NotEmpty().WithMessage("La descripción no puede estar vacía")
                                       .MinimumLength(10).WithMessage("La descripción debe tener al menos 10 caracteres");
            RuleFor(x => x.Categoria).NotEmpty().WithMessage("La categoría es obligatoria");
        }
    }
}