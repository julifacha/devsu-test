using DevsuTest.Repository.DTO;

namespace DevsuTest.Repository.Reportes
{
    public interface IReportesRepository
    {
        IQueryable<EstadoDeCuentaDto> GetEstadoDeCuenta(int cliente, DateTime? fechaDesde, DateTime? fechaHasta);
    }
}
