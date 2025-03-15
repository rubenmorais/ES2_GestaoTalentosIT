using System;
using System.Collections.Generic;
using DbLayer.Models;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;

namespace DbLayer.Context;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CategoriasProfissionai> CategoriasProfissionais { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<Experiencia> Experiencias { get; set; }

    public virtual DbSet<Habilidade> Habilidades { get; set; }

    public virtual DbSet<PropostasHabilidade> PropostasHabilidades { get; set; }

    public virtual DbSet<PropostasTrabalho> PropostasTrabalhos { get; set; }

    public virtual DbSet<Talento> Talentos { get; set; }

    public virtual DbSet<TalentosHabilidade> TalentosHabilidades { get; set; }

    public virtual DbSet<Tipo> Tipos { get; set; }

    public virtual DbSet<Utilizadores> Utilizadores { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=es2;Username=postgres;Password=123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CategoriasProfissionai>(entity =>
        {
            entity.HasKey(e => e.Categoriaid).HasName("categorias_profissionais_pkey");

            entity.ToTable("categorias_profissionais");

            entity.Property(e => e.Categoriaid).HasColumnName("categoriaid");
            entity.Property(e => e.Categoria)
                .HasMaxLength(255)
                .HasColumnName("categoria");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Clienteid).HasName("clientes_pkey");

            entity.ToTable("clientes");

            entity.Property(e => e.Clienteid).HasColumnName("clienteid");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .HasColumnName("nome");
            entity.Property(e => e.Utilizadorid).HasColumnName("utilizadorid");

            entity.HasOne(d => d.Utilizador).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.Utilizadorid)
                .HasConstraintName("clientes_utilizadorid_fkey");
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.Estadoid).HasName("estados_pkey");

            entity.ToTable("estados");

