
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System;
	using System.Collections.Generic;

	public class ObjectivesScreenFrame : IFrame<ChessImage, ChessFont, ChessSound, ChessMusic>
	{
		private GlobalState globalState;
		private SessionState sessionState;

		private HashSet<Objective> completedObjectives;
		private bool hasUnlockedFinalObjective;

		private SettingsIcon settingsIcon;

		private Button backToHackSelectionFrameButton;
		private Button startNextGameButton_finalBattleNotUnlocked;
		private Button startFinalBattleButton;
		private Button startNonFinalBattleButton;

		public ObjectivesScreenFrame(GlobalState globalState, SessionState sessionState)
		{
			this.globalState = globalState;
			this.sessionState = sessionState;
			HashSet<Objective> completedObjectives = sessionState.GetCompletedObjectives();
			this.completedObjectives = completedObjectives;
			this.hasUnlockedFinalObjective = ObjectiveDisplay.HasUnlockedFinalObjective(completedObjectives: completedObjectives);

			this.settingsIcon = new SettingsIcon();

			this.backToHackSelectionFrameButton = new Button(
				x: 62,
				y: 70,
				width: 100,
				height: 40,
				backgroundColor: new DTColor(200, 200, 200),
				hoverColor: new DTColor(250, 249, 200),
				clickColor: new DTColor(252, 251, 154),
				text: "Back",
				textXOffset: 22,
				textYOffset: 9,
				font: ChessFont.Fetamont16Pt);

			this.startNextGameButton_finalBattleNotUnlocked = new Button(
				x: 300,
				y: 50,
				width: 400,
				height: 80,
				backgroundColor: new DTColor(235, 235, 235),
				hoverColor: new DTColor(250, 249, 200),
				clickColor: new DTColor(252, 251, 154),
				text: "Start next round",
				textXOffset: 84,
				textYOffset: 27,
				font: ChessFont.Fetamont20Pt);

			this.startFinalBattleButton = new Button(
				x: 300,
				y: 80,
				width: 400,
				height: 80,
				backgroundColor: new DTColor(235, 235, 235),
				hoverColor: new DTColor(250, 249, 200),
				clickColor: new DTColor(252, 251, 154),
				text: "Start the Final Battle!",
				textXOffset: 46,
				textYOffset: 27,
				font: ChessFont.Fetamont20Pt);

			this.startNonFinalBattleButton = new Button(
				x: 300,
				y: 50,
				width: 400,
				height: 31,
				backgroundColor: new DTColor(200, 200, 200),
				hoverColor: new DTColor(250, 249, 200),
				clickColor: new DTColor(252, 251, 154),
				text: "Start a regular game",
				textXOffset: 83,
				textYOffset: 7,
				font: ChessFont.Fetamont16Pt);
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
			bool clickedSettingsIcon = this.settingsIcon.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput, ignoreMouse: false, displayProcessing: displayProcessing);
			if (clickedSettingsIcon)
				return new SettingsMenuFrame(globalState: this.globalState, sessionState: this.sessionState, underlyingFrame: this, showPausedText: false);
			
			if (keyboardInput.IsPressed(Key.Esc) && !previousKeyboardInput.IsPressed(Key.Esc))
				return new SettingsMenuFrame(globalState: this.globalState, sessionState: this.sessionState, underlyingFrame: this, showPausedText: false);

			bool clickedBackButton = this.backToHackSelectionFrameButton.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);
			if (clickedBackButton)
				return new HackSelectionScreenFrame(globalState: this.globalState, sessionState: this.sessionState);

			if (this.hasUnlockedFinalObjective)
			{
				bool clickedStartFinalBattleButton = this.startFinalBattleButton.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);
				if (clickedStartFinalBattleButton)
					return this.sessionState.StartGame(isFinalBattle: true, globalState: this.globalState);
					
				bool clickedStartNonFinalBattleButton = this.startNonFinalBattleButton.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);
				if (clickedStartNonFinalBattleButton)
					return this.sessionState.StartGame(isFinalBattle: false, globalState: this.globalState);
			}
			else
			{
				bool clickedStartGameButton = this.startNextGameButton_finalBattleNotUnlocked.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);
				if (clickedStartGameButton)
					return this.sessionState.StartGame(isFinalBattle: false, globalState: this.globalState);
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
				x: 389,
				y: 675,
				text: "Objectives",
				font: ChessFont.Fetamont32Pt,
				color: DTColor.Black());

			this.settingsIcon.Render(displayOutput: displayOutput);

			this.backToHackSelectionFrameButton.Render(displayOutput: displayOutput);

			if (this.hasUnlockedFinalObjective)
			{
				this.startFinalBattleButton.Render(displayOutput: displayOutput);
				this.startNonFinalBattleButton.Render(displayOutput: displayOutput);
			}
			else
			{
				this.startNextGameButton_finalBattleNotUnlocked.Render(displayOutput: displayOutput);
			}

			ObjectiveDisplay.RenderNonFinalObjective(
				x: 62,
				y: 450,
				objective: Objective.DefeatComputer,
				completedObjectives: this.completedObjectives,
				displayOutput: displayOutput);

			ObjectiveDisplay.RenderNonFinalObjective(
				x: 375,
				y: 450,
				objective: Objective.DefeatComputerByPlayingAtMost25Moves,
				completedObjectives: this.completedObjectives,
				displayOutput: displayOutput);

			ObjectiveDisplay.RenderNonFinalObjective(
				x: 687,
				y: 450,
				objective: Objective.DefeatComputerWith5QueensOnTheBoard,
				completedObjectives: this.completedObjectives,
				displayOutput: displayOutput);

			ObjectiveDisplay.RenderNonFinalObjective(
				x: 62,
				y: 320,
				objective: Objective.CheckmateUsingAKnight,
				completedObjectives: this.completedObjectives,
				displayOutput: displayOutput);

			ObjectiveDisplay.RenderNonFinalObjective(
				x: 375,
				y: 320,
				objective: Objective.PromoteAPieceToABishop,
				completedObjectives: this.completedObjectives,
				displayOutput: displayOutput);

			ObjectiveDisplay.RenderNonFinalObjective(
				x: 687,
				y: 320,
				objective: Objective.LaunchANuke,
				completedObjectives: this.completedObjectives,
				displayOutput: displayOutput);

			ObjectiveDisplay.RenderFinalObjective(
				x: 250,
				y: 190,
				completedObjectives: this.completedObjectives,
				displayOutput: displayOutput);
		}

		public void RenderMusic(IMusicOutput<ChessMusic> musicOutput)
		{
			this.globalState.RenderMusic(musicOutput: musicOutput);
		}
	}
}
