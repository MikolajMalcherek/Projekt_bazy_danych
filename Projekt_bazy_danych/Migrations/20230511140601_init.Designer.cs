﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using bazy.Data;

#nullable disable

namespace Projekt_bazy_danych.Migrations
{
    [DbContext(typeof(ZawodnikDbContext))]
    [Migration("20230511140601_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("bazy.Models.Miejscowosci", b =>
                {
                    b.Property<int>("idmiejscowosci")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("kraj_miejscowosci")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("nazwa_miejscowosci")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("idmiejscowosci");

                    b.ToTable("Miejscowosci");
                });

            modelBuilder.Entity("bazy.Models.Wyniki", b =>
                {
                    b.Property<int>("idwyniki")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("idmiejscowosci")
                        .HasColumnType("int");

                    b.Property<int>("idzawodnicy")
                        .HasColumnType("int");

                    b.Property<int>("wynik")
                        .HasColumnType("int");

                    b.HasKey("idwyniki");

                    b.HasIndex("idmiejscowosci");

                    b.HasIndex("idzawodnicy");

                    b.ToTable("Wyniki");
                });

            modelBuilder.Entity("bazy.Models.Zawodnik", b =>
                {
                    b.Property<int>("idzawodnicy")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("imie_zawodnika")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("imie_zawodnika");

                    b.Property<string>("kraj_pochodzenia")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("kraj_pochodzenia");

                    b.Property<string>("nazwisko_zawodnika")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("nazwisko_zawodnika");

                    b.HasKey("idzawodnicy");

                    b.ToTable("Zawodnicy");
                });

            modelBuilder.Entity("bazy.Models.Wyniki", b =>
                {
                    b.HasOne("bazy.Models.Miejscowosci", "Miejscowosci")
                        .WithMany()
                        .HasForeignKey("idmiejscowosci")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("bazy.Models.Zawodnik", "Zawodnik")
                        .WithMany()
                        .HasForeignKey("idzawodnicy")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Miejscowosci");

                    b.Navigation("Zawodnik");
                });
#pragma warning restore 612, 618
        }
    }
}
