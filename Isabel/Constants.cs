using System;
using System.IO;
using System.Reflection;

namespace Isabel
{
	public static class Constants
	{
		public static readonly string ApplicationName;
		public static readonly string ConfigurationFolder;
		public static readonly string ApplicationFolder;

		static Constants()
		{
			ApplicationName = "Isabel";
			ApplicationFolder = AssemblyDirectory;
			ConfigurationFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), ApplicationName);
		}

		static string AssemblyDirectory
		{
			get
			{
				string codeBase = Assembly.GetExecutingAssembly().CodeBase;
				UriBuilder uri = new UriBuilder(codeBase);
				string path = Uri.UnescapeDataString(uri.Path);
				return Path.GetDirectoryName(path);
			}
		}
	}
}