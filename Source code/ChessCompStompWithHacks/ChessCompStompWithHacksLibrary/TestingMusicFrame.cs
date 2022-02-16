
namespace ChessCompStompWithHacksLibrary
{
	using DTLibrary;

	public class TestingMusicFrame : IFrame<ChessImage, ChessFont, ChessSound, ChessMusic>
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

		public IFrame<ChessImage, ChessFont, ChessSound, ChessMusic> GetNextFrame(
			IKeyboard keyboardInput,
			IMouse mouseInput,
			IKeyboard previousKeyboardInput,
			IMouse previousMouseInput,
			IDisplayProcessing<ChessImage> displayProcessing,
			ISoundOutput<ChessSound> soundOutput,
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
			
			ChessMusic? music = null;
			if (keyboardInput.IsPressed(Key.One) && !previousKeyboardInput.IsPressed(Key.One))
				music = ChessMusic.TitleScreen;
			if (keyboardInput.IsPressed(Key.Two) && !previousKeyboardInput.IsPressed(Key.Two))
				music = ChessMusic.Level1;
			if (keyboardInput.IsPressed(Key.Three) && !previousKeyboardInput.IsPressed(Key.Three))
				music = ChessMusic.Level2;
			if (keyboardInput.IsPressed(Key.Four) && !previousKeyboardInput.IsPressed(Key.Four))
				music = ChessMusic.Level3;
			if (keyboardInput.IsPressed(Key.Five) && !previousKeyboardInput.IsPressed(Key.Five))
				music = ChessMusic.Ending;

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

		public void Render(IDisplayOutput<ChessImage, ChessFont> displayOutput)
		{
			displayOutput.DrawRectangle(
				x: 0,
				y: 0,
				width: ChessCompStompWithHacks.WINDOW_WIDTH,
				height: ChessCompStompWithHacks.WINDOW_HEIGHT,
				color: new DTColor(128, 128, 128),
				fill: true);

			if (this.volumePicker != null)
				this.volumePicker.Render(displayOutput: displayOutput);

			displayOutput.DrawText(
				x: 50,
				y: ChessCompStompWithHacks.WINDOW_HEIGHT - 50,
				text: "Press 1/2/3/4/5 to switch music tracks." + "\n" + "Press 6 to stop music.",
				font: ChessFont.ChessFont16Pt,
				color: DTColor.Black());
		}

		public void RenderMusic(IMusicOutput<ChessMusic> musicOutput)
		{
			this.globalState.RenderMusic(musicOutput: musicOutput);
		}
	}
}
