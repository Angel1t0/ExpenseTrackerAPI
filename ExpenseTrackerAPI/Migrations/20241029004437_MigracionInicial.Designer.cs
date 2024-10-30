﻿// <auto-generated />
using ExpenseTrackerAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ExpenseTrackerAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241029004437_MigracionInicial")]
    partial class MigracionInicial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ExpenseTrackerAPI.Models.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categorias");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nombre = "Comida"
                        },
                        new
                        {
                            Id = 2,
                            Nombre = "Entretenimiento"
                        },
                        new
                        {
                            Id = 3,
                            Nombre = "Electrónica"
                        },
                        new
                        {
                            Id = 4,
                            Nombre = "Utilidades"
                        },
                        new
                        {
                            Id = 5,
                            Nombre = "Ropa"
                        },
                        new
                        {
                            Id = 6,
                            Nombre = "Salud"
                        },
                        new
                        {
                            Id = 7,
                            Nombre = "Otros"
                        });
                });

            modelBuilder.Entity("ExpenseTrackerAPI.Models.Gasto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoriaId")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FechaCreacion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FechaGasto")
                        .HasColumnType("int");

                    b.Property<float>("Monto")
                        .HasColumnType("real");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Gastos");
                });

            modelBuilder.Entity("ExpenseTrackerAPI.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FechaCreacion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("ExpenseTrackerAPI.Models.Gasto", b =>
                {
                    b.HasOne("ExpenseTrackerAPI.Models.Categoria", "Categoria")
                        .WithMany("Gastos")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ExpenseTrackerAPI.Models.Usuario", "Usuario")
                        .WithMany("Gastos")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("ExpenseTrackerAPI.Models.Categoria", b =>
                {
                    b.Navigation("Gastos");
                });

            modelBuilder.Entity("ExpenseTrackerAPI.Models.Usuario", b =>
                {
                    b.Navigation("Gastos");
                });
#pragma warning restore 612, 618
        }
    }
}
