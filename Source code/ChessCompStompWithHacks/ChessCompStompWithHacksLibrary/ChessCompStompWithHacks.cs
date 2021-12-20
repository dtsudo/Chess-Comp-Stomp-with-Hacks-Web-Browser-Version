
namespace ChessCompStompWithHacksLibrary
{
	using DTLibrary;

	public class ChessCompStompWithHacks
	{
		public const int WINDOW_WIDTH = 1000;
		public const int WINDOW_HEIGHT = 700;

		public static IFrame<ChessImage, ChessFont, ChessSound, ChessMusic> GetFirstFrame(GlobalState globalState)
		{
			var frame = new InitialLoadingScreenFrame(globalState: globalState);
			return frame;
		}
	}
}
