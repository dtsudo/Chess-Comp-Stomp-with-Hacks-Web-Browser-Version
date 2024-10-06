
namespace ChessCompStompWithHacksLibrary
{
	using System;
	using System.Collections.Generic;

	public static class HackSelectionScreenMobileTabUtil
	{
		private static List<Tuple<HackSelectionScreenMobileTab, int>> GetTabIdMapping()
		{
			List<Tuple<HackSelectionScreenMobileTab, int>> list = new List<Tuple<HackSelectionScreenMobileTab, int>>();

			list.Add(new Tuple<HackSelectionScreenMobileTab, int>(HackSelectionScreenMobileTab.Tactics, 1));
			list.Add(new Tuple<HackSelectionScreenMobileTab, int>(HackSelectionScreenMobileTab.Eliteness, 2));
			list.Add(new Tuple<HackSelectionScreenMobileTab, int>(HackSelectionScreenMobileTab.RuleWarping, 3));

			return list;
		}

		/// <summary>
		/// Returns null if the tabId isn't valid
		/// </summary>
		public static HackSelectionScreenMobileTab? GetTabFromTabId(int tabId)
		{
			List<Tuple<HackSelectionScreenMobileTab, int>> mapping = GetTabIdMapping();

			foreach (Tuple<HackSelectionScreenMobileTab, int> tuple in mapping)
			{
				if (tuple.Item2 == tabId)
					return tuple.Item1;
			}

			return null;
		}

		/// <summary>
		/// Maps a tab to an integer identifier (in a consistent but arbitrary way)
		/// </summary>
		public static int GetTabId(this HackSelectionScreenMobileTab tab)
		{
			List<Tuple<HackSelectionScreenMobileTab, int>> mapping = GetTabIdMapping();

			foreach (Tuple<HackSelectionScreenMobileTab, int> tuple in mapping)
			{
				if (tuple.Item1 == tab)
					return tuple.Item2;
			}

			throw new Exception();
		}
	}
}
