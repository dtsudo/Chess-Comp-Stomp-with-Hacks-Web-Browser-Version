
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System;
	using System.Collections.Generic;

	public class ChessFrame : IFrame<ChessImage, ChessFont, ChessSound, ChessMusic>
	{
		private GlobalState globalState;
		private SessionState sessionState;
		private int? delayBeforeShowingPanel;
		private VictoryStalemateOrDefeatPanel victoryStalemateOrDefeatPanel;
		private FinalBattleVictoryPanel finalBattleVictoryPanel;

		private SettingsIcon settingsIcon;

		private Button resignButton;

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
				hoverColor: new DTColor(250, 249, 200),
				clickColor: new DTColor(252, 251, 154),
				text: "Resign",
				textXOffset: 14,
				textYOffset: 9,
				font: ChessFont.Fetamont16Pt);
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
			VictoryStalemateOrDefeatPanel.Result victoryStalemateOrDefeatPanelResult = null;

			if (this.victoryStalemateOrDefeatPanel != null)
			{
				victoryStalemateOrDefeatPanelResult = this.victoryStalemateOrDefeatPanel.ProcessFrame(mouseInput, previousMouseInput);
				if (victoryStalemateOrDefeatPanelResult.HasClickedContinueButton)
					return new HackSelectionScreenFrame(globalState: this.globalState, sessionState: this.sessionState);
			}

			FinalBattleVictoryPanel.Result finalBattleVictoryPanelResult = null;

			if (this.finalBattleVictoryPanel != null)
			{
				finalBattleVictoryPanelResult = this.finalBattleVictoryPanel.ProcessFrame(mouseInput, previousMouseInput);
				if (finalBattleVictoryPanelResult.HasClickedContinueButton)
					return new TitleScreenFrame(globalState: this.globalState, sessionState: this.sessionState);
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

			this.sessionState.AddCompletedObjectives(new HashSet<Objective>(result.CompletedObjectives));

			bool didPlayerWin = result.GameStatus == ComputeMoves.GameStatus.WhiteVictory && result.IsPlayerWhite
				|| result.GameStatus == ComputeMoves.GameStatus.BlackVictory && !result.IsPlayerWhite;

			if (this.delayBeforeShowingPanel == null)
			{
				if (result.GameStatus != ComputeMoves.GameStatus.InProgress)
				{
					this.sessionState.CompleteGame(didPlayerWin: didPlayerWin);
					this.delayBeforeShowingPanel = 0;
				}
			}

			if (this.delayBeforeShowingPanel != null && this.victoryStalemateOrDefeatPanel == null && this.finalBattleVictoryPanel == null)
			{
				this.delayBeforeShowingPanel = this.delayBeforeShowingPanel.Value + this.globalState.ElapsedMicrosPerFrame;

				if (this.delayBeforeShowingPanel.Value >= 1000 * 1000)
				{
					if (didPlayerWin && result.IsFinalBattle && !this.sessionState.HasShownFinalBattleVictoryPanel())
					{
						this.sessionState.SetShownFinalBattleVictoryPanel();
						this.finalBattleVictoryPanel = new FinalBattleVictoryPanel();
					}
					else
						this.victoryStalemateOrDefeatPanel = new VictoryStalemateOrDefeatPanel(
							gameStatus: result.GameStatus,
							isPlayerWhite: result.IsPlayerWhite);
				}
			}

			bool isWaitingForFinalBattleVictoryPanel = didPlayerWin
				&& result.IsFinalBattle
				&& !this.sessionState.HasShownFinalBattleVictoryPanel()
				&& this.finalBattleVictoryPanel == null;

			if (keyboardInput.IsPressed(Key.Esc) && !previousKeyboardInput.IsPressed(Key.Esc) && !isWaitingForFinalBattleVictoryPanel)
				return new SettingsMenuFrame(globalState: this.globalState, sessionState: this.sessionState, underlyingFrame: this, showPausedText: true);

			if (this.delayBeforeShowingPanel == null)
			{
				bool hasClickedResignButton = this.resignButton.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);

				if (hasClickedResignButton)
					return new ResignConfirmationFrame(globalState: this.globalState, sessionState: this.sessionState, underlyingFrame: this);
			}
			
			bool hasClicked = this.settingsIcon.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput, ignoreMouse: isHoverOverPanel, displayProcessing: displayProcessing);

			if (hasClicked && !isWaitingForFinalBattleVictoryPanel)
				return new SettingsMenuFrame(globalState: this.globalState, sessionState: this.sessionState, underlyingFrame: this, showPausedText: true);
			
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

			GameLogic gameLogic = this.sessionState.GetGameLogic();
			if (gameLogic == null)
				gameLogic = this.sessionState.GetMostRecentGameLogic();
			gameLogic.Render(displayOutput: new TranslatedDisplayOutput<ChessImage, ChessFont>(display: displayOutput, xOffsetInPixels: GAME_LOGIC_X_OFFSET, yOffsetInPixels: GAME_LOGIC_Y_OFFSET));
			
			if (this.delayBeforeShowingPanel == null)
				this.resignButton.Render(displayOutput: displayOutput);

			this.settingsIcon.Render(displayOutput: displayOutput);

			if (this.victoryStalemateOrDefeatPanel != null)
				this.victoryStalemateOrDefeatPanel.Render(displayOutput: displayOutput);

			if (this.finalBattleVictoryPanel != null)
				this.finalBattleVictoryPanel.Render(displayOutput: displayOutput);
		}

		public void RenderMusic(IMusicOutput<ChessMusic> musicOutput)
		{
			this.globalState.RenderMusic(musicOutput: musicOutput);
		}
	}
}
