﻿using AudioBooks.Domain;
using Microsoft.EntityFrameworkCore;


namespace AudioBooks.Data
{
    public class AudioBookContext : DbContext
    {
        //public AudioBookContext()
        //{

        //}
        public AudioBookContext(DbContextOptions<AudioBookContext> options) : base(options)
        {
            //  ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbSet<AudioBook> AudioBooks { get; set; }
        public DbSet<Audio> Audios { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<AudioBookCategory> AudioBookCategories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().ToTable("Categories");
            modelBuilder.Entity<AudioBookCategory>().HasKey(s => new { s.AudioBookId, s.CategoryId }); 

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AudiobookDb";
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

    }
}
