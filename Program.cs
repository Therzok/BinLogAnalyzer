using System;
using System.Collections.Generic;

namespace BinLogAnalyzer
{
	static class MainClass
	{
		public static int Main(string[] args)
		{
			var runner = new Runner();
			if (runner.TryParseAndRunArgs(args))
				return 0;
			return 1;
		}
	}
}
