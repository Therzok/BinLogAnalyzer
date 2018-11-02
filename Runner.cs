using System;
using System.Collections.Generic;

namespace BinLogAnalyzer
{
	class Runner
	{
		readonly List<Analyzer> allAnalyzers = new List<Analyzer>();

		public Runner()
		{
			foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
			{
				foreach (var type in asm.GetTypes())
				{
					if (type.IsSubclassOf(typeof(Analyzer)) && !type.IsAbstract)
					{
						var analyzer = (Analyzer)Activator.CreateInstance(type);
						allAnalyzers.Add(analyzer);
					}
				}
			}
		}

		public bool TryParseAndRunArgs (string[] args)
		{
			if (!TryParseArgs (args, out var analyzer, out var binlog))
			{
				PrintHelp();
				return false;
			}

			var processor = new Processor(binlog, analyzer);
			return processor.Process();
		}

		bool TryParseArgs (string[] args, out Analyzer analyzer, out string binlog)
		{
			analyzer = null;
			binlog = null;
			if (args.Length < 2)
				return false;

			// TODO: if analyzers need ids
			analyzer = allAnalyzers.Find(x => x.Id == args[0]);
			binlog = args[1];
			return analyzer != null && binlog != null;
		}

		public void PrintHelp()
		{
			Console.WriteLine("Usage: BinLogAnalyzer <id> [id_args] <binlog> ");
			Console.WriteLine("Available IDs:");
			foreach (var analyzer in allAnalyzers)
			{
				Console.WriteLine(analyzer.Id);
			}
		}
	}
}
