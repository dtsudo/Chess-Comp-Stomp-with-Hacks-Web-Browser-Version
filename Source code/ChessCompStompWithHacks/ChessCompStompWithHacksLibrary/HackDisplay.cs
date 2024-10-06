
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System;

	public class HackDisplay
	{
		public enum Theme
		{
			Blue,
			Green,
			Purple
		}

		public static DTColor GetResearchedHackBackgroundColor(Theme theme)
		{
			switch (theme)
			{
				case Theme.Blue: return new DTColor(196, 234, 255);
				case Theme.Green: return new DTColor(201, 255, 196);
				case Theme.Purple: return new DTColor(202, 196, 255);
				default: throw new Exception();
			}
		}
	}
}
