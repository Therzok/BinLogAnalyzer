using System;
using Microsoft.Build.Framework;

namespace BinLogAnalyzer
{
	public abstract class Analyzer
	{
		public abstract string Id { get; }
		public abstract bool Success { get; }

		public abstract void OnEventRaised(BuildEventArgs args);
	}
}
