using DevsuTest.Application.DTO;
using DevsuTest.Repository.UOW;
using FluentValidation;

namespace DevsuTest.Application.Validators
{
    public class CuentaValidator : AbstractValidator<CuentaDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CuentaValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            When(x => x.Id > 0, () =>
            {
                RuleFor(c => c.Id)
                    .Must(CuentaExistente)
                    .WithMessage(BaseValidationMessages.EntidadNoEncontrada("la cuenta"));
            });

            RuleFor(c => c.TipoCuenta).NotNull();
            RuleFor(c => c.ClienteId)
                .NotNull()
                .NotEmpty()
                .Must(ClienteExistente)
                .WithMessage(BaseValidationMessages.EntidadNoEncontrada("el cliente"));

            RuleFor(c => c.NumeroCuenta)
                .NotNull()
                .NotEmpty()
                .Must(NumeroCuentaDisponible)
                .WithMessage(BaseValidationMessages.ValorExistente("número de cuenta"));
        }

        private bool NumeroCuentaDisponible(int numeroCuenta)
        {
            return !_unitOfWork.CuentasRepository.Find(c => c.NumeroCuenta == numeroCuenta).Any();
        }

        private bool ClienteExistente(int clienteId)
        {
            return _unitOfWork.ClientesRepository.Find(c => c.Id == clienteId).Any();
        }

        private bool CuentaExistente(int cuentaId)
        {
            return _unitOfWork.CuentasRepository.Find(c => c.Id == cuentaId).Any();
        }
    }
}
