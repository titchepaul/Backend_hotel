using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using RoomsManager.Models;

namespace RoomsManager
{
    public class DefaultContext : DbContext
    {
        public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
        {
        }

       /* protected DefaultContext()
        {
        }*/
        public DbSet<MesReserves> MesReserves { get; set; }
        public DbSet<Rooms> Rooms { get; set; }
        public DbSet<Users> Users { get; set; }

    }
}
