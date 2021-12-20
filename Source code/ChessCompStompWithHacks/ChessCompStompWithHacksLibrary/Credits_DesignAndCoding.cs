
namespace ChessCompStompWithHacksLibrary
{
	using DTLibrary;

	public class Credits_DesignAndCoding
	{
		private static string GetWebBrowserVersionText()
		{
			return "Design and coding by dtsudo (https://github.com/dtsudo)" + "\n"
				+ "\n"
				+ "This game is open source, under the MIT license." + "\n"
				+ "\n"
				+ "The source code is written in C#." + "\n"
				+ "\n"
				+ "Bridge.NET is used to transpile the C# source code into javascript." + "\n"
				+ "Bridge.NET is licensed under Apache License 2.0." + "\n"
				+ "(https://github.com/bridgedotnet/Bridge)";
		}

		private static string GetDesktopVersionText()
		{
			return "";
		}

		public static void Render(IDisplayOutput<ChessImage, ChessFont> displayOutput, int width, int height, bool isWebBrowserVersion)
		{
			string text = isWebBrowserVersion ? GetWebBrowserVersionText() : GetDesktopVersionText();

			displayOutput.DrawText(
				x: 10,
				y: height - 10,
				text: text,
				font: ChessFont.Fetamont20Pt,
				color: DTColor.Black());
		}
	}
}
