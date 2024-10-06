
namespace ChessCompStompWithHacksLibrary
{
	using DTLibrary;
	using System;
	using System.Collections.Generic;

	public class TitleScreenMobileFrame : IFrame<GameImage, GameFont, GameSound, GameMusic>
	{
		private GlobalState globalState;
		private SessionState sessionState;

		private SoundAndMusicVolumePicker volumePicker;

		private Button startButton;
		private Button continueButton;

		private Button clearDataButton;

		private Button creditsButton;

		private string versionString;

		public TitleScreenMobileFrame(GlobalState globalState, SessionState sessionState, IDisplayProcessing<GameImage> display)
		{
			this.globalState = globalState;
			this.sessionState = sessionState;
			this.volumePicker = null;
			
			this.versionString = VersionHistory.GetVersionInfo().Version;

			this.startButton = new Button(
				x: 0,
				y: 0,
				width: 400,
				height: 125,
				backgroundColor: new DTColor(200, 200, 200),
				hoverColor: ColorThemeUtil.GetHoverColor(colorTheme: sessionState.GetColorTheme()),
				clickColor: ColorThemeUtil.GetClickColor(colorTheme: sessionState.GetColorTheme()),
				text: "Start",
				textXOffset: 137,
				textYOffset: 41,
				font: GameFont.GameFont32Pt,
				isMobileDisplayType: true);

			this.continueButton = new Button(
				x: 0,
				y: 0,
				width: 400,
				height: 125,
				backgroundColor: new DTColor(200, 200, 200),
				hoverColor: ColorThemeUtil.GetHoverColor(colorTheme: sessionState.GetColorTheme()),
				clickColor: ColorThemeUtil.GetClickColor(colorTheme: sessionState.GetColorTheme()),
				text: "Continue",
				textXOffset: 106,
				textYOffset: 41,
				font: GameFont.GameFont32Pt,
				isMobileDisplayType: true);
			
			this.clearDataButton = new Button(
				x: 0,
				y: 0,
				width: 200,
				height: 50,
				backgroundColor: new DTColor(200, 200, 200),
				hoverColor: ColorThemeUtil.GetHoverColor(colorTheme: sessionState.GetColorTheme()),
				clickColor: ColorThemeUtil.GetClickColor(colorTheme: sessionState.GetColorTheme()),
				text: "Reset data",
				textXOffset: 34,
				textYOffset: 12,
				font: GameFont.GameFont18Pt,
				isMobileDisplayType: true);

			this.creditsButton = new Button(
				x: 0,
				y: 0,
				width: 100,
				height: 50,
				backgroundColor: new DTColor(200, 200, 200),
				hoverColor: ColorThemeUtil.GetHoverColor(colorTheme: sessionState.GetColorTheme()),
				clickColor: ColorThemeUtil.GetClickColor(colorTheme: sessionState.GetColorTheme()),
				text: "Credits",
				textXOffset: 6,
				textYOffset: 12,
				font: GameFont.GameFont18Pt,
				isMobileDisplayType: true);

			this.UpdateButtonPositions(display);
		}

		private void UpdateButtonPositions(IDisplayProcessing<GameImage> display)
		{
			this.startButton.SetX((display.GetMobileScreenWidth() - 400) / 2);
			this.startButton.SetY(display.GetMobileScreenHeight() / 2 - 100);

			this.continueButton.SetX((display.GetMobileScreenWidth() - 400) / 2);
			this.continueButton.SetY(display.GetMobileScreenHeight() / 2 - 100);
			
			this.clearDataButton.SetX(320);
			this.clearDataButton.SetY(5);

			this.creditsButton.SetX(display.GetMobileScreenWidth() - 105);
			this.creditsButton.SetY(5);
		}

		public string GetClickUrl()
		{
			return null;
		}

		public HashSet<string> GetCompletedAchievements()
		{
			return this.sessionState.GetCompletedAchievements();
		}

		public void ProcessExtraTime(int milliseconds)
		{
		}

		public IFrame<GameImage, GameFont, GameSound, GameMusic> ProcessDisplayType(DisplayType displayType, IDisplayProcessing<GameImage> displayProcessing)
		{
			if (displayType == DisplayType.Desktop)
				return new TitleScreenDesktopFrame(this.globalState, this.sessionState);

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
			this.UpdateButtonPositions(displayProcessing);

			if (this.volumePicker == null)
				this.volumePicker = new SoundAndMusicVolumePicker(
					xPos: 0,
					yPos: 0,
					initialSoundVolume: soundOutput.GetSoundVolume(),
					initialMusicVolume: this.globalState.MusicVolume,
					elapsedMicrosPerFrame: this.globalState.ElapsedMicrosPerFrame,
					scalingFactor: 2);
			
			this.volumePicker.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);
			soundOutput.SetSoundVolume(volume: this.volumePicker.GetCurrentSoundVolume());
			this.globalState.MusicVolume = this.volumePicker.GetCurrentMusicVolume();
			
