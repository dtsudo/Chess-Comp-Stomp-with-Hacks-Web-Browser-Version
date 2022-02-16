
namespace ChessCompStompWithHacksLibrary
{
	using DTLibrary;
	using System;

	public class SettingsMenuFrame : IFrame<ChessImage, ChessFont, ChessSound, ChessMusic>
	{
		private GlobalState globalState;
		private SessionState sessionState;
		private SoundAndMusicVolumePicker soundAndMusicVolumePicker;
		private IFrame<ChessImage, ChessFont, ChessSound, ChessMusic> underlyingFrame;

		private Button continueButton;
		private Button backToTitleScreenButton;
		private bool showPausedText;

		private int panelY;
		private int panelHeight;

		private const int PANEL_WIDTH = 300;
		private const int PANEL_HEIGHT_WITH_PAUSE = 380;
		private const int PANEL_HEIGHT_WITHOUT_PAUSE = 263;

		private const int PANEL_X = (ChessCompStompWithHacks.WINDOW_WIDTH - PANEL_WIDTH) / 2;
		private const int PANEL_Y_WITH_PAUSE = (ChessCompStompWithHacks.WINDOW_HEIGHT - PANEL_HEIGHT_WITH_PAUSE) / 2;
		private const int PANEL_Y_WITHOUT_PAUSE = (ChessCompStompWithHacks.WINDOW_HEIGHT - PANEL_HEIGHT_WITHOUT_PAUSE) / 2;

		private const int BUTTON_WIDTH = 240;
		private const int BUTTON_HEIGHT = 40;

		public SettingsMenuFrame(
			GlobalState globalState,
			SessionState sessionState,
			IFrame<ChessImage, ChessFont, ChessSound, ChessMusic> underlyingFrame,
			bool showPausedText)
		{
			this.globalState = globalState;
			this.sessionState = sessionState;
			this.soundAndMusicVolumePicker = null;
			this.underlyingFrame = underlyingFrame;

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
				font: ChessFont.ChessFont14Pt);

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
				font: ChessFont.ChessFont14Pt);
		}

		public void ProcessExtraTime(int milliseconds)
		{
			this.underlyingFrame.ProcessExtraTime(milliseconds: milliseconds);
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
			if (this.soundAndMusicVolumePicker == null)
				this.soundAndMusicVolumePicker = new SoundAndMusicVolumePicker(
					xPos: PANEL_X + (PANEL_WIDTH - BUTTON_WIDTH) / 2,
					yPos: this.panelY + (this.showPausedText ? 170 : 140),
					initialSoundVolume: soundOutput.GetSoundVolume(),
					initialMusicVolume: this.globalState.MusicVolume,
					elapsedMicrosPerFrame: this.globalState.ElapsedMicrosPerFrame);
			
			this.soundAndMusicVolumePicker.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);
			soundOutput.SetSoundVolume(this.soundAndMusicVolumePicker.GetCurrentSoundVolume());
			this.globalState.MusicVolume = this.soundAndMusicVolumePicker.GetCurrentMusicVolume();

			if (keyboardInput.IsPressed(Key.Esc) && !previousKeyboardInput.IsPressed(Key.Esc))
			{
				this.globalState.SaveData(sessionState: this.sessionState, soundVolume: soundOutput.GetSoundVolume());
				soundOutput.PlaySound(ChessSound.Click);
				return this.underlyingFrame;
			}

			bool clickedContinueButton = this.continueButton.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);

			if (clickedContinueButton)
			{
				this.globalState.SaveData(sessionState: this.sessionState, soundVolume: soundOutput.GetSoundVolume());
				soundOutput.PlaySound(ChessSound.Click);
				return this.underlyingFrame;
			}

			bool clickedBackToTitleScreenButton = this.backToTitleScreenButton.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);

			if (clickedBackToTitleScreenButton)
			{
				this.globalState.SaveData(sessionState: this.sessionState, soundVolume: soundOutput.GetSoundVolume());
				soundOutput.PlaySound(ChessSound.Click);
				return new TitleScreenFrame(globalState: this.globalState, sessionState: this.sessionState);
			}

			if (mouseInput.IsLeftMouseButtonPressed() && !previousMouseInput.IsLeftMouseButtonPressed())
			{
				int mouseX = mouseInput.GetX();
				int mouseY = mouseInput.GetY();

				if (mouseX < PANEL_X || mouseX > PANEL_X + PANEL_WIDTH || mouseY < this.panelY || mouseY > this.panelY + this.panelHeight)
				{
					this.globalState.SaveData(sessionState: this.sessionState, soundVolume: soundOutput.GetSoundVolume());
					soundOutput.PlaySound(ChessSound.Click);
					return this.underlyingFrame;
				}
			}

			return this;
		}
		
		public void ProcessMusic()
		{
			this.underlyingFrame.ProcessMusic();
		}

		public void Render(IDisplayOutput<ChessImage, ChessFont> displayOutput)
		{
			this.underlyingFrame.Render(display: displayOutput);

			displayOutput.DrawRectangle(
				x: 0,
				y: 0,
				width: ChessCompStompWithHacks.WINDOW_WIDTH,
				height: ChessCompStompWithHacks.WINDOW_HEIGHT,
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
					font: ChessFont.ChessFont32Pt,
					color: DTColor.Black());

			this.continueButton.Render(displayOutput: displayOutput);
			this.backToTitleScreenButton.Render(displayOutput: displayOutput);
		}

		public void RenderMusic(IMusicOutput<ChessMusic> musicOutput)
		{
			this.underlyingFrame.RenderMusic(musicOutput: musicOutput);
		}
	}
}
