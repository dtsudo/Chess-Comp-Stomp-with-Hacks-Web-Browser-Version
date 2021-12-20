
namespace DTLibrary
{
	using System.Collections.Generic;

	public static class ListUtil
	{
		public static void Shuffle<T>(this List<T> list, IDTRandom random)
		{
			for (int i = list.Count - 1; i > 0; i--)
			{
				int index = random.NextInt(i + 1);
				if (index != i)
				{
					T element = list[index];
					list[index] = list[i];
					list[i] = element;
				}
			}
		}
	}
}
