using DevsuTest.Context.Contexts;
using DevsuTest.Domain.Enum;
using DevsuTest.Domain.DTO;
using DevsuTest.Core.Interfaces;
using DevsuTest.Domain;
using Microsoft.EntityFrameworkCore;

namespace DevsuTest.Repository.Reportes
{
    public class ReportesRepository : IReportesRepository
    {
        protected DevsuDbContext _context;
        private readonly IRepository<Cuenta> _cuentasRepository;
        public ReportesRepository(DevsuDbContext context, IRepository<Cuenta> cuentasRepository)
        { 
            _context = context;
            _cuentasRepository = cuentasRepository;
        }
        public IQueryable<ItemListadoMovimientosDto> GetListadoMovimientos(int clienteId, DateTime? fechaDesde, DateTime? fechaHasta)
        {
            IQueryable<ItemListadoMovimientosDto> estadoCuentaQuery = from movimiento in _context.Movimientos
                                                             join cuenta in _context.Cuentas on movimiento.CuentaId equals cuenta.Id
                                                             join cliente in _context.Clientes on cuenta.ClienteId equals cliente.Id
                                                             where cliente.Id == clienteId
                                                                   && (!fechaDesde.HasValue || movimiento.Fecha >= fechaDesde)
                                                                   && (!fechaHasta.HasValue || movimiento.Fecha <= fechaHasta)
                                                              select new ItemListadoMovimientosDto
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

        public IQueryable<EstadoCuentaDto> GetEstadoCuenta(int clienteId, DateTime? fechaDesde, DateTime? fechaHasta)
        {
            var cuentasCliente = _cuentasRepository.Find(c => c.ClienteId == clienteId, include: i => i.Include(c => c.Movimientos));

            return cuentasCliente.Select(c => new EstadoCuentaDto
            {
                NumeroCuenta = c.NumeroCuenta,
                Saldo = c.SaldoDisponible,
                TotalCreditosPeriodo = c.Movimientos.Where(m => m.TipoMovimiento == TipoMovimientoEnum.Deposito
                                         && (!fechaDesde.HasValue || m.Fecha >= fechaDesde)
                                         && (!fechaHasta.HasValue || m.Fecha <= fechaHasta))
                             .Sum(d => d.Valor),
                TotalDebitosPeriodo = c.Movimientos.Where(m => m.TipoMovimiento == TipoMovimientoEnum.Retiro
                                        && (!fechaDesde.HasValue || m.Fecha >= fechaDesde)
                                        && (!fechaHasta.HasValue || m.Fecha <= fechaHasta))
                            .Sum(d => d.Valor),
            });
        }
    }
}
