
namespace ChessCompStompWithHacksLibrary
{
	using DTLibrary;

	public class Credits_Sound
	{
		private static string GetText()
		{
			return "The sound effects were created by Kenney (https://www.kenney.nl)" + "\n"
				+ "and are licensed under Creative Commons Zero.";
		}

		public static void Render(IDisplayOutput<GameImage, GameFont> displayOutput, int width, int height)
		{
			displayOutput.DrawText(
				x: 10,
				y: height - 10,
				text: GetText(),
				font: GameFont.GameFont20Pt,
				color: DTColor.Black());
		}
	}
}
