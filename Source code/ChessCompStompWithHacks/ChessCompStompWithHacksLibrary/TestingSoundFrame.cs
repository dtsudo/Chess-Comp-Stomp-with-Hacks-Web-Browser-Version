
namespace ChessCompStompWithHacksLibrary
{
	using DTLibrary;

	public class TestingSoundFrame : IFrame<ChessImage, ChessFont, ChessSound, ChessMusic>
	{
		private GlobalState globalState;
		private SessionState sessionState;

		private SoundAndMusicVolumePicker volumePicker;

		private int cooldownInMicroseconds;

		public TestingSoundFrame(GlobalState globalState, SessionState sessionState)
		{
			this.globalState = globalState;
			this.sessionState = sessionState;

			this.volumePicker = null;

			this.cooldownInMicroseconds = 0;
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

			this.cooldownInMicroseconds -= this.globalState.ElapsedMicrosPerFrame;

			if (this.cooldownInMicroseconds <= 0)
			{
				soundOutput.PlaySound(ChessSound.AIMove);

				this.cooldownInMicroseconds += 1000 * 1000;

				if (this.cooldownInMicroseconds < 0)
					this.cooldownInMicroseconds = 0;
			}

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
		}

		public void RenderMusic(IMusicOutput<ChessMusic> musicOutput)
		{
			this.globalState.RenderMusic(musicOutput: musicOutput);
		}
	}
}
