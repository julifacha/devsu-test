using DevsuTest.Application.DTO;
using DevsuTest.Repository.UOW;
using FluentValidation;

namespace DevsuTest.Application.Validators
{
    public class ClienteValidator : AbstractValidator<ClienteDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISet<char> GenerosValidos = new HashSet<char> { 'M', 'F', 'O'};
        private static int CANTIDAD_DIGITOS_CONTRASEÑA = 4;

        public ClienteValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;


            #region Validaciones de creacion
            When(c => c.Id == default, () =>
            {
                RuleFor(c => c.Contraseña)
                    .NotNull()
                    .NotEmpty()
                    .Must(c => c.ToString().Length == CANTIDAD_DIGITOS_CONTRASEÑA)
                    .WithMessage(MensajesValidacionCliente.ContraseñaInvalida);

                RuleFor(c => c.Identificacion)
                    .NotNull()
                    .NotEmpty()
                    .Must(IdentificadorDisponible)
                    .WithMessage(BaseValidationMessages.ValorExistente("identificador"))
                    .When(c => c.Id == default);
            });
            #endregion

            #region Validaciones de creacion y actualizacion
            RuleFor(c => c.Nombre)
                .NotNull()
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(c => c.Genero)
                .NotNull()
                .NotEmpty()
                .Must(GeneroValido)
                .WithMessage(MensajesValidacionCliente.GeneroValido);

            RuleFor(c => c.FechaNacimiento).NotNull().NotEmpty().LessThan(DateTime.Today);
            RuleFor(c => c.Direccion).NotNull().NotEmpty();
            RuleFor(c => c.Telefono).NotNull().NotEmpty();
            #endregion

        }

        private bool IdentificadorDisponible(string identificador) 
        {
            return !_unitOfWork.ClientesRepository.Find(c => c.Identificacion == identificador).Any();
        }

        private bool GeneroValido(char genero)
        {
            return GenerosValidos.Contains(genero);
        }

        public static class MensajesValidacionCliente
        {
            public static string ContraseñaInvalida = $"La contraseña debe ser un número de { CANTIDAD_DIGITOS_CONTRASEÑA } dígitos.";
            public static string GeneroValido = "El género debe ser M (Masculino), F (Femenino) u O (Otro).";
        }
    }
}
