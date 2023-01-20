using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bochacha.Domain;
using System.Reflection.Emit;

namespace Bochacha.Infrastructure.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<USER> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}