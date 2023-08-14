namespace DevsuTest.Application.DTO
{
    public class PersonaDto
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public char Genero { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public string Identificacion { get; set; } = null!;

        public string Direccion { get; set; } = null!;

        public string Telefono { get; set; } = null!;
    }
}
