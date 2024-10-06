﻿
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System;
	using System.Collections.Generic;

	public class ClearDataConfirmationDesktopFrame : IFrame<GameImage, GameFont, GameSound, GameMusic>
	{
		private GlobalState globalState;
		private SessionState sessionState;
		private IFrame<GameImage, GameFont, GameSound, GameMusic> underlyingFrame;

		private Button confirmButton;
		private Button cancelButton;

		private const int PANEL_WIDTH = 480;
		private const int PANEL_HEIGHT = 150;

		private const int PANEL_X = (GlobalConstants.DESKTOP_WINDOW_WIDTH - PANEL_WIDTH) / 2;
		private const int PANEL_Y = (GlobalConstants.DESKTOP_WINDOW_HEIGHT - PANEL_HEIGHT) / 2;
		
		public ClearDataConfirmationDesktopFrame(GlobalState globalState, SessionState sessionState, IFrame<GameImage, GameFont, GameSound, GameMusic> underlyingFrame)
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
				hoverColor: ColorThemeUtil.GetHoverColor(colorTheme: sessionState.GetColorTheme()),
				clickColor: ColorThemeUtil.GetClickColor(colorTheme: sessionState.GetColorTheme()),
				text: "Yes",
				textXOffset: 47,
				textYOffset: 8,
				font: GameFont.GameFont20Pt,
				isMobileDisplayType: false);

			this.cancelButton = new Button(
				x: PANEL_X + 250,
				y: PANEL_Y + 20,
				width: buttonWidth,
				height: buttonHeight,
				backgroundColor: new DTColor(200, 200, 200),
				hoverColor: ColorThemeUtil.GetHoverColor(colorTheme: sessionState.GetColorTheme()),
				clickColor: ColorThemeUtil.GetClickColor(colorTheme: sessionState.GetColorTheme()),
				text: "No",
				textXOffset: 55,
				textYOffset: 8,
				font: GameFont.GameFont20Pt,
				isMobileDisplayType: false);
		}

		public void ProcessExtraTime(int milliseconds)
		{
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
			this.underlyingFrame = this.underlyingFrame.ProcessDisplayType(displayType: displayType, displayProcessing: displayProcessing);

			if (displayType != DisplayType.Desktop)
				return new ClearDataConfirmationMobileFrame(
					globalState: this.globalState,
					sessionState: this.sessionState,
					underlyingFrame: this.underlyingFrame,
					display: displayProcessing);

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
			this.underlyingFrame = this.underlyingFrame.GetNextFrame(
				keyboardInput: new EmptyKeyboard(),
				mouseInput: new EmptyMouse(),
				previousKeyboardInput: new EmptyKeyboard(),
				previousMouseInput: new EmptyMouse(),
				displayProcessing: displayProcessing,
				soundOutput: soundOutput,
				musicProcessing: musicProcessing);

			if (keyboardInput.IsPressed(Key.Esc) && !previousKeyboardInput.IsPressed(Key.Esc))
			{
				soundOutput.PlaySound(GameSound.Click);
				return new TitleScreenDesktopFrame(globalState: this.globalState, sessionState: this.sessionState);
			}

			bool isConfirmClicked = this.confirmButton.ProcessFrame(
				mouseInput: mouseInput,
				previousMouseInput: previousMouseInput);

			bool isCancelClicked = this.cancelButton.ProcessFrame(
				mouseInput: mouseInput,
				previousMouseInput: previousMouseInput);

			if (isConfirmClicked)
			{
				this.sessionState.ClearData();
				this.globalState.SaveData(sessionState: this.sessionState, soundVolume: soundOutput.GetSoundVolume());
				soundOutput.PlaySound(GameSound.Click);
				return new TitleScreenDesktopFrame(globalState: this.globalState, sessionState: this.sessionState);
			}

			if (isCancelClicked)
			{
				soundOutput.PlaySound(GameSound.Click);
				return new TitleScreenDesktopFrame(globalState: this.globalState, sessionState: this.sessionState);
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
				width: GlobalConstants.DESKTOP_WINDOW_WIDTH,
				height: GlobalConstants.DESKTOP_WINDOW_HEIGHT,
				color: new DTColor(223, 220, 217),
				fill: true);

			this.underlyingFrame.Render(display: displayOutput);

			displayOutput.DrawRectangle(
				x: 0,
				y: 0,
				width: GlobalConstants.DESKTOP_WINDOW_WIDTH,
				height: GlobalConstants.DESKTOP_WINDOW_HEIGHT,
				color: new DTColor(r: 0, g: 0, b: 0, alpha: 64),
				fill: true);

			displayOutput.DrawRectangle(
				x: PANEL_X,
				y: PANEL_Y,
				width: PANEL_WIDTH,
				height: PANEL_HEIGHT,
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
				font: GameFont.GameFont20Pt,
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
