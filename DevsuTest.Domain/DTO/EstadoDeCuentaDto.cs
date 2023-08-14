using DevsuTest.Domain.Enum;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DevsuTest.Repository.DTO
{
    public class EstadoDeCuentaDto
    {
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; }
        public int NumeroCuenta { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public TipoCuentaEnum TipoCuenta { get; set; }
        public decimal SaldoInicial { get; set; }
        public bool Estado { get; set; }
        public decimal ValorMovimiento { get; set; }
        public decimal SaldoDisponible { get; set; }
    }
}
