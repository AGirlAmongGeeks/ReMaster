using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using ReMaster.EntityFramework.Model;
using ReMaster.Utilities;
using ReMaster.Utilities.Tools;

namespace ReMaster.EntityFramework
{
	public class ReMasterDbContext : DbContext//, IDbContextFactory<ReMasterDbContext>
	{
		public ReMasterDbContext(string connectionString)
			: base()
		{ }

		public ReMasterDbContext(DbContextOptions<ReMasterDbContext> options)
			: base(options)
		{ }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			var configuration = ConfigurationHelper.Get(RemasterContext.GetPathToProjectFolder());

			var connString = configuration.GetConnectionString("DefaultConnection");
			optionsBuilder.UseSqlServer(connString);
		}

		public virtual DbSet<Company> Companies { get; set; }
	}

	public class MigrationsContextFactory : IDbContextFactory<ReMasterDbContext>
	{
		public ReMasterDbContext Create(DbContextFactoryOptions options)
		{
			return new ReMasterDbContext("DefaultConnection");
		}
	}
}
