
namespace DTLibrary
{
	using System;
	using System.Collections.Generic;

	/*
		This class is needed for Bridge.NET because Bridge.NET has a bug where
		the tuple with Item1 = Item2 = 0 doesn't work when used in hash tables.

		Specifically, calling Bridge.getHashCode({ Item1: 0, Item2: 0 }) generates
		random results.
	*/
	public class IntTupleEqualityComparer : IEqualityComparer<Tuple<int, int>>
	{
		public bool Equals(Tuple<int, int> x, Tuple<int, int> y)
		{
			if (x == null && y == null)
				return true;

			if (x == null || y == null)
				return false;

			return x.Item1 == y.Item1 && x.Item2 == y.Item2;
		}

		public int GetHashCode(Tuple<int, int> obj)
		{
			int a = obj.Item1 << 4;

			return unchecked(a + obj.Item1 + obj.Item2);
		}
	}
}
