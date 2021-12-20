
namespace DTLibrary
{
	using System;

	/// <summary>
	/// An implementation of IDTRandom that simply
	/// uses the System.Random class.
	/// </summary>
	public class DTRandom : IDTRandom
	{
		private Random random;

		public DTRandom()
		{
			random = new Random();
		}

		public void AddSeed(int i)
		{
			random = new Random(i);
		}

		public int NextInt(int i)
		{
			return random.Next(i);
		}

		public bool NextBool()
		{
			return this.NextInt(2) == 1;
		}
	}
}
