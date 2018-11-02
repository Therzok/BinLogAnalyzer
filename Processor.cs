using System;
using Microsoft.Build.Framework;
using Microsoft.Build.Logging;

namespace BinLogAnalyzer
{
	class Processor
	{
		readonly BinaryLogReplayEventSource binlogReader = new BinaryLogReplayEventSource ();
		readonly string filePath;
		readonly Analyzer analyzer;

		public Processor(string filePath, Analyzer analyzer)
		{
			this.filePath = filePath;
			this.analyzer = analyzer;

			binlogReader.AnyEventRaised += AnyEventRaised;
		}

		public bool Process ()
		{
			binlogReader.Replay(filePath);
			return analyzer.Success;
		}

		void AnyEventRaised(object sender, BuildEventArgs args) => analyzer.OnEventRaised(args);
	}
}
