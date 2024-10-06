﻿
namespace ChessCompStompWithHacksLibrary
{
	using DTLibrary;
	using System;
	using System.Collections.Generic;

	public class SettingsMenuDesktopFrame : IFrame<GameImage, GameFont, GameSound, GameMusic>
	{
		private GlobalState globalState;
		private SessionState sessionState;
		private SoundAndMusicVolumePicker soundAndMusicVolumePicker;
		private IFrame<GameImage, GameFont, GameSound, GameMusic> underlyingFrame;

		private Button continueButton;
		private Button backToTitleScreenButton;
		private bool showPausedText;

		private int panelY;
		private int panelHeight;

		private bool renderUnderlyingFrame;

		private const int PANEL_WIDTH = 300;
		private const int PANEL_HEIGHT_WITH_PAUSE = 380;
		private const int PANEL_HEIGHT_WITHOUT_PAUSE = 263;

		private const int PANEL_X = (GlobalConstants.DESKTOP_WINDOW_WIDTH - PANEL_WIDTH) / 2;
		private const int PANEL_Y_WITH_PAUSE = (GlobalConstants.DESKTOP_WINDOW_HEIGHT - PANEL_HEIGHT_WITH_PAUSE) / 2;
		private const int PANEL_Y_WITHOUT_PAUSE = (GlobalConstants.DESKTOP_WINDOW_HEIGHT - PANEL_HEIGHT_WITHOUT_PAUSE) / 2;

		private const int BUTTON_WIDTH = 240;
		private const int BUTTON_HEIGHT = 40;

		public SettingsMenuDesktopFrame(
			GlobalState globalState,
			SessionState sessionState,
			IFrame<GameImage, GameFont, GameSound, GameMusic> underlyingFrame,
			bool showPausedText,
			bool shouldRenderUnderlyingFrame)
		{
			this.globalState = globalState;
			this.sessionState = sessionState;
			this.soundAndMusicVolumePicker = null;
			this.underlyingFrame = underlyingFrame;

			this.renderUnderlyingFrame = shouldRenderUnderlyingFrame;

			this.showPausedText = showPausedText;
			
			this.panelY = showPausedText ? PANEL_Y_WITH_PAUSE : PANEL_Y_WITHOUT_PAUSE;
			this.panelHeight = showPausedText ? PANEL_HEIGHT_WITH_PAUSE : PANEL_HEIGHT_WITHOUT_PAUSE;

			this.continueButton = new Button(
				x: PANEL_X + (PANEL_WIDTH - BUTTON_WIDTH) / 2,
				y: this.panelY + 80,
				width: BUTTON_WIDTH,
				height: BUTTON_HEIGHT,
				backgroundColor: new DTColor(200, 200, 200),
				hoverColor: ColorThemeUtil.GetHoverColor(colorTheme: sessionState.GetColorTheme()),
				clickColor: ColorThemeUtil.GetClickColor(colorTheme: sessionState.GetColorTheme()),
				text: "Continue",
				textXOffset: 76,
				textYOffset: 12,
				font: GameFont.GameFont14Pt,
				isMobileDisplayType: false);

			this.backToTitleScreenButton = new Button(
				x: PANEL_X + (PANEL_WIDTH - BUTTON_WIDTH) / 2,
				y: this.panelY + 20,
				width: BUTTON_WIDTH,
				height: BUTTON_HEIGHT,
				backgroundColor: new DTColor(200, 200, 200),
				hoverColor: ColorThemeUtil.GetHoverColor(colorTheme: sessionState.GetColorTheme()),
				clickColor: ColorThemeUtil.GetClickColor(colorTheme: sessionState.GetColorTheme()),
				text: "Return to title screen",
				textXOffset: 11,
				textYOffset: 12,
				font: GameFont.GameFont14Pt,
				isMobileDisplayType: false);
		}

		public string GetClickUrl()
		{
			return null;
		}

		public HashSet<string> GetCompletedAchievements()
		{
			return new HashSet<string>();
		}

		public void ProcessExtraTime(int milliseconds)
		{
			this.underlyingFrame.ProcessExtraTime(milliseconds: milliseconds);
		}

