using System;
using System.Collections.Generic;
using ControlGastos.context.entities;
using Microsoft.EntityFrameworkCore;

namespace ControlGastos.context;

public partial class ControlGastosContext : DbContext
{
    public ControlGastosContext()
    {
    }

    public ControlGastosContext(DbContextOptions<ControlGastosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Deposito> Depositos { get; set; }

    public virtual DbSet<FondoMonetario> FondoMonetarios { get; set; }

    public virtual DbSet<GastoDetalle> GastoDetalles { get; set; }

    public virtual DbSet<GastoEncabezado> GastoEncabezados { get; set; }

    public virtual DbSet<Presupuesto> Presupuestos { get; set; }

    public virtual DbSet<TipoGasto> TipoGastos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DbConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Deposito>(entity =>
        {
            entity.HasKey(e => e.DepositoId).HasName("PK__Deposito__345C2198322CE2F2");

            entity.ToTable("Deposito");

            entity.Property(e => e.Monto).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.FondoMonetario).WithMany(p => p.Depositos)
                .HasForeignKey(d => d.FondoMonetarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Deposito__FondoM__5165187F");
        });

        modelBuilder.Entity<FondoMonetario>(entity =>
        {
            entity.HasKey(e => e.FondoMonetarioId).HasName("PK__FondoMon__5071579C9E4C9D5A");

            entity.ToTable("FondoMonetario");

            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NumeroCuenta)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TipoFondo)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<GastoDetalle>(entity =>
        {
            entity.HasKey(e => e.GastoDetalleId).HasName("PK__GastoDet__A18326CBB4D5FB86");

            entity.ToTable("GastoDetalle");

            entity.Property(e => e.Monto).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.GastoEncabezado).WithMany(p => p.GastoDetalles)
                .HasForeignKey(d => d.GastoEncabezadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GastoDeta__Gasto__4316F928");

            entity.HasOne(d => d.TipoGasto).WithMany(p => p.GastoDetalles)
                .HasForeignKey(d => d.TipoGastoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GastoDeta__TipoG__440B1D61");
        });

        modelBuilder.Entity<GastoEncabezado>(entity =>
        {
            entity.HasKey(e => e.GastoEncabezadoId).HasName("PK__GastoEnc__5C5EE026962346E7");

            entity.ToTable("GastoEncabezado");

            entity.Property(e => e.NombreComercio)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Observaciones)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.TipoDocumento)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.FondoMonetario).WithMany(p => p.GastoEncabezados)
                .HasForeignKey(d => d.FondoMonetarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GastoEnca__Fondo__403A8C7D");
        });

        modelBuilder.Entity<Presupuesto>(entity =>
        {
            entity.HasKey(e => e.PresupuestoId).HasName("PK__Presupue__E2E362FF4EF7C29A");

            entity.ToTable("Presupuesto");

            entity.HasIndex(e => new { e.Mes, e.Anio, e.TipoGastoId }, "UQ_Presupuesto").IsUnique();

            entity.Property(e => e.Monto).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.TipoGasto).WithMany(p => p.Presupuestos)
                .HasForeignKey(d => d.TipoGastoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Presupues__TipoG__3D5E1FD2");
        });

        modelBuilder.Entity<TipoGasto>(entity =>
        {
            entity.HasKey(e => e.TipoGastoId).HasName("PK__TipoGast__C00E6CD80A668B78");

            entity.ToTable("TipoGasto");

            entity.HasIndex(e => e.Codigo, "UQ__TipoGast__06370DAC420EA985").IsUnique();

            entity.Property(e => e.Codigo)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
