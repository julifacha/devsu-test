namespace DevsuTest.Domain;

public class Cliente : Persona
{
    public Guid ClienteId { get; set; }

    public byte[] ContraseñaHash { get; set; } = null!;

    public bool Estado { get; set; }

    public virtual ICollection<Cuenta> Cuentas { get; set; } = new List<Cuenta>();

    protected Cliente() { }
    protected Cliente (string nombre, char genero, DateTime fechaNacimiento, string identificacion, string direccion, string telefono)
        : base(nombre, genero, fechaNacimiento, identificacion, direccion, telefono)
    {
        ClienteId = Guid.NewGuid();
        Estado = true;
    }

    public static Cliente Create(string nombre, char genero, DateTime fechaNacimiento, string identificacion, string direccion, string telefono)
    {
        return new Cliente(nombre, genero, fechaNacimiento, identificacion, direccion, telefono);
    }
}
