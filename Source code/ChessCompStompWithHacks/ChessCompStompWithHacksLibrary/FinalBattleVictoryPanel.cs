
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System;

	public class FinalBattleVictoryPanel
	{
		private int x;
		private int y;

		private int? mouseDragXStart;
		private int? mouseDragYStart;

		private Button continueButton;

		private IMouse previousMouseInput;

		private const int WIDTH = 850;
		private const int HEIGHT = 300;

		public FinalBattleVictoryPanel(ColorTheme colorTheme)
		{
			this.x = GlobalConstants.WINDOW_WIDTH / 2 - WIDTH / 2;
			this.y = GlobalConstants.WINDOW_HEIGHT / 2 - HEIGHT / 2;
			
			this.mouseDragXStart = null;
			this.mouseDragYStart = null;

			this.previousMouseInput = null;

			this.continueButton = new Button(
				x: (WIDTH - 150) / 2,
				y: 37,
				width: 150,
				height: 40,
				backgroundColor: new DTColor(200, 200, 200),
				hoverColor: ColorThemeUtil.GetHoverColor(colorTheme: colorTheme),
				clickColor: ColorThemeUtil.GetClickColor(colorTheme: colorTheme),
				text: "OK",
				textXOffset: 57,
				textYOffset: 8,
				font: GameFont.GameFont20Pt);
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
			if (this.previousMouseInput != null)
				previousMouseInput = this.previousMouseInput;

			this.previousMouseInput = new CopiedMouse(mouse: mouseInput);

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

				if (this.x > GlobalConstants.WINDOW_WIDTH - WIDTH)
					this.x = GlobalConstants.WINDOW_WIDTH - WIDTH;

				if (this.y > GlobalConstants.WINDOW_HEIGHT - HEIGHT)
					this.y = GlobalConstants.WINDOW_HEIGHT - HEIGHT;
			}

			bool isClicked = this.continueButton.ProcessFrame(
				mouseInput: translatedMouse,
				previousMouseInput: new TranslatedMouse(mouse: previousMouseInput, xOffset: -this.x, yOffset: -this.y));

			return new Result(
				hasClickedContinueButton: isClicked,
				isHoverOverPanel: isHoverOverPanel || this.mouseDragXStart != null);
		}

		public void Render(IDisplayOutput<GameImage, GameFont> displayOutput)
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
			
			displayOutput.DrawText(
				x: this.x + 335,
				y: this.y + 270,
				text: "You Win!",
				font: GameFont.GameFont32Pt,
				color: DTColor.Black());

			displayOutput.DrawText(
				x: this.x + 47,
				y: this.y + 183,
				text: "You've defeated the AI in the Final Battle." + "\n" + "You are an Elite Hacker and an Elite Chess Grandmaster!",
				font: GameFont.GameFont20Pt,
				color: DTColor.Black());

			this.continueButton.Render(displayOutput: new TranslatedDisplayOutput<GameImage, GameFont>(display: displayOutput, xOffsetInPixels: this.x, yOffsetInPixels: this.y));
		}
	}
}
