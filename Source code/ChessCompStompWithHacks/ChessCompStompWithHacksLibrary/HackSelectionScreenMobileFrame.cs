
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System;
	using System.Collections.Generic;

	public class HackSelectionScreenMobileFrame : IFrame<GameImage, GameFont, GameSound, GameMusic>
	{
		private GlobalState globalState;
		private SessionState sessionState;

		private SettingsIcon settingsIcon;
		
		private Button continueButton;

		private HackSelectionScreenDisplayMobile hackSelectionScreenDisplay;

		private int numberOfHacksResearchedInPreviousFrame;
		private HackSelectionScreenMobileTab hackSelectionScreenMobileTabInPreviousFrame;

		public HackSelectionScreenMobileFrame(GlobalState globalState, SessionState sessionState, IDisplayProcessing<GameImage> display)
		{
			this.globalState = globalState;
			this.sessionState = sessionState;

			this.hackSelectionScreenDisplay = new HackSelectionScreenDisplayMobile(sessionState: sessionState, allowResearchingHacks: true, display: display);

			this.settingsIcon = new SettingsIcon(isMobileDisplayType: true);
			
			this.continueButton = new Button(
				x: 0,
				y: 0,
				width: 10,
				height: 80,
				backgroundColor: new DTColor(235, 235, 235),
				hoverColor: ColorThemeUtil.GetHoverColor(colorTheme: sessionState.GetColorTheme()),
				clickColor: ColorThemeUtil.GetClickColor(colorTheme: sessionState.GetColorTheme()),
				text: "Continue",
				textXOffset: 0,
				textYOffset: 27,
				font: GameFont.GameFont20Pt,
				isMobileDisplayType: true);

			this.numberOfHacksResearchedInPreviousFrame = sessionState.GetResearchedHacks().Count;
			this.hackSelectionScreenMobileTabInPreviousFrame = sessionState.GetHackSelectionScreenMobileTab();

			this.UpdateCoordinates(display: display);
		}

		private void UpdateCoordinates(IDisplayProcessing<GameImage> display)
		{
			if (display.IsMobileInLandscapeOrientation())
			{
				this.continueButton.SetX(display.GetMobileScreenWidth() - 300);
				this.continueButton.SetWidth(200);
				this.continueButton.SetTextXOffset(40);
			}
			else
			{
				this.continueButton.SetX(520);
				this.continueButton.SetWidth(160);
				this.continueButton.SetTextXOffset(20);
			}

			this.continueButton.SetY(50);
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
			if (displayType == DisplayType.Desktop)
				return new HackSelectionScreenDesktopFrame(globalState: this.globalState, sessionState: this.sessionState);

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

			Hack? clickedOnMoreDetailsHack = this.hackSelectionScreenDisplay.ProcessFrame(
				mouseInput: mouseInput,
				previousMouseInput: previousMouseInput,
				displayProcessing: displayProcessing,
				soundOutput: soundOutput);
			
			if (clickedOnMoreDetailsHack != null)
			{
				return new HackExplanationMobileFrame(globalState: this.globalState, sessionState: this.sessionState, hack: clickedOnMoreDetailsHack.Value, underlyingFrame: this);
			}

			bool clickedContinueButton = this.continueButton.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);
			if (clickedContinueButton)
			{
				this.globalState.SaveData(sessionState: this.sessionState, soundVolume: soundOutput.GetSoundVolume());
				soundOutput.PlaySound(GameSound.Click);
				return new ObjectivesScreenMobileFrame(globalState: this.globalState, sessionState: this.sessionState, display: displayProcessing);
			}

			SettingsIcon.SettingsIconStatus settingsIconStatus = this.settingsIcon.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput, ignoreMouse: false, displayProcessing: displayProcessing);
			
			if (settingsIconStatus.HasClicked)
			{
				this.globalState.SaveData(sessionState: this.sessionState, soundVolume: soundOutput.GetSoundVolume());
				soundOutput.PlaySound(GameSound.Click);
				return new SettingsMenuMobileFrame(globalState: this.globalState, sessionState: this.sessionState, underlyingFrame: this, showPausedText: false, display: displayProcessing, shouldRenderUnderlyingFrameInitially: true);
			}

			if (keyboardInput.IsPressed(Key.Esc) && !previousKeyboardInput.IsPressed(Key.Esc))
			{
				this.globalState.SaveData(sessionState: this.sessionState, soundVolume: soundOutput.GetSoundVolume());
				soundOutput.PlaySound(GameSound.Click);
				return new SettingsMenuMobileFrame(globalState: this.globalState, sessionState: this.sessionState, underlyingFrame: this, showPausedText: false, display: displayProcessing, shouldRenderUnderlyingFrameInitially: true);
			}

			HashSet<Hack> researchedHacks = this.sessionState.GetResearchedHacks();

			if (researchedHacks.Count != this.numberOfHacksResearchedInPreviousFrame)
				this.globalState.SaveData(sessionState: this.sessionState, soundVolume: soundOutput.GetSoundVolume());
			
			if (this.sessionState.GetHackSelectionScreenMobileTab() != this.hackSelectionScreenMobileTabInPreviousFrame)
				this.globalState.SaveData(sessionState: this.sessionState, soundVolume: soundOutput.GetSoundVolume());

			this.numberOfHacksResearchedInPreviousFrame = researchedHacks.Count;
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

			this.continueButton.Render(displayOutput: displayOutput);
		}

		public void RenderMusic(IMusicOutput<GameMusic> musicOutput)
		{
			this.globalState.RenderMusic(musicOutput: musicOutput);
		}
	}
}
