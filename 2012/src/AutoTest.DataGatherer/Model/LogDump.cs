using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoTest.Net.DataGatherer.Model
{
	public class LogDump
	{
		public LogDump(LogData data)
		{
			if (data != null)
			{
				this.StationName = data.SolutionName;
				this.NumberOfBuildsFailed = data.NumberOfBuildsFailed;
				this.NumberOfBuildsSucceeded = data.NumberOfBuildsSucceeded;
				this.NumberOfProjectsBuilt = data.NumberOfProjectsBuilt;
				this.NumberOfTestsFailed = data.NumberOfTestsFailed;
				this.NumberOfTestsIgnored = data.NumberOfTestsIgnored;
				this.NumberOfTestsPassed = data.NumberOfTestsPassed;
				this.NumberOfTestsRan = data.NumberOfTestsRan;
				this.StationName = data.StationName;
				this.TimeStamp = data.TimeStamp;
				this.UserName = data.UserName;
			}
		}
		public int LogDumpId
		{
			get;
			set;
		}

		public string StationName
		{
			get;
			set;
		}

		public int NumberOfBuildsFailed
		{
			get;
			set;
		}

		public int NumberOfBuildsSucceeded
		{
			get;
			set;
		}

		public int NumberOfProjectsBuilt
		{
			get;
			set;
		}

		public int NumberOfTestsFailed
		{
			get;
			set;
		}

		public int NumberOfTestsIgnored
		{
			get;
			set;
		}

		public int NumberOfTestsPassed
		{
			get;
			set;
		}

		public int NumberOfTestsRan
		{
			get;
			set;
		}

		public DateTime TimeStamp
		{
			get;
			set;
		}

		public string UserName
		{
			get;
			set;
		}

		//AutoTestNetVersionNumber
		//AutoTestEngineRunning
		//SolutionId
		//TestRunnerId

	}
}