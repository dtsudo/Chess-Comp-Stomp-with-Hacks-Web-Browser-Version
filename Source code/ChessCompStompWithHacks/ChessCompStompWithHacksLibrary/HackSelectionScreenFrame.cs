
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System;
	using System.Collections.Generic;

	public class HackSelectionScreenFrame : IFrame<ChessImage, ChessFont, ChessSound, ChessMusic>
	{
		private GlobalState globalState;
		private SessionState sessionState;

		private SettingsIcon settingsIcon;

		private List<HackDisplay> hackDisplays;

		private Button resetHacksButton;
		private Button continueButton;

		public HackSelectionScreenFrame(GlobalState globalState, SessionState sessionState)
		{
			this.globalState = globalState;
			this.sessionState = sessionState;

			this.settingsIcon = new SettingsIcon();

			List<Hack> hacks = new List<Hack>()
			{
				Hack.ExtraPawnFirst,
				Hack.ExtraPawnSecond,
				Hack.PawnsCanMoveThreeSpacesInitially,
				Hack.SuperCastling,
				Hack.StalemateIsVictory,

				Hack.KnightsCanMakeLargeKnightsMove,
				Hack.RooksCanMoveLikeBishops,
				Hack.RooksCanCaptureLikeCannons,
				Hack.ExtraQueen,
				Hack.QueensCanMoveLikeKnights,

				Hack.SuperEnPassant,
				Hack.AnyPieceCanPromote,
				Hack.OpponentMustCaptureWhenPossible,
				Hack.PawnsDestroyCapturingPiece,
				Hack.TacticalNuke
			};
			
			this.hackDisplays = new List<HackDisplay>();

			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 5; j++)
				{
					Hack hack = hacks[0];
					hacks.RemoveAt(0);

					this.hackDisplays.Add(new HackDisplay(
						hack: hack,
						x: 198 * j + 8,
						y: 450 - 115 * i,
						sessionState: this.sessionState));
				}
			}

			this.resetHacksButton = new Button(
				x: 8,
				y: 70,
				width: 170,
				height: 40,
				backgroundColor: new DTColor(200, 200, 200),
				hoverColor: new DTColor(250, 249, 200),
				clickColor: new DTColor(252, 251, 154),
				text: "Reset hacks",
				textXOffset: 18,
				textYOffset: 9,
				font: ChessFont.Fetamont16Pt);
			
			this.continueButton = new Button(
				x: 700,
				y: 50,
				width: 200,
				height: 80,
				backgroundColor: new DTColor(235, 235, 235),
				hoverColor: new DTColor(250, 249, 200),
				clickColor: new DTColor(252, 251, 154),
				text: "Continue",
				textXOffset: 40,
				textYOffset: 27,
				font: ChessFont.Fetamont20Pt);
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
			if (this.globalState.DebugMode)
			{
				if (keyboardInput.IsPressed(Key.One) && !previousKeyboardInput.IsPressed(Key.One))
				{
					this.sessionState.AddCompletedObjectives(new HashSet<Objective>() { Objective.DefeatComputer });
					this.sessionState.Debug_AddWin();
				}

				if (keyboardInput.IsPressed(Key.Two) && !previousKeyboardInput.IsPressed(Key.Two))
				{
					this.sessionState.AddCompletedObjectives(new HashSet<Objective>()
						{
							Objective.DefeatComputer,
							Objective.DefeatComputerByPlayingAtMost25Moves,
							Objective.DefeatComputerWith5QueensOnTheBoard,
							Objective.CheckmateUsingAKnight,
							Objective.PromoteAPieceToABishop,
							Objective.LaunchANuke
						});
				}
			}

			foreach (HackDisplay hackDisplay in this.hackDisplays)
				hackDisplay.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput, displayProcessing: displayProcessing);

			bool clickedContinueButton = this.continueButton.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);
			if (clickedContinueButton)
				return new ObjectivesScreenFrame(globalState: this.globalState, sessionState: this.sessionState);

			bool clickedSettingsIcon = this.settingsIcon.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput, ignoreMouse: false, displayProcessing: displayProcessing);
			if (clickedSettingsIcon)
				return new SettingsMenuFrame(globalState: this.globalState, sessionState: this.sessionState, underlyingFrame: this, showPausedText: false);
			
			if (keyboardInput.IsPressed(Key.Esc) && !previousKeyboardInput.IsPressed(Key.Esc))
				return new SettingsMenuFrame(globalState: this.globalState, sessionState: this.sessionState, underlyingFrame: this, showPausedText: false);

			bool clickedResetHacksButton = this.resetHacksButton.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);
			if (clickedResetHacksButton)
				this.sessionState.ResetResearchedHacks();

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
				x: 436,
				y: 675,
				text: "Hacks",
				font: ChessFont.Fetamont32Pt,
				color: DTColor.Black());
			
			displayOutput.DrawText(
				x: 250,
				y: 122,
				text: "Hack points remaining: " + this.sessionState.GetUnusedHackPoints().ToStringCultureInvariant() + "\n"
					+ "Get more hack points by winning games" + "\n"
					+ "and completing objectives!",
				font: ChessFont.Fetamont16Pt,
				color: DTColor.Black());

			foreach (HackDisplay hackDisplay in this.hackDisplays)
				hackDisplay.RenderButtonDisplay(displayOutput: displayOutput);

			this.resetHacksButton.Render(displayOutput: displayOutput);

			this.settingsIcon.Render(displayOutput: displayOutput);

			this.continueButton.Render(displayOutput: displayOutput);

			foreach (HackDisplay hackDisplay in this.hackDisplays)
				hackDisplay.RenderHoverDisplay(displayOutput: displayOutput);
		}

		public void RenderMusic(IMusicOutput<ChessMusic> musicOutput)
		{
			this.globalState.RenderMusic(musicOutput: musicOutput);
		}
	}
}
