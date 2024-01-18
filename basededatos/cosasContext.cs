using Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace basededatos
{
    public class CosasContext : DbContext
    {
        public CosasContext(DbContextOptions<CosasContext> options) : base(options) { }
        public DbSet<CosasItem> Cosas { get; set; }
        public DbSet<UserItem> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.NoAction;
            }

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }

    public class ServiceContextFactory : IDesignTimeDbContextFactory<CosasContext>
    {
        public CosasContext CreateDbContext(string[] args)
        {
            var builder = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", false, true);
            var config = builder.Build();
            var connectionString = config.GetConnectionString("ServiceContext");
            var optionsBuilder = new DbContextOptionsBuilder<CosasContext>();
            optionsBuilder.UseSqlServer(config.GetConnectionString("ServiceContext"));

            return new CosasContext(optionsBuilder.Options);
        }
    }
}
