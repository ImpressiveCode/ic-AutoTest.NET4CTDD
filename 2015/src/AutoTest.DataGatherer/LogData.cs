using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace AutoTest.Net.DataGatherer
{
	[DataContract]
	public class LogData
	{
		[DataMember]
		public string StationName
		{
			get;
			set;
		}

		[DataMember]
		public int NumberOfBuildsFailed
		{
			get;
			set;
		}

		[DataMember]
		public int NumberOfBuildsSucceeded
		{
			get;
			set;
		}

		[DataMember]
		public int NumberOfProjectsBuilt
		{
			get;
			set;
		}

		[DataMember]
		public int NumberOfTestsFailed
		{
			get;
			set;
		}

		[DataMember]
		public int NumberOfTestsIgnored
		{
			get;
			set;
		}

		[DataMember]
		public int NumberOfTestsPassed
		{
			get;
			set;
		}

		[DataMember]
		public int NumberOfTestsRan
		{
			get;
			set;
		}

		[DataMember]
		public DateTime TimeStamp
		{
			get;
			set;
		}

		[DataMember]
		public string AutoTestNetVersionNumber
		{
			get;
			set;
		}

		[DataMember]
		public bool AutoTestEngineRunning
		{
			get;
			set;
		}

		[DataMember]
		public string UserName
		{
			get;
			set;
		}

		[DataMember]
		public string SolutionName
		{
			get;
			set;
		}

		[DataMember]
		public string TestRunnerName
		{
			get;
			set;
		}

		[DataMember]
		public string[] Projects
		{
			get;
			set;
		}

	}
}