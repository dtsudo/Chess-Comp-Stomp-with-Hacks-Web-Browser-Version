
namespace ChessCompStompWithHacksLibrary
{
	using DTLibrary;

	public class TestingFontFrame : IFrame<ChessImage, ChessFont, ChessSound, ChessMusic>
	{
		private GlobalState globalState;
		private SessionState sessionState;

		public TestingFontFrame(GlobalState globalState, SessionState sessionState)
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
				return new TestingFontFrame2(globalState: this.globalState, sessionState: this.sessionState);

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
				x: 51,
				y: 590,
				width: 836,
				height: 60,
				color: red,
				fill: false);

			displayOutput.DrawText(
				x: 50,
				y: 650,
				text: "Line 1 ABCDEFGHIJKLMNOPQRSTUVWXYZ ABCDEFGHIJKLMNOPQRSTUVWXYZ ABCDEFGHIJKLMNOPQRSTUVWXYZ"
					+ "\n" + "Line 2"
					+ "\n" + "Line 3"
					+ "\n" + "Line 4",
				font: ChessFont.ChessFont12Pt,
				color: DTColor.Black());

			displayOutput.DrawRectangle(
				x: 51,
				y: 479,
				width: 632,
				height: 71,
				color: red,
				fill: false);

			displayOutput.DrawText(
				x: 50,
				y: 550,
				text: "Line 1 abcdefghijklmnopqrstuvwxyz abcdefghijklmnopqrstuvwxyz"
					+ "\n" + "Line 2"
					+ "\n" + "Line 3"
					+ "\n" + "Line 4",
				font: ChessFont.ChessFont14Pt,
				color: DTColor.Black());

			displayOutput.DrawRectangle(
				x: 51,
				y: 364,
				width: 714,
				height: 85,
				color: red,
				fill: false);

			displayOutput.DrawText(
				x: 50,
				y: 450,
				text: "Line 1 Chess Comp Stomp with Hacks Chess Comp Stomp with Hacks"
					+ "\n" + "Line 2"
					+ "\n" + "Line 3"
					+ "\n" + "Line 4",
				font: ChessFont.ChessFont16Pt,
				color: DTColor.Black());

			displayOutput.DrawRectangle(
				x: 51,
				y: 259,
				width: 790,
				height: 90,
				color: red,
				fill: false);

			displayOutput.DrawText(
				x: 50,
				y: 350,
				text: "Line 1 Chess Comp Stomp with Hacks Chess Comp Stomp with Hacks"
					+ "\n" + "Line 2"
					+ "\n" + "Line 3"
					+ "\n" + "Line 4",
				font: ChessFont.ChessFont18Pt,
				color: DTColor.Black());

			displayOutput.DrawRectangle(
				x: 51,
				y: 144,
				width: 874,
				height: 104,
				color: red,
				fill: false);

			displayOutput.DrawText(
				x: 50,
				y: 250,
				text: "Line 1 Chess Comp Stomp with Hacks Chess Comp Stomp with Hacks"
					+ "\n" + "Line 2"
					+ "\n" + "Line 3"
					+ "\n" + "Line 4",
				font: ChessFont.ChessFont20Pt,
				color: DTColor.Black());
		}

		public void RenderMusic(IMusicOutput<ChessMusic> musicOutput)
		{
			this.globalState.RenderMusic(musicOutput: musicOutput);
		}
	}
}
