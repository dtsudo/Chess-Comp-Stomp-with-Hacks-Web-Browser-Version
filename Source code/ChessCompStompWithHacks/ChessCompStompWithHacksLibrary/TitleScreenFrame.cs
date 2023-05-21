
namespace ChessCompStompWithHacksLibrary
{
	using DTLibrary;
	using System;
	using System.Collections.Generic;

	public class TitleScreenFrame : IFrame<GameImage, GameFont, GameSound, GameMusic>
	{
		private GlobalState globalState;
		private SessionState sessionState;

		private SoundAndMusicVolumePicker volumePicker;

		private Button startButton;
		private Button continueButton;
		private Button quitButton;

		private Button clearDataButton;

		private Button creditsButton;

		private string versionString;

		public TitleScreenFrame(GlobalState globalState, SessionState sessionState)
		{
			this.globalState = globalState;
			this.sessionState = sessionState;
			this.volumePicker = null;

			int buttonWidth = 150;

			this.versionString = VersionHistory.GetVersionInfo().Version;

			this.startButton = new Button(
				x: (GlobalConstants.WINDOW_WIDTH - buttonWidth) / 2,
				y: 300,
				width: buttonWidth,
				height: 50,
				backgroundColor: new DTColor(200, 200, 200),
				hoverColor: ColorThemeUtil.GetHoverColor(colorTheme: sessionState.GetColorTheme()),
				clickColor: ColorThemeUtil.GetClickColor(colorTheme: sessionState.GetColorTheme()),
				text: "Start",
				textXOffset: 35,
				textYOffset: 13,
				font: GameFont.GameFont20Pt);

			this.continueButton = new Button(
				x: (GlobalConstants.WINDOW_WIDTH - buttonWidth) / 2,
				y: 300,
				width: buttonWidth,
				height: 50,
				backgroundColor: new DTColor(200, 200, 200),
				hoverColor: ColorThemeUtil.GetHoverColor(colorTheme: sessionState.GetColorTheme()),
				clickColor: ColorThemeUtil.GetClickColor(colorTheme: sessionState.GetColorTheme()),
				text: "Continue",
				textXOffset: 15,
				textYOffset: 13,
				font: GameFont.GameFont20Pt);

			this.quitButton = new Button(
				x: (GlobalConstants.WINDOW_WIDTH - buttonWidth) / 2,
				y: 230,
				width: buttonWidth,
				height: 50,
				backgroundColor: new DTColor(200, 200, 200),
				hoverColor: ColorThemeUtil.GetHoverColor(colorTheme: sessionState.GetColorTheme()),
				clickColor: ColorThemeUtil.GetClickColor(colorTheme: sessionState.GetColorTheme()),
				text: "Quit",
				textXOffset: 46,
				textYOffset: 13,
				font: GameFont.GameFont20Pt);

			this.clearDataButton = new Button(
				x: 160,
				y: 10,
				width: 200,
				height: 31,
				backgroundColor: new DTColor(200, 200, 200),
				hoverColor: ColorThemeUtil.GetHoverColor(colorTheme: sessionState.GetColorTheme()),
				clickColor: ColorThemeUtil.GetClickColor(colorTheme: sessionState.GetColorTheme()),
				text: "Reset data",
				textXOffset: 40,
				textYOffset: 6,
				font: GameFont.GameFont16Pt);

			this.creditsButton = new Button(
				x: GlobalConstants.WINDOW_WIDTH - 105,
				y: 5,
				width: 100,
				height: 35,
				backgroundColor: new DTColor(200, 200, 200),
				hoverColor: ColorThemeUtil.GetHoverColor(colorTheme: sessionState.GetColorTheme()),
				clickColor: ColorThemeUtil.GetClickColor(colorTheme: sessionState.GetColorTheme()),
				text: "Credits",
				textXOffset: 13,
				textYOffset: 10,
				font: GameFont.GameFont14Pt);
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

		public IFrame<GameImage, GameFont, GameSound, GameMusic> GetNextFrame(
			IKeyboard keyboardInput,
			IMouse mouseInput,
			IKeyboard previousKeyboardInput,
			IMouse previousMouseInput,
			IDisplayProcessing<GameImage> displayProcessing,
			ISoundOutput<GameSound> soundOutput,
			IMusicProcessing musicProcessing)
		{
			if (this.volumePicker == null)
				this.volumePicker = new SoundAndMusicVolumePicker(
					xPos: 0,
					yPos: 0,
					initialSoundVolume: soundOutput.GetSoundVolume(),
					initialMusicVolume: this.globalState.MusicVolume,
					elapsedMicrosPerFrame: this.globalState.ElapsedMicrosPerFrame);
			
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
						return new HackSelectionScreenFrame(globalState: this.globalState, sessionState: this.sessionState);
					else
						return new ChessFrame(globalState: this.globalState, sessionState: this.sessionState);
				}
			}
			else
			{
				bool clickedStartButton = this.startButton.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);
				if (clickedStartButton)
				{
					this.globalState.SaveData(sessionState: this.sessionState, soundVolume: soundOutput.GetSoundVolume());
					soundOutput.PlaySound(GameSound.Click);
					return new IntroScreenFrame(globalState: this.globalState, sessionState: this.sessionState);
				}
			}

