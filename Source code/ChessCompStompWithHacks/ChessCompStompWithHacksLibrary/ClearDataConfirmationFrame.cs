
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System;
	
	public class ClearDataConfirmationFrame : IFrame<ChessImage, ChessFont, ChessSound, ChessMusic>
	{
		private GlobalState globalState;
		private SessionState sessionState;
		private IFrame<ChessImage, ChessFont, ChessSound, ChessMusic> underlyingFrame;

		private Button confirmButton;
		private Button cancelButton;

		private const int PANEL_WIDTH = 480;
		private const int PANEL_HEIGHT = 150;

		private const int PANEL_X = (ChessCompStompWithHacks.WINDOW_WIDTH - PANEL_WIDTH) / 2;
		private const int PANEL_Y = (ChessCompStompWithHacks.WINDOW_HEIGHT - PANEL_HEIGHT) / 2;
		
		public ClearDataConfirmationFrame(GlobalState globalState, SessionState sessionState, IFrame<ChessImage, ChessFont, ChessSound, ChessMusic> underlyingFrame)
		{
			this.globalState = globalState;
			this.sessionState = sessionState;
			this.underlyingFrame = underlyingFrame;

			int buttonWidth = 150;
			int buttonHeight = 40;

			this.confirmButton = new Button(
				x: PANEL_X + 80,
				y: PANEL_Y + 20,
				width: buttonWidth,
				height: buttonHeight,
				backgroundColor: new DTColor(200, 200, 200),
				hoverColor: new DTColor(250, 249, 200),
				clickColor: new DTColor(252, 251, 154),
				text: "Yes",
				textXOffset: 47,
				textYOffset: 8,
				font: ChessFont.Fetamont20Pt);

			this.cancelButton = new Button(
				x: PANEL_X + 250,
				y: PANEL_Y + 20,
				width: buttonWidth,
				height: buttonHeight,
				backgroundColor: new DTColor(200, 200, 200),
				hoverColor: new DTColor(250, 249, 200),
				clickColor: new DTColor(252, 251, 154),
				text: "No",
				textXOffset: 55,
				textYOffset: 8,
				font: ChessFont.Fetamont20Pt);
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
				return this.underlyingFrame;
			
			bool isConfirmClicked = this.confirmButton.ProcessFrame(
				mouseInput: mouseInput,
				previousMouseInput: previousMouseInput);

			bool isCancelClicked = this.cancelButton.ProcessFrame(
				mouseInput: mouseInput,
				previousMouseInput: previousMouseInput);

			if (isConfirmClicked)
			{
				this.sessionState.ClearData();
				return this.underlyingFrame;
			}

			if (isCancelClicked)
				return this.underlyingFrame;

			return this;
		}
		
		public void ProcessMusic()
		{
			this.underlyingFrame.ProcessMusic();
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
				x: PANEL_X,
				y: PANEL_Y,
				width: PANEL_WIDTH - 1,
				height: PANEL_HEIGHT - 1,
				color: DTColor.White(),
				fill: true);

			displayOutput.DrawRectangle(
				x: PANEL_X,
				y: PANEL_Y,
				width: PANEL_WIDTH,
				height: PANEL_HEIGHT,
				color: DTColor.Black(),
				fill: false);

			displayOutput.DrawText(
				x: PANEL_X + 27,
				y: PANEL_Y + 132,
				text: "Are you sure you want to reset" + "\n" + "your progress?",
				font: ChessFont.Fetamont20Pt,
				color: DTColor.Black());

			this.confirmButton.Render(displayOutput: displayOutput);
			this.cancelButton.Render(displayOutput: displayOutput);
		}

		public void RenderMusic(IMusicOutput<ChessMusic> musicOutput)
		{
			this.underlyingFrame.RenderMusic(musicOutput: musicOutput);
		}
	}
}
