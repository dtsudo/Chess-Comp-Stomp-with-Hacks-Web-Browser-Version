
namespace ChessCompStompWithHacksLibrary
{
	using DTLibrary;
	using System;

	public class IntroScreenFrame : IFrame<ChessImage, ChessFont, ChessSound, ChessMusic>
	{
		private GlobalState globalState;
		private SessionState sessionState;

		private SettingsIcon settingsIcon;
		private Button continueButton;

		private int elapsedTimeMicros;

		private const int TOTAL_TIME_TO_DISPLAY_TEXT = 4 * 1000 * 1000;

		public IntroScreenFrame(GlobalState globalState, SessionState sessionState)
		{
			this.globalState = globalState;
			this.sessionState = sessionState;
			this.settingsIcon = new SettingsIcon();
			this.elapsedTimeMicros = 0;

			int buttonWidth = 150;
			this.continueButton = new Button(
				x: (ChessCompStompWithHacks.WINDOW_WIDTH - buttonWidth) / 2,
				y: 300,
				width: buttonWidth,
				height: 50,
				backgroundColor: new DTColor(200, 200, 200),
				hoverColor: ColorThemeUtil.GetHoverColor(colorTheme: sessionState.GetColorTheme()),
				clickColor: ColorThemeUtil.GetClickColor(colorTheme: sessionState.GetColorTheme()),
				text: "Begin",
				textXOffset: 38,
				textYOffset: 13,
				font: ChessFont.ChessFont20Pt);
		}

		public void ProcessExtraTime(int milliseconds)
		{
		}

		public IFrame<ChessImage, ChessFont, ChessSound, ChessMusic> GetNextFrame(
			IKeyboard keyboardInput,
			IMouse mouseInput,
			IKeyboard previousKeyboardInput,
			IMouse previousMouseInput,
			IDisplayProcessing<ChessImage> displayProcessing,
			ISoundOutput<ChessSound> soundOutput,
			IMusicProcessing musicProcessing)
		{
			this.elapsedTimeMicros += this.globalState.ElapsedMicrosPerFrame;
			if (this.elapsedTimeMicros >= TOTAL_TIME_TO_DISPLAY_TEXT)
				this.elapsedTimeMicros = TOTAL_TIME_TO_DISPLAY_TEXT + 1;

			if (keyboardInput.IsPressed(Key.Esc) && !previousKeyboardInput.IsPressed(Key.Esc))
			{
				soundOutput.PlaySound(ChessSound.Click);
				return new SettingsMenuFrame(globalState: this.globalState, sessionState: this.sessionState, underlyingFrame: this, showPausedText: false);
			}

			SettingsIcon.SettingsIconStatus settingsIconStatus = this.settingsIcon.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput, ignoreMouse: false, displayProcessing: displayProcessing);
			
			if (settingsIconStatus.HasClicked)
			{
				soundOutput.PlaySound(ChessSound.Click);
				return new SettingsMenuFrame(globalState: this.globalState, sessionState: this.sessionState, underlyingFrame: this, showPausedText: false);
			}

			if (this.elapsedTimeMicros >= TOTAL_TIME_TO_DISPLAY_TEXT)
			{
				bool clickedContinueButton = this.continueButton.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);
				if (clickedContinueButton)
				{
					this.sessionState.StartNewSession();
					ChessMusic music = ChessMusic.Level1;
					this.globalState.MusicPlayer.SetMusic(music: music, volume: 100);
					this.globalState.SaveData(sessionState: this.sessionState, soundVolume: soundOutput.GetSoundVolume());
					soundOutput.PlaySound(ChessSound.Click);
					return new HackSelectionScreenFrame(globalState: this.globalState, sessionState: this.sessionState);
				}
			}

			if (keyboardInput.IsPressed(Key.Space) && !previousKeyboardInput.IsPressed(Key.Space)
					|| keyboardInput.IsPressed(Key.Enter) && !previousKeyboardInput.IsPressed(Key.Enter)
					// occurs after the clickedContinueButton check
					|| mouseInput.IsLeftMouseButtonPressed() && !previousMouseInput.IsLeftMouseButtonPressed() && !settingsIconStatus.IsHover)
			{
				if (this.elapsedTimeMicros <= TOTAL_TIME_TO_DISPLAY_TEXT)
					soundOutput.PlaySound(ChessSound.Click);
				this.elapsedTimeMicros = TOTAL_TIME_TO_DISPLAY_TEXT + 1;
			}
			
			return this;
		}
		
		public void ProcessMusic()
		{
			this.globalState.ProcessMusic();
		}

		public void Render(IDisplayOutput<ChessImage, ChessFont> displayOutput)
		{
			displayOutput.DrawRectangle(
				x: 0,
				y: 0,
				width: ChessCompStompWithHacks.WINDOW_WIDTH,
				height: ChessCompStompWithHacks.WINDOW_HEIGHT,
				color: new DTColor(223, 220, 217),
				fill: true);

			displayOutput.DrawText(
				x: 406,
				y: 675,
				text: "Welcome",
				font: ChessFont.ChessFont32Pt,
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
				font: ChessFont.ChessFont20Pt,
				color: DTColor.Black());
			
			if (this.elapsedTimeMicros >= TOTAL_TIME_TO_DISPLAY_TEXT)
				this.continueButton.Render(displayOutput: displayOutput);

			this.settingsIcon.Render(displayOutput: displayOutput);
		}

		public void RenderMusic(IMusicOutput<ChessMusic> musicOutput)
		{
			this.globalState.RenderMusic(musicOutput: musicOutput);
		}
	}
}
