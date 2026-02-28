using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CelSyte.Models;

namespace CelSyte.Data
{
    public class CelSyteContext : DbContext
    {
        public CelSyteContext (DbContextOptions<CelSyteContext> options)
            : base(options)
        {
        }

        public DbSet<CelSyte.Models.User> User { get; set; } = default!;
    }
}
