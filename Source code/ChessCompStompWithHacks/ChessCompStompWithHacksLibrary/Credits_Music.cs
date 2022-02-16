
namespace ChessCompStompWithHacksLibrary
{
	using DTLibrary;

	public class Credits_Music
	{
		private static string GetText()
		{
			return "The music tracks were created by Juhani Junkala and are licensed" + "\n"
				+ "under the CC0 Creative Commons license." + "\n"
				+ "\n"
				+ "(https://opengameart.org/content/5-chiptunes-action)";
		}

		public static void Render(IDisplayOutput<ChessImage, ChessFont> displayOutput, int width, int height)
		{
			displayOutput.DrawText(
				x: 10,
				y: height - 10,
				text: GetText(),
				font: ChessFont.ChessFont20Pt,
				color: DTColor.Black());
		}
	}
}
