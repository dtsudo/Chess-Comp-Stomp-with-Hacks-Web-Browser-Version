
namespace ChessCompStompWithHacksLibrary
{
	using DTLibrary;
	using System;
	using System.Collections.Generic;

	public class SettingsMenuMobileFrame : IFrame<GameImage, GameFont, GameSound, GameMusic>
	{
		private GlobalState globalState;
		private SessionState sessionState;
		private SoundAndMusicVolumePicker soundAndMusicVolumePicker;
		private IFrame<GameImage, GameFont, GameSound, GameMusic> underlyingFrame;

		private Button continueButton;
		private Button backToTitleScreenButton;
		private bool showPausedText;

		private int panelX;
		private int panelY;
		private int panelWidth;
		private int panelHeight;
				
		private const int BUTTON_WIDTH = 400;
		private const int BUTTON_HEIGHT = 120;

		private int widthOfUnderlyingFrame;
		private int heightOfUnderlyingFrame;
		private bool renderUnderlyingFrame;

		public SettingsMenuMobileFrame(
			GlobalState globalState,
			SessionState sessionState,
			IFrame<GameImage, GameFont, GameSound, GameMusic> underlyingFrame,
			bool showPausedText,
			IDisplayProcessing<GameImage> display,
			bool shouldRenderUnderlyingFrameInitially)
		{
			this.widthOfUnderlyingFrame = display.GetMobileScreenWidth();
			this.heightOfUnderlyingFrame = display.GetMobileScreenHeight();
			this.renderUnderlyingFrame = shouldRenderUnderlyingFrameInitially;

			this.globalState = globalState;
			this.sessionState = sessionState;
			this.soundAndMusicVolumePicker = null;
			this.underlyingFrame = underlyingFrame;

			this.showPausedText = showPausedText;

			this.continueButton = new Button(
				x: 0,
				y: 0,
				width: BUTTON_WIDTH,
				height: BUTTON_HEIGHT,
				backgroundColor: new DTColor(200, 200, 200),
				hoverColor: ColorThemeUtil.GetHoverColor(colorTheme: sessionState.GetColorTheme()),
				clickColor: ColorThemeUtil.GetClickColor(colorTheme: sessionState.GetColorTheme()),
				text: "Continue",
				textXOffset: 140,
				textYOffset: 48,
				font: GameFont.GameFont20Pt,
				isMobileDisplayType: true);

			this.backToTitleScreenButton = new Button(
				x: 0,
				y: 0,
				width: BUTTON_WIDTH,
				height: BUTTON_HEIGHT,
				backgroundColor: new DTColor(200, 200, 200),
				hoverColor: ColorThemeUtil.GetHoverColor(colorTheme: sessionState.GetColorTheme()),
				clickColor: ColorThemeUtil.GetClickColor(colorTheme: sessionState.GetColorTheme()),
				text: "Return to title screen",
				textXOffset: 48,
				textYOffset: 48,
				font: GameFont.GameFont20Pt,
				isMobileDisplayType: true);

			this.UpdateCoordinates(display: display);
		}

		private void UpdateCoordinates(IDisplayProcessing<GameImage> display)
		{
			if (display.IsMobileInLandscapeOrientation())
			{
				this.panelWidth = 600;
				this.panelHeight = this.showPausedText
					? 582
					: 520;

				this.panelX = (display.GetMobileScreenWidth() - this.panelWidth) / 2;

				this.panelY = (display.GetMobileScreenHeight() - this.panelHeight) / 2;

				this.continueButton.SetX((display.GetMobileScreenWidth() - BUTTON_WIDTH) / 2);
				this.continueButton.SetY(this.panelY + 160);

				this.backToTitleScreenButton.SetX((display.GetMobileScreenWidth() - BUTTON_WIDTH) / 2);
				this.backToTitleScreenButton.SetY(this.panelY + 20);

				if (this.soundAndMusicVolumePicker != null)
				{
					this.soundAndMusicVolumePicker.SetX(display.GetMobileScreenWidth() / 2 - 180);
					this.soundAndMusicVolumePicker.SetY(this.panelY + 300);
				}
			}
			else
			{
				this.panelWidth = 600;
				this.panelHeight = this.showPausedText
					? 732
					: 640;

				this.panelX = (display.GetMobileScreenWidth() - this.panelWidth) / 2;

				this.panelY = (display.GetMobileScreenHeight() - this.panelHeight) / 2;

				this.continueButton.SetX((display.GetMobileScreenWidth() - BUTTON_WIDTH) / 2);
				this.continueButton.SetY(this.panelY + 220);

				this.backToTitleScreenButton.SetX((display.GetMobileScreenWidth() - BUTTON_WIDTH) / 2);
				this.backToTitleScreenButton.SetY(this.panelY + 50);

				if (this.soundAndMusicVolumePicker != null)
				{
					this.soundAndMusicVolumePicker.SetX(display.GetMobileScreenWidth() / 2 - 180);
					this.soundAndMusicVolumePicker.SetY(this.panelY + 390);
				}
			}
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
			if (displayType == DisplayType.Desktop)
			{
				this.underlyingFrame = this.underlyingFrame.ProcessDisplayType(displayType: displayType, displayProcessing: displayProcessing);

				return new SettingsMenuDesktopFrame(
					globalState: this.globalState,
					sessionState: this.sessionState,
					underlyingFrame: this.underlyingFrame,
					showPausedText: this.showPausedText,
					shouldRenderUnderlyingFrame: false);
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
					xPos: 0,
					yPos: 0,
					initialSoundVolume: soundOutput.GetSoundVolume(),
					initialMusicVolume: this.globalState.MusicVolume,
					elapsedMicrosPerFrame: this.globalState.ElapsedMicrosPerFrame,
					scalingFactor: 2);

			this.UpdateCoordinates(display: displayProcessing);

			if (displayProcessing.GetMobileScreenWidth() != this.widthOfUnderlyingFrame)
				this.renderUnderlyingFrame = false;
			if (displayProcessing.GetMobileScreenHeight() != this.heightOfUnderlyingFrame)
				this.renderUnderlyingFrame = false;

			this.soundAndMusicVolumePicker.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);
			soundOutput.SetSoundVolume(this.soundAndMusicVolumePicker.GetCurrentSoundVolume());
			this.globalState.MusicVolume = this.soundAndMusicVolumePicker.GetCurrentMusicVolume();
			
			bool clickedContinueButton = this.continueButton.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);

			if (clickedContinueButton || keyboardInput.IsPressed(Key.Esc) && !previousKeyboardInput.IsPressed(Key.Esc))
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
				return new TitleScreenMobileFrame(globalState: this.globalState, sessionState: this.sessionState, display: displayProcessing);
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
				width: this.panelWidth - 1,
				height: this.panelHeight - 1,
				color: DTColor.White(),
				fill: true);

			displayOutput.DrawRectangle(
				x: this.panelX,
				y: this.panelY,
				width: this.panelWidth,
				height: this.panelHeight,
				color: DTColor.Black(),
				fill: false);

			if (this.soundAndMusicVolumePicker != null)
				this.soundAndMusicVolumePicker.Render(displayOutput: displayOutput);
			
			if (this.showPausedText)
				displayOutput.DrawText(
					x: displayOutput.GetMobileScreenWidth() / 2 - 116,
					y: displayOutput.IsMobileInLandscapeOrientation() ? (this.panelY + 562) : (this.panelY + 682),
					text: "Paused",
					font: GameFont.GameFont48Pt,
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
