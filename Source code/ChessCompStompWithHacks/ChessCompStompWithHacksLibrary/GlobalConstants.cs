
namespace ChessCompStompWithHacksLibrary
{
	using DTLibrary;

	public class GlobalConstants
	{
		/// <summary>
		/// The width in pixels, when the display type is "desktop"
		/// 
		/// When the display type is mobile, the width may vary.
		/// </summary>
		public const int DESKTOP_WINDOW_WIDTH = 1000;
		
		/// <summary>
		/// The height in pixels, when the display type is "desktop"
		/// 
		/// When the display type is mobile, the height may vary.
		/// </summary>
		public const int DESKTOP_WINDOW_HEIGHT = 700;

		public const int FILE_ID_FOR_GLOBAL_CONFIGURATION = 1;
		public const int FILE_ID_FOR_SESSION_STATE = 2;
		public const int FILE_ID_FOR_SOUND_AND_MUSIC_VOLUME = 3;
	}
}
