using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using AutoTest.Net.DataGatherer.Model;

namespace AutoTest.Net.DataGatherer
{
	public class Service : IService
	{
		public void AddLogDump(LogData data)
		{
			
			if (data == null) return;

			// create data object/s
			LogDump LogDump = new LogDump(data);

			// save the data
			using (DataContext context = new DataContext())
			{
				context.LogDumps.Add(LogDump);
				context.SaveChanges();
			}
		}
	}
}
