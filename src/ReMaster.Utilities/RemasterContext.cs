using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Reflection;
using ReMaster.Utilities.Tools;
using Microsoft.Extensions.Configuration;

namespace ReMaster.Utilities
{
	public class RemasterContext
	{
		#region GetPathToProjectFolder()
		public static string GetPathToProjectFolder()
		{
			var mainDirectoryPath = Path.GetDirectoryName(typeof(RemasterContext).GetTypeInfo().Assembly.Location);
			var mainDirectoryInfo = new DirectoryInfo(mainDirectoryPath);
			var pathToSln = Path.Combine(mainDirectoryPath, "ReMaster.sln");

			while (!System.IO.File.Exists(pathToSln))
			{
				if (mainDirectoryInfo.Parent == null)
				{
					break;
				}

				mainDirectoryInfo = mainDirectoryInfo.Parent;
				pathToSln = Path.Combine(mainDirectoryInfo.FullName, "ReMaster.sln");
			}
			var path = Path.Combine(mainDirectoryInfo.FullName, @"src\ReMaster");
			return path;
		} 
		#endregion
	}
}
