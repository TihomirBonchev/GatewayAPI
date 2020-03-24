using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusalaApi.Model
{
    public class ApiContext:DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Gateway> Gateway { get; set; }
        public DbSet<Device> Device { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Gateway>()
        //        //.HasOne(i => i.DeviceId)
        //        .WithMany(c => c.)
        //        .OnDelete(DeleteBehavior.SetNull + -*);
        //}
    }
}
