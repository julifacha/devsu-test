using DevsuTest.Domain;
using DevsuTest.Application.Mappings;

namespace DevsuTest.Application.DTO
{
    public class ClienteDto : PersonaDto, IMapFrom<Cliente>
    {
        public Guid ClienteId { get; set; }

        public int Contraseña { get; set; }

        public bool Estado { get; set; }

        public ICollection<CuentaDto> Cuentas { get; set; } = new HashSet<CuentaDto>();
    }
}
