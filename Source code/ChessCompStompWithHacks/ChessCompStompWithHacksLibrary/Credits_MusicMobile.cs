
namespace ChessCompStompWithHacksLibrary
{
	using DTLibrary;

	public class Credits_MusicMobile
	{
		public static void Render(IDisplayOutput<GameImage, GameFont> displayOutput, int width, int height)
		{
			string newline = "\n";

			string text;

			if (displayOutput.IsMobileInLandscapeOrientation())
				text = "Music track authors:" + newline
					+ "* Juhani Junkala" + newline
					+ newline
					+ "See the source code for more information (including licensing details).";
			else
				text = "Music track authors:" + newline
					+ "* Juhani Junkala" + newline
					+ newline
					+ "See the source code for more information" + newline
					+ "(including licensing details).";

			displayOutput.DrawText(
				x: 10,
				y: height - 10,
				text: text,
				font: GameFont.GameFont20Pt,
				color: DTColor.Black());
		}
	}
}