			if (this.sessionState.HasStarted)
			{
				bool clickedContinueButton = this.continueButton.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);
				if (clickedContinueButton)
				{
					this.globalState.SaveData(sessionState: this.sessionState, soundVolume: soundOutput.GetSoundVolume());
					soundOutput.PlaySound(GameSound.Click);

					if (this.sessionState.GetGameLogic() == null)
						return new HackSelectionScreenMobileFrame(globalState: this.globalState, sessionState: this.sessionState, display: displayProcessing); 
					else
						return new ChessMobileFrame(globalState: this.globalState, sessionState: this.sessionState, display: displayProcessing);
				}
			}
			else
			{
				bool clickedStartButton = this.startButton.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);
				if (clickedStartButton)
				{
					this.globalState.SaveData(sessionState: this.sessionState, soundVolume: soundOutput.GetSoundVolume());
					soundOutput.PlaySound(GameSound.Click);
					return new IntroScreenMobileFrame(globalState: this.globalState, sessionState: this.sessionState, display: displayProcessing);
				}
			}
			
			if (this.sessionState.HasStarted)
			{
				bool clickedClearDataButton = this.clearDataButton.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);
				if (clickedClearDataButton)
				{
					soundOutput.PlaySound(GameSound.Click);
					return new ClearDataConfirmationMobileFrame(globalState: this.globalState, sessionState: this.sessionState, underlyingFrame: this, display: displayProcessing);
				}
			}

			bool clickedCreditsButton = this.creditsButton.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);
			if (clickedCreditsButton)
			{
				this.globalState.SaveData(sessionState: this.sessionState, soundVolume: soundOutput.GetSoundVolume());
				soundOutput.PlaySound(GameSound.Click);
				return new CreditsMobileFrame(globalState: this.globalState, sessionState: this.sessionState, display: displayProcessing);
			}

			if (this.globalState.DebugMode)
			{
				if (keyboardInput.IsPressed(Key.Two) && !previousKeyboardInput.IsPressed(Key.Two))
					return new DebugInfoFrame(globalState: this.globalState, sessionState: this.sessionState, display: displayProcessing, isMobileDisplayType: true);
			}

			return this;
		}
		
		public void ProcessMusic()
		{
			this.globalState.ProcessMusic();
		}

		public void Render(IDisplayOutput<GameImage, GameFont> displayOutput)
		{
			bool isLandscape = displayOutput.IsMobileInLandscapeOrientation();

			displayOutput.DrawRectangle(
				x: 0,
				y: 0,
				width: displayOutput.GetMobileScreenWidth(),
				height: displayOutput.GetMobileScreenHeight(),
				color: new DTColor(223, 220, 217),
				fill: true);

			if (isLandscape)
				displayOutput.DrawText(
					x: displayOutput.GetMobileScreenWidth() / 2 - 474,
					y: displayOutput.GetMobileScreenHeight() * 3 / 4 + 32,
					text: "Chess Comp Stomp With Hacks",
					font: GameFont.GameFont48Pt,
					color: DTColor.Black());
			else
				displayOutput.DrawText(
					x: displayOutput.GetMobileScreenWidth() / 2 - 319,
					y: displayOutput.GetMobileScreenHeight() * 3 / 4 + 32,
					text: "Chess Comp Stomp With Hacks",
					font: GameFont.GameFont32Pt,
					color: DTColor.Black());

			if (this.sessionState.HasStarted)
				this.clearDataButton.Render(displayOutput: displayOutput);

			if (this.sessionState.HasStarted)
				this.continueButton.Render(displayOutput: displayOutput);
			else
				this.startButton.Render(displayOutput: displayOutput);
			
			displayOutput.DrawText(
				x: displayOutput.GetMobileScreenWidth() - 52,
				y: 74,
				text: "v" + this.versionString,
				font: GameFont.GameFont14Pt,
				color: DTColor.Black());

			this.creditsButton.Render(displayOutput: displayOutput);
			
			if (this.volumePicker != null)
				this.volumePicker.Render(displayOutput: displayOutput);
		}

		public void RenderMusic(IMusicOutput<GameMusic> musicOutput)
		{
			this.globalState.RenderMusic(musicOutput: musicOutput);
		}
	}
}
