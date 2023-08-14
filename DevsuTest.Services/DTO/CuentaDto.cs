using DevsuTest.Domain;
using DevsuTest.Domain.Enum;
using DevsuTest.Application.Mappings;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DevsuTest.Application.DTO
{
    public class CuentaDto : IMapFrom<Cuenta>
    {
        public int Id { get; set; }

        public int ClienteId { get; set; }

        public int NumeroCuenta { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public TipoCuentaEnum TipoCuenta { get; set; }

        public decimal SaldoInicial { get; set; }
        public decimal SaldoDisponible { get; set; }
        public bool Estado { get; set; }

        public ClienteDto? Cliente { get; set; }

        public ICollection<MovimientoDto> Movimientos { get; set; } = new HashSet<MovimientoDto>();
    }
}
