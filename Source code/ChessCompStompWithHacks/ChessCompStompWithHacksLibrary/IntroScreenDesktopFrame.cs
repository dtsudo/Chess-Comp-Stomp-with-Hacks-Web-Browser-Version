﻿
namespace ChessCompStompWithHacksLibrary
{
	using DTLibrary;
	using System;
	using System.Collections.Generic;

	public class IntroScreenDesktopFrame : IFrame<GameImage, GameFont, GameSound, GameMusic>
	{
		private GlobalState globalState;
		private SessionState sessionState;

		private SettingsIcon settingsIcon;
		private Button continueButton;

		private int elapsedTimeMicros;

		private const int TOTAL_TIME_TO_DISPLAY_TEXT = 4 * 1000 * 1000;

		public IntroScreenDesktopFrame(GlobalState globalState, SessionState sessionState)
		{
			this.globalState = globalState;
			this.sessionState = sessionState;
			this.settingsIcon = new SettingsIcon(isMobileDisplayType: false);
			this.elapsedTimeMicros = 0;

			int buttonWidth = 150;
			this.continueButton = new Button(
				x: (GlobalConstants.DESKTOP_WINDOW_WIDTH - buttonWidth) / 2,
				y: 300,
				width: buttonWidth,
				height: 50,
				backgroundColor: new DTColor(200, 200, 200),
				hoverColor: ColorThemeUtil.GetHoverColor(colorTheme: sessionState.GetColorTheme()),
				clickColor: ColorThemeUtil.GetClickColor(colorTheme: sessionState.GetColorTheme()),
				text: "Begin",
				textXOffset: 38,
				textYOffset: 13,
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
			if (displayType != DisplayType.Desktop)
				return new IntroScreenMobileFrame(globalState: this.globalState, sessionState: this.sessionState, display: displayProcessing);

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
			this.elapsedTimeMicros += this.globalState.ElapsedMicrosPerFrame;
			if (this.elapsedTimeMicros >= TOTAL_TIME_TO_DISPLAY_TEXT)
				this.elapsedTimeMicros = TOTAL_TIME_TO_DISPLAY_TEXT + 1;

			if (keyboardInput.IsPressed(Key.Esc) && !previousKeyboardInput.IsPressed(Key.Esc))
			{
				soundOutput.PlaySound(GameSound.Click);
				return new SettingsMenuDesktopFrame(globalState: this.globalState, sessionState: this.sessionState, underlyingFrame: this, showPausedText: false, shouldRenderUnderlyingFrame: true);
			}

			SettingsIcon.SettingsIconStatus settingsIconStatus = this.settingsIcon.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput, ignoreMouse: false, displayProcessing: displayProcessing);
			
			if (settingsIconStatus.HasClicked)
			{
				soundOutput.PlaySound(GameSound.Click);
				return new SettingsMenuDesktopFrame(globalState: this.globalState, sessionState: this.sessionState, underlyingFrame: this, showPausedText: false, shouldRenderUnderlyingFrame: true);
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
					return new HackSelectionScreenDesktopFrame(globalState: this.globalState, sessionState: this.sessionState);
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
				width: GlobalConstants.DESKTOP_WINDOW_WIDTH,
				height: GlobalConstants.DESKTOP_WINDOW_HEIGHT,
				color: new DTColor(223, 220, 217),
				fill: true);

			displayOutput.DrawText(
				x: 406,
				y: 675,
				text: "Welcome",
				font: GameFont.GameFont32Pt,
				color: DTColor.Black());

			string text = "Today, you're playing against a Powerful Chess AI." + "\n"
				+ "You are not a great chess player but you are an Elite Hacker." + "\n"
				+ "Use your elite hacking skills to defeat the AI.";

			int index;
			if (this.elapsedTimeMicros >= TOTAL_TIME_TO_DISPLAY_TEXT)
				index = text.Length;
			else
				index = (int)(((long)this.elapsedTimeMicros) * ((long)text.Length) / ((long)TOTAL_TIME_TO_DISPLAY_TEXT));

			displayOutput.DrawText(
				x: 100,
				y: 500,
				text: index >= text.Length ? text : text.Substring(0, index),
				font: GameFont.GameFont20Pt,
				color: DTColor.Black());
			
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
