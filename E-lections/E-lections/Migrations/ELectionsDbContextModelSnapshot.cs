﻿// <auto-generated />
using System;
using E_lections.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace E_lections.Migrations
{
    [DbContext(typeof(ELectionsDbContext))]
    partial class ELectionsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("E_lections.Models.Admin", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("JMBG");

                    b.Property<string>("Lozinka");

                    b.HasKey("ID");

                    b.ToTable("Admin");
                });

            modelBuilder.Entity("E_lections.Models.BirackoMjesto", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Kanton");

                    b.HasKey("ID");

                    b.ToTable("BirackoMjesto");
                });

            modelBuilder.Entity("E_lections.Models.BirackoMjestoKandidat", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BirackoMjestoID");

                    b.Property<int>("BrojGlasova");

                    b.Property<int>("KandidatId");

                    b.HasKey("ID");

                    b.HasIndex("BirackoMjestoID");

                    b.HasIndex("KandidatId");

                    b.ToTable("BirackoMjestoKandidat");
                });

            modelBuilder.Entity("E_lections.Models.GlasackiListic", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BrojGlasova");

                    b.Property<int>("IzborId");

                    b.Property<int>("MaxOdabir");

                    b.Property<string>("Opis");

                    b.HasKey("ID");

                    b.HasIndex("IzborId");

                    b.ToTable("GlasackiListic");
                });

            modelBuilder.Entity("E_lections.Models.Izbor", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("KantonOgranicenje");

                    b.Property<string>("Opis");

                    b.Property<DateTime>("Pocetak");

                    b.Property<int>("Status");

                    b.HasKey("ID");

                    b.ToTable("Izbor");
                });

            modelBuilder.Entity("E_lections.Models.Osoba", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BirackoMjestoID");

                    b.Property<string>("BrojLicneKarte")
                        .IsRequired()
                        .HasMaxLength(9);

                    b.Property<DateTime>("DatumRodjenja");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Ime")
                        .IsRequired();

                    b.Property<string>("JMBG")
                        .IsRequired()
                        .HasMaxLength(13);

                    b.Property<string>("Kanton");

                    b.Property<string>("Lozinka");

                    b.Property<string>("Prezime")
                        .IsRequired();

                    b.Property<int>("Spol");

                    b.Property<int>("StrankaId");

                    b.Property<string>("Ulica");

                    b.HasKey("ID");

                    b.HasIndex("BirackoMjestoID");

                    b.HasIndex("StrankaId");

                    b.ToTable("Osoba");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Osoba");
                });

            modelBuilder.Entity("E_lections.Models.Profil", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Opis");

                    b.Property<string>("PutanjaSlike");

                    b.HasKey("ID");

                    b.ToTable("Profil");
                });

            modelBuilder.Entity("E_lections.Models.Stranka", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv");

                    b.HasKey("ID");

                    b.ToTable("Stranka");
                });

            modelBuilder.Entity("E_lections.Models.Glasac", b =>
                {
                    b.HasBaseType("E_lections.Models.Osoba");

                    b.ToTable("Glasac");

                    b.HasDiscriminator().HasValue("Glasac");
                });

            modelBuilder.Entity("E_lections.Models.Kandidat", b =>
                {
                    b.HasBaseType("E_lections.Models.Osoba");

                    b.Property<int>("GlasackiListicId");

                    b.Property<int>("ProfilId");

                    b.HasIndex("GlasackiListicId");

                    b.HasIndex("ProfilId")
                        .IsUnique()
                        .HasFilter("[ProfilId] IS NOT NULL");

                    b.ToTable("Kandidat");

                    b.HasDiscriminator().HasValue("Kandidat");
                });

            modelBuilder.Entity("E_lections.Models.BirackoMjestoKandidat", b =>
                {
                    b.HasOne("E_lections.Models.BirackoMjesto")
                        .WithMany("BirackoMjestoKandidati")
                        .HasForeignKey("BirackoMjestoID");

                    b.HasOne("E_lections.Models.Kandidat", "Kandidat")
                        .WithMany("BirackoMjestoKandidati")
                        .HasForeignKey("KandidatId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("E_lections.Models.GlasackiListic", b =>
                {
                    b.HasOne("E_lections.Models.Izbor", "Izbor")
                        .WithMany("GlasackiListici")
                        .HasForeignKey("IzborId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("E_lections.Models.Osoba", b =>
                {
                    b.HasOne("E_lections.Models.BirackoMjesto", "BirackoMjesto")
                        .WithMany()
                        .HasForeignKey("BirackoMjestoID");

                    b.HasOne("E_lections.Models.Stranka", "Stranka")
                        .WithMany("UpisiUStranku")
                        .HasForeignKey("StrankaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("E_lections.Models.Kandidat", b =>
                {
                    b.HasOne("E_lections.Models.GlasackiListic", "GlasackiListic")
                        .WithMany("Kandidati")
                        .HasForeignKey("GlasackiListicId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("E_lections.Models.Profil", "Profil")
                        .WithOne("Kandidat")
                        .HasForeignKey("E_lections.Models.Kandidat", "ProfilId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
