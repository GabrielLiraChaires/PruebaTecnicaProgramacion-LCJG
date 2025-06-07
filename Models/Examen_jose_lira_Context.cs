using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Examen_jose_lira.Models;

public partial class Examen_jose_lira_Context : DbContext
{
    public Examen_jose_lira_Context()
    {
    }

    public Examen_jose_lira_Context(DbContextOptions<Examen_jose_lira_Context> options)
        : base(options)
    {
    }

    public virtual DbSet<TCliente> TClientes { get; set; }

    public virtual DbSet<TDetalleFactura> TDetalleFacturas { get; set; }

    public virtual DbSet<TFactura> TFacturas { get; set; }

    public virtual DbSet<TProducto> TProductos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TCliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__t_client__D59466429CD3A911");

            entity.ToTable("t_clientes");

            entity.Property(e => e.Domicilio)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TDetalleFactura>(entity =>
        {
            entity.HasKey(e => e.IdDetalle).HasName("PK__t_detall__E43646A5D6ED0144");

            entity.ToTable("t_detalleFactura");

            entity.HasOne(d => d.IdFacturaNavigation).WithMany(p => p.TDetalleFacturas)
                .HasForeignKey(d => d.IdFactura)
                .HasConstraintName("FK__t_detalle__IdFac__3E52440B");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.TDetalleFacturas)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__t_detalle__IdPro__3F466844");
        });

        modelBuilder.Entity<TFactura>(entity =>
        {
            entity.HasKey(e => e.IdFactura).HasName("PK__t_factur__50E7BAF1F7313E95");

            entity.ToTable("t_facturas");

            entity.Property(e => e.FechaHora).HasColumnType("datetime");
            entity.Property(e => e.NumeroFactura)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.TFacturas)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK__t_factura__IdCli__3B75D760");
        });

        modelBuilder.Entity<TProducto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__t_produc__098892106159CBA5");

            entity.ToTable("t_productos");

            entity.Property(e => e.Costo).HasColumnType("numeric(5, 2)");
            entity.Property(e => e.Marca)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PrecioVenta).HasColumnType("numeric(5, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
