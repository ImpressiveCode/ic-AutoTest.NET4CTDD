#region Usings
using System;
using System.IO;
using System.Linq;
using AutoTest.Core.DebugLog;
using AutoTest.Core.Messaging;
#endregion

namespace AutoTest.Core.FileSystem
{
	public class DeveloperCustomIgnoreProvider : ICustomIgnoreProvider
	{
		#region Private variables 
		private readonly string[] _IgnoreValues = new[] { "AUTOTEST_IGNORE" };
		private IMessageBus _Bus;
		#endregion

		#region Constructors 
		public DeveloperCustomIgnoreProvider(IMessageBus bus)
		{
			_Bus = bus;  
			
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

			FileStream FileStream = new FileStream(filename, FileMode.Open);

			try
			{
				Debug.WriteDebug("File Opened: " + filename);

				using (TextReader TextReader = new StreamReader(FileStream))
				{
					if (FileStream.CanRead && FileStream.Length > 0)
					{
						string FirstLine = TextReader.ReadLine();

						if (!string.IsNullOrEmpty(FirstLine))
						{
							FirstLine = FirstLine.Trim().ToUpperInvariant();

							if (FirstLine.StartsWith("//") && _IgnoreValues.Any(item => FirstLine.Contains(item)))
							{
								Publish = false;
							}
						}
					}
				}
			}
			catch (Exception Exception)
			{
				Debug.WriteDebug(Exception.ToString());
				_Bus.Publish(Exception.ToString());

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
