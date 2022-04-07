using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using Domain;

namespace DataAccessLayer
{
    //manage db
    class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options): base(options)
        {

        }

        public DbSet<Users> Users { get; set; }

        public DbSet<Vragen> Vragen { get; set; }

        public DbSet<Team> Team { get; set; }

        public DbSet<Highscore> Highscore { get; set; }

        public DbSet<Ability> Ability { get; set; }
    }
}
