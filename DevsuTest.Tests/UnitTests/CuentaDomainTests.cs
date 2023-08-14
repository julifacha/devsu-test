using DevsuTest.Domain;

namespace DevsuTest.Tests.UnitTests
{
    public class CuentaDomainTests
    {
        [Theory]
        [InlineData(200, false)]
        [InlineData(50, true)]
        public void Test_ValidarSaldo(decimal saldoARetirar, bool expected)
        {
            // arrange
            Cuenta cuenta = Cuenta.Create(1, 100, Domain.Enum.TipoCuentaEnum.Ahorro, 100);

            // act
            bool result = cuenta.ValidarSaldo(saldoARetirar);

            // assert
            Assert.Equal(result, expected);
        }

        [Fact]
        public async void Test_ValidarLimiteRetiro()
        {
            // arrange
            Cuenta cuenta = Cuenta.Create(1, 100, Domain.Enum.TipoCuentaEnum.Ahorro, 1000);
            int LIMITE_RETIRO_DIARIO = 500;

            cuenta.Retirar(100);
            cuenta.Retirar(100);
            cuenta.Retirar(100);
            cuenta.Retirar(100);

            // assert
            Assert.True(cuenta.ValidarCupoDiario(50, LIMITE_RETIRO_DIARIO));
            Assert.False(cuenta.ValidarCupoDiario(200, LIMITE_RETIRO_DIARIO));
        }
    }
}
