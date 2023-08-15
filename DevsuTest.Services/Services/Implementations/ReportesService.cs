using DevsuTest.Application.Services.Interfaces;
using DevsuTest.Domain;
using DevsuTest.Domain.DTO;
using DevsuTest.Repository.Reportes;
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

        public async Task<IEnumerable<EstadoCuentaDto>> GetEstadoCuenta(int clienteId, DateTime? fechaDesde, DateTime? fechaHasta)
        {
            return await _reportesRepository.GetEstadoCuenta(clienteId, fechaDesde, fechaHasta).ToListAsync();
        }

        public async Task<IEnumerable<ItemListadoMovimientosDto>> GetListadoMovimientos(int clienteId, DateTime? fechaDesde, DateTime? fechaHasta)
        {
            return await _reportesRepository.GetListadoMovimientos(clienteId, fechaDesde, fechaHasta).ToListAsync();
        }
    }
}
