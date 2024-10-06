﻿
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System;
	using System.Collections.Generic;

	public class ObjectivesScreenMobileFrame : IFrame<GameImage, GameFont, GameSound, GameMusic>
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

		private ObjectivesScreenDisplayMobile objectivesScreenDisplay;

		public ObjectivesScreenMobileFrame(GlobalState globalState, SessionState sessionState, IDisplayProcessing<GameImage> display)
		{
			ObjectiveDisplay objectiveDisplay = new ObjectiveDisplay();

			this.objectivesScreenDisplay = new ObjectivesScreenDisplayMobile(sessionState: sessionState);

			this.globalState = globalState;
			this.sessionState = sessionState;
			HashSet<Objective> completedObjectives = sessionState.GetCompletedObjectives();
			this.hasUnlockedFinalObjective = objectiveDisplay.HasUnlockedFinalObjective(completedObjectives: completedObjectives);
			this.hasCompletedFinalObjective = completedObjectives.Contains(Objective.WinFinalBattle);

			this.settingsIcon = new SettingsIcon(isMobileDisplayType: true);

			this.backToHackSelectionFrameButton = new Button(
				x: 62,
				y: 70,
				width: 150,
				height: 60,
				backgroundColor: new DTColor(200, 200, 200),
				hoverColor: ColorThemeUtil.GetHoverColor(colorTheme: sessionState.GetColorTheme()),
				clickColor: ColorThemeUtil.GetClickColor(colorTheme: sessionState.GetColorTheme()),
				text: "Back",
				textXOffset: 47,
				textYOffset: 19,
				font: GameFont.GameFont16Pt,
				isMobileDisplayType: true);

			this.startNextGameButton_finalBattleNotUnlocked = new Button(
				x: 0,
				y: 0,
				width: 400,
				height: 80,
				backgroundColor: new DTColor(235, 235, 235),
				hoverColor: ColorThemeUtil.GetHoverColor(colorTheme: sessionState.GetColorTheme()),
				clickColor: ColorThemeUtil.GetClickColor(colorTheme: sessionState.GetColorTheme()),
				text: "Start next round",
				textXOffset: 84,
				textYOffset: 27,
				font: GameFont.GameFont20Pt,
				isMobileDisplayType: true);

			this.startFinalBattleButton = new Button(
				x: 0,
				y: 0,
				width: 400,
				height: 80,
				backgroundColor: new DTColor(235, 235, 235),
				hoverColor: ColorThemeUtil.GetHoverColor(colorTheme: sessionState.GetColorTheme()),
				clickColor: ColorThemeUtil.GetClickColor(colorTheme: sessionState.GetColorTheme()),
				text: "Start the Final Battle!",
				textXOffset: 46,
				textYOffset: 27,
				font: GameFont.GameFont20Pt,
				isMobileDisplayType: true);

			this.startNonFinalBattleButton_finalBattleNotComplete = new Button(
				x: 0,
				y: 0,
				width: 400,
				height: 51,
				backgroundColor: new DTColor(200, 200, 200),
				hoverColor: ColorThemeUtil.GetHoverColor(colorTheme: sessionState.GetColorTheme()),
				clickColor: ColorThemeUtil.GetClickColor(colorTheme: sessionState.GetColorTheme()),
				text: "Start a regular game",
				textXOffset: 83,
				textYOffset: 17,
				font: GameFont.GameFont16Pt,
				isMobileDisplayType: true);

			this.aiHackLevelSelectionButton = new AIHackLevelSelectionButton(
				x: 0,
				y: 0,
				width: 400,
				height: 51,
				text: "Start a regular game",
				textXOffset: 83,
				textYOffset: 17,
				font: GameFont.GameFont16Pt,
				isPlayerWhite: sessionState.WillPlayerBeWhiteNextGame(),
				researchedHacks: new DTImmutableList<Hack>(sessionState.GetResearchedHacks()),
				isMobileDisplayType: true);

			this.UpdateCoordinates(display: display);
		}

		private void UpdateCoordinates(IDisplayProcessing<GameImage> display)
		{
			int x = display.IsMobileInLandscapeOrientation()
				? display.GetMobileScreenWidth() / 2 - 200
				: 250;

			this.startNextGameButton_finalBattleNotUnlocked.SetX(x);
			this.startNextGameButton_finalBattleNotUnlocked.SetY(50);

			this.startFinalBattleButton.SetX(x);
			this.startFinalBattleButton.SetY(80);

			this.startNonFinalBattleButton_finalBattleNotComplete.SetX(x);
			this.startNonFinalBattleButton_finalBattleNotComplete.SetY(30);

			this.aiHackLevelSelectionButton.SetX(x);
			this.aiHackLevelSelectionButton.SetY(30);
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

		public IFrame<GameImage, GameFont, GameSound, GameMusic> ProcessDisplayType(DisplayType displayType, IDisplayProcessing<GameImage> displayProcessing)
		{
			if (displayType == DisplayType.Desktop)
				return new ObjectivesScreenDesktopFrame(globalState: this.globalState, sessionState: this.sessionState);

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

			bool clickedSettingsIcon = this.settingsIcon.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput, ignoreMouse: false, displayProcessing: displayProcessing).HasClicked;
			if (clickedSettingsIcon)
			{
				soundOutput.PlaySound(GameSound.Click);
				return new SettingsMenuMobileFrame(globalState: this.globalState, sessionState: this.sessionState, underlyingFrame: this, showPausedText: false, display: displayProcessing, shouldRenderUnderlyingFrameInitially: true);
			}

			if (keyboardInput.IsPressed(Key.Esc) && !previousKeyboardInput.IsPressed(Key.Esc))
			{
				soundOutput.PlaySound(GameSound.Click);
				return new SettingsMenuMobileFrame(globalState: this.globalState, sessionState: this.sessionState, underlyingFrame: this, showPausedText: false, display: displayProcessing, shouldRenderUnderlyingFrameInitially: true);
			}

			bool clickedBackButton = this.backToHackSelectionFrameButton.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);
			if (clickedBackButton)
			{
				soundOutput.PlaySound(GameSound.Click);
				return new HackSelectionScreenMobileFrame(globalState: this.globalState, sessionState: this.sessionState, display: displayProcessing);
			}

			if (this.hasUnlockedFinalObjective)
			{
				bool clickedStartFinalBattleButton = this.startFinalBattleButton.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);
				if (clickedStartFinalBattleButton)
				{
					soundOutput.PlaySound(GameSound.Click);
					return this.sessionState.StartGame(isFinalBattle: true, globalState: this.globalState, aiHackLevelOverride: null, display: displayProcessing, isMobileDisplayType: true);
				}

				if (this.hasCompletedFinalObjective)
				{
					SessionState.AIHackLevel? selectedAIHackLevel = this.aiHackLevelSelectionButton.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);

					if (selectedAIHackLevel.HasValue)
					{
						soundOutput.PlaySound(GameSound.Click);
						return this.sessionState.StartGame(isFinalBattle: false, globalState: this.globalState, aiHackLevelOverride: selectedAIHackLevel.Value, display: displayProcessing, isMobileDisplayType: true);
					}
				}
				else
				{
					bool clickedStartNonFinalBattleButton = this.startNonFinalBattleButton_finalBattleNotComplete.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);
					if (clickedStartNonFinalBattleButton)
					{
						soundOutput.PlaySound(GameSound.Click);
						return this.sessionState.StartGame(isFinalBattle: false, globalState: this.globalState, aiHackLevelOverride: null, display: displayProcessing, isMobileDisplayType: true);
					}
				}
			}
			else
			{
				bool clickedStartGameButton = this.startNextGameButton_finalBattleNotUnlocked.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);
				if (clickedStartGameButton)
				{
					soundOutput.PlaySound(GameSound.Click);
					return this.sessionState.StartGame(isFinalBattle: false, globalState: this.globalState, aiHackLevelOverride: null, display: displayProcessing, isMobileDisplayType: true);
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
				width: displayOutput.GetMobileScreenWidth(),
				height: displayOutput.GetMobileScreenHeight(),
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