
namespace ChessCompStompWithHacksLibrary
{
	using DTLibrary;
	using System;
	using System.Collections.Generic;

	public class IntroScreenMobileFrame : IFrame<GameImage, GameFont, GameSound, GameMusic>
	{
		private GlobalState globalState;
		private SessionState sessionState;

		private SettingsIcon settingsIcon;
		private Button continueButton;

		private int elapsedTimeMicros;

		private const int TOTAL_TIME_TO_DISPLAY_TEXT = 4 * 1000 * 1000;

		public IntroScreenMobileFrame(GlobalState globalState, SessionState sessionState, IDisplayProcessing<GameImage> display)
		{
			this.globalState = globalState;
			this.sessionState = sessionState;
			this.settingsIcon = new SettingsIcon(isMobileDisplayType: true);
			this.elapsedTimeMicros = 0;
			
			this.continueButton = new Button(
				x: 0,
				y: 0,
				width: 10,
				height: 10,
				backgroundColor: new DTColor(200, 200, 200),
				hoverColor: ColorThemeUtil.GetHoverColor(colorTheme: sessionState.GetColorTheme()),
				clickColor: ColorThemeUtil.GetClickColor(colorTheme: sessionState.GetColorTheme()),
				text: "Begin",
				textXOffset: 0,
				textYOffset: 0,
				font: GameFont.GameFont32Pt,
				isMobileDisplayType: true);

			this.UpdateCoordinates(display: display);
		}

		private void UpdateCoordinates(IDisplayProcessing<GameImage> display)
		{
			bool isLandscape = display.IsMobileInLandscapeOrientation();

			int buttonWidth = 350;
			int buttonHeight = 100;
			this.continueButton.SetX(display.GetMobileScreenWidth() / 2 - buttonWidth / 2);
			if (isLandscape)
				this.continueButton.SetY(190);
			else
				this.continueButton.SetY(display.GetMobileScreenHeight() - 520);

			this.continueButton.SetWidth(buttonWidth);
			this.continueButton.SetHeight(buttonHeight);

			this.continueButton.SetTextXOffset(118);
			this.continueButton.SetTextYOffset(29);
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
			if (displayType == DisplayType.Desktop)
				return new IntroScreenDesktopFrame(globalState: this.globalState, sessionState: this.sessionState);

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

			this.elapsedTimeMicros += this.globalState.ElapsedMicrosPerFrame;
			if (this.elapsedTimeMicros >= TOTAL_TIME_TO_DISPLAY_TEXT)
				this.elapsedTimeMicros = TOTAL_TIME_TO_DISPLAY_TEXT + 1;

			if (keyboardInput.IsPressed(Key.Esc) && !previousKeyboardInput.IsPressed(Key.Esc))
			{
				soundOutput.PlaySound(GameSound.Click);
				return new SettingsMenuMobileFrame(globalState: this.globalState, sessionState: this.sessionState, underlyingFrame: this, showPausedText: false, display: displayProcessing, shouldRenderUnderlyingFrameInitially: true);
			}

			SettingsIcon.SettingsIconStatus settingsIconStatus = this.settingsIcon.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput, ignoreMouse: false, displayProcessing: displayProcessing);
			
			if (settingsIconStatus.HasClicked)
			{
				soundOutput.PlaySound(GameSound.Click);
				return new SettingsMenuMobileFrame(globalState: this.globalState, sessionState: this.sessionState, underlyingFrame: this, showPausedText: false, display: displayProcessing, shouldRenderUnderlyingFrameInitially: true);
			}

			if (this.elapsedTimeMicros >= TOTAL_TIME_TO_DISPLAY_TEXT)
			{
				bool clickedContinueButton = this.continueButton.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);
				if (clickedContinueButton)
				{
					this.sessionState.StartNewSession();
					GameMusic music = GameMusic.Level1;
					this.globalState.MusicPlayer.SetMusic(music: music, volume: 100);
					this.globalState.SaveData(sessionState: this.sessionState, soundVolume: soundOutput.GetSoundVolume());
					soundOutput.PlaySound(GameSound.Click);
					return new HackSelectionScreenMobileFrame(globalState: this.globalState, sessionState: this.sessionState, display: displayProcessing); 
				}
			}

			if (keyboardInput.IsPressed(Key.Space) && !previousKeyboardInput.IsPressed(Key.Space)
					|| keyboardInput.IsPressed(Key.Enter) && !previousKeyboardInput.IsPressed(Key.Enter)
					// occurs after the clickedContinueButton check
					|| mouseInput.IsLeftMouseButtonPressed() && !previousMouseInput.IsLeftMouseButtonPressed() && !settingsIconStatus.IsHover)
			{
				if (this.elapsedTimeMicros <= TOTAL_TIME_TO_DISPLAY_TEXT)
					soundOutput.PlaySound(GameSound.Click);
				this.elapsedTimeMicros = TOTAL_TIME_TO_DISPLAY_TEXT + 1;
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
				width: displayOutput.GetMobileScreenWidth(),
				height: displayOutput.GetMobileScreenHeight(),
				color: new DTColor(223, 220, 217),
				fill: true);

			displayOutput.DrawText(
				x: displayOutput.GetMobileScreenWidth() / 2 - 135,
				y: displayOutput.GetMobileScreenHeight() - 100,
				text: "Welcome",
				font: GameFont.GameFont48Pt,
				color: DTColor.Black());

			string text;

			bool isLandscape = displayOutput.IsMobileInLandscapeOrientation();

			if (isLandscape)
			{
				text = "Today, you're playing against a Powerful" + "\n"
					+ "Chess AI. You are not a great chess player" + "\n"
					+ "but you are an Elite Hacker. Use your elite" + "\n"
					+ "hacking skills to defeat the AI.";
			}
			else
			{
				text = "Today, you're playing against a Powerful" + "\n"
					+ "Chess AI. You are not a great chess player" + "\n"
					+ "but you are an Elite Hacker. Use your elite" + "\n"
					+ "hacking skills to defeat the AI.";
			}

			int index;
			if (this.elapsedTimeMicros >= TOTAL_TIME_TO_DISPLAY_TEXT)
				index = text.Length;
			else
				index = (int)(((long)this.elapsedTimeMicros) * ((long)text.Length) / ((long)TOTAL_TIME_TO_DISPLAY_TEXT));

			if (isLandscape)
			{
				displayOutput.DrawText(
					x: (displayOutput.GetMobileScreenWidth() - 920) / 2,
					y: 500,
					text: index >= text.Length ? text : text.Substring(0, index),
					font: GameFont.GameFont32Pt,
					color: DTColor.Black());
			}
			else
			{
				displayOutput.DrawText(
					x: 50,
					y: displayOutput.GetMobileScreenHeight() - 220,
					text: index >= text.Length ? text : text.Substring(0, index),
					font: GameFont.GameFont20Pt,
					color: DTColor.Black());
			}
			
			if (this.elapsedTimeMicros >= TOTAL_TIME_TO_DISPLAY_TEXT)
				this.continueButton.Render(displayOutput: displayOutput);

			this.settingsIcon.Render(displayOutput: displayOutput);
		}

		public void RenderMusic(IMusicOutput<GameMusic> musicOutput)
		{
			this.globalState.RenderMusic(musicOutput: musicOutput);
		}
	}
}
