using DevsuTest.Domain.DTO;

namespace DevsuTest.Application.Services.Interfaces
{
    public interface IReportesService
    {
        Task<IEnumerable<ItemListadoMovimientosDto>> GetListadoMovimientos(int clienteId, DateTime? fechaDesde, DateTime? fechaHasta);
        Task<IEnumerable<EstadoCuentaDto>> GetEstadoCuenta(int clienteId, DateTime? fechaDesde, DateTime? fechaHasta);
    }
}