			if (this.globalState.BuildType == BuildType.Desktop)
			{
				bool clickedQuitButton = this.quitButton.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);
				if (clickedQuitButton)
				{
					this.globalState.SaveData(sessionState: this.sessionState, soundVolume: soundOutput.GetSoundVolume());
					return null;
				}
			}

			if (this.sessionState.HasStarted)
			{
				bool clickedClearDataButton = this.clearDataButton.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);
				if (clickedClearDataButton)
				{
					soundOutput.PlaySound(GameSound.Click);
					return new ClearDataConfirmationFrame(globalState: this.globalState, sessionState: this.sessionState, underlyingFrame: this);
				}
			}

			bool clickedCreditsButton = this.creditsButton.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);
			if (clickedCreditsButton)
			{
				this.globalState.SaveData(sessionState: this.sessionState, soundVolume: soundOutput.GetSoundVolume());
				soundOutput.PlaySound(GameSound.Click);
				return new CreditsFrame(globalState: this.globalState, sessionState: this.sessionState);
			}

			if (this.globalState.DebugMode)
			{
				if (keyboardInput.IsPressed(Key.T) && !previousKeyboardInput.IsPressed(Key.T))
					return new TestingFrame(globalState: this.globalState, sessionState: this.sessionState);
			}

			return this;
		}
		
		public void ProcessMusic()
		{
			this.globalState.ProcessMusic();
		}

		public void Render(IDisplayOutput<GameImage, GameFont> displayOutput)
		{
			displayOutput.DrawRectangle(
				x: 0,
				y: 0,
				width: GlobalConstants.WINDOW_WIDTH,
				height: GlobalConstants.WINDOW_HEIGHT,
				color: new DTColor(223, 220, 217),
				fill: true);

			displayOutput.DrawText(
				x: 182,
				y: 510,
				text: "Chess Comp Stomp With Hacks",
				font: GameFont.GameFont32Pt,
				color: DTColor.Black());
			
			if (this.sessionState.HasStarted)
				this.clearDataButton.Render(displayOutput: displayOutput);

			if (this.sessionState.HasStarted)
				this.continueButton.Render(displayOutput: displayOutput);
			else
				this.startButton.Render(displayOutput: displayOutput);

			if (this.globalState.BuildType == BuildType.Desktop)
				this.quitButton.Render(displayOutput: displayOutput);

			displayOutput.DrawText(
				x: GlobalConstants.WINDOW_WIDTH - 42,
				y: 55,
				text: "v" + this.versionString,
				font: GameFont.GameFont12Pt,
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
