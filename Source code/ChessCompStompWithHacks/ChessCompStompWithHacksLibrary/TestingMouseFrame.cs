
namespace ChessCompStompWithHacksLibrary
{
	using DTLibrary;
	using System;

	public class TestingMouseFrame : IFrame<ChessImage, ChessFont, ChessSound, ChessMusic>
	{
		private GlobalState globalState;
		private SessionState sessionState;

		private int x;
		private int y;
		private int color;
		private bool shouldFill;

		public TestingMouseFrame(GlobalState globalState, SessionState sessionState)
		{
			this.globalState = globalState;
			this.sessionState = sessionState;

			this.x = 0;
			this.y = 0;
			this.color = 0;
			this.shouldFill = true;
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

			this.x = mouseInput.GetX();
			this.y = mouseInput.GetY();

			if (mouseInput.IsLeftMouseButtonPressed() && !previousMouseInput.IsLeftMouseButtonPressed())
			{
				this.color++;
				if (this.color == 4)
					this.color = 0;
			}

			if (mouseInput.IsRightMouseButtonPressed() && !previousMouseInput.IsRightMouseButtonPressed())
				this.shouldFill = !this.shouldFill;

			return this;
		}
		
		public void ProcessMusic()
		{
			this.globalState.ProcessMusic();
		}

		public void Render(IDisplayOutput<ChessImage, ChessFont> displayOutput)
		{
			DTColor dtColor;

			switch (this.color)
			{
				case 0:
					dtColor = DTColor.Black();
					break;
				case 1:
					dtColor = new DTColor(255, 0, 0);
					break;
				case 2:
					dtColor = new DTColor(0, 255, 0);
					break;
				case 3:
					dtColor = new DTColor(0, 0, 255);
					break;
				default:
					throw new Exception();
			}

			displayOutput.DrawRectangle(
				x: this.x - 5,
				y: this.y - 5,
				width: 11,
				height: 11,
				color: dtColor,
				fill: this.shouldFill);
		}

		public void RenderMusic(IMusicOutput<ChessMusic> musicOutput)
		{
			this.globalState.RenderMusic(musicOutput: musicOutput);
		}
	}
}
