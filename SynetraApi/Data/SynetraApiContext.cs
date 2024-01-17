using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SynetraApi.Models;

namespace SynetraApi.Data
{
    public class SynetraApiContext : DbContext
    {
        public SynetraApiContext (DbContextOptions<SynetraApiContext> options)
            : base(options)
        {
        }

        public DbSet<SynetraApi.Models.Computers> Computers { get; set; } = default!;
        public DbSet<SynetraApi.Models.Users> Users { get; set; } = default!;
        public DbSet<SynetraApi.Models.Logs> Logs { get; set; } = default!;
        public DbSet<SynetraApi.Models.Rooms> Rooms { get; set; } = default!;
        public DbSet<SynetraApi.Models.Parcs> Parcs { get; set; } = default!;
    }
}
