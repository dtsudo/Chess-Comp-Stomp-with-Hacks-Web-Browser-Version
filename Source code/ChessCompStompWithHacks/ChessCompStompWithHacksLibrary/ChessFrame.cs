
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System;
	using System.Collections.Generic;

	public class ChessFrame : IFrame<GameImage, GameFont, GameSound, GameMusic>
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

		private const int GAME_LOGIC_X_OFFSET = 0;
		private const int GAME_LOGIC_Y_OFFSET = 50;

		public ChessFrame(GlobalState globalState, SessionState sessionState)
		{
			this.globalState = globalState;
			this.sessionState = sessionState;
			this.delayBeforeShowingPanel = null;
			this.victoryStalemateOrDefeatPanel = null;
			this.finalBattleVictoryPanel = null;

			this.settingsIcon = new SettingsIcon();

			this.resignButton = new Button(
				x: 869,
				y: 100,
				width: 100,
				height: 40,
				backgroundColor: new DTColor(200, 200, 200),
				hoverColor: ColorThemeUtil.GetHoverColor(colorTheme: sessionState.GetColorTheme()),
				clickColor: ColorThemeUtil.GetClickColor(colorTheme: sessionState.GetColorTheme()),
				text: "Resign",
				textXOffset: 14,
				textYOffset: 9,
				font: GameFont.GameFont16Pt);

			this.viewObjectivesButton = new Button(
				x: 770,
				y: 30,
				width: 199,
				height: 40,
				backgroundColor: new DTColor(200, 200, 200),
				hoverColor: ColorThemeUtil.GetHoverColor(colorTheme: sessionState.GetColorTheme()),
				clickColor: ColorThemeUtil.GetClickColor(colorTheme: sessionState.GetColorTheme()),
				text: "View objectives",
				textXOffset: 16,
				textYOffset: 9,
				font: GameFont.GameFont16Pt);

			this.viewHacksButton = new Button(
				x: 621,
				y: 30,
				width: 150,
				height: 40,
				backgroundColor: new DTColor(200, 200, 200),
				hoverColor: ColorThemeUtil.GetHoverColor(colorTheme: sessionState.GetColorTheme()),
				clickColor: ColorThemeUtil.GetClickColor(colorTheme: sessionState.GetColorTheme()),
				text: "View hacks",
				textXOffset: 16,
				textYOffset: 9,
				font: GameFont.GameFont16Pt);
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

		public IFrame<GameImage, GameFont, GameSound, GameMusic> GetNextFrame(
			IKeyboard keyboardInput,
			IMouse mouseInput,
			IKeyboard previousKeyboardInput,
			IMouse previousMouseInput,
			IDisplayProcessing<GameImage> displayProcessing,
			ISoundOutput<GameSound> soundOutput,
			IMusicProcessing musicProcessing)
		{
			if (this.finalBattleVictoryPanel == null)
			{
				GameMusic music = GameMusicUtil.GetGameMusic(colorTheme: this.sessionState.GetColorTheme());
				this.globalState.MusicPlayer.SetMusic(music: music, volume: 100);
			}

			VictoryStalemateOrDefeatPanel.Result victoryStalemateOrDefeatPanelResult = null;

			if (this.victoryStalemateOrDefeatPanel != null)
			{
				victoryStalemateOrDefeatPanelResult = this.victoryStalemateOrDefeatPanel.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput, elapsedMicrosPerFrame: this.globalState.ElapsedMicrosPerFrame);
				if (victoryStalemateOrDefeatPanelResult.HasClickedContinueButton)
				{
					this.globalState.SaveData(sessionState: this.sessionState, soundVolume: soundOutput.GetSoundVolume());
					soundOutput.PlaySound(GameSound.Click);
					return new HackSelectionScreenFrame(globalState: this.globalState, sessionState: this.sessionState);
				}
			}

			FinalBattleVictoryPanel.Result finalBattleVictoryPanelResult = null;

			if (this.finalBattleVictoryPanel != null)
			{
				this.globalState.MusicPlayer.SetMusic(music: GameMusic.Ending, volume: 100);

				finalBattleVictoryPanelResult = this.finalBattleVictoryPanel.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);
				if (finalBattleVictoryPanelResult.HasClickedContinueButton)
				{
					this.globalState.SaveData(sessionState: this.sessionState, soundVolume: soundOutput.GetSoundVolume());
					soundOutput.PlaySound(GameSound.Click);
					return new TitleScreenFrame(globalState: this.globalState, sessionState: this.sessionState);
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
				mouseInput: isHoverOverPanel ? dummyMouseInput : new TranslatedMouse(mouse: mouseInput, xOffset: -GAME_LOGIC_X_OFFSET, yOffset: -GAME_LOGIC_Y_OFFSET),
				displayProcessing: displayProcessing,
				soundOutput: soundOutput,
				elapsedMicrosPerFrame: this.globalState.ElapsedMicrosPerFrame);

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
						this.finalBattleVictoryPanel = new FinalBattleVictoryPanel(colorTheme: this.sessionState.GetColorTheme());
					}
					else
						this.victoryStalemateOrDefeatPanel = new VictoryStalemateOrDefeatPanel(
							gameStatus: result.GameStatus,
							isPlayerWhite: result.IsPlayerWhite,
							completedObjectives: this.sessionState.GetCompletedObjectives(),
							objectivesThatWereAlreadyCompletedPriorToThisGame: this.sessionState.GetObjectivesThatWereAlreadyCompletedPriorToCurrentGame(),
							colorTheme: this.sessionState.GetColorTheme());
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
				return new SettingsMenuFrame(globalState: this.globalState, sessionState: this.sessionState, underlyingFrame: this, showPausedText: true);
			}

			if (this.delayBeforeShowingPanel == null)
			{
				bool hasClickedResignButton = this.resignButton.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);

				if (hasClickedResignButton)
				{
					soundOutput.PlaySound(GameSound.Click);
					return new ResignConfirmationFrame(globalState: this.globalState, sessionState: this.sessionState, underlyingFrame: this);
				}

				bool hasClickedViewObjectivesButton = this.viewObjectivesButton.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);
				if (hasClickedViewObjectivesButton)
				{
					this.globalState.SaveData(sessionState: this.sessionState, soundVolume: soundOutput.GetSoundVolume());
					soundOutput.PlaySound(GameSound.Click);
					return new ViewObjectivesFrame(globalState: this.globalState, sessionState: this.sessionState);
				}

				bool hasClickedViewHacksButton = this.viewHacksButton.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);
				if (hasClickedViewHacksButton)
				{
					this.globalState.SaveData(sessionState: this.sessionState, soundVolume: soundOutput.GetSoundVolume());
					soundOutput.PlaySound(GameSound.Click);
					return new ViewHacksFrame(globalState: this.globalState, sessionState: this.sessionState);
				}
			}
			
			bool hasClicked = this.settingsIcon.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput, ignoreMouse: isHoverOverPanel, displayProcessing: displayProcessing).HasClicked;

			if (hasClicked && !isWaitingForFinalBattleVictoryPanel)
			{
				this.globalState.SaveData(sessionState: this.sessionState, soundVolume: soundOutput.GetSoundVolume());
				soundOutput.PlaySound(GameSound.Click);
				return new SettingsMenuFrame(globalState: this.globalState, sessionState: this.sessionState, underlyingFrame: this, showPausedText: true);
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
				width: GlobalConstants.WINDOW_WIDTH,
				height: GlobalConstants.WINDOW_HEIGHT,
				color: new DTColor(223, 220, 217),
				fill: true);

			GameLogic gameLogic = this.sessionState.GetGameLogic();
			if (gameLogic == null)
				gameLogic = this.sessionState.GetMostRecentGameLogic();
			gameLogic.Render(displayOutput: new TranslatedDisplayOutput<GameImage, GameFont>(display: displayOutput, xOffsetInPixels: GAME_LOGIC_X_OFFSET, yOffsetInPixels: GAME_LOGIC_Y_OFFSET));

			if (this.delayBeforeShowingPanel == null)
			{
				this.resignButton.Render(displayOutput: displayOutput);
				this.viewObjectivesButton.Render(displayOutput: displayOutput);
				this.viewHacksButton.Render(displayOutput: displayOutput);
			}

			this.settingsIcon.Render(displayOutput: displayOutput);

			if (this.victoryStalemateOrDefeatPanel != null)
				this.victoryStalemateOrDefeatPanel.Render(displayOutput: displayOutput);

			if (this.finalBattleVictoryPanel != null)
				this.finalBattleVictoryPanel.Render(displayOutput: displayOutput);
		}

		public void RenderMusic(IMusicOutput<GameMusic> musicOutput)
		{
			this.globalState.RenderMusic(musicOutput: musicOutput);
		}
	}
}
