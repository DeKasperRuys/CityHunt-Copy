using System;
//using Domain;
using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebApplication2.Models;
//using System.Data.Entity;
//using System.Data.Entity;

namespace WebApplication2.Context
{
    public partial class DBFirstContext : DbContext
    {
        public DBFirstContext() {}
        
        public DBFirstContext(DbContextOptions<DBFirstContext> options)  : base(options) {}
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Vragen> Vragen { get; set; }
        public virtual DbSet<Team> Team { get; set; }

        public virtual DbSet<Highscore> Highscore { get; set; }
        public virtual DbSet<Ability> Ability { get; set; }

        /*
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<Grade> Grade { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DBFirst");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Student>()
                .HasOne<Grade>(s => s.CurrentGrade)
                .WithMany(ad => ad.Students)
                .HasForeignKey(s=>s.CurrentGradeId);


            OnModelCreatingPartial(modelBuilder);
        }
        
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        
   */
    }
}
