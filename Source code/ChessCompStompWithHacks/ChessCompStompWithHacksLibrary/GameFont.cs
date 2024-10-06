
namespace ChessCompStompWithHacksLibrary
{
	using System;

	public enum GameFont
	{
		GameFont12Pt,
		GameFont14Pt,
		GameFont16Pt,
		GameFont18Pt,
		GameFont20Pt,
		GameFont32Pt,
		GameFont48Pt
	}

	public static class GameFontUtil
	{
		public class FontInfo
		{
			public FontInfo(
				string ttfFontFilename,
				string woffFontFilename,
				int fontSize,
				string javascriptFontSize,
				string lineHeight)
			{
				this.TtfFontFilename = ttfFontFilename;
				this.WoffFontFilename = woffFontFilename;
				this.FontSize = fontSize;
				this.JavascriptFontSize = javascriptFontSize;
				this.LineHeight = lineHeight;
			}

			public string TtfFontFilename { get; private set; }
			public string WoffFontFilename { get; private set; }
			public int FontSize { get; private set; }
			public string JavascriptFontSize { get; private set; }
			public string LineHeight { get; private set; }
		}

		public static FontInfo GetFontInfo(this GameFont font)
		{
			switch (font)
			{
				case GameFont.GameFont12Pt:
					return new FontInfo(
						ttfFontFilename: "Metaflop/dtchessfont.ttf",
						woffFontFilename: "Metaflop/dtchessfont.woff",
						fontSize: 12,
						javascriptFontSize: "15.86",
						lineHeight: "15.5");
				case GameFont.GameFont14Pt:
					return new FontInfo(
						ttfFontFilename: "Metaflop/dtchessfont.ttf",
						woffFontFilename: "Metaflop/dtchessfont.woff",
						fontSize: 14,
						javascriptFontSize: "19.31",
						lineHeight: "18.5");
				case GameFont.GameFont16Pt:
					return new FontInfo(
						ttfFontFilename: "Metaflop/dtchessfont.ttf",
						woffFontFilename: "Metaflop/dtchessfont.woff",
						fontSize: 16,
						javascriptFontSize: "21.85",
						lineHeight: "23");
				case GameFont.GameFont18Pt:
					return new FontInfo(
						ttfFontFilename: "Metaflop/dtchessfont.ttf",
						woffFontFilename: "Metaflop/dtchessfont.woff",
						fontSize: 18,
						javascriptFontSize: "24.19",
						lineHeight: "24");
				case GameFont.GameFont20Pt:
					return new FontInfo(
						ttfFontFilename: "Metaflop/dtchessfont.ttf",
						woffFontFilename: "Metaflop/dtchessfont.woff",
						fontSize: 20,
						javascriptFontSize: "26.76",
						lineHeight: "28.2");
				case GameFont.GameFont32Pt:
					return new FontInfo(
						ttfFontFilename: "Metaflop/dtchessfont.ttf",
						woffFontFilename: "Metaflop/dtchessfont.woff",
						fontSize: 32,
						javascriptFontSize: "42.95",
						lineHeight: "44");
				case GameFont.GameFont48Pt:
					return new FontInfo(
						ttfFontFilename: "Metaflop/dtchessfont.ttf",
						woffFontFilename: "Metaflop/dtchessfont.woff",
						fontSize: 48,
						javascriptFontSize: "64",
						lineHeight: "66");
				default: throw new Exception();
			}
		}
	}
}
