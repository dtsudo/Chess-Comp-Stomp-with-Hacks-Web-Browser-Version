
namespace ChessCompStompWithHacksLibrary
{
	using DTLibrary;
	using System.Collections.Generic;

	public class InitialLoadingScreenFrame : IFrame<GameImage, GameFont, GameSound, GameMusic>
	{
		private GlobalState globalState;

		public InitialLoadingScreenFrame(GlobalState globalState)
		{
			this.globalState = globalState;
		}

		public void ProcessExtraTime(int milliseconds)
		{
		}

		public string GetClickUrl()
		{
			return null;
		}

		public HashSet<string> GetCompletedAchievements()
		{
			return new HashSet<string>();
		}

		public IFrame<GameImage, GameFont, GameSound, GameMusic> GetNextFrame(
			IKeyboard keyboardInput,
			IMouse mouseInput,
			IKeyboard previousKeyboardInput,
			IMouse previousMouseInput,
			IDisplayProcessing<GameImage> displayProcessing,
			ISoundOutput<GameSound> soundOutput,
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

		private IFrame<GameImage, GameFont, GameSound, GameMusic> GetNextFrameHelper(IDisplayProcessing<GameImage> displayProcessing, ISoundOutput<GameSound> soundOutput, IMusicProcessing musicProcessing)
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

			SessionState sessionState = new SessionState(timer: this.globalState.Timer);

			this.globalState.LoadSessionState(sessionState: sessionState);

			int? soundVolume = this.globalState.LoadSoundVolume();
			if (soundVolume.HasValue)
				soundOutput.SetSoundVolumeImmediately(soundVolume.Value);

			this.globalState.LoadMusicVolume();
			
			GameMusic music = GameMusic.TitleScreen;
			this.globalState.MusicPlayer.SetMusic(music: music, volume: 100);

			return new TitleScreenFrame(globalState: this.globalState, sessionState: sessionState);
		}

		public void ProcessMusic()
		{
		}

		public void Render(IDisplayOutput<GameImage, GameFont> displayOutput)
		{
			displayOutput.DrawRectangle(
				x: 0,
				y: 0,
				width: GlobalConstants.WINDOW_WIDTH,
				height: GlobalConstants.WINDOW_HEIGHT,
				color: new DTColor(223, 220, 217),
				fill: true);

			displayOutput.TryDrawText(
				x: 440,
				y: 400,
				text: "Loading...",
				font: GameFont.GameFont20Pt,
				color: DTColor.Black());
		}

		public void RenderMusic(IMusicOutput<GameMusic> musicOutput)
		{
		}
	}
}
