
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System;
	using System.Collections.Generic;

	public class ObjectivesScreenFrame : IFrame<GameImage, GameFont, GameSound, GameMusic>
	{
		private GlobalState globalState;
		private SessionState sessionState;
		
		private bool hasUnlockedFinalObjective;
		private bool hasCompletedFinalObjective;

		private SettingsIcon settingsIcon;

		private Button backToHackSelectionFrameButton;
		private Button startNextGameButton_finalBattleNotUnlocked;
		private Button startFinalBattleButton;
		private Button startNonFinalBattleButton_finalBattleNotComplete;

		private AIHackLevelSelectionButton aiHackLevelSelectionButton;

		private ObjectivesScreenDisplay objectivesScreenDisplay;

		public ObjectivesScreenFrame(GlobalState globalState, SessionState sessionState)
		{
			ObjectiveDisplay objectiveDisplay = new ObjectiveDisplay();

			this.objectivesScreenDisplay = new ObjectivesScreenDisplay(sessionState: sessionState);

			this.globalState = globalState;
			this.sessionState = sessionState;
			HashSet<Objective> completedObjectives = sessionState.GetCompletedObjectives();
			this.hasUnlockedFinalObjective = objectiveDisplay.HasUnlockedFinalObjective(completedObjectives: completedObjectives);
			this.hasCompletedFinalObjective = completedObjectives.Contains(Objective.WinFinalBattle);

			this.settingsIcon = new SettingsIcon();

			this.backToHackSelectionFrameButton = new Button(
				x: 62,
				y: 70,
				width: 100,
				height: 40,
				backgroundColor: new DTColor(200, 200, 200),
				hoverColor: ColorThemeUtil.GetHoverColor(colorTheme: sessionState.GetColorTheme()),
				clickColor: ColorThemeUtil.GetClickColor(colorTheme: sessionState.GetColorTheme()),
				text: "Back",
				textXOffset: 22,
				textYOffset: 9,
				font: GameFont.GameFont16Pt);

			this.startNextGameButton_finalBattleNotUnlocked = new Button(
				x: 300,
				y: 50,
				width: 400,
				height: 80,
				backgroundColor: new DTColor(235, 235, 235),
				hoverColor: ColorThemeUtil.GetHoverColor(colorTheme: sessionState.GetColorTheme()),
				clickColor: ColorThemeUtil.GetClickColor(colorTheme: sessionState.GetColorTheme()),
				text: "Start next round",
				textXOffset: 84,
				textYOffset: 27,
				font: GameFont.GameFont20Pt);

			this.startFinalBattleButton = new Button(
				x: 300,
				y: 80,
				width: 400,
				height: 80,
				backgroundColor: new DTColor(235, 235, 235),
				hoverColor: ColorThemeUtil.GetHoverColor(colorTheme: sessionState.GetColorTheme()),
				clickColor: ColorThemeUtil.GetClickColor(colorTheme: sessionState.GetColorTheme()),
				text: "Start the Final Battle!",
				textXOffset: 46,
				textYOffset: 27,
				font: GameFont.GameFont20Pt);

			this.startNonFinalBattleButton_finalBattleNotComplete = new Button(
				x: 300,
				y: 50,
				width: 400,
				height: 31,
				backgroundColor: new DTColor(200, 200, 200),
				hoverColor: ColorThemeUtil.GetHoverColor(colorTheme: sessionState.GetColorTheme()),
				clickColor: ColorThemeUtil.GetClickColor(colorTheme: sessionState.GetColorTheme()),
				text: "Start a regular game",
				textXOffset: 83,
				textYOffset: 7,
				font: GameFont.GameFont16Pt);

			this.aiHackLevelSelectionButton = new AIHackLevelSelectionButton(
				x: 300,
				y: 50,
				width: 400,
				height: 31,
				text: "Start a regular game",
				textXOffset: 83,
				textYOffset: 7,
				font: GameFont.GameFont16Pt,
				isPlayerWhite: sessionState.WillPlayerBeWhiteNextGame(),
				researchedHacks: new DTImmutableList<Hack>(sessionState.GetResearchedHacks()));
		}

		public string GetClickUrl()
		{
			return null;
		}

		public HashSet<string> GetCompletedAchievements()
		{
			return new HashSet<string>();
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
			bool clickedSettingsIcon = this.settingsIcon.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput, ignoreMouse: false, displayProcessing: displayProcessing).HasClicked;
			if (clickedSettingsIcon)
			{
				soundOutput.PlaySound(GameSound.Click);
				return new SettingsMenuFrame(globalState: this.globalState, sessionState: this.sessionState, underlyingFrame: this, showPausedText: false);
			}

			if (keyboardInput.IsPressed(Key.Esc) && !previousKeyboardInput.IsPressed(Key.Esc))
			{
				soundOutput.PlaySound(GameSound.Click);
				return new SettingsMenuFrame(globalState: this.globalState, sessionState: this.sessionState, underlyingFrame: this, showPausedText: false);
			}

			bool clickedBackButton = this.backToHackSelectionFrameButton.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);
			if (clickedBackButton)
			{
				soundOutput.PlaySound(GameSound.Click);
				return new HackSelectionScreenFrame(globalState: this.globalState, sessionState: this.sessionState);
			}

			if (this.hasUnlockedFinalObjective)
			{
				bool clickedStartFinalBattleButton = this.startFinalBattleButton.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);
				if (clickedStartFinalBattleButton)
				{
					soundOutput.PlaySound(GameSound.Click);
					return this.sessionState.StartGame(isFinalBattle: true, globalState: this.globalState, aiHackLevelOverride: null);
				}

				if (this.hasCompletedFinalObjective)
				{
					SessionState.AIHackLevel? selectedAIHackLevel = this.aiHackLevelSelectionButton.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);

					if (selectedAIHackLevel.HasValue)
					{
						soundOutput.PlaySound(GameSound.Click);
						return this.sessionState.StartGame(isFinalBattle: false, globalState: this.globalState, aiHackLevelOverride: selectedAIHackLevel.Value);
					}
				}
				else
				{
					bool clickedStartNonFinalBattleButton = this.startNonFinalBattleButton_finalBattleNotComplete.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);
					if (clickedStartNonFinalBattleButton)
					{
						soundOutput.PlaySound(GameSound.Click);
						return this.sessionState.StartGame(isFinalBattle: false, globalState: this.globalState, aiHackLevelOverride: null);
					}
				}
			}
			else
			{
				bool clickedStartGameButton = this.startNextGameButton_finalBattleNotUnlocked.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);
				if (clickedStartGameButton)
				{
					soundOutput.PlaySound(GameSound.Click);
					return this.sessionState.StartGame(isFinalBattle: false, globalState: this.globalState, aiHackLevelOverride: null);
				}
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

			this.settingsIcon.Render(displayOutput: displayOutput);

			this.backToHackSelectionFrameButton.Render(displayOutput: displayOutput);

			if (this.hasUnlockedFinalObjective)
			{
				this.startFinalBattleButton.Render(displayOutput: displayOutput);
				if (this.hasCompletedFinalObjective)
					this.aiHackLevelSelectionButton.RenderButton(displayOutput: displayOutput);
				else
					this.startNonFinalBattleButton_finalBattleNotComplete.Render(displayOutput: displayOutput);
			}
			else
			{
				this.startNextGameButton_finalBattleNotUnlocked.Render(displayOutput: displayOutput);
			}

			this.objectivesScreenDisplay.Render(displayOutput: displayOutput);

			if (this.hasUnlockedFinalObjective && this.hasCompletedFinalObjective)
				this.aiHackLevelSelectionButton.RenderBoardPreview(displayOutput: displayOutput);
		}

		public void RenderMusic(IMusicOutput<GameMusic> musicOutput)
		{
			this.globalState.RenderMusic(musicOutput: musicOutput);
		}
	}
}
