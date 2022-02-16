
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;

	public class ViewHacksFrame : IFrame<ChessImage, ChessFont, ChessSound, ChessMusic>
	{
		private GlobalState globalState;
		private SessionState sessionState;

		private SettingsIcon settingsIcon;

		private Button backToGameButton;

		private HackSelectionScreenDisplay hackSelectionScreenDisplay;

		public ViewHacksFrame(GlobalState globalState, SessionState sessionState)
		{
			this.globalState = globalState;
			this.sessionState = sessionState;

			this.hackSelectionScreenDisplay = new HackSelectionScreenDisplay(sessionState: sessionState, allowResearchingHacks: false);

			this.settingsIcon = new SettingsIcon();

			this.backToGameButton = new Button(
				x: 300,
				y: 55,
				width: 400,
				height: 80,
				backgroundColor: new DTColor(235, 235, 235),
				hoverColor: ColorThemeUtil.GetHoverColor(colorTheme: sessionState.GetColorTheme()),
				clickColor: ColorThemeUtil.GetClickColor(colorTheme: sessionState.GetColorTheme()),
				text: "Back to game",
				textXOffset: 113,
				textYOffset: 27,
				font: ChessFont.ChessFont20Pt);
		}

		public void ProcessExtraTime(int milliseconds)
		{
			GameLogic gameLogic = this.sessionState.GetGameLogic();
			if (gameLogic != null)
				gameLogic.ProcessExtraTime(milliseconds: milliseconds);
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
			Hack? rightClickedHack = this.hackSelectionScreenDisplay.ProcessFrame(
				mouseInput: mouseInput,
				previousMouseInput: previousMouseInput,
				displayProcessing: displayProcessing,
				soundOutput: soundOutput);

			if (rightClickedHack != null)
			{
				return new HackExplanationFrame(globalState: this.globalState, sessionState: this.sessionState, hack: rightClickedHack.Value, underlyingFrame: this);
			}

			bool clickedBackToGameButton = this.backToGameButton.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);
			if (clickedBackToGameButton)
			{
				soundOutput.PlaySound(ChessSound.Click);
				return new ChessFrame(globalState: this.globalState, sessionState: this.sessionState);
			}

			SettingsIcon.SettingsIconStatus settingsIconStatus = this.settingsIcon.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput, ignoreMouse: false, displayProcessing: displayProcessing);

			if (settingsIconStatus.HasClicked)
			{
				soundOutput.PlaySound(ChessSound.Click);
				return new SettingsMenuFrame(globalState: this.globalState, sessionState: this.sessionState, underlyingFrame: this, showPausedText: false);
			}

			if (keyboardInput.IsPressed(Key.Esc) && !previousKeyboardInput.IsPressed(Key.Esc))
			{
				soundOutput.PlaySound(ChessSound.Click);
				return new ChessFrame(globalState: this.globalState, sessionState: this.sessionState);
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

			this.hackSelectionScreenDisplay.RenderButtons(displayOutput: displayOutput);

			this.settingsIcon.Render(displayOutput: displayOutput);

			this.backToGameButton.Render(displayOutput: displayOutput);

			this.hackSelectionScreenDisplay.RenderHoverDisplay(displayOutput: displayOutput);
		}

		public void RenderMusic(IMusicOutput<ChessMusic> musicOutput)
		{
			this.globalState.RenderMusic(musicOutput: musicOutput);
		}
	}
}
