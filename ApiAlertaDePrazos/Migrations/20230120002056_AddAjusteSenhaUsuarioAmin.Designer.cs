﻿// <auto-generated />
using System;
using ApiAlertaDePrazos.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApiAlertaDePrazos.Migrations
{
    [DbContext(typeof(SistemaDeAlertaDePrazosContext))]
    [Migration("20230120002056_AddAjusteSenhaUsuarioAmin")]
    partial class AddAjusteSenhaUsuarioAmin
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AlertaDePrazosLibrary.Entities.AlertaDePrazos.Alerta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataAlerta")
                        .HasColumnType("datetime2");

                    b.Property<string>("MatriculaAluno")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RegraId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Alertas");
                });

            modelBuilder.Entity("AlertaDePrazosLibrary.Entities.AlertaDePrazos.Regra", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Parametros")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Regras");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Descricao = "Regra que define os critérios de avaliação do rendimento do discente",
                            IsActive = true,
                            Nome = "Rendimento",
                            Parametros = "[]"
                        },
                        new
                        {
                            Id = 2,
                            Descricao = "Regra que verifica se o aluno já trancou alguma disciplina",
                            IsActive = true,
                            Nome = "Trancamento Parical Ativo",
                            Parametros = "[]"
                        },
                        new
                        {
                            Id = 3,
                            Descricao = "Regra que verifica se o aluno já realizou trancamento geral do semestre",
                            IsActive = true,
                            Nome = "Trancamento Geral Ativo",
                            Parametros = "[]"
                        },
                        new
                        {
                            Id = 4,
                            Descricao = "Notifica os alunos sobre os requisitos e limites previstos em norma para realizar trancamento parcial.",
                            IsActive = true,
                            Nome = "Trancamento Parcial Passivo",
                            Parametros = "[]"
                        },
                        new
                        {
                            Id = 5,
                            Descricao = "Notifica os alunos sobre os requisitos e limites previstos em norma para realizar trancamento geral.",
                            IsActive = true,
                            Nome = "Trancamento Geral Passivo",
                            Parametros = "[]"
                        },
                        new
                        {
                            Id = 6,
                            Descricao = "Verifica se o discente já atende aos requisitos previstos nas normas da graduação para poder dar início ao estágio obrigatório.",
                            IsActive = true,
                            Nome = "Estagio Obrigatório Possível",
                            Parametros = "[]"
                        },
                        new
                        {
                            Id = 7,
                            Descricao = "Verifica se o discente já atende aos requisitos previstos nas normas da graduação para poder dar início ao estágio não obrigatório.",
                            IsActive = true,
                            Nome = "Estagio Não Obrigatório Possível",
                            Parametros = "[]"
                        },
                        new
                        {
                            Id = 8,
                            Descricao = "Verifica se o aluno está com estágio obrigatório em andamento para notifica-lo sobre prazos em relação ao estágio.",
                            IsActive = true,
                            Nome = "Estagio Obrigatório Ativo",
                            Parametros = "[]"
                        },
                        new
                        {
                            Id = 9,
                            Descricao = "Verifica se o aluno está com estágio não obrigatório em andamento para notifica-lo sobre prazos em relação ao estágio.",
                            IsActive = true,
                            Nome = "Estagio Não Obrigatório Ativo",
                            Parametros = "[]"
                        });
                });

            modelBuilder.Entity("AlertaDePrazosLibrary.Entities.AlertaDePrazos.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "erickcristianup@gmail.com",
                            IsActive = true,
                            Nome = "Administrador",
                            PasswordHash = "7C4A8D09CA3762AF61E59520943DC26494F8941B"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}