
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System;

	public class HackDisplay
	{
		private Hack hack;
		private int x;
		private int y;
		private SessionState sessionState;

		private bool isHover;
		private bool isLeftClick;
		private bool isRightClick;

		private int mouseX;
		private int mouseY;

		private IMouse previousMouseInput;

		private bool allowResearchingHacks;

		public enum Theme
		{
			Blue,
			Green,
			Purple
		}

		private Theme theme;

		private const int WIDTH = 155;
		private const int HEIGHT = 100;

		public HackDisplay(
			Hack hack,
			int x,
			int y,
			bool allowResearchingHacks,
			SessionState sessionState,
			Theme theme)
		{
			this.hack = hack;
			this.x = x;
			this.y = y;
			this.allowResearchingHacks = allowResearchingHacks;
			this.sessionState = sessionState;
			this.theme = theme;

			this.mouseX = 0;
			this.mouseY = 0;

			this.isHover = false;
			this.isLeftClick = false;
			this.isRightClick = false;

			this.previousMouseInput = null;
		}

		public Hack GetHack()
		{
			return this.hack;
		}

		/// <summary>
		/// Returns true iff the player right-clicked the hack
		/// </summary>
		public bool ProcessFrame(
			IMouse mouseInput,
			IMouse previousMouseInput,
			ISoundOutput<GameSound> soundOutput,
			IDisplayProcessing<GameImage> displayProcessing)
		{
			if (this.previousMouseInput != null)
				previousMouseInput = this.previousMouseInput;

			this.previousMouseInput = new CopiedMouse(mouse: mouseInput);

			int mouseX = mouseInput.GetX();
			int mouseY = mouseInput.GetY();

			this.mouseX = mouseX;
			this.mouseY = mouseY;

			bool isHover = this.x <= mouseX && mouseX <= this.x + WIDTH && this.y <= mouseY && mouseY <= this.y + HEIGHT;

			this.isHover = isHover;
			if (mouseInput.IsLeftMouseButtonPressed() && !previousMouseInput.IsLeftMouseButtonPressed() && isHover)
				this.isLeftClick = true;

			if (mouseInput.IsRightMouseButtonPressed() && !previousMouseInput.IsRightMouseButtonPressed() && isHover)
				this.isRightClick = true;

			if (this.isLeftClick && !mouseInput.IsLeftMouseButtonPressed() && previousMouseInput.IsLeftMouseButtonPressed())
			{
				this.isLeftClick = false;

				if (this.allowResearchingHacks)
				{
					if (isHover)
						soundOutput.PlaySound(GameSound.Click);

					if (isHover && this.CanAffordHack())
						this.sessionState.AddResearchedHack(this.hack);
				}
			}

			bool returnValue = false;

			if (this.isRightClick && !mouseInput.IsRightMouseButtonPressed() && previousMouseInput.IsRightMouseButtonPressed())
			{
				this.isRightClick = false;

				if (isHover)
				{
					soundOutput.PlaySound(GameSound.Click);
					returnValue = true;
				}
			}

			return returnValue;
		}

		private bool CanAffordHack()
		{
			return this.sessionState.GetUnusedHackPoints() >= this.hack.GetHackCost();
		}

		public void RenderHoverDisplay(IDisplayOutput<GameImage, GameFont> displayOutput)
		{
			if (this.isHover)
			{
				HackUtil.HackDescription hackDescription = this.hack.GetHackDescriptionForHackSelectionScreen();
				string text = hackDescription.Description + "\n\n" + "Right click for more details";

				int numberOfNewLines = 0;
				for (int i = 0; i < text.Length; i++)
				{
					if (text[i] == '\n')
						numberOfNewLines++;
				}

				int width = Math.Max(hackDescription.Width, 320);
				int height = 19 * (numberOfNewLines + 1) + 20;

				int x;
				if (this.mouseX + width > GlobalConstants.WINDOW_WIDTH)
					x = this.mouseX - width;
				else
					x = this.mouseX;

				int y;
				if (this.mouseY - height < 0)
					y = this.mouseY;
				else
					y = this.mouseY - height;

				displayOutput.DrawRectangle(
					x: x,
					y: y,
					width: width - 1,
					height: height - 1,
					color: ColorThemeUtil.GetTextBackgroundColor(colorTheme: this.sessionState.GetColorTheme()),
					fill: true);
				displayOutput.DrawRectangle(
					x: x,
					y: y,
					width: width,
					height: height,
					color: DTColor.Black(),
					fill: false);

				displayOutput.DrawText(
					x: x + 25,
					y: y + height - 10,
					text: text,
					font: GameFont.GameFont14Pt,
					color: DTColor.Black());
			}
		}

		private static DTColor GetResearchedHackBackgroundColor(Theme theme)
		{
			switch (theme)
			{
				case Theme.Blue: return new DTColor(196, 234, 255);
				case Theme.Green: return new DTColor(201, 255, 196);
				case Theme.Purple: return new DTColor(202, 196, 255);
				default: throw new Exception();
			}
		}

		public void RenderButtonDisplay(IDisplayOutput<GameImage, GameFont> displayOutput)
		{
			DTColor backgroundColor;

			bool hasResearchedHack = this.sessionState.GetResearchedHacks().Contains(this.hack);
			bool canAffordHack = this.CanAffordHack();

			if (hasResearchedHack)
				backgroundColor = GetResearchedHackBackgroundColor(theme: this.theme);
			else if (canAffordHack && this.isLeftClick && this.allowResearchingHacks)
				backgroundColor = ColorThemeUtil.GetClickColor(colorTheme: this.sessionState.GetColorTheme());
			else if (canAffordHack && this.isHover && this.allowResearchingHacks)
				backgroundColor = ColorThemeUtil.GetHoverColor(colorTheme: this.sessionState.GetColorTheme());
			else if (canAffordHack && this.allowResearchingHacks)
				backgroundColor = new DTColor(235, 235, 235);
			else
				backgroundColor = new DTColor(200, 200, 200);

			displayOutput.DrawRectangle(
				x: this.x,
				y: this.y,
				width: WIDTH - 1,
				height: HEIGHT - 1,
				color: backgroundColor,
				fill: true);

			displayOutput.DrawRectangle(
				x: this.x,
				y: this.y,
				width: WIDTH,
				height: HEIGHT,
				color: DTColor.Black(),
				fill: false);

			displayOutput.DrawText(
				x: this.x + 3,
				y: this.y + 90,
				text: this.hack.GetHackNameForHackSelectionScreen(),
				font: GameFont.GameFont16Pt,
				color: DTColor.Black());

			displayOutput.DrawText(
				x: this.x + 3,
				y: this.y + 39,
				text: "Cost: " + this.hack.GetHackCost().ToStringCultureInvariant() + " points",
				font: GameFont.GameFont12Pt,
				color: new DTColor(128, 128, 128));

			if (hasResearchedHack)
			{
				displayOutput.DrawText(
					x: this.x + 3,
					y: this.y + 20,
					text: "Hack implemented",
					font: GameFont.GameFont12Pt,
					color: new DTColor(128, 128, 128));
			}
		}
	}
}
