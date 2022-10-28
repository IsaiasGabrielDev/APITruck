
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APITruck2.Models
{
    public class BaseContext : DbContext
    {
        
        public BaseContext(DbContextOptions<BaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Caminhao> Caminhoes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Caminhao>()
            .Property(p => p.NomeModelo).HasConversion(
            p => p.ToString(),
            p => (ModeloNome)Enum.Parse(typeof(ModeloNome), p));

        }
        

    }
}
