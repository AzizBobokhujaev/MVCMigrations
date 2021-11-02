using Microsoft.EntityFrameworkCore;
using MVCMigrations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCMigrations.Context
{
    public class MVCContext : DbContext
    {
        public MVCContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Food> Foods { get; set; }
    }
}
