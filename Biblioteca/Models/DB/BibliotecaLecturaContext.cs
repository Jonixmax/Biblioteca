using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Models.DB;

public partial class BibliotecaLecturaContext : DbContext
{
    public BibliotecaLecturaContext()
    {
    }

    public BibliotecaLecturaContext(DbContextOptions<BibliotecaLecturaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Calificacione> Calificaciones { get; set; }

    public virtual DbSet<Libro> Libros { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-JNP8EEG\\MSSQLSERVER01;Database=BibliotecaLectura21;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Calificacione>(entity =>
        {
            entity.HasKey(e => e.IdCalificacion).HasName("PK__Califica__E056358FBFF79441");

            entity.Property(e => e.IdCalificacion).HasColumnName("idCalificacion");
            entity.Property(e => e.FechaHora)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaHora");
            entity.Property(e => e.IdLibro).HasColumnName("idLibro");
            entity.Property(e => e.Puntuacion).HasColumnName("puntuacion");

            entity.HasOne(d => d.IdLibroNavigation).WithMany(p => p.Calificaciones)
                .HasForeignKey(d => d.IdLibro)
                .HasConstraintName("FK__Calificac__idLib__3B75D760");
        });

        modelBuilder.Entity<Libro>(entity =>
        {
            entity.HasKey(e => e.IdLibro).HasName("PK__Libros__5BC0F026B90F706F");

            entity.Property(e => e.IdLibro).HasColumnName("idLibro");
            entity.Property(e => e.Autor)
                .HasMaxLength(255)
                .HasColumnName("autor");
            entity.Property(e => e.Genero)
                .HasMaxLength(100)
                .HasColumnName("genero");
            entity.Property(e => e.PortadaUrl)
                .HasMaxLength(500)
                .HasColumnName("portada_url");
            entity.Property(e => e.Sinopsis).HasColumnName("sinopsis");
            entity.Property(e => e.Titulo)
                .HasMaxLength(255)
                .HasColumnName("titulo");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
