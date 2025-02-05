using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ITLATV.Models;

namespace ITLATV.Data
{
    public class ITLATVContext : DbContext
    {
        public ITLATVContext (DbContextOptions<ITLATVContext> options)
            : base(options)
        {
        }

        public DbSet<ITLATV.Models.Serie> Serie { get; set; } = default!;
        public DbSet<ITLATV.Models.Genero> Genero { get; set; } = default!;
        public DbSet<ITLATV.Models.Productora> Productora { get; set; } = default!;
    }
}
