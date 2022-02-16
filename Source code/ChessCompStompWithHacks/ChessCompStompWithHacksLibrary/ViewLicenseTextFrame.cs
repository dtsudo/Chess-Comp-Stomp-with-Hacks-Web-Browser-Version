
namespace ChessCompStompWithHacksLibrary
{
	using DTLibrary;

	public class ViewLicenseTextFrame : IFrame<ChessImage, ChessFont, ChessSound, ChessMusic>
	{
		private GlobalState globalState;
		private SessionState sessionState;

		private ScrollableTextDisplay scrollableTextDisplay;

		private Button backButton;

		private IFrame<ChessImage, ChessFont, ChessSound, ChessMusic> underlyingFrame;

		public ViewLicenseTextFrame(
			GlobalState globalState,
			SessionState sessionState,
			string text,
			IFrame<ChessImage, ChessFont, ChessSound, ChessMusic> underlyingFrame)
		{
			this.globalState = globalState;
			this.sessionState = sessionState;
			this.underlyingFrame = underlyingFrame;
			
			this.scrollableTextDisplay = new ScrollableTextDisplay(
				x: 23,
				y: 23 + 100,
				width: ChessCompStompWithHacks.WINDOW_WIDTH - 48,
				height: ChessCompStompWithHacks.WINDOW_HEIGHT - 46 - 100,
				lineHeightInPixels: 16,
				maxLinesOfTextToRender: 34,
				font: ChessFont.ChessFont12Pt,
				color: DTColor.Black(),
				text: text);

			this.backButton = new Button(
				x: 400,
				y: 50,
				width: 200,
				height: 60,
				backgroundColor: new DTColor(235, 235, 235),
				hoverColor: ColorThemeUtil.GetHoverColor(colorTheme: sessionState.GetColorTheme()),
				clickColor: ColorThemeUtil.GetClickColor(colorTheme: sessionState.GetColorTheme()),
				text: "Back",
				textXOffset: 67,
				textYOffset: 16,
				font: ChessFont.ChessFont20Pt);
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
			this.scrollableTextDisplay.ProcessFrame(
				keyboardInput: keyboardInput,
				mouseInput: mouseInput,
				previousKeyboardInput: previousKeyboardInput,
				previousMouseInput: previousMouseInput,
				displayProcessing: displayProcessing,
				soundOutput: soundOutput);

			bool clickedBackButton = this.backButton.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);

			if (clickedBackButton)
			{
				soundOutput.PlaySound(ChessSound.Click);
				return this.underlyingFrame;
			}

			if (keyboardInput.IsPressed(Key.Esc) && !previousKeyboardInput.IsPressed(Key.Esc))
			{
				soundOutput.PlaySound(ChessSound.Click);
				return this.underlyingFrame;
			}

			return this;
		}

		public void ProcessExtraTime(int milliseconds)
		{
			this.underlyingFrame.ProcessExtraTime(milliseconds: milliseconds);
		}

		public void ProcessMusic()
		{
			this.globalState.ProcessMusic();
		}

		public void Render(IDisplayOutput<ChessImage, ChessFont> displayOutput)
		{
			this.underlyingFrame.Render(display: displayOutput);

			displayOutput.DrawRectangle(
				x: 0,
				y: 0,
				width: ChessCompStompWithHacks.WINDOW_WIDTH,
				height: ChessCompStompWithHacks.WINDOW_HEIGHT,
				color: new DTColor(r: 0, g: 0, b: 0, alpha: 64),
				fill: true);

			displayOutput.DrawRectangle(
				x: 20,
				y: 20,
				width: ChessCompStompWithHacks.WINDOW_WIDTH - 41,
				height: ChessCompStompWithHacks.WINDOW_HEIGHT - 41,
				color: DTColor.White(),
				fill: true);

			displayOutput.DrawRectangle(
				x: 20,
				y: 20,
				width: ChessCompStompWithHacks.WINDOW_WIDTH - 40,
				height: ChessCompStompWithHacks.WINDOW_HEIGHT - 40,
				color: DTColor.Black(),
				fill: false);

			this.scrollableTextDisplay.Render(displayOutput: displayOutput);

			this.backButton.Render(displayOutput: displayOutput);
		}

		public void RenderMusic(IMusicOutput<ChessMusic> musicOutput)
		{
			this.globalState.RenderMusic(musicOutput: musicOutput);
		}
	}
}
