using System;
using System.Xml;
using System.IO;

namespace MonoWebAppDeployer
{
	public class Processing
	{
		const string MSBUILD_NS = "http://schemas.microsoft.com/developer/msbuild/2003";
		public static void ProcessFiles(FileInfo file, DirectoryInfo dir)
		{
			var doc = new XmlDocument();
			doc.Load(file.FullName);

			var ns = new XmlNamespaceManager(doc.NameTable);
			ns.AddNamespace("msbld", MSBUILD_NS);

			Console.WriteLine("Processing Content");
			var nodes = doc.DocumentElement.SelectNodes("//msbld:Content", ns);
			foreach (XmlElement element in nodes)
			{
				var source = Path.Combine(file.Directory.FullName, element.GetAttribute("Include").Replace("\\", "/"));
				var destination = Path.Combine(dir.FullName, element.GetAttribute("Include").Replace("\\", "/"));
				Directory.CreateDirectory(Path.GetDirectoryName(destination));
				File.Copy(source, destination, true);
				Console.WriteLine("Processing: {0}", source);
			}
			Console.WriteLine("Finished Processing Content");

			var destBin = Directory.CreateDirectory(Path.Combine(dir.FullName, "bin"));
			var binDir = new DirectoryInfo(Path.Combine(file.Directory.FullName, "bin"));

			Console.WriteLine("Processing Bin files");
			foreach (var binFile in binDir.GetFiles())
			{
				var source = binFile.FullName;
				var destination = Path.Combine(destBin.FullName, binFile.Name);
				File.Copy(source, destination, true);
				Console.WriteLine("Processing: {0}", source);
			}
			Console.WriteLine("Finished Processing Bin Files");

		}
	}
}

