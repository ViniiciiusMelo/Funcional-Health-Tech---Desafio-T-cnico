using Microsoft.EntityFrameworkCore;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Data
{
    public class Banco : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Conta> Conta { get; internal set; }

        public Banco(DbContextOptions option) : base(option) { }

        public Banco() => this.Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("bancoIn");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .HasOne(a => a.Conta)
            .WithOne(a => a.User)
            .HasForeignKey<Conta>(c => c.UserId);

            modelBuilder.Entity<Conta>()
            .HasOne(a => a.User)
            .WithOne(a => a.Conta)
            .HasForeignKey<User>(c => c.ContaId);
        }
    }
}
