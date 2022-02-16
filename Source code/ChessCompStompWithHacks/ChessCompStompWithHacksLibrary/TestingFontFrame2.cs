
namespace ChessCompStompWithHacksLibrary
{
	using DTLibrary;

	public class TestingFontFrame2 : IFrame<ChessImage, ChessFont, ChessSound, ChessMusic>
	{
		private GlobalState globalState;
		private SessionState sessionState;

		public TestingFontFrame2(GlobalState globalState, SessionState sessionState)
		{
			this.globalState = globalState;
			this.sessionState = sessionState;
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
			
			if (keyboardInput.IsPressed(Key.Enter) && !previousKeyboardInput.IsPressed(Key.Enter))
				return new TestingFontFrame(globalState: this.globalState, sessionState: this.sessionState);

			return this;
		}
		
		public void ProcessMusic()
		{
			this.globalState.ProcessMusic();
		}

		public void Render(IDisplayOutput<ChessImage, ChessFont> displayOutput)
		{
			DTColor red = new DTColor(255, 0, 0);

			displayOutput.DrawRectangle(
				x: 53,
				y: 483,
				width: 895,
				height: 162,
				color: red,
				fill: false);

			displayOutput.DrawText(
				x: 50,
				y: 650,
				text: "Line 1 Chess Comp Stomp with Hacks Chess"
					+ "\n" + "Line 2"
					+ "\n" + "Line 3"
					+ "\n" + "Line 4",
				font: ChessFont.ChessFont32Pt,
				color: DTColor.Black());
		}

		public void RenderMusic(IMusicOutput<ChessMusic> musicOutput)
		{
			this.globalState.RenderMusic(musicOutput: musicOutput);
		}
	}
}
