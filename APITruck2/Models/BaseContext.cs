
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

        }
        public DbSet<Caminhao> Caminhoes { get; set; }
        public DbSet<Modelo> Modelos { get; set; }

    }
}
