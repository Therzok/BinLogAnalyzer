using System;
using Microsoft.Build.Framework;

namespace BinLogAnalyzer
{
	public class IncrementalBuildAnalyzer : Analyzer
	{
		bool success = true;

		public override string Id => "incremental";

		public override bool Success => success;

		public override void OnEventRaised(BuildEventArgs args)
		{
			if (!(args is TaskStartedEventArgs taskStarted))
				return;

			var taskName = taskStarted.TaskName;
			if (taskName == "Csc")
			{
				success = false;
				Console.Error.WriteLine("Incremental build failure: {0}", taskStarted.ProjectFile);
			}
		}
	}
}
