
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System;
	using System.Collections.Generic;

	public class ChessMobileFrame : IFrame<GameImage, GameFont, GameSound, GameMusic>
	{
		private GlobalState globalState;
		private SessionState sessionState;
		private int? delayBeforeShowingPanel;
		private VictoryStalemateOrDefeatPanel victoryStalemateOrDefeatPanel;
		private FinalBattleVictoryPanel finalBattleVictoryPanel;

		private SettingsIcon settingsIcon;

		private Button resignButton;

		private Button viewObjectivesButton;
		private Button viewHacksButton;

		private int gameLogicXOffset;
		private int gameLogicYOffset;

		public ChessMobileFrame(
			GlobalState globalState,
			SessionState sessionState,
			IDisplayProcessing<GameImage> display)
			: this(globalState: globalState, sessionState: sessionState, display: display, delayBeforeShowingPanel: null, victoryStalemateOrDefeatPanel: null, finalBattleVictoryPanel: null)
		{
		}

		public ChessMobileFrame(
			GlobalState globalState,
			SessionState sessionState,
			IDisplayProcessing<GameImage> display,
			int? delayBeforeShowingPanel,
			VictoryStalemateOrDefeatPanel victoryStalemateOrDefeatPanel,
			FinalBattleVictoryPanel finalBattleVictoryPanel)
		{
			this.globalState = globalState;
			this.sessionState = sessionState;
			this.delayBeforeShowingPanel = delayBeforeShowingPanel;
			this.victoryStalemateOrDefeatPanel = victoryStalemateOrDefeatPanel;
			this.finalBattleVictoryPanel = finalBattleVictoryPanel;

			this.settingsIcon = new SettingsIcon(isMobileDisplayType: true);

			this.resignButton = new Button(
				x: 0,
				y: 0,
				width: 140,
				height: 100,
				backgroundColor: new DTColor(200, 200, 200),
				hoverColor: ColorThemeUtil.GetHoverColor(colorTheme: sessionState.GetColorTheme()),
				clickColor: ColorThemeUtil.GetClickColor(colorTheme: sessionState.GetColorTheme()),
				text: "Resign",
				textXOffset: 34,
				textYOffset: 39,
				font: GameFont.GameFont16Pt,
				isMobileDisplayType: true);

			this.viewObjectivesButton = new Button(
				x: 0,
				y: 0,
				width: 10,
				height: 10,
				backgroundColor: new DTColor(200, 200, 200),
				hoverColor: ColorThemeUtil.GetHoverColor(colorTheme: sessionState.GetColorTheme()),
				clickColor: ColorThemeUtil.GetClickColor(colorTheme: sessionState.GetColorTheme()),
				text: "",
				textXOffset: 16,
				textYOffset: 0,
				font: GameFont.GameFont16Pt,
				isMobileDisplayType: true);

			this.viewHacksButton = new Button(
				x: 0,
				y: 0,
				width: 10,
				height: 10,
				backgroundColor: new DTColor(200, 200, 200),
				hoverColor: ColorThemeUtil.GetHoverColor(colorTheme: sessionState.GetColorTheme()),
				clickColor: ColorThemeUtil.GetClickColor(colorTheme: sessionState.GetColorTheme()),
				text: "",
				textXOffset: 16,
				textYOffset: 0,
				font: GameFont.GameFont16Pt,
				isMobileDisplayType: true);

			this.UpdateCoordinates(display: display);
		}

		private void UpdateCoordinates(IDisplayProcessing<GameImage> display)
		{
			if (display.IsMobileInLandscapeOrientation())
			{
				this.gameLogicXOffset = (display.GetMobileScreenWidth() - 1000) / 2;
				this.gameLogicYOffset = 0;

				int x = this.gameLogicXOffset + 850;

				this.resignButton.SetX(x);
				this.resignButton.SetY(250);

				this.viewObjectivesButton.SetX(x);
				this.viewObjectivesButton.SetY(10);
				this.viewObjectivesButton.SetText("     View \nobjectives");
				this.viewObjectivesButton.SetWidth(140);
				this.viewObjectivesButton.SetHeight(100);
				this.viewObjectivesButton.SetTextYOffset(26);

				this.viewHacksButton.SetX(x);
				this.viewHacksButton.SetY(110);
				this.viewHacksButton.SetText("     View \n    hacks");
				this.viewHacksButton.SetWidth(140);
				this.viewHacksButton.SetHeight(100);
				this.viewHacksButton.SetTextYOffset(26);
			}
			else
			{
				this.gameLogicXOffset = 0;
				this.gameLogicYOffset = display.GetMobileScreenHeight() - 1000;

				this.resignButton.SetX(this.gameLogicXOffset + 550);
				this.resignButton.SetY(this.gameLogicYOffset + 100);

				this.viewObjectivesButton.SetX(490);
				this.viewObjectivesButton.SetY(this.gameLogicYOffset + 10);
				this.viewObjectivesButton.SetText("View objectives");
				this.viewObjectivesButton.SetWidth(200);
				this.viewObjectivesButton.SetHeight(80);
				this.viewObjectivesButton.SetTextYOffset(29);

				this.viewHacksButton.SetX(340);
				this.viewHacksButton.SetY(this.gameLogicYOffset + 10);
				this.viewHacksButton.SetText("View hacks");
				this.viewHacksButton.SetWidth(150);
				this.viewHacksButton.SetHeight(80);
				this.viewHacksButton.SetTextYOffset(29);
			}
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
			GameLogic gameLogic = this.sessionState.GetGameLogic();
			if (gameLogic != null)
				gameLogic.ProcessExtraTime(milliseconds: milliseconds);
		}

		public IFrame<GameImage, GameFont, GameSound, GameMusic> ProcessDisplayType(DisplayType displayType, IDisplayProcessing<GameImage> displayProcessing)
		{
			if (displayType == DisplayType.Desktop)
			{
				return new ChessDesktopFrame(
					globalState: this.globalState,
					sessionState: this.sessionState,
					delayBeforeShowingPanel: this.delayBeforeShowingPanel,
					victoryStalemateOrDefeatPanel: this.victoryStalemateOrDefeatPanel,
					finalBattleVictoryPanel: this.finalBattleVictoryPanel);
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
			this.UpdateCoordinates(display: displayProcessing);

			if (this.finalBattleVictoryPanel == null)
			{
				GameMusic music = GameMusicUtil.GetGameMusic(colorTheme: this.sessionState.GetColorTheme());
				this.globalState.MusicPlayer.SetMusic(music: music, volume: 100);
			}

			VictoryStalemateOrDefeatPanel.Result victoryStalemateOrDefeatPanelResult = null;

			if (this.victoryStalemateOrDefeatPanel != null)
			{
				victoryStalemateOrDefeatPanelResult = this.victoryStalemateOrDefeatPanel.ProcessFrame(
					mouseInput: mouseInput,
					previousMouseInput: previousMouseInput,
					elapsedMicrosPerFrame: this.globalState.ElapsedMicrosPerFrame,
					display: displayProcessing,
					isMobileDisplayType: true);

				if (victoryStalemateOrDefeatPanelResult.HasClickedContinueButton)
				{
					this.globalState.SaveData(sessionState: this.sessionState, soundVolume: soundOutput.GetSoundVolume());
					soundOutput.PlaySound(GameSound.Click);
					return new HackSelectionScreenMobileFrame(globalState: this.globalState, sessionState: this.sessionState, display: displayProcessing);
				}
			}

			FinalBattleVictoryPanel.Result finalBattleVictoryPanelResult = null;

			if (this.finalBattleVictoryPanel != null)
			{
				this.globalState.MusicPlayer.SetMusic(music: GameMusic.Ending, volume: 100);

				finalBattleVictoryPanelResult = this.finalBattleVictoryPanel.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput, display: displayProcessing, isMobileDisplayType: true);
				if (finalBattleVictoryPanelResult.HasClickedContinueButton)
				{
					this.globalState.SaveData(sessionState: this.sessionState, soundVolume: soundOutput.GetSoundVolume());
					soundOutput.PlaySound(GameSound.Click);
					return new TitleScreenMobileFrame(globalState: this.globalState, sessionState: this.sessionState, display: displayProcessing);
				}
			}

			bool isHoverOverPanel = victoryStalemateOrDefeatPanelResult != null && victoryStalemateOrDefeatPanelResult.IsHoverOverPanel
				|| finalBattleVictoryPanelResult != null && finalBattleVictoryPanelResult.IsHoverOverPanel;

			IMouse dummyMouseInput = new SimulatedMouse(
				x: -999, // arbitrary
				y: -999,
				isLeftMouseButtonPressed: mouseInput.IsLeftMouseButtonPressed(),
				isRightMouseButtonPressed: mouseInput.IsRightMouseButtonPressed());
			
			GameLogic gameLogic = this.sessionState.GetGameLogic();
			if (gameLogic == null)
				gameLogic = this.sessionState.GetMostRecentGameLogic();
			GameLogic.Result result = gameLogic.ProcessNextFrame(
				mouseInput: isHoverOverPanel ? dummyMouseInput : new TranslatedMouse(mouse: mouseInput, xOffset: -this.gameLogicXOffset, yOffset: -this.gameLogicYOffset),
				displayProcessing: displayProcessing,
				soundOutput: soundOutput,
				elapsedMicrosPerFrame: this.globalState.ElapsedMicrosPerFrame,
				isMobileDisplayType: true);

			bool hasCompletedANewObjective = this.sessionState.AddCompletedObjectives(new HashSet<Objective>(result.CompletedObjectives));

			if (hasCompletedANewObjective)
				this.globalState.SaveData(sessionState: this.sessionState, soundVolume: soundOutput.GetSoundVolume());

			bool didPlayerWin = result.GameStatus == ComputeMoves.GameStatus.WhiteVictory && result.IsPlayerWhite
				|| result.GameStatus == ComputeMoves.GameStatus.BlackVictory && !result.IsPlayerWhite;

			if (this.delayBeforeShowingPanel == null)
			{
				if (result.GameStatus != ComputeMoves.GameStatus.InProgress)
				{
					this.sessionState.CompleteGame(didPlayerWin: didPlayerWin);
					this.delayBeforeShowingPanel = 0;
					this.globalState.SaveData(sessionState: this.sessionState, soundVolume: soundOutput.GetSoundVolume());
				}
			}

			if (this.delayBeforeShowingPanel != null && this.victoryStalemateOrDefeatPanel == null && this.finalBattleVictoryPanel == null)
			{
				this.delayBeforeShowingPanel = this.delayBeforeShowingPanel.Value + this.globalState.ElapsedMicrosPerFrame;

				if (this.delayBeforeShowingPanel.Value >= 1000 * 1000)
				{
					soundOutput.PlaySound(sound: didPlayerWin ? GameSound.Win : GameSound.StalemateOrDefeat);

					if (didPlayerWin && result.IsFinalBattle && !this.sessionState.HasShownFinalBattleVictoryPanel())
					{
						this.sessionState.SetShownFinalBattleVictoryPanel();
						this.finalBattleVictoryPanel = new FinalBattleVictoryPanel(colorTheme: this.sessionState.GetColorTheme(), display: displayProcessing, isMobileDisplayType: true);
					}
					else
						this.victoryStalemateOrDefeatPanel = new VictoryStalemateOrDefeatPanel(
							gameStatus: result.GameStatus,
							isPlayerWhite: result.IsPlayerWhite,
							completedObjectives: this.sessionState.GetCompletedObjectives(),
							objectivesThatWereAlreadyCompletedPriorToThisGame: this.sessionState.GetObjectivesThatWereAlreadyCompletedPriorToCurrentGame(),
							colorTheme: this.sessionState.GetColorTheme(),
							display: displayProcessing,
							isMobileDisplayType: true);
				}
			}

			bool isWaitingForFinalBattleVictoryPanel = didPlayerWin
				&& result.IsFinalBattle
				&& !this.sessionState.HasShownFinalBattleVictoryPanel()
				&& this.finalBattleVictoryPanel == null;

			if (keyboardInput.IsPressed(Key.Esc) && !previousKeyboardInput.IsPressed(Key.Esc) && !isWaitingForFinalBattleVictoryPanel)
			{
				this.globalState.SaveData(sessionState: this.sessionState, soundVolume: soundOutput.GetSoundVolume());
				soundOutput.PlaySound(GameSound.Click);
				return new SettingsMenuMobileFrame(globalState: this.globalState, sessionState: this.sessionState, underlyingFrame: this, showPausedText: true, display: displayProcessing, shouldRenderUnderlyingFrameInitially: true);
			}

			if (this.delayBeforeShowingPanel == null)
			{
				bool hasClickedResignButton = this.resignButton.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);

				if (hasClickedResignButton)
				{
					soundOutput.PlaySound(GameSound.Click);
					return new ResignConfirmationMobileFrame(globalState: this.globalState, sessionState: this.sessionState, underlyingFrame: this, display: displayProcessing, shouldRenderUnderlyingFrameInitially: true);
				}

				bool hasClickedViewObjectivesButton = this.viewObjectivesButton.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);
				if (hasClickedViewObjectivesButton)
				{
					this.globalState.SaveData(sessionState: this.sessionState, soundVolume: soundOutput.GetSoundVolume());
					soundOutput.PlaySound(GameSound.Click);
					return new ViewObjectivesMobileFrame(globalState: this.globalState, sessionState: this.sessionState, display: displayProcessing);
				}

				bool hasClickedViewHacksButton = this.viewHacksButton.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);
				if (hasClickedViewHacksButton)
				{
					this.globalState.SaveData(sessionState: this.sessionState, soundVolume: soundOutput.GetSoundVolume());
					soundOutput.PlaySound(GameSound.Click);
					return new ViewHacksMobileFrame(globalState: this.globalState, sessionState: this.sessionState, display: displayProcessing);
				}
			}
			
			bool hasClicked = this.settingsIcon.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput, ignoreMouse: isHoverOverPanel, displayProcessing: displayProcessing).HasClicked;

			if (hasClicked && !isWaitingForFinalBattleVictoryPanel)
			{
				this.globalState.SaveData(sessionState: this.sessionState, soundVolume: soundOutput.GetSoundVolume());
				soundOutput.PlaySound(GameSound.Click);
				return new SettingsMenuMobileFrame(globalState: this.globalState, sessionState: this.sessionState, underlyingFrame: this, showPausedText: true, display: displayProcessing, shouldRenderUnderlyingFrameInitially: true);
			}

			if (this.globalState.DebugMode)
			{
				if (keyboardInput.IsPressed(Key.One) && !previousKeyboardInput.IsPressed(Key.One))
					this.globalState.UseDebugAI = !this.globalState.UseDebugAI;
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

			if (this.delayBeforeShowingPanel == null)
			{
				this.resignButton.Render(displayOutput: displayOutput);
				this.viewObjectivesButton.Render(displayOutput: displayOutput);
				this.viewHacksButton.Render(displayOutput: displayOutput);
			}

			GameLogic gameLogic = this.sessionState.GetGameLogic();
			if (gameLogic == null)
				gameLogic = this.sessionState.GetMostRecentGameLogic();
			gameLogic.Render(
				displayOutput: new TranslatedDisplayOutput<GameImage, GameFont>(display: displayOutput, xOffsetInPixels: this.gameLogicXOffset, yOffsetInPixels: this.gameLogicYOffset),
				isMobileDisplayType: true);

			this.settingsIcon.Render(displayOutput: displayOutput);

			if (this.victoryStalemateOrDefeatPanel != null)
				this.victoryStalemateOrDefeatPanel.Render(displayOutput: displayOutput);

			if (this.finalBattleVictoryPanel != null)
				this.finalBattleVictoryPanel.Render(displayOutput: displayOutput, isMobileDisplayType: true);
		}

		public void RenderMusic(IMusicOutput<GameMusic> musicOutput)
		{
			this.globalState.RenderMusic(musicOutput: musicOutput);
		}
	}
}
