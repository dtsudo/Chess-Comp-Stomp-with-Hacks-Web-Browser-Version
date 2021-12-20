
namespace ChessCompStompWithHacksLibrary
{
	using DTLibrary;

	public class TestingKeyboardFrame : IFrame<ChessImage, ChessFont, ChessSound, ChessMusic>
	{
		private GlobalState globalState;
		private SessionState sessionState;

		private int x;
		private int y;

		public TestingKeyboardFrame(GlobalState globalState, SessionState sessionState)
		{
			this.globalState = globalState;
			this.sessionState = sessionState;

			this.x = 50;
			this.y = 50;
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

			int delta = this.globalState.ElapsedMicrosPerFrame / 2000;

			if (keyboardInput.IsPressed(Key.LeftArrow))
				this.x -= delta;
			if (keyboardInput.IsPressed(Key.RightArrow))
				this.x += delta;
			if (keyboardInput.IsPressed(Key.DownArrow))
				this.y -= delta;
			if (keyboardInput.IsPressed(Key.UpArrow))
				this.y += delta;

			if (this.x < 0)
				this.x = 0;
			if (this.x > ChessCompStompWithHacks.WINDOW_WIDTH)
				this.x = ChessCompStompWithHacks.WINDOW_WIDTH;
			if (this.y < 0)
				this.y = 0;
			if (this.y > ChessCompStompWithHacks.WINDOW_HEIGHT)
				this.y = ChessCompStompWithHacks.WINDOW_HEIGHT;

			return this;
		}
		
		public void ProcessMusic()
		{
			this.globalState.ProcessMusic();
		}

		public void Render(IDisplayOutput<ChessImage, ChessFont> displayOutput)
		{
			displayOutput.DrawRectangle(
				x: this.x - 5,
				y: this.y - 5,
				width: 11,
				height: 11,
				color: DTColor.Black(),
				fill: true);
		}

		public void RenderMusic(IMusicOutput<ChessMusic> musicOutput)
		{
			this.globalState.RenderMusic(musicOutput: musicOutput);
		}
	}
}
