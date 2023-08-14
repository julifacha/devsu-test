using DevsuTest.Application.DTO;
using DevsuTest.Domain;
using DevsuTest.Repository.UOW;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DevsuTest.Application.Validators
{
    public class MovimientoValidator : AbstractValidator<MovimientoDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly decimal LIMITE_DIARIO_RETIRO;
        private Cuenta? _cuenta;

        public MovimientoValidator(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            LIMITE_DIARIO_RETIRO = decimal.Parse(configuration.GetRequiredSection("LimiteDiarioRetiro").Value ?? "1000");

            RuleFor(m => m.CuentaId)
                .NotNull()
                .NotEmpty()
                .MustAsync((cuentaId, _) => CuentaExistente(cuentaId))
                .WithMessage(BaseValidationMessages.EntidadNoEncontrada("la cuenta"))
                .DependentRules(() =>
                {
                    When(x => x.TipoMovimiento == Domain.Enum.TipoMovimientoEnum.Retiro, () =>
                    {
                        RuleFor(c => c.Valor)
                            .MustAsync((valor, _) => ValidarSaldo(valor))
                            .WithMessage(MensajesValidacionMovimiento.SaldoInsuficiente);

                        RuleFor(c => c.Valor)
                            .MustAsync((valor, _) => ValidarCupoDiario(valor))
                            .WithMessage(MensajesValidacionMovimiento.CupoDiarioExcedido);
                    });
                });

        }

        private async Task<bool> CuentaExistente(int cuentaId)
        {
            _cuenta = await _unitOfWork.CuentasRepository.FindOneAsync(c => c.Id == cuentaId, include: i => i.Include(c => c.Movimientos));
            return _cuenta != null;
        }

        private async Task<bool> ValidarSaldo(decimal valorARetirar)
        {
            return _cuenta.ValidarSaldo(valorARetirar);
        }

        private async Task<bool> ValidarCupoDiario(decimal valorARetirar)
        {
            return _cuenta.ValidarCupoDiario(valorARetirar, LIMITE_DIARIO_RETIRO);
        }
    }

    public static class MensajesValidacionMovimiento
    {
        public const string SaldoInsuficiente = "Saldo no disponible.";
        public const string CupoDiarioExcedido = "Cupo diario excedido.";
    }
}
