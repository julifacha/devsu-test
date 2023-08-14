using DevsuTest.Repository.DTO;

namespace DevsuTest.Application.Services.Interfaces
{
    public interface IReportesService
    {
        Task<IEnumerable<EstadoDeCuentaDto>> GetEstadoDeCuenta(int clienteId, DateTime? fechaDesde, DateTime? fechaHasta);
    }
}
