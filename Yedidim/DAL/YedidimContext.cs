using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Yedidim.Models;

namespace Yedidim.DAL
{
    public class YedidimContext : DbContext
    {
        public DbSet<Yadid> Yedidim { get; set; }
        public DbSet<Child> Children { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }

    
}