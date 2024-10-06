﻿
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System;
	using System.Collections.Generic;

	public class HackSelectionScreenDesktopFrame : IFrame<GameImage, GameFont, GameSound, GameMusic>
	{
		private GlobalState globalState;
		private SessionState sessionState;

		private SettingsIcon settingsIcon;
		
		private Button continueButton;

		private HackSelectionScreenDisplayDesktop hackSelectionScreenDisplay;

		private int numberOfHacksResearchedInPreviousFrame;

		public HackSelectionScreenDesktopFrame(GlobalState globalState, SessionState sessionState)
		{
			this.globalState = globalState;
			this.sessionState = sessionState;

			this.hackSelectionScreenDisplay = new HackSelectionScreenDisplayDesktop(sessionState: sessionState, allowResearchingHacks: true);

			this.settingsIcon = new SettingsIcon(isMobileDisplayType: false);
			
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
				font: GameFont.GameFont20Pt,
				isMobileDisplayType: false);

			this.numberOfHacksResearchedInPreviousFrame = sessionState.GetResearchedHacks().Count;
		}

		public void ProcessExtraTime(int milliseconds)
		{
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
			if (displayType != DisplayType.Desktop)
				return new HackSelectionScreenMobileFrame(globalState: this.globalState, sessionState: this.sessionState, display: displayProcessing);

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

				if (keyboardInput.IsPressed(Key.Three) && !previousKeyboardInput.IsPressed(Key.Three))
				{
					this.sessionState.AddCompletedObjectives(new HashSet<Objective>()
						{
							Objective.DefeatComputer,
							Objective.DefeatComputerByPlayingAtMost25Moves,
							Objective.DefeatComputerWith5QueensOnTheBoard,
							Objective.CheckmateUsingAKnight,
							Objective.PromoteAPieceToABishop,
							Objective.LaunchANuke,
							Objective.WinFinalBattle
						});
					this.globalState.SaveData(sessionState: this.sessionState, soundVolume: soundOutput.GetSoundVolume());
				}

				if (keyboardInput.IsPressed(Key.Four) && !previousKeyboardInput.IsPressed(Key.Four))
				{
					this.sessionState.AddCompletedObjectives(new HashSet<Objective>()
						{
							Objective.PlayAStupidOpening,
							Objective.NukeYourOwnPieces,
							Objective.WinByCastlingVeryLongAndPromotingRookToQueen
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
				return new HackExplanationDesktopFrame(globalState: this.globalState, sessionState: this.sessionState, hack: rightClickedHack.Value, underlyingFrame: this);
			}

			bool clickedContinueButton = this.continueButton.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);
			if (clickedContinueButton)
			{
				this.globalState.SaveData(sessionState: this.sessionState, soundVolume: soundOutput.GetSoundVolume());
				soundOutput.PlaySound(GameSound.Click);
				return new ObjectivesScreenDesktopFrame(globalState: this.globalState, sessionState: this.sessionState);
			}

			SettingsIcon.SettingsIconStatus settingsIconStatus = this.settingsIcon.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput, ignoreMouse: false, displayProcessing: displayProcessing);
			
			if (settingsIconStatus.HasClicked)
			{
				this.globalState.SaveData(sessionState: this.sessionState, soundVolume: soundOutput.GetSoundVolume());
				soundOutput.PlaySound(GameSound.Click);
				return new SettingsMenuDesktopFrame(globalState: this.globalState, sessionState: this.sessionState, underlyingFrame: this, showPausedText: false, shouldRenderUnderlyingFrame: true);
			}

			if (keyboardInput.IsPressed(Key.Esc) && !previousKeyboardInput.IsPressed(Key.Esc))
			{
				this.globalState.SaveData(sessionState: this.sessionState, soundVolume: soundOutput.GetSoundVolume());
				soundOutput.PlaySound(GameSound.Click);
				return new SettingsMenuDesktopFrame(globalState: this.globalState, sessionState: this.sessionState, underlyingFrame: this, showPausedText: false, shouldRenderUnderlyingFrame: true);
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

		public void Render(IDisplayOutput<GameImage, GameFont> displayOutput)
		{
			displayOutput.DrawRectangle(
				x: 0,
				y: 0,
				width: GlobalConstants.DESKTOP_WINDOW_WIDTH,
				height: GlobalConstants.DESKTOP_WINDOW_HEIGHT,
				color: new DTColor(223, 220, 217),
				fill: true);

			this.hackSelectionScreenDisplay.RenderButtons(displayOutput: displayOutput);

			this.settingsIcon.Render(displayOutput: displayOutput);

			this.continueButton.Render(displayOutput: displayOutput);

			this.hackSelectionScreenDisplay.RenderHoverDisplay(displayOutput: displayOutput);
		}

		public void RenderMusic(IMusicOutput<GameMusic> musicOutput)
		{
			this.globalState.RenderMusic(musicOutput: musicOutput);
		}
	}
}
