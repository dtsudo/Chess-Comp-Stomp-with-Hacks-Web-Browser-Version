
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System;
	using System.Collections.Generic;

	public class ResignConfirmationMobileFrame : IFrame<GameImage, GameFont, GameSound, GameMusic>
	{
		private GlobalState globalState;
		private SessionState sessionState;
		private IFrame<GameImage, GameFont, GameSound, GameMusic> underlyingFrame;

		private Button confirmButton;
		private Button cancelButton;

		private int panelX;
		private int panelY;
		private int panelWidth;
		private int panelHeight;

		private int widthOfUnderlyingFrame;
		private int heightOfUnderlyingFrame;
		private bool renderUnderlyingFrame;

		public ResignConfirmationMobileFrame(
			GlobalState globalState,
			SessionState sessionState,
			IFrame<GameImage, GameFont, GameSound, GameMusic> underlyingFrame,
			IDisplayProcessing<GameImage> display,
			bool shouldRenderUnderlyingFrameInitially)
		{
			this.widthOfUnderlyingFrame = display.GetMobileScreenWidth();
			this.heightOfUnderlyingFrame = display.GetMobileScreenHeight();
			this.renderUnderlyingFrame = shouldRenderUnderlyingFrameInitially;

			this.globalState = globalState;
			this.sessionState = sessionState;
			this.underlyingFrame = underlyingFrame;
			
			this.confirmButton = new Button(
				x: 0,
				y: 0,
				width: 10,
				height: 10,
				backgroundColor: new DTColor(200, 200, 200),
				hoverColor: ColorThemeUtil.GetHoverColor(colorTheme: sessionState.GetColorTheme()),
				clickColor: ColorThemeUtil.GetClickColor(colorTheme: sessionState.GetColorTheme()),
				text: "Yes",
				textXOffset: 0,
				textYOffset: 0,
				font: GameFont.GameFont32Pt,
				isMobileDisplayType: true);

			this.cancelButton = new Button(
				x: 0,
				y: 0,
				width: 10,
				height: 10,
				backgroundColor: new DTColor(200, 200, 200),
				hoverColor: ColorThemeUtil.GetHoverColor(colorTheme: sessionState.GetColorTheme()),
				clickColor: ColorThemeUtil.GetClickColor(colorTheme: sessionState.GetColorTheme()),
				text: "No",
				textXOffset: 0,
				textYOffset: 0,
				font: GameFont.GameFont32Pt,
				isMobileDisplayType: true);

			this.UpdateCoordinates(display: display);
		}

		private void UpdateCoordinates(IDisplayProcessing<GameImage> display)
		{
			bool isLandscape = display.IsMobileInLandscapeOrientation();

			this.panelWidth = isLandscape ? 800 : 600;
			this.panelHeight = isLandscape ? 500 : 400;
			this.panelX = (display.GetMobileScreenWidth() - this.panelWidth) / 2;
			this.panelY = (display.GetMobileScreenHeight() - this.panelHeight) / 2;

			this.confirmButton.SetX(this.panelX + 50);
			this.confirmButton.SetY(this.panelY + 50);

			int buttonWidth = (this.panelWidth - 50 - 50 - 50) / 2;
			int buttonHeight = 100;

			this.confirmButton.SetWidth(buttonWidth);
			this.confirmButton.SetHeight(buttonHeight);

			this.confirmButton.SetTextXOffset(buttonWidth / 2 - 38);
			this.confirmButton.SetTextYOffset(30);

			this.cancelButton.SetX(this.panelX + 50 + buttonWidth + 50);
			this.cancelButton.SetY(this.panelY + 50);

			this.cancelButton.SetWidth(buttonWidth);
			this.cancelButton.SetHeight(buttonHeight);

			this.cancelButton.SetTextXOffset(buttonWidth / 2 - 26);
			this.cancelButton.SetTextYOffset(30);
		}

		public void ProcessExtraTime(int milliseconds)
		{
			GameLogic gameLogic = this.sessionState.GetGameLogic();
			if (gameLogic != null)
				gameLogic.ProcessExtraTime(milliseconds: milliseconds);
		}

		public string GetClickUrl()
		{
			return null;
		}

		public HashSet<string> GetCompletedAchievements()
		{
			return new HashSet<string>();
		}

		public IFrame<GameImage, GameFont, GameSound, GameMusic> ProcessDisplayType(DisplayType displayType, IDisplayProcessing<GameImage> displayProcessing)
		{
			if (displayType == DisplayType.Desktop)
			{
				this.underlyingFrame = this.underlyingFrame.ProcessDisplayType(displayType: displayType, displayProcessing: displayProcessing);

				return new ResignConfirmationDesktopFrame(globalState: this.globalState, sessionState: this.sessionState, underlyingFrame: this.underlyingFrame, shouldRenderUnderlyingFrame: false);
			}

			return this;
		}

		public IFrame<GameImage, GameFont, GameSound, GameMusic> GetNextFrame(
			IKeyboard keyboardInput,
			IMouse mouseInput,
			IKeyboard previousKeyboardInput,
			IMouse previousMouseInput,
			IDisplayProcessing<GameImage> displayProcessing,
			ISoundOutput<GameSound> soundOutput,
			IMusicProcessing musicProcessing)
		{
			this.UpdateCoordinates(display: displayProcessing);

			if (displayProcessing.GetMobileScreenWidth() != this.widthOfUnderlyingFrame)
				this.renderUnderlyingFrame = false;
			if (displayProcessing.GetMobileScreenHeight() != this.heightOfUnderlyingFrame)
				this.renderUnderlyingFrame = false;
			
			if (keyboardInput.IsPressed(Key.Esc) && !previousKeyboardInput.IsPressed(Key.Esc))
			{
				soundOutput.PlaySound(GameSound.Click);
				return this.underlyingFrame;
			}

			bool isConfirmClicked = this.confirmButton.ProcessFrame(
				mouseInput: mouseInput,
				previousMouseInput: previousMouseInput);

			bool isCancelClicked = this.cancelButton.ProcessFrame(
				mouseInput: mouseInput,
				previousMouseInput: previousMouseInput);

			if (isConfirmClicked)
			{
				this.sessionState.CompleteGame(didPlayerWin: false);
				this.globalState.SaveData(sessionState: this.sessionState, soundVolume: soundOutput.GetSoundVolume());
				soundOutput.PlaySound(GameSound.Click);
				return new HackSelectionScreenMobileFrame(globalState: this.globalState, sessionState: this.sessionState, display: displayProcessing);
			}

			if (isCancelClicked)
			{
				soundOutput.PlaySound(GameSound.Click);
				return this.underlyingFrame;
			}

			return this;
		}

		public void ProcessMusic()
		{
			this.underlyingFrame.ProcessMusic();
		}

		public void Render(IDisplayOutput<GameImage, GameFont> displayOutput)
		{
			displayOutput.DrawRectangle(
				x: 0,
				y: 0,
				width: displayOutput.GetMobileScreenWidth(),
				height: displayOutput.GetMobileScreenHeight(),
				color: new DTColor(223, 220, 217),
				fill: true);

			if (this.renderUnderlyingFrame)
				this.underlyingFrame.Render(display: displayOutput);

			displayOutput.DrawRectangle(
				x: 0,
				y: 0,
				width: displayOutput.GetMobileScreenWidth(),
				height: displayOutput.GetMobileScreenHeight(),
				color: new DTColor(r: 0, g: 0, b: 0, alpha: 64),
				fill: true);

			displayOutput.DrawRectangle(
				x: this.panelX,
				y: this.panelY,
				width: this.panelWidth,
				height: this.panelHeight,
				color: DTColor.White(),
				fill: true);

			displayOutput.DrawRectangle(
				x: this.panelX,
				y: this.panelY,
				width: this.panelWidth,
				height: this.panelHeight,
				color: DTColor.Black(),
				fill: false);

			displayOutput.DrawText(
				x: this.panelX + 27,
				y: this.panelY + this.panelHeight - 27,
				text: displayOutput.IsMobileInLandscapeOrientation() ? "Are you sure you want to resign?" : "Are you sure you want to \nresign?",
				font: GameFont.GameFont32Pt,
				color: DTColor.Black());

			this.confirmButton.Render(displayOutput: displayOutput);
			this.cancelButton.Render(displayOutput: displayOutput);
		}

		public void RenderMusic(IMusicOutput<GameMusic> musicOutput)
		{
			this.underlyingFrame.RenderMusic(musicOutput: musicOutput);
		}
	}
}
