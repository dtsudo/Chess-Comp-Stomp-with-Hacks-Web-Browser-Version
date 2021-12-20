
namespace DTLibrary
{
	using System;

	public class ConsoleLogger : IDTLogger
	{
		public void Write(string str)
		{
			Console.Write(str);
		}

		public void WriteLine(string str)
		{
			Console.WriteLine(str);
		}

		public void WriteLine()
		{
			Console.WriteLine();
		}
	}
}
