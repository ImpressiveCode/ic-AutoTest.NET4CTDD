#region Usings
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
#endregion

namespace AutoTest.Core.FileSystem
{
	public class DeveloperCustomIgnoreProvider : ICustomIgnoreProvider
	{
		#region Private variables
		private readonly string[] _IgnoreValues = new[] { "AUTOTEST_IGNORE" };
		#endregion

		#region Constructors
		public DeveloperCustomIgnoreProvider()
		{

		}
		#endregion

		#region ICustomIgnoreProvider Methods
		public bool ShouldPublish(string filename)
		{
			if (string.IsNullOrEmpty(filename))
			{
				return false;
			}


			bool Publish = true;

			FileStream FileStream = null;

			try
			{
				FileStream = File.OpenRead(filename);

				TextReader TextReader = new StreamReader(FileStream);

				string FirstLine = TextReader.ReadLine();

				if (!string.IsNullOrEmpty(FirstLine))
				{
					FirstLine = FirstLine.Trim().ToUpperInvariant();

					Publish = !FirstLine.StartsWith("//") && _IgnoreValues.Any(item => FirstLine.Contains(item));
				}

			}
			catch (Exception)
			{
				Publish = false;
			}
			finally
			{
				if (FileStream != null)
				{
					FileStream.Dispose();
				}
			}

			return Publish;
		}
		#endregion
	}
}
