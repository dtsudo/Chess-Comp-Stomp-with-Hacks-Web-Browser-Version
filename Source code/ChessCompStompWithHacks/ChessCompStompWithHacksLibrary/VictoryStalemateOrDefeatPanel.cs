
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System;

	public class VictoryStalemateOrDefeatPanel
	{
		private int x;
		private int y;
		private ComputeMoves.GameStatus gameStatus;
		private bool isPlayerWhite;

		private int? mouseDragXStart;
		private int? mouseDragYStart;

		private Button continueButton;

		private const int WIDTH = 300;
		private const int HEIGHT = 200;

		public VictoryStalemateOrDefeatPanel(ComputeMoves.GameStatus gameStatus, bool isPlayerWhite)
		{
			this.x = ChessCompStompWithHacks.WINDOW_WIDTH / 2 - WIDTH / 2;
			this.y = ChessCompStompWithHacks.WINDOW_HEIGHT / 2 - HEIGHT / 2;
			this.gameStatus = gameStatus;
			this.isPlayerWhite = isPlayerWhite;
			
			this.mouseDragXStart = null;
			this.mouseDragYStart = null;

			this.continueButton = new Button(
				x: (WIDTH - 150) / 2,
				y: 55,
				width: 150,
				height: 40,
				backgroundColor: new DTColor(200, 200, 200),
				hoverColor: new DTColor(250, 249, 200),
				clickColor: new DTColor(252, 251, 154),
				text: "Continue",
				textXOffset: 14,
				textYOffset: 8,
				font: ChessFont.Fetamont20Pt);
		}

		private bool IsPlayerVictory()
		{
			return this.gameStatus == ComputeMoves.GameStatus.WhiteVictory && this.isPlayerWhite
				|| this.gameStatus == ComputeMoves.GameStatus.BlackVictory && !this.isPlayerWhite;
		}
		
		public class Result
		{
			public Result(
				bool hasClickedContinueButton,
				bool isHoverOverPanel)
			{
				this.HasClickedContinueButton = hasClickedContinueButton;
				this.IsHoverOverPanel = isHoverOverPanel;
			}

			public bool HasClickedContinueButton { get; private set; }
			public bool IsHoverOverPanel { get; private set; }
		}

		public Result ProcessFrame(IMouse mouseInput, IMouse previousMouseInput)
		{
			int mouseX = mouseInput.GetX();
			int mouseY = mouseInput.GetY();

			IMouse translatedMouse = new TranslatedMouse(mouse: mouseInput, xOffset: -this.x, yOffset: -this.y);

			bool isHoverOverPanel = this.x <= mouseX && mouseX <= this.x + WIDTH && this.y <= mouseY && mouseY <= this.y + HEIGHT;
			
			if (mouseInput.IsLeftMouseButtonPressed() && !previousMouseInput.IsLeftMouseButtonPressed() && isHoverOverPanel && !this.continueButton.IsHover(translatedMouse))
			{
				this.mouseDragXStart = mouseX;
				this.mouseDragYStart = mouseY;
			}

			if (this.mouseDragXStart != null && mouseInput.IsLeftMouseButtonPressed())
			{
				this.x = this.x + (mouseX - this.mouseDragXStart.Value);
				this.y = this.y + (mouseY - this.mouseDragYStart.Value);

				this.mouseDragXStart = mouseX;
				this.mouseDragYStart = mouseY;
			}

			if (!mouseInput.IsLeftMouseButtonPressed() && previousMouseInput.IsLeftMouseButtonPressed())
			{
				this.mouseDragXStart = null;
				this.mouseDragYStart = null;

				if (this.x < 0)
					this.x = 0;

				if (this.y < 0)
					this.y = 0;

				if (this.x > ChessCompStompWithHacks.WINDOW_WIDTH - WIDTH)
					this.x = ChessCompStompWithHacks.WINDOW_WIDTH - WIDTH;

				if (this.y > ChessCompStompWithHacks.WINDOW_HEIGHT - HEIGHT)
					this.y = ChessCompStompWithHacks.WINDOW_HEIGHT - HEIGHT;
			}

			bool isClicked = this.continueButton.ProcessFrame(
				mouseInput: translatedMouse,
				previousMouseInput: new TranslatedMouse(mouse: previousMouseInput, xOffset: -this.x, yOffset: -this.y));

			return new Result(
				hasClickedContinueButton: isClicked,
				isHoverOverPanel: isHoverOverPanel || this.mouseDragXStart != null);
		}

		public void Render(IDisplayOutput<ChessImage, ChessFont> displayOutput)
		{
			displayOutput.DrawRectangle(
				x: this.x,
				y: this.y,
				width: WIDTH - 1,
				height: HEIGHT - 1,
				color: DTColor.White(),
				fill: true);

			displayOutput.DrawRectangle(
				x: this.x,
				y: this.y,
				width: WIDTH,
				height: HEIGHT,
				color: DTColor.Black(),
				fill: false);

			string text;
			int textXOffset;
			if (this.IsPlayerVictory())
			{
				text = "Victory!";
				textXOffset = 64;
			}
			else if (this.gameStatus == ComputeMoves.GameStatus.Stalemate)
			{
				text = "Stalemate!";
				textXOffset = 38;
			}
			else if (this.gameStatus == ComputeMoves.GameStatus.WhiteVictory || this.gameStatus == ComputeMoves.GameStatus.BlackVictory)
			{
				text = "Defeat!";
				textXOffset = 70;
			}
			else
				throw new Exception();

			displayOutput.DrawText(
				x: this.x + textXOffset,
				y: this.y + 170,
				text: text,
				font: ChessFont.Fetamont32Pt,
				color: DTColor.Black());

			this.continueButton.Render(displayOutput: new TranslatedDisplayOutput<ChessImage, ChessFont>(display: displayOutput, xOffsetInPixels: this.x, yOffsetInPixels: this.y));
		}
	}
}
