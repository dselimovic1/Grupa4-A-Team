﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace E_lections.Models
{
    public class ELectionsDbContext : DbContext
    {
        public ELectionsDbContext(DbContextOptions<ELectionsDbContext> options) : base(options)
        {

        }

        public DbSet<Osoba> Osoba { get; set; }
        public DbSet<Glasac> Glasac { get; set; }
        public DbSet<Kandidat> Kandidat { get; set; }
        public DbSet<Profil> Profil { get; set; }
        public DbSet<BirackoMjesto> BirackoMjesto { get; set; }
        public DbSet<BirackoMjestoKandidat> BirackoMjestoKandidat { get; set; }
        public DbSet<GlasackiListic> GlasackiListic { get; set; }
        public DbSet<Izbor> Izbor { get; set; }
        public DbSet<Stranka> Stranka { get; set; }
        public DbSet<Admin> Admin { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Osoba>().ToTable("Osoba");
            modelBuilder.Entity<Glasac>().ToTable("Glasac");
            modelBuilder.Entity<Kandidat>().ToTable("Kandidat");
            modelBuilder.Entity<Profil>().ToTable("Profil");
            modelBuilder.Entity<BirackoMjesto>().ToTable("BirackoMjesto");
            modelBuilder.Entity<BirackoMjestoKandidat>().ToTable("BirackoMjestoKandidat");
            modelBuilder.Entity<GlasackiListic>().ToTable("GlasackiListic");
            modelBuilder.Entity<Izbor>().ToTable("Izbor");
            modelBuilder.Entity<Stranka>().ToTable("Stranka");
            modelBuilder.Entity<Admin>().ToTable("Admin");
        }
    }
}
