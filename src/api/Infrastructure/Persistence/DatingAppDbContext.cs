using System;
using System.Reflection;
using Application.Common.Persistence;
using Domain.Aggregates.Users.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    internal class DatingAppDbContext : DbContext, IDatingAppDbContext, IUnitOfWork
    {
        public DatingAppDbContext(DbContextOptions<DatingAppDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSnakeCaseNamingConvention();
#if DEBUG
            optionsBuilder.LogTo(Console.WriteLine);
#endif
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users => Set<User>();

        public DbSet<Message> Messages => Set<Message>();
    }
}
