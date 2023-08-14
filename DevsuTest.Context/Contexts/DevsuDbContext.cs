using Microsoft.EntityFrameworkCore;
using DevsuTest.Domain;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using DevsuTest.Domain.Enum;

namespace DevsuTest.Context.Contexts;

public partial class DevsuDbContext : DbContext
{
    public DevsuDbContext()
    {
    }

    public DevsuDbContext(DbContextOptions<DevsuDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Cuenta> Cuentas { get; set; }

    public virtual DbSet<Movimiento> Movimientos { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Persona>(entity =>
        {
            entity.UseTptMappingStrategy();
            entity.HasIndex(e => e.Identificacion, "UQ__Personas__D6F931E56DE325C5").IsUnique();

            entity.Property(e => e.Direccion).HasMaxLength(100);
            entity.Property(e => e.FechaNacimiento).HasColumnType("date");
            entity.Property(e => e.Genero)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Identificacion).HasMaxLength(50);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Telefono).HasMaxLength(50);
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.Property(e => e.ContraseñaHash).HasMaxLength(255);
        });

        modelBuilder.Entity<Cuenta>(entity =>
        {
            entity.HasIndex(e => e.NumeroCuenta, "UQ__Cuentas__E039507B80B479E9").IsUnique();
            entity.Property(e => e.SaldoInicial).HasColumnType("decimal(18, 2)");

            entity.Property(e => e.TipoCuenta)
                .HasMaxLength(20)
                .HasConversion(new EnumToStringConverter<TipoCuentaEnum>());
        });

        modelBuilder.Entity<Cuenta>(entity =>
        {
            entity.HasIndex(e => e.NumeroCuenta, "UQ__Cuentas__E039507B80B479E9").IsUnique();

            entity.Property(e => e.SaldoInicial).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TipoCuenta).HasMaxLength(20);

            entity.HasOne(d => d.Cliente).WithMany(p => p.Cuentas)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cuentas_Cliente_Id");
        });

        modelBuilder.Entity<Movimiento>(entity =>
        {
            entity.Property(e => e.Fecha).HasColumnType("smalldatetime");
            entity.Property(e => e.Saldo).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Valor).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TipoMovimiento)
                .HasMaxLength(20)
                .HasConversion(new EnumToStringConverter<TipoMovimientoEnum>());

            entity.HasOne(d => d.Cuenta).WithMany(p => p.Movimientos)
                .HasForeignKey(d => d.CuentaId)
                .HasConstraintName("FK_Movimientos_Cuentas_Id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
