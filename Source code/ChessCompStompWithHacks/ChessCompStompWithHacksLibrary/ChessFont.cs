
namespace ChessCompStompWithHacksLibrary
{
	using System;

	public enum ChessFont
	{
		ChessFont12Pt,
		ChessFont14Pt,
		ChessFont16Pt,
		ChessFont18Pt,
		ChessFont20Pt,
		ChessFont32Pt
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
				string lineHeight,
				string monoGameSpriteFontName)
			{
				this.TtfFontFilename = ttfFontFilename;
				this.WoffFontFilename = woffFontFilename;
				this.FontFamilyName = fontFamilyName;
				this.FontSize = fontSize;
				this.JavascriptFontSize = javascriptFontSize;
				this.LineHeight = lineHeight;
				this.MonoGameSpriteFontName = monoGameSpriteFontName;
			}

			public string TtfFontFilename { get; private set; }
			public string WoffFontFilename { get; private set; }
			public string FontFamilyName { get; private set; }
			public int FontSize { get; private set; }
			public string JavascriptFontSize { get; private set; }
			public string LineHeight { get; private set; }
			public string MonoGameSpriteFontName { get; private set; }
		}

		public static FontInfo GetFontInfo(this ChessFont font)
		{
			switch (font)
			{
				case ChessFont.ChessFont12Pt:
					return new FontInfo(
						ttfFontFilename: "Metaflop/dtchessfont.ttf",
						woffFontFilename: "Metaflop/dtchessfont.woff",
						fontFamilyName: "dtchessfont",
						fontSize: 12,
						javascriptFontSize: "15.86",
						lineHeight: "15.5",
						monoGameSpriteFontName: "dtchessfont12");

				case ChessFont.ChessFont14Pt:
					return new FontInfo(
						ttfFontFilename: "Metaflop/dtchessfont.ttf",
						woffFontFilename: "Metaflop/dtchessfont.woff",
						fontFamilyName: "dtchessfont",
						fontSize: 14,
						javascriptFontSize: "19.31",
						lineHeight: "18.5",
						monoGameSpriteFontName: "dtchessfont14");

				case ChessFont.ChessFont16Pt:
					return new FontInfo(
						ttfFontFilename: "Metaflop/dtchessfont.ttf",
						woffFontFilename: "Metaflop/dtchessfont.woff",
						fontFamilyName: "dtchessfont",
						fontSize: 16,
						javascriptFontSize: "21.85",
						lineHeight: "23",
						monoGameSpriteFontName: "dtchessfont16");

				case ChessFont.ChessFont18Pt:
					return new FontInfo(
						ttfFontFilename: "Metaflop/dtchessfont.ttf",
						woffFontFilename: "Metaflop/dtchessfont.woff",
						fontFamilyName: "dtchessfont",
						fontSize: 18,
						javascriptFontSize: "24.19",
						lineHeight: "24",
						monoGameSpriteFontName: "dtchessfont18");

				case ChessFont.ChessFont20Pt:
					return new FontInfo(
						ttfFontFilename: "Metaflop/dtchessfont.ttf",
						woffFontFilename: "Metaflop/dtchessfont.woff",
						fontFamilyName: "dtchessfont",
						fontSize: 20,
						javascriptFontSize: "26.76",
						lineHeight: "28.2",
						monoGameSpriteFontName: "dtchessfont20");

				case ChessFont.ChessFont32Pt:
					return new FontInfo(
						ttfFontFilename: "Metaflop/dtchessfont.ttf",
						woffFontFilename: "Metaflop/dtchessfont.woff",
						fontFamilyName: "dtchessfont",
						fontSize: 32,
						javascriptFontSize: "42.95",
						lineHeight: "44",
						monoGameSpriteFontName: "dtchessfont32");

				default: throw new Exception();
			}
		}
	}
}
