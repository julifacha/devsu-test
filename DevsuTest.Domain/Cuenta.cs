using DevsuTest.Domain.Enum;

namespace DevsuTest.Domain;

public class Cuenta
{
    public int Id { get; set; }

    public int ClienteId { get; set; }

    public int NumeroCuenta { get; set; }

    public TipoCuentaEnum TipoCuenta { get; set; }

    public decimal SaldoInicial { get; set; }

    public bool Estado { get; set; }

    public virtual Cliente Cliente { get; set; }

    public virtual ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();
    public decimal SaldoDisponible => Movimientos?.OrderByDescending(m => m.Id).FirstOrDefault()?.Saldo ?? SaldoInicial;
    protected Cuenta() { }
    protected Cuenta(int clienteId, int numeroCuenta, TipoCuentaEnum tipoCuenta, decimal saldoInicial)
    {
        ClienteId = clienteId;
        NumeroCuenta = numeroCuenta;
        TipoCuenta = tipoCuenta;
        SaldoInicial = saldoInicial;
        Estado = true;
    }

    public static Cuenta Create(int clienteId, int numeroCuenta, TipoCuentaEnum tipoCuenta, decimal saldoInicial)
    {
        return new Cuenta(clienteId, numeroCuenta, tipoCuenta, saldoInicial);
    }

    public bool ValidarSaldo(decimal valorARetirar)
    {
        return SaldoInicial >= valorARetirar;
    }

    public bool ValidarCupoDiario(decimal valorARetirar, decimal limiteRetiro)
    { 
        decimal valorRetiradoHoy = Movimientos
            .Where(m => m.Fecha.Date == DateTime.Today && m.TipoMovimiento == TipoMovimientoEnum.Retiro)
            .Sum(x => x.Valor);
        return valorRetiradoHoy + valorARetirar <= limiteRetiro;
    }

    public Movimiento AddMovimiento(TipoMovimientoEnum tipoMovimiento, decimal valor, decimal saldoActualizado)
    {
        Movimiento movimiento = Movimiento.Create(Id, tipoMovimiento, valor, saldoActualizado);
        Movimientos.Add(movimiento);
        return movimiento;
    }
    
    public Movimiento Retirar(decimal valor)
    {
        decimal saldoActualizado = SaldoDisponible - valor;
        return AddMovimiento(TipoMovimientoEnum.Retiro, valor, saldoActualizado);
    }

    public Movimiento Depositar(decimal valor)
    {
        decimal saldoActualizado = SaldoDisponible + valor;
        return AddMovimiento(TipoMovimientoEnum.Deposito, valor, saldoActualizado);
    }
}
