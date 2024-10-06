
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
		private int previousScreenWidth;
		private int previousScreenHeight;

		private int width;
		private int height;

		public FinalBattleVictoryPanel(
			ColorTheme colorTheme,
			IDisplayProcessing<GameImage> display,
			bool isMobileDisplayType)
		{
			int screenWidth = isMobileDisplayType
				? display.GetMobileScreenWidth()
				: GlobalConstants.DESKTOP_WINDOW_WIDTH;

			int screenHeight = isMobileDisplayType
				? display.GetMobileScreenHeight()
				: GlobalConstants.DESKTOP_WINDOW_HEIGHT;

			this.mouseDragXStart = null;
			this.mouseDragYStart = null;

			this.previousMouseInput = null;
			this.previousScreenWidth = screenWidth;
			this.previousScreenHeight = screenHeight;

			this.continueButton = new Button(
				x: 0,
				y: 37,
				width: 150,
				height: 40,
				backgroundColor: new DTColor(200, 200, 200),
				hoverColor: ColorThemeUtil.GetHoverColor(colorTheme: colorTheme),
				clickColor: ColorThemeUtil.GetClickColor(colorTheme: colorTheme),
				text: "OK",
				textXOffset: 57,
				textYOffset: 8,
				font: GameFont.GameFont20Pt,
				isMobileDisplayType: isMobileDisplayType);

			this.UpdateCoordinates(display: display, isMobileDisplayType: isMobileDisplayType);

			// Needs to happen after UpdateCoordinates since UpdateCoordinates will set this.width and this.height
			this.x = screenWidth / 2 - this.width / 2;
			this.y = screenHeight / 2 - this.height / 2;
		}

		private void UpdateCoordinates(
			IDisplayProcessing<GameImage> display,
			bool isMobileDisplayType)
		{
			if (isMobileDisplayType && !display.IsMobileInLandscapeOrientation())
			{
				this.width = 690;
				this.height = 300;
				
				this.continueButton.SetX((this.width - 150) / 2);
			}
			else
			{
				this.width = 850;
				this.height = 300;

				this.continueButton.SetX((this.width - 150) / 2);
			}
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

		public Result ProcessFrame(
			IMouse mouseInput, 
			IMouse previousMouseInput,
			IDisplayProcessing<GameImage> display,
			bool isMobileDisplayType)
		{
			this.UpdateCoordinates(display: display, isMobileDisplayType: isMobileDisplayType);

			if (this.previousMouseInput != null)
				previousMouseInput = this.previousMouseInput;

			this.previousMouseInput = new CopiedMouse(mouse: mouseInput);

			this.continueButton.SetIsMobileDisplayType(isMobileDisplayType: isMobileDisplayType);

			int screenWidth = isMobileDisplayType
				? display.GetMobileScreenWidth()
				: GlobalConstants.DESKTOP_WINDOW_WIDTH;

			int screenHeight = isMobileDisplayType
				? display.GetMobileScreenHeight()
				: GlobalConstants.DESKTOP_WINDOW_HEIGHT;

			int mouseX = mouseInput.GetX();
			int mouseY = mouseInput.GetY();

			IMouse translatedMouse = new TranslatedMouse(mouse: mouseInput, xOffset: -this.x, yOffset: -this.y);

			bool isHoverOverPanel = this.x <= mouseX && mouseX <= this.x + this.width && this.y <= mouseY && mouseY <= this.y + this.height;
			
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

				if (this.x > screenWidth - this.width)
					this.x = screenWidth - this.width;

				if (this.y > screenHeight - this.height)
					this.y = screenHeight - this.height;
			}

			if (screenWidth != this.previousScreenWidth || screenHeight != this.previousScreenHeight)
			{
				int panelCenterX = this.x + this.width / 2;
				int panelCenterY = this.y + this.height / 2;

				int newPanelCenterX = panelCenterX * screenWidth / this.previousScreenWidth;
				int newPanelCenterY = panelCenterY * screenHeight / this.previousScreenHeight;

				this.x = newPanelCenterX - this.width / 2;
				this.y = newPanelCenterY - this.height / 2;

				this.previousScreenWidth = screenWidth;
				this.previousScreenHeight = screenHeight;

				if (this.x < 0)
					this.x = 0;

				if (this.y < 0)
					this.y = 0;

				if (this.x > screenWidth - this.width)
					this.x = screenWidth - this.width;

				if (this.y > screenHeight - this.height)
					this.y = screenHeight - this.height;
			}

			bool isClicked = this.continueButton.ProcessFrame(
				mouseInput: translatedMouse,
				previousMouseInput: new TranslatedMouse(mouse: previousMouseInput, xOffset: -this.x, yOffset: -this.y));

			return new Result(
				hasClickedContinueButton: isClicked,
				isHoverOverPanel: isHoverOverPanel || this.mouseDragXStart != null);
		}

		public void Render(IDisplayOutput<GameImage, GameFont> displayOutput, bool isMobileDisplayType)
		{
			displayOutput.DrawRectangle(
				x: this.x,
				y: this.y,
				width: this.width,
				height: this.height,
				color: DTColor.White(),
				fill: true);

			displayOutput.DrawRectangle(
				x: this.x,
				y: this.y,
				width: this.width,
				height: this.height,
				color: DTColor.Black(),
				fill: false);
			
			displayOutput.DrawText(
				x: this.x + this.width / 2 - 90,
				y: this.y + 270,
				text: "You Win!",
				font: GameFont.GameFont32Pt,
				color: DTColor.Black());

			if (isMobileDisplayType && !displayOutput.IsMobileInLandscapeOrientation())
			{
				displayOutput.DrawText(
					x: this.x + 47,
					y: this.y + 193,
					text: "You've defeated the AI in the Final Battle." + "\n" + "You are an Elite Hacker and an Elite Chess" + "\n" + "Grandmaster!",
					font: GameFont.GameFont20Pt,
					color: DTColor.Black());
			}
			else
			{
				displayOutput.DrawText(
					x: this.x + 47,
					y: this.y + 183,
					text: "You've defeated the AI in the Final Battle." + "\n" + "You are an Elite Hacker and an Elite Chess Grandmaster!",
					font: GameFont.GameFont20Pt,
					color: DTColor.Black());
			}

			this.continueButton.Render(displayOutput: new TranslatedDisplayOutput<GameImage, GameFont>(display: displayOutput, xOffsetInPixels: this.x, yOffsetInPixels: this.y));
		}
	}
}
