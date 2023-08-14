using DevsuTest.Context.Contexts;
using DevsuTest.Domain.Enum;
using DevsuTest.Repository.DTO;

namespace DevsuTest.Repository.Reportes
{
    public class ReportesRepository : IReportesRepository
    {
        protected DevsuDbContext _context;
        public ReportesRepository(DevsuDbContext context)
        { 
            _context = context;
        }
        public IQueryable<EstadoDeCuentaDto> GetEstadoDeCuenta(int clienteId, DateTime? fechaDesde, DateTime? fechaHasta)
        {
            IQueryable<EstadoDeCuentaDto> estadoCuentaQuery = from movimiento in _context.Movimientos
                                                             join cuenta in _context.Cuentas on movimiento.CuentaId equals cuenta.Id
                                                             join cliente in _context.Clientes on cuenta.ClienteId equals cliente.Id
                                                             where cliente.Id == clienteId
                                                                   && (!fechaDesde.HasValue || movimiento.Fecha >= fechaDesde)
                                                                   && (!fechaHasta.HasValue || movimiento.Fecha <= fechaHasta)
                                                              select new EstadoDeCuentaDto
                                                             {
                                                                 Fecha = movimiento.Fecha,
                                                                 Cliente = cliente.Nombre,
                                                                 NumeroCuenta = cuenta.NumeroCuenta,
                                                                 TipoCuenta = cuenta.TipoCuenta,
                                                                 SaldoInicial = cuenta.SaldoInicial,
                                                                 Estado = cuenta.Estado,
                                                                 ValorMovimiento = movimiento.TipoMovimiento == TipoMovimientoEnum.Deposito ? movimiento.Valor : movimiento.Valor * -1,
                                                                 SaldoDisponible = movimiento.Saldo
                                                             };

            return estadoCuentaQuery;
        }
    }
}
