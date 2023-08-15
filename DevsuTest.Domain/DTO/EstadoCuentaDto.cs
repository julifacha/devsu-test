namespace DevsuTest.Domain.DTO
{
    public class EstadoCuentaDto
    {
        public int NumeroCuenta { get; set; }
        public decimal Saldo { get; set; }
        public decimal TotalDebitosPeriodo { get; set; }
        public decimal TotalCreditosPeriodo { get; set; }
    }
}
