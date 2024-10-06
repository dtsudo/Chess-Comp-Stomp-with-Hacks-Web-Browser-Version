
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System.Collections.Generic;

	public class ViewHacksMobileFrame : IFrame<GameImage, GameFont, GameSound, GameMusic>
	{
		private GlobalState globalState;
		private SessionState sessionState;

		private SettingsIcon settingsIcon;

		private Button backToGameButton;

		private HackSelectionScreenDisplayMobile hackSelectionScreenDisplay;

		private HackSelectionScreenMobileTab hackSelectionScreenMobileTabInPreviousFrame;

		public ViewHacksMobileFrame(GlobalState globalState, SessionState sessionState, IDisplayProcessing<GameImage> display)
		{
			this.globalState = globalState;
			this.sessionState = sessionState;

			this.hackSelectionScreenDisplay = new HackSelectionScreenDisplayMobile(sessionState: sessionState, allowResearchingHacks: false, display: display);

			this.settingsIcon = new SettingsIcon(isMobileDisplayType: true);

			this.backToGameButton = new Button(
				x: 0,
				y: 0,
				width: 400,
				height: 80,
				backgroundColor: new DTColor(235, 235, 235),
				hoverColor: ColorThemeUtil.GetHoverColor(colorTheme: sessionState.GetColorTheme()),
				clickColor: ColorThemeUtil.GetClickColor(colorTheme: sessionState.GetColorTheme()),
				text: "Back to game",
				textXOffset: 113,
				textYOffset: 27,
				font: GameFont.GameFont20Pt,
				isMobileDisplayType: true);

			this.hackSelectionScreenMobileTabInPreviousFrame = sessionState.GetHackSelectionScreenMobileTab();

			this.UpdateCoordinates(display: display);
		}

		private void UpdateCoordinates(IDisplayProcessing<GameImage> display)
		{
			this.backToGameButton.SetX(display.GetMobileScreenWidth() / 2 - 200);
			this.backToGameButton.SetY(55);
		}

		public void ProcessExtraTime(int milliseconds)
		{
			GameLogic gameLogic = this.sessionState.GetGameLogic();
			if (gameLogic != null)
				gameLogic.ProcessExtraTime(milliseconds: milliseconds);
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
				return new ViewHacksDesktopFrame(globalState: this.globalState, sessionState: this.sessionState);

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

			Hack? clickedOnMoreDetailsHack = this.hackSelectionScreenDisplay.ProcessFrame(
				mouseInput: mouseInput,
				previousMouseInput: previousMouseInput,
				displayProcessing: displayProcessing,
				soundOutput: soundOutput);

			if (clickedOnMoreDetailsHack != null)
			{
				return new HackExplanationMobileFrame(globalState: this.globalState, sessionState: this.sessionState, hack: clickedOnMoreDetailsHack.Value, underlyingFrame: this);
			}

			bool clickedBackToGameButton = this.backToGameButton.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);
			if (clickedBackToGameButton)
			{
				soundOutput.PlaySound(GameSound.Click);
				return new ChessMobileFrame(globalState: this.globalState, sessionState: this.sessionState, display: displayProcessing);
			}

			SettingsIcon.SettingsIconStatus settingsIconStatus = this.settingsIcon.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput, ignoreMouse: false, displayProcessing: displayProcessing);

			if (settingsIconStatus.HasClicked)
			{
				soundOutput.PlaySound(GameSound.Click);
				return new SettingsMenuMobileFrame(globalState: this.globalState, sessionState: this.sessionState, underlyingFrame: this, showPausedText: false, display: displayProcessing, shouldRenderUnderlyingFrameInitially: true);
			}

			if (keyboardInput.IsPressed(Key.Esc) && !previousKeyboardInput.IsPressed(Key.Esc))
			{
				soundOutput.PlaySound(GameSound.Click);
				return new ChessMobileFrame(globalState: this.globalState, sessionState: this.sessionState, display: displayProcessing);
			}
			
			if (this.sessionState.GetHackSelectionScreenMobileTab() != this.hackSelectionScreenMobileTabInPreviousFrame)
				this.globalState.SaveData(sessionState: this.sessionState, soundVolume: soundOutput.GetSoundVolume());

			this.hackSelectionScreenMobileTabInPreviousFrame = this.sessionState.GetHackSelectionScreenMobileTab();

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

			this.hackSelectionScreenDisplay.RenderButtons(displayOutput: displayOutput);

			this.settingsIcon.Render(displayOutput: displayOutput);

			this.backToGameButton.Render(displayOutput: displayOutput);
		}

		public void RenderMusic(IMusicOutput<GameMusic> musicOutput)
		{
			this.globalState.RenderMusic(musicOutput: musicOutput);
		}
	}
}
