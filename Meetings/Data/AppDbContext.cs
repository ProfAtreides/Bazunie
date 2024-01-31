using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;
using Meetings.Models;


namespace Meetings.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Dział> Działy { get; set; }

    public virtual DbSet<Filia> Filie { get; set; }

    public virtual DbSet<Grafik> Grafik { get; set; }

    public virtual DbSet<Pracownik> Pracownicy { get; set; }

    public virtual DbSet<PracownicyHasSpotkania> PracownicyHasSpotkania { get; set; }

    public virtual DbSet<Sala> Sala { get; set; }

    public virtual DbSet<Spotkanie> Spotkania { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;port=3306;database=planer;user=root;password=13579", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.33-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Dział>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("działy")
                .UseCollation("utf8mb4_polish_ci");

            entity.HasIndex(e => e.IdFilii, "FKfk_idFilii");

            entity.HasIndex(e => e.Id, "idDzialu").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("idDzialu");
            entity.Property(e => e.IdFilii).HasColumnName("idFilii");
            entity.Property(e => e.NazwaDzialu)
                .HasMaxLength(45)
                .HasColumnName("nazwa_Dzialu");

            entity.HasOne(d => d.IdFiliiNavigation).WithMany(p => p.Działy)
                .HasForeignKey(d => d.IdFilii)
                .HasConstraintName("FKfk_idFilii");
        });

        modelBuilder.Entity<Filia>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("filie")
                .UseCollation("utf8mb4_polish_ci");

            entity.HasIndex(e => e.NazwaFilii, "nazwa_Filii").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("idFilii");
            entity.Property(e => e.NazwaFilii)
                .HasMaxLength(45)
                .HasColumnName("nazwa_Filii");
        });

        modelBuilder.Entity<Grafik>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("grafik")
                .UseCollation("utf8mb4_polish_ci");

            entity.HasIndex(e => e.IdPracownika, "fk_idPracownika");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("idGrafiku");
            entity.Property(e => e.DzienTygodnia)
                .HasMaxLength(45)
                .HasColumnName("dzien_tygodnia");
            entity.Property(e => e.GodzinaRozpoczecia)
                .HasColumnType("time")
                .HasColumnName("godzina_rozpoczecia");
            entity.Property(e => e.GodzinaZakonczenia)
                .HasColumnType("time")
                .HasColumnName("godzina_zakonczenia");
            entity.Property(e => e.IdPracownika).HasColumnName("idPracownika");
            entity.Property(e => e.IloscGodzin).HasColumnName("ilosc_godzin");

            entity.HasOne(d => d.IdPracownikaNavigation).WithMany(p => p.Grafik)
                .HasForeignKey(d => d.IdPracownika)
                .HasConstraintName("fk_idPracownika");
        });

        modelBuilder.Entity<Pracownik>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("pracownicy")
                .UseCollation("utf8mb4_polish_ci");

            entity.HasIndex(e => e.IdDzialu, "fk_dzialy");

            entity.HasIndex(e => e.IdFilii, "idFilii");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("idPracownika");
            entity.Property(e => e.Admin).HasColumnName("admin");
            entity.Property(e => e.Haslo)
                .HasMaxLength(45)
                .HasColumnName("haslo");
            entity.Property(e => e.IdDzialu).HasColumnName("idDzialu");
            entity.Property(e => e.IdFilii).HasColumnName("idFilii");
            entity.Property(e => e.ImiePracownika)
                .HasMaxLength(45)
                .HasColumnName("imie_pracownika");
            entity.Property(e => e.NazwiskoPracownika)
                .HasMaxLength(45)
                .HasColumnName("nazwisko_pracownika");
            entity.Property(e => e.Stanowisko)
                .HasMaxLength(45)
                .HasColumnName("stanowisko");

            entity.HasOne(d => d.IdDzialuNavigation).WithMany(p => p.Pracownicy)
                .HasForeignKey(d => d.IdDzialu)
                .HasConstraintName("fk_dzialy");

            entity.HasOne(d => d.IdFiliiNavigation).WithMany(p => p.Pracownicy)
                .HasForeignKey(d => d.IdFilii)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Pracownicy_ibfk_3");
        });

        modelBuilder.Entity<PracownicyHasSpotkania>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("pracownicy_has_spotkania")
                .UseCollation("utf8mb4_polish_ci");

            entity.HasIndex(e => e.IdPracownika, "FK_P");

            entity.HasIndex(e => e.IdSpotkania, "FK_S");

            entity.Property(e => e.IdPracownika).HasColumnName("idPracownika");
            entity.Property(e => e.IdSpotkania).HasColumnName("idSpotkania");

            entity.HasOne(d => d.IdPracownikaNavigation).WithMany()
                .HasForeignKey(d => d.IdPracownika)
                .HasConstraintName("FK_P");

            entity.HasOne(d => d.IdSpotkaniaNavigation).WithMany()
                .HasForeignKey(d => d.IdSpotkania)
                .HasConstraintName("FK_S");
        });

        modelBuilder.Entity<Sala>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("sala")
                .UseCollation("utf8mb4_polish_ci");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("idSali");
            entity.Property(e => e.Pojemnosc).HasColumnName("pojemnosc");
        });

        modelBuilder.Entity<Spotkanie>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("spotkania")
                .UseCollation("utf8mb4_polish_ci");

            entity.HasIndex(e => e.IdFilii, "fk_idFilii");

            entity.HasIndex(e => e.IdSali, "fk_sala");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("idSpotkania");
            entity.Property(e => e.DzienTygodnia)
                .HasMaxLength(45)
                .HasColumnName("dzien_tygodnia");
            entity.Property(e => e.GodzinaSpotkania)
                .HasColumnType("time")
                .HasColumnName("godzina_spotkania");
            entity.Property(e => e.IdFilii).HasColumnName("idFilii");
            entity.Property(e => e.IdSali).HasColumnName("idSali");
            entity.Property(e => e.MaxLiczbaUczestnikow).HasColumnName("max_liczba_uczestnikow");

            entity.HasOne(d => d.IdFiliiNavigation).WithMany(p => p.Spotkania)
                .HasForeignKey(d => d.IdFilii)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_idFilii");

            entity.HasOne(d => d.IdSaliNavigation).WithMany(p => p.Spotkania)
                .HasForeignKey(d => d.IdSali)
                .HasConstraintName("fk_sala");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
