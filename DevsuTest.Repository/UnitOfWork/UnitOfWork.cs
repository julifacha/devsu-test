using DevsuTest.Domain;
using DevsuTest.Context.Contexts;
using DevsuTest.Core.Interfaces;
using DevsuTest.Repository.Base;

namespace DevsuTest.Repository.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private DevsuDbContext _context;
        
        public UnitOfWork(DevsuDbContext context)
        {
            _context = context;
        }

        private IRepository<Cliente>? _clientesRepository;
        public IRepository<Cliente> ClientesRepository => _clientesRepository ??= new GenericRepository<Cliente>(_context);


        private IRepository<Cuenta>? _cuentasRepository;
        public IRepository<Cuenta> CuentasRepository => _cuentasRepository ??= new GenericRepository<Cuenta>(_context);

        private IRepository<Movimiento>? _movimientoRepository;

        public IRepository<Movimiento> MovimientosRepository => _movimientoRepository ??= new GenericRepository<Movimiento>(_context);
        private IRepository<Persona>? _personasRepository;

        public IRepository<Persona> PersonasRepository => _personasRepository ??= new GenericRepository<Persona>(_context);

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Complete()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}