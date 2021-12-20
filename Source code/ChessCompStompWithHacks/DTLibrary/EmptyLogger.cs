
namespace DTLibrary
{
	public class EmptyLogger : IDTLogger
	{
		public void Write(string str)
		{
		}

		public void WriteLine(string str)
		{
		}

		public void WriteLine()
		{
		}
	}
}
