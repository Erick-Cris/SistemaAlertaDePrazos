﻿// <auto-generated />
using System;
using ApiUFU.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApiUFU.Migrations
{
    [DbContext(typeof(UFUContext))]
    partial class UFUContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AlertaDePrazosLibrary.Entities.Aluno", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("CursoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataIngresso")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Alunos");
                });

            modelBuilder.Entity("AlertaDePrazosLibrary.Entities.Curso", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cursos");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Titulo = "Sistemas de Informação - Santa Mônica"
                        },
                        new
                        {
                            Id = 2,
                            Titulo = "Sistemas de Informação - Monte Carmelo"
                        },
                        new
                        {
                            Id = 3,
                            Titulo = "Ciências da Computação - Santa Mônica"
                        });
                });

            modelBuilder.Entity("AlertaDePrazosLibrary.Entities.Disciplina", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("CargaHoraria")
                        .HasColumnType("int");

                    b.Property<int>("CursoId")
                        .HasColumnType("int");

                    b.Property<string>("IdDisciplina")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Obrigatoria")
                        .HasColumnType("bit");

                    b.Property<int>("Periodo")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Disciplinas");

                    b.HasData(
                        new
                        {
                            Id = "GSI001",
                            CargaHoraria = 60,
                            CursoId = 1,
                            Obrigatoria = true,
                            Periodo = 1,
                            Titulo = "Empreendedorismo em Informática"
                        },
                        new
                        {
                            Id = "GSI002",
                            CargaHoraria = 60,
                            CursoId = 1,
                            Obrigatoria = true,
                            Periodo = 1,
                            Titulo = "Introdução à Programação de Computadores"
                        },
                        new
                        {
                            Id = "GSI003",
                            CargaHoraria = 60,
                            CursoId = 1,
                            Obrigatoria = true,
                            Periodo = 1,
                            Titulo = "Introdução aos Sistemas de Informação"
                        },
                        new
                        {
                            Id = "GSI004",
                            CargaHoraria = 60,
                            CursoId = 1,
                            Obrigatoria = true,
                            Periodo = 1,
                            Titulo = "Programação Funcional"
                        },
                        new
                        {
                            Id = "GSI005",
                            CargaHoraria = 60,
                            CursoId = 1,
                            Obrigatoria = true,
                            Periodo = 1,
                            Titulo = "Lógica para Computação"
                        },
                        new
                        {
                            Id = "GSI006",
                            CargaHoraria = 60,
                            CursoId = 1,
                            IdDisciplina = "GSI002",
                            Obrigatoria = true,
                            Periodo = 2,
                            Titulo = "Estrutura de Dados 1"
                        },
                        new
                        {
                            Id = "GSI007",
                            CargaHoraria = 60,
                            CursoId = 1,
                            Obrigatoria = true,
                            Periodo = 2,
                            Titulo = "Matemática 1"
                        },
                        new
                        {
                            Id = "GSI008",
                            CargaHoraria = 60,
                            CursoId = 1,
                            Obrigatoria = true,
                            Periodo = 2,
                            Titulo = "Sistemas Digitais"
                        },
                        new
                        {
                            Id = "GSI009",
                            CargaHoraria = 60,
                            CursoId = 1,
                            Obrigatoria = true,
                            Periodo = 2,
                            Titulo = "Profissão em Sistemas de Informação"
                        },
                        new
                        {
                            Id = "GSI010",
                            CargaHoraria = 60,
                            CursoId = 1,
                            IdDisciplina = "GSI005",
                            Obrigatoria = true,
                            Periodo = 2,
                            Titulo = "Programação Lógica"
                        },
                        new
                        {
                            Id = "GSI011",
                            CargaHoraria = 60,
                            CursoId = 1,
                            IdDisciplina = "GSI006",
                            Obrigatoria = true,
                            Periodo = 3,
                            Titulo = "Estrutura de Dados 2"
                        },
                        new
                        {
                            Id = "GSI012",
                            CargaHoraria = 60,
                            CursoId = 1,
                            IdDisciplina = "GSI007",
                            Obrigatoria = true,
                            Periodo = 3,
                            Titulo = "Matemática 2"
                        },
                        new
                        {
                            Id = "GSI013",
                            CargaHoraria = 60,
                            CursoId = 1,
                            IdDisciplina = "GSI008",
                            Obrigatoria = true,
                            Periodo = 3,
                            Titulo = "Arquitetura e Organização de Computadores"
                        },
                        new
                        {
                            Id = "GSI014",
                            CargaHoraria = 60,
                            CursoId = 1,
                            Obrigatoria = true,
                            Periodo = 3,
                            Titulo = "Matemática para Ciência da Computação"
                        },
                        new
                        {
                            Id = "GSI015",
                            CargaHoraria = 60,
                            CursoId = 1,
                            Obrigatoria = true,
                            Periodo = 3,
                            Titulo = "Programação Orientada a Objetos 1"
                        },
                        new
                        {
                            Id = "GSI016",
                            CargaHoraria = 60,
                            CursoId = 1,
                            Obrigatoria = true,
                            Periodo = 4,
                            Titulo = "Banco de Dados 1"
                        },
                        new
                        {
                            Id = "GSI017",
                            CargaHoraria = 60,
                            CursoId = 1,
                            Obrigatoria = true,
                            Periodo = 4,
                            Titulo = "Estatística"
                        },
                        new
                        {
                            Id = "GSI018",
                            CargaHoraria = 60,
                            CursoId = 1,
                            IdDisciplina = "GSI013",
                            Obrigatoria = true,
                            Periodo = 4,
                            Titulo = "Sistemas Operacionais"
                        },
                        new
                        {
                            Id = "GSI019",
                            CargaHoraria = 60,
                            CursoId = 1,
                            Obrigatoria = true,
                            Periodo = 4,
                            Titulo = "Programação para Internet"
                        },
                        new
                        {
                            Id = "GSI020",
                            CargaHoraria = 60,
                            CursoId = 1,
                            IdDisciplina = "GSI015",
                            Obrigatoria = true,
                            Periodo = 4,
                            Titulo = "Programação Orientada a Objetos 2"
                        },
                        new
                        {
                            Id = "GSI021",
                            CargaHoraria = 60,
                            CursoId = 1,
                            IdDisciplina = "GSI016",
                            Obrigatoria = true,
                            Periodo = 5,
                            Titulo = "Banco de Dados 2"
                        },
                        new
                        {
                            Id = "GSI022",
                            CargaHoraria = 60,
                            CursoId = 1,
                            Obrigatoria = true,
                            Periodo = 5,
                            Titulo = "Matemática Financeira e Análise de Investimentos"
                        },
                        new
                        {
                            Id = "GSI023",
                            CargaHoraria = 60,
                            CursoId = 1,
                            IdDisciplina = "GSI018",
                            Obrigatoria = true,
                            Periodo = 5,
                            Titulo = "Redes de Computadores"
                        },
                        new
                        {
                            Id = "GSI024",
                            CargaHoraria = 60,
                            CursoId = 1,
                            Obrigatoria = true,
                            Periodo = 5,
                            Titulo = "Organização e Recuperação da Informação"
                        },
                        new
                        {
                            Id = "GSI025",
                            CargaHoraria = 60,
                            CursoId = 1,
                            Obrigatoria = true,
                            Periodo = 5,
                            Titulo = "Modelagem de Software"
                        },
                        new
                        {
                            Id = "GSI026",
                            CargaHoraria = 60,
                            CursoId = 1,
                            Obrigatoria = true,
                            Periodo = 6,
                            Titulo = "Gestão Empresarial"
                        },
                        new
                        {
                            Id = "GSI027",
                            CargaHoraria = 60,
                            CursoId = 1,
                            Obrigatoria = true,
                            Periodo = 6,
                            Titulo = "Otimização"
                        },
                        new
                        {
                            Id = "GSI028",
                            CargaHoraria = 60,
                            CursoId = 1,
                            IdDisciplina = "GSI023",
                            Obrigatoria = true,
                            Periodo = 6,
                            Titulo = "Sistemas Distribuídos"
                        },
                        new
                        {
                            Id = "GSI029",
                            CargaHoraria = 60,
                            CursoId = 1,
                            Obrigatoria = true,
                            Periodo = 6,
                            Titulo = "Contabilidade e Análise de Balanços"
                        },
                        new
                        {
                            Id = "GSI030",
                            CargaHoraria = 60,
                            CursoId = 1,
                            IdDisciplina = "GSI025",
                            Obrigatoria = true,
                            Periodo = 6,
                            Titulo = "Engenharia de Software"
                        },
                        new
                        {
                            Id = "GSI031",
                            CargaHoraria = 60,
                            CursoId = 1,
                            Obrigatoria = true,
                            Periodo = 7,
                            Titulo = "Economia"
                        },
                        new
                        {
                            Id = "GSI032",
                            CargaHoraria = 60,
                            CursoId = 1,
                            Obrigatoria = true,
                            Periodo = 7,
                            Titulo = "Fundamentos de Marketing"
                        },
                        new
                        {
                            Id = "GSI033",
                            CargaHoraria = 60,
                            CursoId = 1,
                            Obrigatoria = true,
                            Periodo = 7,
                            Titulo = "Gerência de Projetos de Tecnologia da Informação"
                        },
                        new
                        {
                            Id = "GSI034",
                            CargaHoraria = 60,
                            CursoId = 1,
                            IdDisciplina = "GSI030",
                            Obrigatoria = true,
                            Periodo = 7,
                            Titulo = "Projeto e Desenvolvimento de Sistemas de Informação 1"
                        },
                        new
                        {
                            Id = "GSI039",
                            CargaHoraria = 60,
                            CursoId = 1,
                            Obrigatoria = true,
                            Periodo = 7,
                            Titulo = "Trabalho de Conclusão de Curso 1"
                        },
                        new
                        {
                            Id = "GSI035",
                            CargaHoraria = 60,
                            CursoId = 1,
                            Obrigatoria = true,
                            Periodo = 8,
                            Titulo = "Auditoria e Segurança da Informação"
                        },
                        new
                        {
                            Id = "GSI036",
                            CargaHoraria = 60,
                            CursoId = 1,
                            Obrigatoria = true,
                            Periodo = 8,
                            Titulo = "Direito e Legislação"
                        },
                        new
                        {
                            Id = "GSI037",
                            CargaHoraria = 60,
                            CursoId = 1,
                            Obrigatoria = true,
                            Periodo = 8,
                            Titulo = "Interação Humano-Computador"
                        },
                        new
                        {
                            Id = "GSI038",
                            CargaHoraria = 60,
                            CursoId = 1,
                            IdDisciplina = "GSI034",
                            Obrigatoria = true,
                            Periodo = 8,
                            Titulo = "Projeto e Desenvolvimento de Sistemas de Informação 2"
                        },
                        new
                        {
                            Id = "GSI040",
                            CargaHoraria = 60,
                            CursoId = 1,
                            IdDisciplina = "GSI039",
                            Obrigatoria = true,
                            Periodo = 8,
                            Titulo = "Trabalho de Conclusão de Curso 2"
                        });
                });

            modelBuilder.Entity("AlertaDePrazosLibrary.Entities.Estagio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AlunoId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CursoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataInicio")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("Tipo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Estagios");
                });

            modelBuilder.Entity("AlertaDePrazosLibrary.Entities.MatriculaDisciplina", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AlunoId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DisciplinaId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Nota")
                        .HasColumnType("int");

                    b.Property<int>("SemestreId")
                        .HasColumnType("int");

                    b.Property<bool>("Trancamento")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("MatriculaDisciplinas");
                });

            modelBuilder.Entity("AlertaDePrazosLibrary.Entities.Semestre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataFim")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataInicio")
                        .HasColumnType("datetime2");

                    b.Property<int>("ano")
                        .HasColumnType("int");

                    b.Property<int>("ordem")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Semestres");
                });
#pragma warning restore 612, 618
        }
    }
}
