﻿// <auto-generated />
using System;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace TicketManagement.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Entities.Resumen", b =>
                {
                    b.Property<string>("Id_Registradora")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Id_Tienda")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("Tickets")
                        .HasColumnType("int");

                    b.HasIndex("Id_Tienda", "Id_Registradora")
                        .IsUnique();

                    b.ToTable("Resumen", (string)null);
                });

            modelBuilder.Entity("Entities.Ticket", b =>
                {
                    b.Property<DateTime?>("FechaHora")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaHora_Creacion")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("Id_Registradora")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Id_Tienda")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<decimal?>("Impuesto")
                        .IsRequired()
                        .HasColumnType("money");

                    b.Property<int?>("TicketNumber")
                        .IsRequired()
                        .HasColumnType("int")
                        .HasColumnName("Ticket");

                    b.Property<decimal?>("Total")
                        .IsRequired()
                        .HasColumnType("money");

                    b.HasIndex("TicketNumber")
                        .IsUnique();

                    b.ToTable("Tickets", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
