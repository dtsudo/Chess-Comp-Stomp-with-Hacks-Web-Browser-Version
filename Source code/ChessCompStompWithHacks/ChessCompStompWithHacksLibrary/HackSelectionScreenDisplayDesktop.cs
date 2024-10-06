﻿
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System;
	using System.Collections.Generic;

	public class HackSelectionScreenDisplayDesktop
	{
		private SessionState sessionState;

		private List<HackDisplayDesktop> hackDisplays;

		private Button resetHacksButton;

		private bool allowResearchingHacks;

		private const int FIRST_SET_OF_HACKS_X = 8;
		private const int SECOND_SET_OF_HACKS_X = 8 + 328;
		private const int THIRD_SET_OF_HACKS_X = 8 + 328 + 328;

		public HackSelectionScreenDisplayDesktop(SessionState sessionState, bool allowResearchingHacks)
		{
			this.sessionState = sessionState;

			this.allowResearchingHacks = allowResearchingHacks;

			this.resetHacksButton = new Button(
				x: 8,
				y: 70,
				width: 170,
				height: 40,
				backgroundColor: new DTColor(200, 200, 200),
				hoverColor: ColorThemeUtil.GetHoverColor(colorTheme: sessionState.GetColorTheme()),
				clickColor: ColorThemeUtil.GetClickColor(colorTheme: sessionState.GetColorTheme()),
				text: "Reset hacks",
				textXOffset: 18,
				textYOffset: 9,
				font: GameFont.GameFont16Pt,
				isMobileDisplayType: false);

			this.hackDisplays = new List<HackDisplayDesktop>();

			Action<Hack, int, int, HackDisplay.Theme> addHackDisplay = (hack, x, y, theme) =>
			{
				this.hackDisplays.Add(new HackDisplayDesktop(
					hack: hack,
					x: x,
					y: y,
					allowResearchingHacks: allowResearchingHacks,
					sessionState: sessionState,
					theme: theme));
			};

			Action<Hack, int, int> addHackDisplayInFirstSet = (hack, x, y) =>
			{
				addHackDisplay(hack, FIRST_SET_OF_HACKS_X + x, y, HackDisplay.Theme.Green);
			};

			Action<Hack, int, int> addHackDisplayInSecondSet = (hack, x, y) =>
			{
				addHackDisplay(hack, SECOND_SET_OF_HACKS_X + x, y, HackDisplay.Theme.Blue);
			};

			Action<Hack, int, int> addHackDisplayInThirdSet = (hack, x, y) =>
			{
				addHackDisplay(hack, THIRD_SET_OF_HACKS_X + x, y, HackDisplay.Theme.Purple);
			};

			int firstXOffset = 6;
			int secondXOffset = 6 + 155 + 6;
			
			int firstRow = 431;
			int secondRow = firstRow - 127;
			int thirdRow = secondRow - 126;

			addHackDisplayInFirstSet(Hack.PawnsCanMoveThreeSpacesInitially, firstXOffset, firstRow);
			addHackDisplayInFirstSet(Hack.KnightsCanMakeLargeKnightsMove, firstXOffset, secondRow);
			addHackDisplayInFirstSet(Hack.RooksCanCaptureLikeCannons, secondXOffset, secondRow);
			addHackDisplayInFirstSet(Hack.TacticalNuke, firstXOffset, thirdRow);

			addHackDisplayInSecondSet(Hack.ExtraPawnFirst, firstXOffset, firstRow);
			addHackDisplayInSecondSet(Hack.ExtraPawnSecond, secondXOffset, firstRow);
			addHackDisplayInSecondSet(Hack.ExtraQueen, firstXOffset, secondRow);
			addHackDisplayInSecondSet(Hack.OpponentMustCaptureWhenPossible, firstXOffset, thirdRow);
			addHackDisplayInSecondSet(Hack.PawnsDestroyCapturingPiece, secondXOffset, thirdRow);

			addHackDisplayInThirdSet(Hack.SuperCastling, firstXOffset, firstRow);
			addHackDisplayInThirdSet(Hack.StalemateIsVictory, secondXOffset, firstRow);
			addHackDisplayInThirdSet(Hack.RooksCanMoveLikeBishops, firstXOffset, secondRow);
			addHackDisplayInThirdSet(Hack.QueensCanMoveLikeKnights, secondXOffset, secondRow);
			addHackDisplayInThirdSet(Hack.SuperEnPassant, firstXOffset, thirdRow);
			addHackDisplayInThirdSet(Hack.AnyPieceCanPromote, secondXOffset, thirdRow);
		}

		/// <summary>
		/// Returns the hack that the player right-clicked (or null if the player didn't right-click any hacks)
		/// </summary>
		public Hack? ProcessFrame(
			IMouse mouseInput,
			IMouse previousMouseInput,
			IDisplayProcessing<GameImage> displayProcessing,
			ISoundOutput<GameSound> soundOutput)
		{
			Hack? rightClickedHack = null;

			foreach (HackDisplayDesktop hackDisplay in this.hackDisplays)
			{
				bool hasRightClicked = hackDisplay.ProcessFrame(
					mouseInput: mouseInput, 
					previousMouseInput: previousMouseInput, 
					soundOutput: soundOutput, 
					displayProcessing: displayProcessing);

				if (hasRightClicked)
					rightClickedHack = hackDisplay.GetHack();
			}

			if (this.allowResearchingHacks)
			{
				bool clickedResetHacksButton = this.resetHacksButton.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);
				if (clickedResetHacksButton)
				{
					soundOutput.PlaySound(GameSound.Click);
					this.sessionState.ResetResearchedHacks();
				}
			}

			return rightClickedHack;
		}

		public void RenderButtons(IDisplayOutput<GameImage, GameFont> displayOutput)
		{
			displayOutput.DrawText(
				x: 436,
				y: 675,
				text: "Hacks",
				font: GameFont.GameFont32Pt,
				color: DTColor.Black());

			DTColor gray = new DTColor(150, 150, 150);

			displayOutput.DrawRectangle(
				x: FIRST_SET_OF_HACKS_X,
				y: 150,
				width: 329,
				height: 450,
				color: gray,
				fill: false);
			
			displayOutput.DrawRectangle(
				x: SECOND_SET_OF_HACKS_X,
				y: 150,
				width: 329,
				height: 450,
				color: gray,
				fill: false);
			
			displayOutput.DrawRectangle(
				x: THIRD_SET_OF_HACKS_X,
				y: 150,
				width: 329,
				height: 450,
				color: gray,
				fill: false);
			
			displayOutput.DrawText(
				x: 122,
				y: 590,
				text: "Tactics",
				font: GameFont.GameFont20Pt,
				color: DTColor.Black());
			
			displayOutput.DrawText(
				x: 435,
				y: 590,
				text: "Eliteness",
				font: GameFont.GameFont20Pt,
				color: DTColor.Black());
			
			displayOutput.DrawText(
				x: 740,
				y: 590,
				text: "Rule warping",
				font: GameFont.GameFont20Pt,
				color: DTColor.Black());

			foreach (HackDisplayDesktop hackDisplay in this.hackDisplays)
				hackDisplay.RenderButtonDisplay(displayOutput: displayOutput);

			if (this.allowResearchingHacks)
			{
				displayOutput.DrawText(
					x: 250,
					y: 122,
					text: "Hack points remaining: " + this.sessionState.GetUnusedHackPoints().ToStringCultureInvariant() + "\n"
						+ "Get more hack points by winning games" + "\n"
						+ "and completing objectives!",
					font: GameFont.GameFont16Pt,
					color: DTColor.Black());

				this.resetHacksButton.Render(displayOutput: displayOutput);
			}
		}

		public void RenderHoverDisplay(IDisplayOutput<GameImage, GameFont> displayOutput)
		{
			foreach (HackDisplayDesktop hackDisplay in this.hackDisplays)
				hackDisplay.RenderHoverDisplay(displayOutput: displayOutput);
		}
	}
}
