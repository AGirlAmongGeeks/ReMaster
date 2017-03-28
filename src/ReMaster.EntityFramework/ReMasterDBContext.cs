using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ReMaster.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ReMaster.Utilities.Tools;
using ReMaster.Core.Model.Company;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Reflection;

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
