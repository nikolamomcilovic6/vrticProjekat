﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models;

namespace Projekat.Migrations
{
    [DbContext(typeof(VrticContext))]
    [Migration("20220311155348_V4")]
    partial class V4
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Models.Administrator", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("JMBG")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<string>("Sifra")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.HasKey("ID");

                    b.ToTable("Administratori");
                });

            modelBuilder.Entity("Models.Aktivnost", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("ID");

                    b.ToTable("Aktivnosti");
                });

            modelBuilder.Entity("Models.Dete", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("JMBG")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("VrticID")
                        .HasColumnType("int");

                    b.Property<string>("brojRoditelja")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("VrticID");

                    b.ToTable("Deca");
                });

            modelBuilder.Entity("Models.Nadgleda", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("AktivnostID")
                        .HasColumnType("int");

                    b.Property<int?>("VaspitacID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("AktivnostID");

                    b.HasIndex("VaspitacID");

                    b.ToTable("Nadgledaju");
                });

            modelBuilder.Entity("Models.Odrzava", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("AktivnostID")
                        .HasColumnType("int");

                    b.Property<DateTime>("DatumOdrzavanja")
                        .HasColumnType("datetime2");

                    b.Property<int?>("VrticID")
                        .HasColumnType("int");

                    b.Property<int>("brojMesta")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("AktivnostID");

                    b.HasIndex("VrticID");

                    b.ToTable("Odrzavaju");
                });

            modelBuilder.Entity("Models.Ucestvuje", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("AktivnostID")
                        .HasColumnType("int");

                    b.Property<int?>("DeteID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("AktivnostID");

                    b.HasIndex("DeteID");

                    b.ToTable("Ucestvuju");
                });

            modelBuilder.Entity("Models.Vaspitac", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("JMBG")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("VrticID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("VrticID");

                    b.ToTable("Vaspitaci");
                });

            modelBuilder.Entity("Models.Vrtic", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("AdministratorID")
                        .HasColumnType("int");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.HasIndex("AdministratorID");

                    b.ToTable("Vrtici");
                });

            modelBuilder.Entity("Models.Dete", b =>
                {
                    b.HasOne("Models.Vrtic", "Vrtic")
                        .WithMany("Deca")
                        .HasForeignKey("VrticID");

                    b.Navigation("Vrtic");
                });

            modelBuilder.Entity("Models.Nadgleda", b =>
                {
                    b.HasOne("Models.Aktivnost", "Aktivnost")
                        .WithMany("AktivnostVaspitac")
                        .HasForeignKey("AktivnostID");

                    b.HasOne("Models.Vaspitac", "Vaspitac")
                        .WithMany("VaspitacAktivnost")
                        .HasForeignKey("VaspitacID");

                    b.Navigation("Aktivnost");

                    b.Navigation("Vaspitac");
                });

            modelBuilder.Entity("Models.Odrzava", b =>
                {
                    b.HasOne("Models.Aktivnost", "Aktivnost")
                        .WithMany("AktivnostVrtic")
                        .HasForeignKey("AktivnostID");

                    b.HasOne("Models.Vrtic", "Vrtic")
                        .WithMany("VrticAktivnost")
                        .HasForeignKey("VrticID");

                    b.Navigation("Aktivnost");

                    b.Navigation("Vrtic");
                });

            modelBuilder.Entity("Models.Ucestvuje", b =>
                {
                    b.HasOne("Models.Aktivnost", "Aktivnost")
                        .WithMany("AktivnostDete")
                        .HasForeignKey("AktivnostID");

                    b.HasOne("Models.Dete", "Dete")
                        .WithMany("DeteAktivnost")
                        .HasForeignKey("DeteID");

                    b.Navigation("Aktivnost");

                    b.Navigation("Dete");
                });

            modelBuilder.Entity("Models.Vaspitac", b =>
                {
                    b.HasOne("Models.Vrtic", "Vrtic")
                        .WithMany("Vaspitaci")
                        .HasForeignKey("VrticID");

                    b.Navigation("Vrtic");
                });

            modelBuilder.Entity("Models.Vrtic", b =>
                {
                    b.HasOne("Models.Administrator", "Administrator")
                        .WithMany("Vrtici")
                        .HasForeignKey("AdministratorID");

                    b.Navigation("Administrator");
                });

            modelBuilder.Entity("Models.Administrator", b =>
                {
                    b.Navigation("Vrtici");
                });

            modelBuilder.Entity("Models.Aktivnost", b =>
                {
                    b.Navigation("AktivnostDete");

                    b.Navigation("AktivnostVaspitac");

                    b.Navigation("AktivnostVrtic");
                });

            modelBuilder.Entity("Models.Dete", b =>
                {
                    b.Navigation("DeteAktivnost");
                });

            modelBuilder.Entity("Models.Vaspitac", b =>
                {
                    b.Navigation("VaspitacAktivnost");
                });

            modelBuilder.Entity("Models.Vrtic", b =>
                {
                    b.Navigation("Deca");

                    b.Navigation("Vaspitaci");

                    b.Navigation("VrticAktivnost");
                });
#pragma warning restore 612, 618
        }
    }
}