            entity.Property(e => e.Estadoid).HasColumnName("estadoid");
            entity.Property(e => e.Estado1)
                .HasMaxLength(255)
                .HasColumnName("estado");
        });

        modelBuilder.Entity<Experiencia>(entity =>
        {
            entity.HasKey(e => e.Experienciaid).HasName("experiencias_pkey");

            entity.ToTable("experiencias");

            entity.Property(e => e.Experienciaid).HasColumnName("experienciaid");
            entity.Property(e => e.AnoFim).HasColumnName("ano_fim");
            entity.Property(e => e.AnoInicio).HasColumnName("ano_inicio");
            entity.Property(e => e.Empresa)
                .HasMaxLength(255)
                .HasColumnName("empresa");
            entity.Property(e => e.Talentoid).HasColumnName("talentoid");
            entity.Property(e => e.Titulo)
                .HasMaxLength(255)
                .HasColumnName("titulo");

            entity.HasOne(d => d.Talento).WithMany(p => p.Experiencia)
                .HasForeignKey(d => d.Talentoid)
                .HasConstraintName("experiencias_talentoid_fkey");
        });

        modelBuilder.Entity<Habilidade>(entity =>
        {
            entity.HasKey(e => e.Habilidadeid).HasName("habilidades_pkey");

            entity.ToTable("habilidades");

            entity.Property(e => e.Habilidadeid).HasColumnName("habilidadeid");
            entity.Property(e => e.Categoriaid).HasColumnName("categoriaid");
            entity.Property(e => e.Criadorid).HasColumnName("criadorid");
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .HasColumnName("nome");

            entity.HasOne(d => d.Categoria).WithMany(p => p.Habilidades)
                .HasForeignKey(d => d.Categoriaid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("habilidades_categoriaid_fkey");

            entity.HasOne(d => d.Criador).WithMany(p => p.Habilidades)
                .HasForeignKey(d => d.Criadorid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("habilidades_criadorid_fkey");
        });

        modelBuilder.Entity<PropostasHabilidade>(entity =>
        {
            entity.HasKey(e => new { e.Propostaid, e.Habilidadeid }).HasName("propostas_habilidades_pkey");

            entity.ToTable("propostas_habilidades");

            entity.Property(e => e.Propostaid).HasColumnName("propostaid");
            entity.Property(e => e.Habilidadeid).HasColumnName("habilidadeid");
            entity.Property(e => e.MinAnosExp).HasColumnName("min_anos_exp");

            entity.HasOne(d => d.Habilidade).WithMany(p => p.PropostasHabilidades)
                .HasForeignKey(d => d.Habilidadeid)
                .HasConstraintName("propostas_habilidades_habilidadeid_fkey");

            entity.HasOne(d => d.Proposta).WithMany(p => p.PropostasHabilidades)
                .HasForeignKey(d => d.Propostaid)
                .HasConstraintName("propostas_habilidades_propostaid_fkey");
        });

        modelBuilder.Entity<PropostasTrabalho>(entity =>
        {
            entity.HasKey(e => e.Propostaid).HasName("propostas_trabalho_pkey");

            entity.ToTable("propostas_trabalho");

            entity.Property(e => e.Propostaid).HasColumnName("propostaid");
            entity.Property(e => e.Categoriaid).HasColumnName("categoriaid");
            entity.Property(e => e.Clienteid).HasColumnName("clienteid");
            entity.Property(e => e.Descricao).HasColumnName("descricao");
            entity.Property(e => e.Estadoid).HasColumnName("estadoid");
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .HasColumnName("nome");
            entity.Property(e => e.TotalHoras).HasColumnName("total_horas");
            entity.Property(e => e.Utilizadorid).HasColumnName("utilizadorid");

            entity.HasOne(d => d.Categoria).WithMany(p => p.PropostasTrabalhos)
                .HasForeignKey(d => d.Categoriaid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("propostas_trabalho_categoriaid_fkey");

            entity.HasOne(d => d.Cliente).WithMany(p => p.PropostasTrabalhos)
                .HasForeignKey(d => d.Clienteid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("propostas_trabalho_clienteid_fkey");

            entity.HasOne(d => d.Estado).WithMany(p => p.PropostasTrabalhos)
                .HasForeignKey(d => d.Estadoid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("propostas_trabalho_estadoid_fkey");

            entity.HasOne(d => d.Utilizador).WithMany(p => p.PropostasTrabalhos)
                .HasForeignKey(d => d.Utilizadorid)
                .HasConstraintName("propostas_trabalho_utilizadorid_fkey");

            entity.HasMany(d => d.Talentos).WithMany(p => p.Proposta)
                .UsingEntity<Dictionary<string, object>>(
                    "PropostaTalento",
                    r => r.HasOne<Talento>().WithMany()
                        .HasForeignKey("Talentoid")
                        .HasConstraintName("proposta_talentos_talentoid_fkey"),
                    l => l.HasOne<PropostasTrabalho>().WithMany()
                        .HasForeignKey("Propostaid")
                        .HasConstraintName("proposta_talentos_propostaid_fkey"),
                    j =>
                    {
                        j.HasKey("Propostaid", "Talentoid").HasName("proposta_talentos_pkey");
                        j.ToTable("proposta_talentos");
                        j.IndexerProperty<int>("Propostaid").HasColumnName("propostaid");
                        j.IndexerProperty<int>("Talentoid").HasColumnName("talentoid");
                    });
        });

        modelBuilder.Entity<Talento>(entity =>
        {
            entity.HasKey(e => e.Talentoid).HasName("talentos_pkey");

            entity.ToTable("talentos");

            entity.Property(e => e.Talentoid).HasColumnName("talentoid");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .HasColumnName("nome");
            entity.Property(e => e.Pais)
                .HasMaxLength(100)
                .HasColumnName("pais");
            entity.Property(e => e.PrecoHora)
                .HasPrecision(10, 2)
                .HasColumnName("preco_hora");
            entity.Property(e => e.Utilizadorid).HasColumnName("utilizadorid");
            entity.Property(e => e.Visibilidade)
                .HasDefaultValue(true)
                .HasColumnName("visibilidade");

            entity.HasOne(d => d.Utilizador).WithMany(p => p.Talentos)
                .HasForeignKey(d => d.Utilizadorid)
                .HasConstraintName("talentos_utilizadorid_fkey");
        });

        modelBuilder.Entity<TalentosHabilidade>(entity =>
        {
            entity.HasKey(e => new { e.Talentoid, e.Habilidadeid }).HasName("talentos_habilidades_pkey");

            entity.ToTable("talentos_habilidades");

            entity.Property(e => e.Talentoid).HasColumnName("talentoid");
            entity.Property(e => e.Habilidadeid).HasColumnName("habilidadeid");
            entity.Property(e => e.AnosExperiencia).HasColumnName("anos_experiencia");

            entity.HasOne(d => d.Habilidade).WithMany(p => p.TalentosHabilidades)
                .HasForeignKey(d => d.Habilidadeid)
                .HasConstraintName("talentos_habilidades_habilidadeid_fkey");

            entity.HasOne(d => d.Talento).WithMany(p => p.TalentosHabilidades)
                .HasForeignKey(d => d.Talentoid)
                .HasConstraintName("talentos_habilidades_talentoid_fkey");
        });

        modelBuilder.Entity<Tipo>(entity =>
        {
            entity.HasKey(e => e.Tipoid).HasName("tipos_pkey");

            entity.ToTable("tipos");

            entity.Property(e => e.Tipoid).HasColumnName("tipoid");
            entity.Property(e => e.Tipo1)
                .HasMaxLength(255)
                .HasColumnName("tipo");
        });

        modelBuilder.Entity<Utilizadores>(entity =>
        {
            entity.HasKey(e => e.Utilizadorid).HasName("utilizador_pkey");

            entity.ToTable("utilizadores");

            entity.HasIndex(e => e.Email, "utilizador_email_key").IsUnique();

            entity.Property(e => e.Utilizadorid)
                .HasDefaultValueSql("nextval('utilizador_utilizadorid_seq'::regclass)")
                .HasColumnName("utilizadorid");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .HasColumnName("nome");
            entity.Property(e => e.PalavraPasse)
                .HasMaxLength(512)
                .HasColumnName("palavra_passe");
            entity.Property(e => e.Tipoid).HasColumnName("tipoid");

            entity.HasOne(d => d.Tipo).WithMany(p => p.Utilizadores)
                .HasForeignKey(d => d.Tipoid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("utilizador_tipoid_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
