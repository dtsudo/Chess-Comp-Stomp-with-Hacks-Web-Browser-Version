
namespace ChessCompStompWithHacksLibrary
{
	using DTLibrary;
	using System.Collections.Generic;

	public class InitialLoadingScreenFrame : IFrame<GameImage, GameFont, GameSound, GameMusic>
	{
		private GlobalState globalState;
		private DisplayType displayType;

		private string loadingText;

		public InitialLoadingScreenFrame(GlobalState globalState)
		{
			this.globalState = globalState;
			this.displayType = DisplayType.Desktop;
			this.loadingText = "";
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

		public IFrame<GameImage, GameFont, GameSound, GameMusic> ProcessDisplayType(DisplayType displayType, IDisplayProcessing<GameImage> displayProcessing)
		{
			this.displayType = displayType;

			return this;
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
			
			this.loadingText = "Loading...";

			if (this.globalState.DebugMode)
			{
				this.loadingText += "\n";
				int? numImageElementsToLoad = displayProcessing.GetNumTotalElementsToLoad();
				if (numImageElementsToLoad.HasValue)
					this.loadingText += "Images: " + displayProcessing.GetNumElementsLoaded().ToStringCultureInvariant() + " / " + numImageElementsToLoad.Value.ToStringCultureInvariant() + "\n";
				else
					this.loadingText += "Images: pending \n";

				int? numSoundElementsToLoad = soundOutput.GetNumTotalElementsToLoad();
				if (numSoundElementsToLoad.HasValue)
					this.loadingText += "Sound: " + soundOutput.GetNumElementsLoaded().ToStringCultureInvariant() + " / " + numSoundElementsToLoad.Value.ToStringCultureInvariant() + "\n";
				else
					this.loadingText += "Sound: pending \n";

				int? numMusicElementsToLoad = musicProcessing.GetNumTotalElementsToLoad();
				if (numMusicElementsToLoad.HasValue)
					this.loadingText += "Music: " + musicProcessing.GetNumElementsLoaded().ToStringCultureInvariant() + " / " + numMusicElementsToLoad.Value.ToStringCultureInvariant();
				else
					this.loadingText += "Music: pending";
			}

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
			
			return TitleScreenFrame.GetTitleScreenFrame(
				globalState: this.globalState,
				sessionState: sessionState,
				displayType: this.displayType,
				display: displayProcessing);
		}

		public void ProcessMusic()
		{
		}

		public void Render(IDisplayOutput<GameImage, GameFont> displayOutput)
		{
			displayOutput.DrawRectangle(
				x: 0,
				y: 0,
				width: this.displayType == DisplayType.Desktop ? GlobalConstants.DESKTOP_WINDOW_WIDTH : displayOutput.GetMobileScreenWidth(),
				height: this.displayType == DisplayType.Desktop ? GlobalConstants.DESKTOP_WINDOW_HEIGHT : displayOutput.GetMobileScreenHeight(),
				color: new DTColor(223, 220, 217),
				fill: true);
			
			if (this.displayType == DisplayType.Desktop)
				displayOutput.TryDrawText(
					x: 440,
					y: 400,
					text: this.loadingText,
					font: GameFont.GameFont20Pt,
					color: DTColor.Black());
			else
				displayOutput.TryDrawText(
					x: displayOutput.GetMobileScreenWidth() / 2 - 78,
					y: displayOutput.GetMobileScreenHeight() / 2 + 100,
					text: this.loadingText,
					font: GameFont.GameFont32Pt,
					color: DTColor.Black());
		}

		public void RenderMusic(IMusicOutput<GameMusic> musicOutput)
		{
		}
	}
}
