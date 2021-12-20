
namespace DTLibrary
{
	using System;

	public class SimpleTimer : ITimer
	{
		public long GetNumberOfMicroSeconds()
		{
			return DateTime.Now.Ticks / 10;
		}
	}
}
