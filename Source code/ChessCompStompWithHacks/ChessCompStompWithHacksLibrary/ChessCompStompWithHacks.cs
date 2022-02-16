
namespace ChessCompStompWithHacksLibrary
{
	using DTLibrary;

	public class ChessCompStompWithHacks
	{
		public const int WINDOW_WIDTH = 1000;
		public const int WINDOW_HEIGHT = 700;

		public const int FILE_ID_FOR_GLOBAL_CONFIGURATION = 1;
		public const int FILE_ID_FOR_SESSION_STATE = 2;
		public const int FILE_ID_FOR_SOUND_AND_MUSIC_VOLUME = 3;

		public static IFrame<ChessImage, ChessFont, ChessSound, ChessMusic> GetFirstFrame(GlobalState globalState)
		{
			var frame = new InitialLoadingScreenFrame(globalState: globalState);
			return frame;
		}
	}
}
