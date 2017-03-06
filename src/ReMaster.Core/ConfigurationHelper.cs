using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ReMaster.Core
{
	public static class ConfigurationHelper
	{
		private static readonly ConcurrentDictionary<string, IConfigurationRoot> ConfigurationCache;

		static ConfigurationHelper()
		{
			ConfigurationCache = new ConcurrentDictionary<string, IConfigurationRoot>();
		}

		#region Get()
		/// <summary>
		/// Gets configuration from cache or from appsettings.json if cache is empty.
		/// </summary>
		/// <param name="path"></param>
		/// <param name="environmentName"></param>
		/// <param name="addUserSecrets"></param>
		/// <returns></returns>
		public static IConfigurationRoot Get(string path, string environmentName = null, bool addUserSecrets = false)
		{
			var cacheKey = path + "#" + environmentName + "#" + addUserSecrets;
			
			return ConfigurationCache.GetOrAdd(
				cacheKey,
				_ => BuildConfiguration(path, environmentName, addUserSecrets)
			);
		}
		#endregion

		#region BuildConfiguration()
		/// <summary>
		/// Reads configuration from appsettings.{environmentName}.json.
		/// </summary>
		/// <param name="path"></param>
		/// <param name="environmentName"></param>
		/// <param name="addUserSecrets"></param>
		/// <returns></returns>
		private static IConfigurationRoot BuildConfiguration(string path, string environmentName = null, bool addUserSecrets = false)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(path)
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

			if (!String.IsNullOrWhiteSpace(environmentName))
			{
				builder = builder.AddJsonFile($"appsettings.{environmentName}.json", optional: true);
			}
			
			builder = builder.AddEnvironmentVariables();

			if (addUserSecrets)
			{
				builder.AddUserSecrets();
			}

			return builder.Build();
		}
		#endregion

	}
}
