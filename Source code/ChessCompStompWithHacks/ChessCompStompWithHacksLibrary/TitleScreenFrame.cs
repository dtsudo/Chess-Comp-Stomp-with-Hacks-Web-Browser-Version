
namespace ChessCompStompWithHacksLibrary
{
	using DTLibrary;

	public class TitleScreenFrame
	{
		public static IFrame<GameImage, GameFont, GameSound, GameMusic> GetTitleScreenFrame(
			GlobalState globalState,
			SessionState sessionState,
			DisplayType displayType,
			IDisplayProcessing<GameImage> display)
		{
			if (displayType == DisplayType.Desktop)
				return new TitleScreenDesktopFrame(globalState: globalState, sessionState: sessionState);
			
			return new TitleScreenMobileFrame(globalState: globalState, sessionState: sessionState, display: display);
		}
	}
}
