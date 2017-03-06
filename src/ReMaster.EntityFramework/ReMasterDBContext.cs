using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ReMaster.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ReMaster.EntityFramework
{
    public class ReMasterDbContext : DbContext
	{
		public ReMasterDbContext(DbContextOptions<ReMasterDbContext> options)
            : base(options)
        { }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			var configuration = ConfigurationHelper.Get(Directory.GetCurrentDirectory());
			var connString = configuration.GetConnectionString("DefaultConnection");

			optionsBuilder.UseSqlServer(connString);
		}
	}
}