		public IFrame<GameImage, GameFont, GameSound, GameMusic> ProcessDisplayType(DisplayType displayType, IDisplayProcessing<GameImage> displayProcessing)
		{
			if (displayType != DisplayType.Desktop)
			{
				this.underlyingFrame = this.underlyingFrame.ProcessDisplayType(displayType: displayType, displayProcessing: displayProcessing);

				return new SettingsMenuMobileFrame(
					globalState: this.globalState,
					sessionState: this.sessionState,
					underlyingFrame: this.underlyingFrame,
					showPausedText: this.showPausedText,
					display: displayProcessing,
					shouldRenderUnderlyingFrameInitially: false);
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
			if (this.soundAndMusicVolumePicker == null)
				this.soundAndMusicVolumePicker = new SoundAndMusicVolumePicker(
					xPos: PANEL_X + (PANEL_WIDTH - BUTTON_WIDTH) / 2,
					yPos: this.panelY + (this.showPausedText ? 170 : 140),
					initialSoundVolume: soundOutput.GetSoundVolume(),
					initialMusicVolume: this.globalState.MusicVolume,
					elapsedMicrosPerFrame: this.globalState.ElapsedMicrosPerFrame,
					scalingFactor: 1);
			
			this.soundAndMusicVolumePicker.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);
			soundOutput.SetSoundVolume(this.soundAndMusicVolumePicker.GetCurrentSoundVolume());
			this.globalState.MusicVolume = this.soundAndMusicVolumePicker.GetCurrentMusicVolume();

			if (keyboardInput.IsPressed(Key.Esc) && !previousKeyboardInput.IsPressed(Key.Esc))
			{
				this.globalState.SaveData(sessionState: this.sessionState, soundVolume: soundOutput.GetSoundVolume());
				soundOutput.PlaySound(GameSound.Click);
				return this.underlyingFrame;
			}

			bool clickedContinueButton = this.continueButton.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);

			if (clickedContinueButton)
			{
				this.globalState.SaveData(sessionState: this.sessionState, soundVolume: soundOutput.GetSoundVolume());
				soundOutput.PlaySound(GameSound.Click);
				return this.underlyingFrame;
			}

			bool clickedBackToTitleScreenButton = this.backToTitleScreenButton.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);

			if (clickedBackToTitleScreenButton)
			{
				this.globalState.SaveData(sessionState: this.sessionState, soundVolume: soundOutput.GetSoundVolume());
				soundOutput.PlaySound(GameSound.Click);
				return new TitleScreenDesktopFrame(globalState: this.globalState, sessionState: this.sessionState);
			}

			if (mouseInput.IsLeftMouseButtonPressed() && !previousMouseInput.IsLeftMouseButtonPressed())
			{
				int mouseX = mouseInput.GetX();
				int mouseY = mouseInput.GetY();

				if (mouseX < PANEL_X || mouseX > PANEL_X + PANEL_WIDTH || mouseY < this.panelY || mouseY > this.panelY + this.panelHeight)
				{
					this.globalState.SaveData(sessionState: this.sessionState, soundVolume: soundOutput.GetSoundVolume());
					soundOutput.PlaySound(GameSound.Click);
					return this.underlyingFrame;
				}
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

			if (this.renderUnderlyingFrame)
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
				y: this.panelY,
				width: PANEL_WIDTH - 1,
				height: this.panelHeight - 1,
				color: DTColor.White(),
				fill: true);

			displayOutput.DrawRectangle(
				x: PANEL_X,
				y: this.panelY,
				width: PANEL_WIDTH,
				height: this.panelHeight,
				color: DTColor.Black(),
				fill: false);

			if (this.soundAndMusicVolumePicker != null)
				this.soundAndMusicVolumePicker.Render(displayOutput: displayOutput);
			
			if (this.showPausedText)
				displayOutput.DrawText(
					x: PANEL_X + 72,
					y: this.panelY + 362,
					text: "Paused",
					font: GameFont.GameFont32Pt,
					color: DTColor.Black());

			this.continueButton.Render(displayOutput: displayOutput);
			this.backToTitleScreenButton.Render(displayOutput: displayOutput);
		}

		public void RenderMusic(IMusicOutput<GameMusic> musicOutput)
		{
			this.underlyingFrame.RenderMusic(musicOutput: musicOutput);
		}
	}
}
