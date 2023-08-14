namespace DevsuTest.Domain;

public abstract class Persona
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public char Genero { get; set; }

    public DateTime FechaNacimiento { get; set; }

    public string Identificacion { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    protected Persona() { }

    protected Persona(string nombre, char genero, DateTime fechaNacimiento, string identificacion, string direccion, string telefono)
    {
        Nombre = nombre;
        Genero = genero;
        FechaNacimiento = fechaNacimiento;
        Identificacion = identificacion;
        Direccion = direccion;
        Telefono = telefono;
    }
}
