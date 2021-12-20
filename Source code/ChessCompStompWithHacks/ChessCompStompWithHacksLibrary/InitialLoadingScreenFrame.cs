
namespace ChessCompStompWithHacksLibrary
{
	using DTLibrary;

	public class InitialLoadingScreenFrame : IFrame<ChessImage, ChessFont, ChessSound, ChessMusic>
	{
		private GlobalState globalState;

		public InitialLoadingScreenFrame(GlobalState globalState)
		{
			this.globalState = globalState;
		}

		public void ProcessExtraTime(int milliseconds)
		{
		}

		public IFrame<ChessImage, ChessFont, ChessSound, ChessMusic> GetNextFrame(
			IKeyboard keyboardInput,
			IMouse mouseInput,
			IKeyboard previousKeyboardInput,
			IMouse previousMouseInput,
			IDisplayProcessing<ChessImage> displayProcessing,
			ISoundOutput<ChessSound> soundOutput,
			IMusicProcessing musicProcessing)
		{
			var returnValue = this.GetNextFrameHelper(displayProcessing: displayProcessing, soundOutput: soundOutput, musicProcessing: musicProcessing);

			if (returnValue != null)
				return returnValue;

			returnValue = this.GetNextFrameHelper(displayProcessing: displayProcessing, soundOutput: soundOutput, musicProcessing: musicProcessing);

			if (returnValue != null)
				return returnValue;

			return this;
		}

		private IFrame<ChessImage, ChessFont, ChessSound, ChessMusic> GetNextFrameHelper(IDisplayProcessing<ChessImage> displayProcessing, ISoundOutput<ChessSound> soundOutput, IMusicProcessing musicProcessing)
		{
			bool isDoneLoadingImages = displayProcessing.LoadImages();

			if (!isDoneLoadingImages)
				return null;
			
			bool isDoneLoadingSounds = soundOutput.LoadSounds();

			if (!isDoneLoadingSounds)
				return null;
						
			bool isDoneLoadingMusic = musicProcessing.LoadMusic();

			if (!isDoneLoadingMusic)
				return null;
			
			return new TitleScreenFrame(globalState: this.globalState, sessionState: new SessionState(timer: this.globalState.Timer));
		}

		public void ProcessMusic()
		{
		}

		public void Render(IDisplayOutput<ChessImage, ChessFont> displayOutput)
		{
			displayOutput.DrawInitialLoadingScreen();
		}

		public void RenderMusic(IMusicOutput<ChessMusic> musicOutput)
		{
		}
	}
}
