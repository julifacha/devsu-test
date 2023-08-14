using DevsuTest.Domain;
using DevsuTest.Domain.Enum;
using DevsuTest.Application.Mappings;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DevsuTest.Application.DTO
{
    public class MovimientoDto : IMapFrom<Movimiento>
    {
        public int Id { get; set; }

        public int CuentaId { get; set; }

        public DateTime Fecha { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public TipoMovimientoEnum TipoMovimiento { get; set; }

        public decimal Valor { get; set; }

        public decimal Saldo { get; set; }
    }
}
