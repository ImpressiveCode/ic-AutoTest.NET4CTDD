using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace AutoTest.Net.DataGatherer.Model
{
	public class DataContext : DbContext
	{
         /// <summary>
        /// Creates instance of BaseDatabaseContext
        /// </summary>
        public DataContext()
            : base(nameOrConnectionString: "AutoTestDatabase")
        {
        }

		public DbSet<LogDump> LogDumps
		{
			get;
			set;
		}

		public DbSet<Project> Projects
		{
			get;
			set;
		}

		public DbSet<User> Users
		{
			get;
			set;
		}
	}
}