﻿// <auto-generated />
using System;
using KsiazeczkaPttk.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace KsiazeczkaPttk.DAL.Migrations
{
    [DbContext(typeof(KsiazeczkaContext))]
    [Migration("20211224130019_AutoIncrement")]
    partial class AutoIncrement
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

            modelBuilder.Entity("KsiazeczkaPttk.Domain.Models.GotPttk", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

                    b.Property<string>("Nazwa")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Poziom")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("Poziom")
                        .IsUnique();

                    b.ToTable("GotPttk");
                });

            modelBuilder.Entity("KsiazeczkaPttk.Domain.Models.GrupaGorska", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("Nazwa")
                        .IsUnique();

                    b.ToTable("GrupyGorskie");
                });

            modelBuilder.Entity("KsiazeczkaPttk.Domain.Models.Ksiazeczka", b =>
                {
                    b.Property<string>("Wlasciciel")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<bool>("Niepelnosprawnosc")
                        .HasColumnType("boolean");

                    b.HasKey("Wlasciciel");

                    b.ToTable("Ksiazeczki");
                });

            modelBuilder.Entity("KsiazeczkaPttk.Domain.Models.Odcinek", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

                    b.Property<int>("Do")
                        .HasColumnType("integer");

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("Od")
                        .HasColumnType("integer");

                    b.Property<int>("Pasmo")
                        .HasColumnType("integer");

                    b.Property<int>("Punkty")
                        .HasColumnType("integer");

                    b.Property<int>("PunktyPowrot")
                        .HasColumnType("integer");

                    b.Property<int>("Wersja")
                        .HasColumnType("integer");

                    b.Property<string>("Wlasciciel")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.HasKey("Id");

                    b.HasIndex("Do");

                    b.HasIndex("Od");

                    b.HasIndex("Pasmo");

                    b.HasIndex("Wlasciciel");

                    b.ToTable("Odcinki");
                });

            modelBuilder.Entity("KsiazeczkaPttk.Domain.Models.PasmoGorskie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

                    b.Property<int>("Grupa")
                        .HasColumnType("integer");

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("Grupa");

                    b.HasIndex("Nazwa")
                        .IsUnique();

                    b.ToTable("PasmaGorskie");
                });

            modelBuilder.Entity("KsiazeczkaPttk.Domain.Models.PosiadanieGotPttk", b =>
                {
                    b.Property<string>("Wlasciciel")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<int>("Odznaka")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("DataPrzyznania")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DataRozpoczeciaZdobywania")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DataZakonczeniaZdobywania")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Wlasciciel", "Odznaka");

                    b.HasIndex("Odznaka");

                    b.ToTable("PosiadaneGotPttk");
                });

            modelBuilder.Entity("KsiazeczkaPttk.Domain.Models.PotwierdzenieTerenowe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

                    b.Property<bool>("Administracyjny")
                        .HasColumnType("boolean");

                    b.Property<int>("Punkt")
                        .HasColumnType("integer");

                    b.Property<string>("Typ")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.HasKey("Id");

                    b.HasIndex("Punkt");

                    b.HasIndex("Typ");

                    b.ToTable("PotwierdzeniaTerenowe");
                });

            modelBuilder.Entity("KsiazeczkaPttk.Domain.Models.PotwierdzenieTerenowePrzebytegoOdcinka", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

                    b.Property<int>("Potwierdzenie")
                        .HasColumnType("integer");

                    b.Property<int>("PrzebytyOdcinekId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Potwierdzenie");

                    b.HasIndex("PrzebytyOdcinekId");

                    b.ToTable("PotwierdzeniaTerenowePrzebytychOdcinkow");
                });

            modelBuilder.Entity("KsiazeczkaPttk.Domain.Models.PrzebycieOdcinka", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

                    b.Property<int>("Kolejnosc")
                        .HasColumnType("integer");

                    b.Property<int>("OdcinekId")
                        .HasColumnType("integer");

                    b.Property<bool>("Powrot")
                        .HasColumnType("boolean");

                    b.Property<int>("Wycieczka")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OdcinekId");

                    b.HasIndex("Wycieczka");

                    b.ToTable("PrzebyteOdcinki");
                });

            modelBuilder.Entity("KsiazeczkaPttk.Domain.Models.PunktTerenowy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

                    b.Property<double>("Lat")
                        .HasColumnType("double precision");

                    b.Property<double>("Lng")
                        .HasColumnType("double precision");

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Wlasciciel")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.HasKey("Id");

                    b.HasIndex("Nazwa")
                        .IsUnique();

                    b.HasIndex("Wlasciciel");

                    b.ToTable("PunktyTerenowe");
                });

            modelBuilder.Entity("KsiazeczkaPttk.Domain.Models.RolaUzytkownika", b =>
                {
                    b.Property<string>("Nazwa")
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)");

                    b.HasKey("Nazwa");

                    b.ToTable("RoleUzytkownikow");
                });

            modelBuilder.Entity("KsiazeczkaPttk.Domain.Models.StatusWycieczki", b =>
                {
                    b.Property<string>("Status")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Status");

                    b.ToTable("StatusyWycieczek");
                });

            modelBuilder.Entity("KsiazeczkaPttk.Domain.Models.TypPotwierdzeniaTerenowego", b =>
                {
                    b.Property<string>("Typ")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.HasKey("Typ");

                    b.ToTable("TypyPotwierdzenTerenowych");
                });

            modelBuilder.Entity("KsiazeczkaPttk.Domain.Models.Uzytkownik", b =>
                {
                    b.Property<string>("Login")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(160)
                        .HasColumnType("character varying(160)");

                    b.Property<string>("Haslo")
                        .IsRequired()
                        .HasMaxLength(160)
                        .HasColumnType("character varying(160)");

                    b.Property<string>("Imie")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<string>("Nazwisko")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Rola")
                        .IsRequired()
                        .HasColumnType("character varying(40)");

                    b.HasKey("Login");

                    b.HasIndex("Rola");

                    b.ToTable("Uzytkownicy");
                });

            modelBuilder.Entity("KsiazeczkaPttk.Domain.Models.Weryfikacje", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

                    b.Property<DateTime>("Data")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("PowodOdrzucenia")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Przodownik")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<int>("Wycieczka")
                        .HasColumnType("integer");

                    b.Property<bool>("Zaakceptiowana")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("Przodownik");

                    b.HasIndex("Wycieczka");

                    b.ToTable("Weryfikacje");
                });

            modelBuilder.Entity("KsiazeczkaPttk.Domain.Models.Wycieczka", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Wlasciciel")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.HasKey("Id");

                    b.HasIndex("Status");

                    b.HasIndex("Wlasciciel");

                    b.ToTable("Wycieczki");
                });

            modelBuilder.Entity("KsiazeczkaPttk.Domain.Models.ZamkniecieOdcinka", b =>
                {
                    b.Property<int>("OdcinekId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DataZamkniecia")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DataOtwarcia")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Przyczyna")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.HasKey("OdcinekId", "DataZamkniecia");

                    b.ToTable("ZamknieciaOdcinkow");
                });

            modelBuilder.Entity("KsiazeczkaPttk.Domain.Models.Ksiazeczka", b =>
                {
                    b.HasOne("KsiazeczkaPttk.Domain.Models.Uzytkownik", "WlascicielKsiazeczki")
                        .WithMany()
                        .HasForeignKey("Wlasciciel")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WlascicielKsiazeczki");
                });

            modelBuilder.Entity("KsiazeczkaPttk.Domain.Models.Odcinek", b =>
                {
                    b.HasOne("KsiazeczkaPttk.Domain.Models.PunktTerenowy", "PunktTerenowyDo")
                        .WithMany()
                        .HasForeignKey("Do")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KsiazeczkaPttk.Domain.Models.PunktTerenowy", "PunktTerenowyOd")
                        .WithMany()
                        .HasForeignKey("Od")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KsiazeczkaPttk.Domain.Models.PasmoGorskie", "PasmoGorskie")
                        .WithMany()
                        .HasForeignKey("Pasmo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KsiazeczkaPttk.Domain.Models.Uzytkownik", "Uzytkownik")
                        .WithMany()
                        .HasForeignKey("Wlasciciel");

                    b.Navigation("PasmoGorskie");

                    b.Navigation("PunktTerenowyDo");

                    b.Navigation("PunktTerenowyOd");

                    b.Navigation("Uzytkownik");
                });

            modelBuilder.Entity("KsiazeczkaPttk.Domain.Models.PasmoGorskie", b =>
                {
                    b.HasOne("KsiazeczkaPttk.Domain.Models.GrupaGorska", "GrupaGorska")
                        .WithMany()
                        .HasForeignKey("Grupa")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GrupaGorska");
                });

            modelBuilder.Entity("KsiazeczkaPttk.Domain.Models.PosiadanieGotPttk", b =>
                {
                    b.HasOne("KsiazeczkaPttk.Domain.Models.GotPttk", "OdznakaPttk")
                        .WithMany()
                        .HasForeignKey("Odznaka")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KsiazeczkaPttk.Domain.Models.Ksiazeczka", "Ksiazeczka")
                        .WithMany()
                        .HasForeignKey("Wlasciciel")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ksiazeczka");

                    b.Navigation("OdznakaPttk");
                });

            modelBuilder.Entity("KsiazeczkaPttk.Domain.Models.PotwierdzenieTerenowe", b =>
                {
                    b.HasOne("KsiazeczkaPttk.Domain.Models.PunktTerenowy", "PunktTerenowy")
                        .WithMany()
                        .HasForeignKey("Punkt")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KsiazeczkaPttk.Domain.Models.TypPotwierdzeniaTerenowego", "TypPotwierdzeniaTerenowego")
                        .WithMany()
                        .HasForeignKey("Typ")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PunktTerenowy");

                    b.Navigation("TypPotwierdzeniaTerenowego");
                });

            modelBuilder.Entity("KsiazeczkaPttk.Domain.Models.PotwierdzenieTerenowePrzebytegoOdcinka", b =>
                {
                    b.HasOne("KsiazeczkaPttk.Domain.Models.PotwierdzenieTerenowe", "PotwierdzenieTerenowe")
                        .WithMany()
                        .HasForeignKey("Potwierdzenie")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KsiazeczkaPttk.Domain.Models.PrzebycieOdcinka", "PrzebycieOdcinka")
                        .WithMany()
                        .HasForeignKey("PrzebytyOdcinekId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PotwierdzenieTerenowe");

                    b.Navigation("PrzebycieOdcinka");
                });

            modelBuilder.Entity("KsiazeczkaPttk.Domain.Models.PrzebycieOdcinka", b =>
                {
                    b.HasOne("KsiazeczkaPttk.Domain.Models.Odcinek", "Odcinek")
                        .WithMany()
                        .HasForeignKey("OdcinekId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KsiazeczkaPttk.Domain.Models.Wycieczka", "DotyczacaWycieczka")
                        .WithMany()
                        .HasForeignKey("Wycieczka")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DotyczacaWycieczka");

                    b.Navigation("Odcinek");
                });

            modelBuilder.Entity("KsiazeczkaPttk.Domain.Models.PunktTerenowy", b =>
                {
                    b.HasOne("KsiazeczkaPttk.Domain.Models.Uzytkownik", "Uzytkownik")
                        .WithMany()
                        .HasForeignKey("Wlasciciel");

                    b.Navigation("Uzytkownik");
                });

            modelBuilder.Entity("KsiazeczkaPttk.Domain.Models.Uzytkownik", b =>
                {
                    b.HasOne("KsiazeczkaPttk.Domain.Models.RolaUzytkownika", "RolaUzytkownika")
                        .WithMany()
                        .HasForeignKey("Rola")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RolaUzytkownika");
                });

            modelBuilder.Entity("KsiazeczkaPttk.Domain.Models.Weryfikacje", b =>
                {
                    b.HasOne("KsiazeczkaPttk.Domain.Models.Uzytkownik", "Uzytkownik")
                        .WithMany()
                        .HasForeignKey("Przodownik")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KsiazeczkaPttk.Domain.Models.Wycieczka", "DotyczacaWycieczka")
                        .WithMany()
                        .HasForeignKey("Wycieczka")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DotyczacaWycieczka");

                    b.Navigation("Uzytkownik");
                });

            modelBuilder.Entity("KsiazeczkaPttk.Domain.Models.Wycieczka", b =>
                {
                    b.HasOne("KsiazeczkaPttk.Domain.Models.StatusWycieczki", "StatusWycieczki")
                        .WithMany()
                        .HasForeignKey("Status")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KsiazeczkaPttk.Domain.Models.Uzytkownik", "Uzytkownik")
                        .WithMany()
                        .HasForeignKey("Wlasciciel")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StatusWycieczki");

                    b.Navigation("Uzytkownik");
                });

            modelBuilder.Entity("KsiazeczkaPttk.Domain.Models.ZamkniecieOdcinka", b =>
                {
                    b.HasOne("KsiazeczkaPttk.Domain.Models.Odcinek", "Odcinek")
                        .WithMany()
                        .HasForeignKey("OdcinekId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Odcinek");
                });
#pragma warning restore 612, 618
        }
    }
}
