
namespace ChessCompStompWithHacksLibrary
{
	using DTLibrary;
	using System;
	using System.Collections.Generic;

	public class CreditsMobileFrame : IFrame<GameImage, GameFont, GameSound, GameMusic>
	{
		private enum Tab
		{
			DesignAndCoding,
			Images,
			Font,
			Sound,
			Music
		}

		private class TabButton
		{
			public TabButton(
				int x,
				int y,
				int width,
				int height,
				Tab tab,
				string tabName)
			{
				this.X = x;
				this.Y = y;
				this.Width = width;
				this.Height = height;
				this.Tab = tab;
				this.TabName = tabName;
			}

			public void SetX(int x)
			{
				this.X = x;
			}

			public void SetY(int y)
			{
				this.Y = y;
			}

			public int X { get; private set; }
			public int Y { get; private set; }
			public int Width { get; private set; }
			public int Height { get; private set; }
			public Tab Tab { get; private set; }
			public string TabName { get; private set; }
		}

		private GlobalState globalState;
		private SessionState sessionState;

		private List<TabButton> tabButtons;
		private Tab selectedTab;

		private Tab? hoverTab;
		private Tab? clickTab;

		private Button backButton;
		
		public CreditsMobileFrame(GlobalState globalState, SessionState sessionState, IDisplayProcessing<GameImage> display)
		{
			this.globalState = globalState;
			this.sessionState = sessionState;

			this.selectedTab = Tab.DesignAndCoding;
			this.hoverTab = null;
			this.clickTab = null;
			
			this.tabButtons = new List<TabButton>();
			this.tabButtons.Add(new TabButton(x: 20, y: 0, width: 234, height: 40, tab: Tab.DesignAndCoding, tabName: "Design and coding"));
			this.tabButtons.Add(new TabButton(x: 254, y: 0, width: 103, height: 40, tab: Tab.Images, tabName: "Images"));
			this.tabButtons.Add(new TabButton(x: 357, y: 0, width: 82, height: 40, tab: Tab.Font, tabName: "Font"));
			this.tabButtons.Add(new TabButton(x: 439, y: 0, width: 96, height: 40, tab: Tab.Sound, tabName: "Sound"));
			this.tabButtons.Add(new TabButton(x: 535, y: 0, width: 90, height: 40, tab: Tab.Music, tabName: "Music"));

			this.backButton = new Button(
				x: 0,
				y: 0,
				width: 300,
				height: 125,
				backgroundColor: new DTColor(235, 235, 235),
				hoverColor: ColorThemeUtil.GetHoverColor(colorTheme: sessionState.GetColorTheme()),
				clickColor: ColorThemeUtil.GetClickColor(colorTheme: sessionState.GetColorTheme()),
				text: "Back",
				textXOffset: 99,
				textYOffset: 43,
				font: GameFont.GameFont32Pt,
				isMobileDisplayType: true);

			this.UpdateCoordinates(display: display);
		}

		private void UpdateCoordinates(IDisplayProcessing<GameImage> display)
		{
			this.backButton.SetX(display.GetMobileScreenWidth() - 320);
			this.backButton.SetY(20);

			int tabButtonY;
			if (display.IsMobileInLandscapeOrientation())
				tabButtonY = 569;
			else
				tabButtonY = display.GetMobileScreenHeight() - 131;

			foreach (TabButton tabButton in this.tabButtons)
			{
				tabButton.SetY(tabButtonY);
			}
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
				return new CreditsDesktopFrame(globalState: this.globalState, sessionState: this.sessionState);

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

			int mouseX = mouseInput.GetX();
			int mouseY = mouseInput.GetY();

			this.hoverTab = null;
			foreach (TabButton tabButton in this.tabButtons)
			{
				if (tabButton.X <= mouseX && mouseX <= tabButton.X + tabButton.Width && tabButton.Y <= mouseY && mouseY <= tabButton.Y + tabButton.Height)
					this.hoverTab = tabButton.Tab;
			}

			if (mouseInput.IsLeftMouseButtonPressed() && !previousMouseInput.IsLeftMouseButtonPressed())
			{
				if (this.hoverTab != null)
					this.clickTab = this.hoverTab;
			}

			if (this.clickTab != null && !mouseInput.IsLeftMouseButtonPressed() && previousMouseInput.IsLeftMouseButtonPressed())
			{
				if (this.hoverTab.HasValue && this.hoverTab.Value == this.clickTab.Value)
				{
					soundOutput.PlaySound(GameSound.Click);
					this.selectedTab = this.clickTab.Value;
				}

				this.clickTab = null;
			}

			if (keyboardInput.IsPressed(Key.Esc) && !previousKeyboardInput.IsPressed(Key.Esc))
			{
				soundOutput.PlaySound(GameSound.Click);
				return new TitleScreenMobileFrame(globalState: this.globalState, sessionState: this.sessionState, display: displayProcessing);
			}

			bool clickedBackButton = this.backButton.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);
			if (clickedBackButton)
			{
				soundOutput.PlaySound(GameSound.Click);
				return new TitleScreenMobileFrame(globalState: this.globalState, sessionState: this.sessionState, display: displayProcessing);
			}
						
