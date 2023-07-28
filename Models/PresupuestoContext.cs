using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Presupuestos.Models;

public partial class PresupuestoContext : DbContext
{
    public PresupuestoContext()
    {
    }

    public PresupuestoContext(DbContextOptions<PresupuestoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbEgreso> TbEgresos { get; set; }

    public virtual DbSet<TbIngreso> TbIngresos { get; set; }

    public virtual DbSet<ViewSumaTotal> ViewSumaTotals { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbEgreso>(entity =>
        {
            entity.ToTable("tb_egresos");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Valor).HasColumnName("valor");
        });

        modelBuilder.Entity<TbIngreso>(entity =>
        {
            entity.ToTable("tb_ingresos");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Valor).HasColumnName("valor");
        });

        modelBuilder.Entity<ViewSumaTotal>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("View_SumaTotal");

            entity.Property(e => e.Total).HasColumnName("total");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
