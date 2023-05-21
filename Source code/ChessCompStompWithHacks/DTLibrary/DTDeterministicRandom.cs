
namespace DTLibrary
{
	using System;

	public class DTDeterministicRandom : IDTDeterministicRandom
	{
		private int x;

		public DTDeterministicRandom()
		{
			this.x = 0;
		}

		public DTDeterministicRandom(int seed)
		{
			this.x = seed;
		}

		public string SerializeToString()
		{
			return this.x.ToStringCultureInvariant();
		}

		public void DeserializeFromString(string str)
		{
			this.x = str.ParseAsIntCultureInvariant();
		}

		public void AddSeed(int i)
		{
			this.x = unchecked(this.x + i);
		}

		public void Reset()
		{
			this.x = 0;
		}

		public int NextInt(int i)
		{
			if (i == 1)
				return 0;

			int a = unchecked(48271 * this.x + 11);
			int b = unchecked(48271 * a + 11);

			this.x = b;

			int c = ((a >> 16) << 16) | ((b >> 16) & 0xffff);

			if (c < 0)
				c = unchecked(-c);

			if (c < 0)
				c = 0;

			return c % i;
		}

		public bool NextBool()
		{
			return this.NextInt(2) == 1;
		}
	}
}
