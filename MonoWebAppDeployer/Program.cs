using System;
using System.IO;

namespace MonoWebAppDeployer
{
	class MainClass
	{
		[STAThread]
		public static void Main(string[] args)
		{			
			var file = new FileInfo(args [0]);
			if (Directory.Exists(args [1]))
				Directory.Delete(args [1], true);

			var dir = Directory.CreateDirectory(args [1]);

			Processing.ProcessFiles(file, dir);
			Console.ReadLine();
		}
	}
}
