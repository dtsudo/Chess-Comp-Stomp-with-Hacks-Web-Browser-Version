
namespace ChessCompStompWithHacksLibrary
{
	using DTLibrary;

	public class Credits_Music
	{
		private static string GetText()
		{
			return "";
		}

		public static void Render(IDisplayOutput<ChessImage, ChessFont> displayOutput, int width, int height)
		{
			displayOutput.DrawText(
				x: 10,
				y: height - 10,
				text: GetText(),
				font: ChessFont.Fetamont20Pt,
				color: DTColor.Black());
		}
	}
}
