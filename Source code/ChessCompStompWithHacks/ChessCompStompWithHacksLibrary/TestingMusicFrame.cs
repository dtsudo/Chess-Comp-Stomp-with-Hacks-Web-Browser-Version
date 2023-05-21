
namespace ChessCompStompWithHacksLibrary
{
	using DTLibrary;
	using System.Collections.Generic;

	public class TestingMusicFrame : IFrame<GameImage, GameFont, GameSound, GameMusic>
	{
		private GlobalState globalState;
		private SessionState sessionState;

		private SoundAndMusicVolumePicker volumePicker;

		public TestingMusicFrame(GlobalState globalState, SessionState sessionState)
		{
			this.globalState = globalState;
			this.sessionState = sessionState;

			this.volumePicker = null;
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
			if (keyboardInput.IsPressed(Key.Esc) && !previousKeyboardInput.IsPressed(Key.Esc))
				return new TestingFrame(globalState: this.globalState, sessionState: this.sessionState);

			if (this.volumePicker == null)
				this.volumePicker = new SoundAndMusicVolumePicker(
					xPos: 0,
					yPos: 0,
					initialSoundVolume: soundOutput.GetSoundVolume(),
					initialMusicVolume: this.globalState.MusicVolume,
					elapsedMicrosPerFrame: this.globalState.ElapsedMicrosPerFrame);

			this.volumePicker.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);
			soundOutput.SetSoundVolume(volume: this.volumePicker.GetCurrentSoundVolume());
			this.globalState.MusicVolume = this.volumePicker.GetCurrentMusicVolume();
			
			GameMusic? music = null;
			if (keyboardInput.IsPressed(Key.One) && !previousKeyboardInput.IsPressed(Key.One))
				music = GameMusic.TitleScreen;
			if (keyboardInput.IsPressed(Key.Two) && !previousKeyboardInput.IsPressed(Key.Two))
				music = GameMusic.Level1;
			if (keyboardInput.IsPressed(Key.Three) && !previousKeyboardInput.IsPressed(Key.Three))
				music = GameMusic.Level2;
			if (keyboardInput.IsPressed(Key.Four) && !previousKeyboardInput.IsPressed(Key.Four))
				music = GameMusic.Level3;
			if (keyboardInput.IsPressed(Key.Five) && !previousKeyboardInput.IsPressed(Key.Five))
				music = GameMusic.Ending;

			if (music != null)
				this.globalState.MusicPlayer.SetMusic(music: music.Value, volume: 100);

			if (keyboardInput.IsPressed(Key.Six) && !previousKeyboardInput.IsPressed(Key.Six))
				this.globalState.MusicPlayer.StopMusic();

			return this;
		}
		
		public void ProcessMusic()
		{
			this.globalState.ProcessMusic();
		}

		public void Render(IDisplayOutput<GameImage, GameFont> displayOutput)
		{
			displayOutput.DrawRectangle(
				x: 0,
				y: 0,
				width: GlobalConstants.WINDOW_WIDTH,
				height: GlobalConstants.WINDOW_HEIGHT,
				color: new DTColor(128, 128, 128),
				fill: true);

			if (this.volumePicker != null)
				this.volumePicker.Render(displayOutput: displayOutput);

			displayOutput.DrawText(
				x: 50,
				y: GlobalConstants.WINDOW_HEIGHT - 50,
				text: "Press 1/2/3/4/5 to switch music tracks." + "\n" + "Press 6 to stop music.",
				font: GameFont.GameFont16Pt,
				color: DTColor.Black());
		}

		public void RenderMusic(IMusicOutput<GameMusic> musicOutput)
		{
			this.globalState.RenderMusic(musicOutput: musicOutput);
		}
	}
}
