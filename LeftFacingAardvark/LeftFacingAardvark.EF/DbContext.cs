using LeftFacingAardvark.EF.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace LeftFacingAardvark.EF
{
    public class AardvarkContext : DbContext
    {
        public DbSet<Agent> Agents { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerTag> CustomerTags { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=AarkvarkDB.db", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            optionsBuilder.EnableSensitiveDataLogging();
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map table names
            modelBuilder.Entity<Customer>()
                .HasOne(x => x.Agent)
                .WithMany(y => y.Customers)
                .HasForeignKey(x => x.AgentId);
            modelBuilder.Entity<Customer>().HasIndex(x => x.Guid).IsUnique();
            modelBuilder.Entity<CustomerTag>()
                .HasOne(x => x.Customer)
                .WithMany(y => y.CustomerTags)
                .HasForeignKey(x => x.CustomerID);
            modelBuilder.Entity<CustomerTag>()
                .HasOne(x => x.Tag)
                .WithMany(y => y.CustomerTags)
                .HasForeignKey(x => x.TagID);

           
            base.OnModelCreating(modelBuilder);
        }
    }
}
