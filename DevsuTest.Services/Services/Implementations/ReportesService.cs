using DevsuTest.Application.Services.Interfaces;
using DevsuTest.Repository.Reportes;
using DevsuTest.Repository.DTO;
using Microsoft.EntityFrameworkCore;

namespace DevsuTest.Application.Services.Implementations
{
    public class ReportesService : IReportesService
    {
        private readonly IReportesRepository _reportesRepository;
        public ReportesService(IReportesRepository reportesRepository) 
        { 
            _reportesRepository = reportesRepository;
        }

        public async Task<IEnumerable<EstadoDeCuentaDto>> GetEstadoDeCuenta(int clienteId, DateTime? fechaDesde, DateTime? fechaHasta)
        {
            return await _reportesRepository.GetEstadoDeCuenta(clienteId, fechaDesde, fechaHasta).ToListAsync();
        }
    }
}
