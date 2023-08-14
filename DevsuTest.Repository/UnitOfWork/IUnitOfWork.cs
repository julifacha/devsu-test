using DevsuTest.Core.Interfaces;
using DevsuTest.Domain;

namespace DevsuTest.Repository.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Cliente> ClientesRepository { get; }
        IRepository<Cuenta> CuentasRepository { get; }
        IRepository<Movimiento> MovimientosRepository { get; }
        IRepository<Persona> PersonasRepository { get; }
        Task CompleteAsync();
        void Complete();
    }
}
