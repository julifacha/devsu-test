using DevsuTest.Domain.Enum;

namespace DevsuTest.Domain;

public class Movimiento
{
    public int Id { get; set; }

    public int CuentaId { get; set; }

    public DateTime Fecha { get; set; }

    public TipoMovimientoEnum TipoMovimiento { get; set; }

    public decimal Valor { get; set; }

    public decimal Saldo { get; set; }

    public virtual Cuenta Cuenta { get; set; }

    protected Movimiento() { }
    protected Movimiento(int cuentaId, TipoMovimientoEnum tipoMovimiento, decimal valor, decimal saldo)
    {
        CuentaId = cuentaId;
        Fecha = DateTime.Now;
        TipoMovimiento = tipoMovimiento;
        Valor = valor;
        Saldo = saldo;
    }

    public static Movimiento Create(int cuentaId, TipoMovimientoEnum tipoMovimiento, decimal valor, decimal saldo)
    {
        return new Movimiento(cuentaId, tipoMovimiento, valor, saldo);
    }
}
