
namespace ChessCompStompWithHacksLibrary
{
	using DTLibrary;
	using System;
	using System.Collections.Generic;

	public enum ColorTheme
	{
		Initial,
		Progress1,
		Progress2,
		Progress3,
		Final
	}

	public static class ColorThemeUtil
	{
		public static DTColor GetTextBackgroundColor(ColorTheme colorTheme)
		{
			switch (colorTheme)
			{
				case ColorTheme.Initial: return new DTColor(255, 245, 171);
				case ColorTheme.Progress1: return new DTColor(255, 234, 152);
				case ColorTheme.Progress2: return new DTColor(255, 223, 134);
				case ColorTheme.Progress3: return new DTColor(255, 211, 115);
				case ColorTheme.Final: return new DTColor(255, 200, 97);
				default: throw new Exception();
			}
		}

		public static DTColor GetHoverColor(ColorTheme colorTheme)
		{
			switch (colorTheme)
			{
				case ColorTheme.Initial: return new DTColor(250, 249, 200);
				case ColorTheme.Progress1: return new DTColor(251, 239, 178);
				case ColorTheme.Progress2: return new DTColor(252, 228, 155);
				case ColorTheme.Progress3: return new DTColor(253, 218, 133);
				case ColorTheme.Final: return new DTColor(255, 207, 110);
				default: throw new Exception();
			}
		}

		public static DTColor GetClickColor(ColorTheme colorTheme)
		{
			switch (colorTheme)
			{
				case ColorTheme.Initial: return new DTColor(252, 251, 154);
				case ColorTheme.Progress1: return new DTColor(249, 231, 130);
				case ColorTheme.Progress2: return new DTColor(246, 210, 106);
				case ColorTheme.Progress3: return new DTColor(243, 190, 82);
				case ColorTheme.Final: return new DTColor(240, 170, 58);
				default: throw new Exception();
			}
		}

		private static List<Tuple<ColorTheme, int>> GetColorThemeIdMapping()
		{
			List<Tuple<ColorTheme, int>> list = new List<Tuple<ColorTheme, int>>();

			list.Add(new Tuple<ColorTheme, int>(ColorTheme.Initial, 1));
			list.Add(new Tuple<ColorTheme, int>(ColorTheme.Progress1, 2));
			list.Add(new Tuple<ColorTheme, int>(ColorTheme.Progress2, 3));
			list.Add(new Tuple<ColorTheme, int>(ColorTheme.Progress3, 4));
			list.Add(new Tuple<ColorTheme, int>(ColorTheme.Final, 5));

			return list;
		}

		/// <summary>
		/// Returns null if the colorThemeId isn't valid
		/// </summary>
		public static ColorTheme? GetColorThemeFromColorThemeId(int colorThemeId)
		{
			List<Tuple<ColorTheme, int>> mapping = GetColorThemeIdMapping();

			foreach (Tuple<ColorTheme, int> tuple in mapping)
			{
				if (tuple.Item2 == colorThemeId)
					return tuple.Item1;
			}

			return null;
		}

		/// <summary>
		/// Maps a color theme to an integer identifier (in a consistent but arbitrary way)
		/// </summary>
		public static int GetColorThemeId(this ColorTheme colorTheme)
		{
			List<Tuple<ColorTheme, int>> mapping = GetColorThemeIdMapping();

			foreach (Tuple<ColorTheme, int> tuple in mapping)
			{
				if (tuple.Item1 == colorTheme)
					return tuple.Item2;
			}

			throw new Exception();
		}
	}
}
