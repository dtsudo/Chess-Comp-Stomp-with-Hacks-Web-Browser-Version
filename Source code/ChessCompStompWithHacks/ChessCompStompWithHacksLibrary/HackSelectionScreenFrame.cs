
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
		
		private Button continueButton;

		private HackSelectionScreenDisplay hackSelectionScreenDisplay;

		private int numberOfHacksResearchedInPreviousFrame;

		public HackSelectionScreenFrame(GlobalState globalState, SessionState sessionState)
		{
			this.globalState = globalState;
			this.sessionState = sessionState;

			this.hackSelectionScreenDisplay = new HackSelectionScreenDisplay(sessionState: sessionState, allowResearchingHacks: true);

			this.settingsIcon = new SettingsIcon();
			
			this.continueButton = new Button(
				x: 700,
				y: 50,
				width: 200,
				height: 80,
				backgroundColor: new DTColor(235, 235, 235),
				hoverColor: ColorThemeUtil.GetHoverColor(colorTheme: sessionState.GetColorTheme()),
				clickColor: ColorThemeUtil.GetClickColor(colorTheme: sessionState.GetColorTheme()),
				text: "Continue",
				textXOffset: 40,
				textYOffset: 27,
				font: ChessFont.ChessFont20Pt);

			this.numberOfHacksResearchedInPreviousFrame = sessionState.GetResearchedHacks().Count;
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
					this.globalState.SaveData(sessionState: this.sessionState, soundVolume: soundOutput.GetSoundVolume());
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
					this.globalState.SaveData(sessionState: this.sessionState, soundVolume: soundOutput.GetSoundVolume());
				}
			}

			Hack? rightClickedHack = this.hackSelectionScreenDisplay.ProcessFrame(
				mouseInput: mouseInput,
				previousMouseInput: previousMouseInput,
				displayProcessing: displayProcessing,
				soundOutput: soundOutput);
			
			if (rightClickedHack != null)
			{
				return new HackExplanationFrame(globalState: this.globalState, sessionState: this.sessionState, hack: rightClickedHack.Value, underlyingFrame: this);
			}

			bool clickedContinueButton = this.continueButton.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);
			if (clickedContinueButton)
			{
				this.globalState.SaveData(sessionState: this.sessionState, soundVolume: soundOutput.GetSoundVolume());
				soundOutput.PlaySound(ChessSound.Click);
				return new ObjectivesScreenFrame(globalState: this.globalState, sessionState: this.sessionState);
			}

			SettingsIcon.SettingsIconStatus settingsIconStatus = this.settingsIcon.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput, ignoreMouse: false, displayProcessing: displayProcessing);
			
			if (settingsIconStatus.HasClicked)
			{
				this.globalState.SaveData(sessionState: this.sessionState, soundVolume: soundOutput.GetSoundVolume());
				soundOutput.PlaySound(ChessSound.Click);
				return new SettingsMenuFrame(globalState: this.globalState, sessionState: this.sessionState, underlyingFrame: this, showPausedText: false);
			}

			if (keyboardInput.IsPressed(Key.Esc) && !previousKeyboardInput.IsPressed(Key.Esc))
			{
				this.globalState.SaveData(sessionState: this.sessionState, soundVolume: soundOutput.GetSoundVolume());
				soundOutput.PlaySound(ChessSound.Click);
				return new SettingsMenuFrame(globalState: this.globalState, sessionState: this.sessionState, underlyingFrame: this, showPausedText: false);
			}

			HashSet<Hack> researchedHacks = this.sessionState.GetResearchedHacks();

			if (researchedHacks.Count != this.numberOfHacksResearchedInPreviousFrame)
				this.globalState.SaveData(sessionState: this.sessionState, soundVolume: soundOutput.GetSoundVolume());
			
			this.numberOfHacksResearchedInPreviousFrame = researchedHacks.Count;

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

			this.continueButton.Render(displayOutput: displayOutput);

			this.hackSelectionScreenDisplay.RenderHoverDisplay(displayOutput: displayOutput);
		}

		public void RenderMusic(IMusicOutput<ChessMusic> musicOutput)
		{
			this.globalState.RenderMusic(musicOutput: musicOutput);
		}
	}
}
