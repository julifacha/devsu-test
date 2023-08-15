using DevsuTest.Domain.DTO;

namespace DevsuTest.Repository.Reportes
{
    public interface IReportesRepository
    {
        IQueryable<ItemListadoMovimientosDto> GetListadoMovimientos(int cliente, DateTime? fechaDesde, DateTime? fechaHasta);
        IQueryable<EstadoCuentaDto> GetEstadoCuenta(int cliente, DateTime? fechaDesde, DateTime? fechaHasta);
    }
}