			return this;
		}

		public void ProcessExtraTime(int milliseconds)
		{
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

			displayOutput.DrawText(
				x: displayOutput.GetMobileScreenWidth() / 2 - 78,
				y: displayOutput.GetMobileScreenHeight() - 25,
				text: "Credits",
				font: GameFont.GameFont32Pt,
				color: DTColor.Black());
			
			int tabWidth = displayOutput.GetMobileScreenWidth() - 40;
			int tabHeight = this.tabButtons[0].Y - 165;

			displayOutput.DrawRectangle(
				x: 20,
				y: 165,
				width: tabWidth,
				height: tabHeight,
				color: DTColor.White(),
				fill: true);

			displayOutput.DrawRectangle(
				x: 20,
				y: 165,
				width: tabWidth,
				height: tabHeight,
				color: DTColor.Black(),
				fill: false);

			foreach (TabButton tabButton in this.tabButtons)
			{
				DTColor backgroundColor;

				if (tabButton.Tab == this.selectedTab)
					backgroundColor = DTColor.White();
				else if (this.clickTab.HasValue && this.clickTab.Value == tabButton.Tab)
					backgroundColor = ColorThemeUtil.GetClickColor(colorTheme: this.sessionState.GetColorTheme());
				else
					backgroundColor = new DTColor(200, 200, 200);

				displayOutput.DrawRectangle(
					x: tabButton.X,
					y: tabButton.Y,
					width: tabButton.Width - 1,
					height: tabButton.Height - 1,
					color: backgroundColor,
					fill: true);

				displayOutput.DrawRectangle(
					x: tabButton.X,
					y: tabButton.Y,
					width: tabButton.Width,
					height: tabButton.Height,
					color: DTColor.Black(),
					fill: false);

				if (this.selectedTab == tabButton.Tab)
					displayOutput.DrawRectangle(
						x: tabButton.X + 1,
						y: tabButton.Y - 1,
						width: tabButton.Width - 2,
						height: 3,
						color: DTColor.White(),
						fill: true);

				displayOutput.DrawText(
					x: tabButton.X + 10,
					y: tabButton.Y + tabButton.Height - 10,
					text: tabButton.TabName,
					font: GameFont.GameFont18Pt,
					color: DTColor.Black());
			}

			IDisplayOutput<GameImage, GameFont> translatedDisplay = new TranslatedDisplayOutput<GameImage, GameFont>(
				display: displayOutput,
				xOffsetInPixels: 20,
				yOffsetInPixels: 165);

			if (this.selectedTab == Tab.DesignAndCoding)
				Credits_DesignAndCodingMobile.Render(displayOutput: translatedDisplay, buildType: this.globalState.BuildType, width: tabWidth, height: tabHeight);
			if (this.selectedTab == Tab.Images)
				Credits_ImagesMobile.Render(displayOutput: translatedDisplay, width: tabWidth, height: tabHeight);
			if (this.selectedTab == Tab.Font)
				Credits_FontMobile.Render(displayOutput: translatedDisplay, width: tabWidth, height: tabHeight);
			if (this.selectedTab == Tab.Sound)
				Credits_SoundMobile.Render(displayOutput: translatedDisplay, width: tabWidth, height: tabHeight);
			if (this.selectedTab == Tab.Music)
				Credits_MusicMobile.Render(displayOutput: translatedDisplay, width: tabWidth, height: tabHeight);

			this.backButton.Render(displayOutput: displayOutput);
		}

		public void RenderMusic(IMusicOutput<GameMusic> musicOutput)
		{
			this.globalState.RenderMusic(musicOutput: musicOutput);
		}
	}
}
