
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System;
	using System.Collections.Generic;

	public class HackSelectionScreenDisplayMobile
	{
		private class TabButton
		{
			public TabButton(
				int x,
				int y,
				int width,
				int height,
				HackSelectionScreenMobileTab tab,
				string tabName,
				int textWidth,
				int textHeight)
			{
				this.X = x;
				this.Y = y;
				this.Width = width;
				this.Height = height;
				this.Tab = tab;
				this.TabName = tabName;
				this.TextWidth = textWidth;
				this.TextHeight = textHeight;
			}

			public void SetX(int x)
			{
				this.X = x;
			}

			public void SetY(int y)
			{
				this.Y = y;
			}

			public void SetWidth(int width)
			{
				this.Width = width;
			}

			public void SetHeight(int height)
			{
				this.Height = height;
			}

			public int X { get; private set; }
			public int Y { get; private set; }
			public int Width { get; private set; }
			public int Height { get; private set; }
			public HackSelectionScreenMobileTab Tab { get; private set; }
			public string TabName { get; private set; }
			public int TextWidth { get; private set; }
			public int TextHeight { get; private set; }
		}

		private SessionState sessionState;

		private Dictionary<HackSelectionScreenMobileTab, List<List<HackDisplayMobile>>> hackDisplays;

		private List<TabButton> tabButtons;
		
		private HackSelectionScreenMobileTab? clickTab;

		private Button resetHacksButton;

		private bool allowResearchingHacks;
		
		public HackSelectionScreenDisplayMobile(SessionState sessionState, bool allowResearchingHacks, IDisplayProcessing<GameImage> display)
		{
			this.tabButtons = new List<TabButton>()
			{
				new TabButton(x: 0, y: 0, width: 10, height: 10, tab: HackSelectionScreenMobileTab.Tactics, tabName: "Tactics", textWidth: 97, textHeight: 27),
				new TabButton(x: 0, y: 0, width: 10, height: 10, tab: HackSelectionScreenMobileTab.Eliteness, tabName: "Eliteness", textWidth: 124, textHeight: 27),
				new TabButton(x: 0, y: 0, width: 10, height: 10, tab: HackSelectionScreenMobileTab.RuleWarping, tabName: "Rule Warping", textWidth: 173, textHeight: 27)
			};

			this.clickTab = null;

			this.sessionState = sessionState;

			this.allowResearchingHacks = allowResearchingHacks;

			this.resetHacksButton = new Button(
				x: 20,
				y: 50,
				width: 10,
				height: 70,
				backgroundColor: new DTColor(200, 200, 200),
				hoverColor: ColorThemeUtil.GetHoverColor(colorTheme: sessionState.GetColorTheme()),
				clickColor: ColorThemeUtil.GetClickColor(colorTheme: sessionState.GetColorTheme()),
				text: "Reset hacks",
				textXOffset: 0,
				textYOffset: 23,
				font: GameFont.GameFont18Pt,
				isMobileDisplayType: true);

			this.hackDisplays = new Dictionary<HackSelectionScreenMobileTab, List<List<HackDisplayMobile>>>();
			this.hackDisplays[HackSelectionScreenMobileTab.Tactics] = new List<List<HackDisplayMobile>>()
			{
				new List<HackDisplayMobile>(),
				new List<HackDisplayMobile>(),
				new List<HackDisplayMobile>()
			};
			this.hackDisplays[HackSelectionScreenMobileTab.Eliteness] = new List<List<HackDisplayMobile>>()
			{
				new List<HackDisplayMobile>(),
				new List<HackDisplayMobile>(),
				new List<HackDisplayMobile>()
			};
			this.hackDisplays[HackSelectionScreenMobileTab.RuleWarping] = new List<List<HackDisplayMobile>>()
			{
				new List<HackDisplayMobile>(),
				new List<HackDisplayMobile>(),
				new List<HackDisplayMobile>()
			};

			Action<HackSelectionScreenMobileTab, Hack, int, HackDisplay.Theme> addHackDisplay = (tab, hack, row, theme) =>
			{
				this.hackDisplays[tab][row].Add(new HackDisplayMobile(
					hack: hack,
					x: 0,
					y: 0,
					allowResearchingHacks: allowResearchingHacks,
					sessionState: sessionState,
					theme: theme));
			};

			Action<Hack, int> addHackDisplayInFirstSet = (hack, row) =>
			{
				addHackDisplay(HackSelectionScreenMobileTab.Tactics, hack, row, HackDisplay.Theme.Green);
			};

			Action<Hack, int> addHackDisplayInSecondSet = (hack, row) =>
			{
				addHackDisplay(HackSelectionScreenMobileTab.Eliteness, hack, row, HackDisplay.Theme.Blue);
			};

			Action<Hack, int> addHackDisplayInThirdSet = (hack, row) =>
			{
				addHackDisplay(HackSelectionScreenMobileTab.RuleWarping, hack, row, HackDisplay.Theme.Purple);
			};
			
			addHackDisplayInFirstSet(Hack.PawnsCanMoveThreeSpacesInitially, 0);
			addHackDisplayInFirstSet(Hack.KnightsCanMakeLargeKnightsMove, 1);
			addHackDisplayInFirstSet(Hack.RooksCanCaptureLikeCannons, 1);
			addHackDisplayInFirstSet(Hack.TacticalNuke, 2);

			addHackDisplayInSecondSet(Hack.ExtraPawnFirst, 0);
			addHackDisplayInSecondSet(Hack.ExtraPawnSecond, 0);
			addHackDisplayInSecondSet(Hack.ExtraQueen, 1);
			addHackDisplayInSecondSet(Hack.OpponentMustCaptureWhenPossible, 2);
			addHackDisplayInSecondSet(Hack.PawnsDestroyCapturingPiece, 2);

			addHackDisplayInThirdSet(Hack.SuperCastling, 0);
			addHackDisplayInThirdSet(Hack.StalemateIsVictory, 0);
			addHackDisplayInThirdSet(Hack.RooksCanMoveLikeBishops, 1);
			addHackDisplayInThirdSet(Hack.QueensCanMoveLikeKnights, 1);
			addHackDisplayInThirdSet(Hack.SuperEnPassant, 2);
			addHackDisplayInThirdSet(Hack.AnyPieceCanPromote, 2);

			this.UpdateCoordinates(display: display);
		}

		private void UpdateCoordinates(IDisplayProcessing<GameImage> display)
		{
			bool isLandscape = display.IsMobileInLandscapeOrientation();
			
			if (isLandscape)
			{
				this.resetHacksButton.SetWidth(200);
				this.resetHacksButton.SetTextXOffset(27);

				this.tabButtons[0].SetX(50);
				this.tabButtons[1].SetX(50);
				this.tabButtons[2].SetX(50);

				this.tabButtons[0].SetY(440);
				this.tabButtons[1].SetY(340);
				this.tabButtons[2].SetY(240);

				int tabButtonWidth = display.GetMobileScreenWidth() / 4;
				this.tabButtons[0].SetWidth(tabButtonWidth);
				this.tabButtons[1].SetWidth(tabButtonWidth);
				this.tabButtons[2].SetWidth(tabButtonWidth);

				this.tabButtons[0].SetHeight(100);
				this.tabButtons[1].SetHeight(100);
				this.tabButtons[2].SetHeight(100);

				int xStartOfTabContent = 50 + tabButtonWidth;

				int widthOfTabContent = display.GetMobileScreenWidth() - 50 - 50 - tabButtonWidth;

				int amountOfWhitespace = widthOfTabContent - 255 - 255;

				int xColumn0 = xStartOfTabContent + amountOfWhitespace / 3;
				int xColumn1 = xStartOfTabContent + amountOfWhitespace / 3 + 255 + amountOfWhitespace / 3;

				foreach (List<List<HackDisplayMobile>> hackDisplaysInTab in this.hackDisplays.Values)
				{
					for (int row = 0; row < 3; row++)
					{
						for (int column = 0; column < hackDisplaysInTab[row].Count; column++)
						{
							HackDisplayMobile hackDisplay = hackDisplaysInTab[row][column];

							hackDisplay.SetX(column == 0 ? xColumn0 : xColumn1);
							hackDisplay.SetY(row == 0 ? 431 : (row == 1 ? 431 - 127 : 431 - 127 - 126));
						}
					}
				}
			}
			else
			{
				this.resetHacksButton.SetWidth(160);
				this.resetHacksButton.SetTextXOffset(7);

				int tabButtonWidth = 190;

				this.tabButtons[0].SetX(60);
				this.tabButtons[1].SetX(60 + tabButtonWidth);
				this.tabButtons[2].SetX(60 + tabButtonWidth * 2);

				int tabButtonY = display.GetMobileScreenHeight() - 270;

				this.tabButtons[0].SetY(tabButtonY);
				this.tabButtons[1].SetY(tabButtonY);
				this.tabButtons[2].SetY(tabButtonY);

				this.tabButtons[0].SetWidth(tabButtonWidth);
				this.tabButtons[1].SetWidth(tabButtonWidth);
				this.tabButtons[2].SetWidth(tabButtonWidth);

				this.tabButtons[0].SetHeight(70);
				this.tabButtons[1].SetHeight(70);
				this.tabButtons[2].SetHeight(70);
				
				int amountOfWhitespace = (tabButtonY - 158) - 100 - 100 - 100;

				int yRow2 = 158 + amountOfWhitespace / 4;
				int yRow1 = yRow2 + 100 + amountOfWhitespace / 4;
				int yRow0 = yRow1 + 100 + amountOfWhitespace / 4;

				foreach (List<List<HackDisplayMobile>> hackDisplaysInTab in this.hackDisplays.Values)
				{
					for (int row = 0; row < 3; row++)
					{
						for (int column = 0; column < hackDisplaysInTab[row].Count; column++)
						{
							HackDisplayMobile hackDisplay = hackDisplaysInTab[row][column];

							hackDisplay.SetX(column == 0 ? 80 : 365);
							hackDisplay.SetY(row == 0 ? yRow0 : (row == 1 ? yRow1 : yRow2));
						}
					}
				}
			}
		}

		/// <summary>
		/// Returns the hack that the player clicked "More Details" on (or null if the player didn't click "More Details" on any hacks)
		/// </summary>
		public Hack? ProcessFrame(
			IMouse mouseInput,
			IMouse previousMouseInput,
			IDisplayProcessing<GameImage> displayProcessing,
			ISoundOutput<GameSound> soundOutput)
		{
			this.UpdateCoordinates(display: displayProcessing);

			Hack? clickedOnMoreDetailsHack = null;
			
			int mouseX = mouseInput.GetX();
			int mouseY = mouseInput.GetY();

			HackSelectionScreenMobileTab? hoverTab = null;
			foreach (TabButton tabButton in this.tabButtons)
			{
				if (tabButton.X <= mouseX && mouseX <= tabButton.X + tabButton.Width && tabButton.Y <= mouseY && mouseY <= tabButton.Y + tabButton.Height)
					hoverTab = tabButton.Tab;
			}

			if (mouseInput.IsLeftMouseButtonPressed() && !previousMouseInput.IsLeftMouseButtonPressed())
			{
				if (hoverTab != null)
					this.clickTab = hoverTab;
			}

			if (this.clickTab != null && !mouseInput.IsLeftMouseButtonPressed() && previousMouseInput.IsLeftMouseButtonPressed())
			{
				if (hoverTab.HasValue && hoverTab.Value == this.clickTab.Value)
				{
					soundOutput.PlaySound(GameSound.Click);
					this.sessionState.SetHackSelectionScreenMobileTab(this.clickTab.Value);
				}

				this.clickTab = null;
			}

			foreach (List<HackDisplayMobile> hackDisplayList in this.hackDisplays[this.sessionState.GetHackSelectionScreenMobileTab()])
			{
				foreach (HackDisplayMobile hackDisplay in hackDisplayList)
				{
					bool hasClickedOnMoreDetails = hackDisplay.ProcessFrame(
						mouseInput: mouseInput,
						previousMouseInput: previousMouseInput,
						soundOutput: soundOutput,
						displayProcessing: displayProcessing);

					if (hasClickedOnMoreDetails)
						clickedOnMoreDetailsHack = hackDisplay.GetHack();
				}
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

			return clickedOnMoreDetailsHack;
		}

		public void RenderButtons(IDisplayOutput<GameImage, GameFont> displayOutput)
		{
			bool isLandscape = displayOutput.IsMobileInLandscapeOrientation();

			DTColor gray = new DTColor(150, 150, 150);

			if (isLandscape)
			{
				displayOutput.DrawText(
					x: displayOutput.GetMobileScreenWidth() / 2 - 90,
					y: 675,
					text: "Hacks",
					font: GameFont.GameFont48Pt,
					color: DTColor.Black());

				int xStartOfTabContent = this.tabButtons[0].X + this.tabButtons[0].Width;

				displayOutput.DrawRectangle(
					x: xStartOfTabContent,
					y: 158,
					width: displayOutput.GetMobileScreenWidth() - xStartOfTabContent - 50,
					height: 400,
					color: new DTColor(235, 235, 235),
					fill: true);

				displayOutput.DrawRectangle(
					x: xStartOfTabContent,
					y: 158,
					width: displayOutput.GetMobileScreenWidth() - xStartOfTabContent - 50,
					height: 400,
					color: gray,
					fill: false);
			}
			else
			{
				displayOutput.DrawText(
					x: 260,
					y: displayOutput.GetMobileScreenHeight() - 100,
					text: "Hacks",
					font: GameFont.GameFont48Pt,
					color: DTColor.Black());

				displayOutput.DrawRectangle(
					x: 50,
					y: 158,
					width: 600,
					height: this.tabButtons[0].Y - 158,
					color: new DTColor(235, 235, 235),
					fill: true);

				displayOutput.DrawRectangle(
					x: 50,
					y: 158,
					width: 600,
					height: this.tabButtons[0].Y - 158,
					color: gray,
					fill: false);
			}

			foreach (TabButton tabButton in this.tabButtons)
			{
				bool isSelectedTab = this.sessionState.GetHackSelectionScreenMobileTab() == tabButton.Tab;

				displayOutput.DrawRectangle(
					x: tabButton.X,
					y: tabButton.Y,
					width: tabButton.Width,
					height: tabButton.Height,
					color: isSelectedTab
						? new DTColor(235, 235, 235)
						: (this.clickTab.HasValue && this.clickTab.Value == tabButton.Tab ? ColorThemeUtil.GetClickColor(colorTheme: this.sessionState.GetColorTheme()) : new DTColor(200, 200, 200)),
					fill: true);

				displayOutput.DrawRectangle(
					x: tabButton.X,
					y: tabButton.Y,
					width: tabButton.Width,
					height: tabButton.Height,
					color: gray,
					fill: false);

				displayOutput.DrawText(
					x: tabButton.X + tabButton.Width / 2 - tabButton.TextWidth / 2,
					y: tabButton.Y + tabButton.Height / 2 + tabButton.TextHeight / 2,
					text: tabButton.TabName,
					font: GameFont.GameFont20Pt,
					color: DTColor.Black());

				if (isSelectedTab)
				{
					if (isLandscape)
					{
						displayOutput.DrawRectangle(
							x: tabButton.X + tabButton.Width - 2,
							y: tabButton.Y + 1,
							width: 5,
							height: tabButton.Height - 2,
							color: new DTColor(235, 235, 235),
							fill: true);
					}
					else
					{
						displayOutput.DrawRectangle(
							x: tabButton.X + 1,
							y: tabButton.Y - 2,
							width: tabButton.Width - 2,
							height: 5,
							color: new DTColor(235, 235, 235),
							fill: true);
					}
				}
			}

			foreach (List<HackDisplayMobile> hackDisplayList in this.hackDisplays[this.sessionState.GetHackSelectionScreenMobileTab()])
			{
				foreach (HackDisplayMobile hackDisplay in hackDisplayList)
				{
					hackDisplay.RenderButtonDisplay(displayOutput: displayOutput);
				}
			}

			if (this.allowResearchingHacks)
			{
				if (isLandscape)
				{
					displayOutput.DrawText(
						x: displayOutput.GetMobileScreenWidth() / 2 - 250,
						y: 122,
						text: "Hack points remaining: " + this.sessionState.GetUnusedHackPoints().ToStringCultureInvariant() + "\n"
							+ "Get more hack points by winning games" + "\n"
							+ "and completing objectives!",
						font: GameFont.GameFont16Pt,
						color: DTColor.Black());
				}
				else
				{
					displayOutput.DrawText(
						x: 200,
						y: 122,
						text: "Hack points remaining: " + this.sessionState.GetUnusedHackPoints().ToStringCultureInvariant() + "\n"
							+ "Get more hack points by winning" + "\n"
							+ "games and completing" + "\n"
							+ "objectives!",
						font: GameFont.GameFont14Pt,
						color: DTColor.Black());
				}

				this.resetHacksButton.Render(displayOutput: displayOutput);
			}
		}
	}
}
