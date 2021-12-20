
namespace ChessCompStompWithHacksLibrary
{
	using System;

	public enum ChessFont
	{
		Fetamont12Pt,
		Fetamont14Pt,
		Fetamont16Pt,
		Fetamont18Pt,
		Fetamont20Pt,
		Fetamont32Pt
	}

	public static class ChessFontUtil
	{
		public class FontInfo
		{
			public FontInfo(
				string ttfFontFilename,
				string woffFontFilename,
				string fontFamilyName,
				int fontSize,
				string javascriptFontSize,
				string javascriptLineHeight)
			{
				this.TtfFontFilename = ttfFontFilename;
				this.WoffFontFilename = woffFontFilename;
				this.FontFamilyName = fontFamilyName;
				this.FontSize = fontSize;
				this.JavascriptFontSize = javascriptFontSize;
				this.JavascriptLineHeight = javascriptLineHeight;
			}

			public string TtfFontFilename { get; private set; }
			public string WoffFontFilename { get; private set; }
			public string FontFamilyName { get; private set; }
			public int FontSize { get; private set; }
			public string JavascriptFontSize { get; private set; }
			public string JavascriptLineHeight { get; private set; }
		}

		public static FontInfo GetFontInfo(this ChessFont font)
		{
			switch (font)
			{
				case ChessFont.Fetamont12Pt:
					return new FontInfo(
						ttfFontFilename: "Metaflop/Fetamont.ttf",
						woffFontFilename: "Metaflop/Fetamont.woff",
						fontFamilyName: "Fetamont",
						fontSize: 12,
						javascriptFontSize: "15.86",
						javascriptLineHeight: "15.5");

				case ChessFont.Fetamont14Pt:
					return new FontInfo(
						ttfFontFilename: "Metaflop/Fetamont.ttf",
						woffFontFilename: "Metaflop/Fetamont.woff",
						fontFamilyName: "Fetamont",
						fontSize: 14,
						javascriptFontSize: "19.31",
						javascriptLineHeight: "18.5");

				case ChessFont.Fetamont16Pt:
					return new FontInfo(
						ttfFontFilename: "Metaflop/Fetamont.ttf",
						woffFontFilename: "Metaflop/Fetamont.woff",
						fontFamilyName: "Fetamont",
						fontSize: 16,
						javascriptFontSize: "21.85",
						javascriptLineHeight: "23");

				case ChessFont.Fetamont18Pt:
					return new FontInfo(
						ttfFontFilename: "Metaflop/Fetamont.ttf",
						woffFontFilename: "Metaflop/Fetamont.woff",
						fontFamilyName: "Fetamont",
						fontSize: 18,
						javascriptFontSize: "24.19",
						javascriptLineHeight: "24");

				case ChessFont.Fetamont20Pt:
					return new FontInfo(
						ttfFontFilename: "Metaflop/Fetamont.ttf",
						woffFontFilename: "Metaflop/Fetamont.woff",
						fontFamilyName: "Fetamont",
						fontSize: 20,
						javascriptFontSize: "26.76",
						javascriptLineHeight: "28.2");

				case ChessFont.Fetamont32Pt:
					return new FontInfo(
						ttfFontFilename: "Metaflop/Fetamont.ttf",
						woffFontFilename: "Metaflop/Fetamont.woff",
						fontFamilyName: "Fetamont",
						fontSize: 32,
						javascriptFontSize: "42.95",
						javascriptLineHeight: "44");

				default: throw new Exception();
			}
		}
	}
}
