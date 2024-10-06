
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System;

	public class HackDisplayMobile
	{
		private Hack hack;
		private int x;
		private int y;
		private SessionState sessionState;
		
		private bool hasClickedOnHack;
		private bool hasClickedOnMoreDetails;

		private IMouse previousMouseInput;

		private bool allowResearchingHacks;
		
		private HackDisplay.Theme theme;

		private const int HACK_DISPLAY_WIDTH = 155;
		private const int MORE_DETAILS_WIDTH = 100;
		private const int HEIGHT = 100;

		public HackDisplayMobile(
			Hack hack,
			int x,
			int y,
			bool allowResearchingHacks,
			SessionState sessionState,
			HackDisplay.Theme theme)
		{
			this.hack = hack;
			this.x = x;
			this.y = y;
			this.allowResearchingHacks = allowResearchingHacks;
			this.sessionState = sessionState;
			this.theme = theme;
			
			this.hasClickedOnHack = false;
			this.hasClickedOnMoreDetails = false;

			this.previousMouseInput = null;
		}

		public Hack GetHack()
		{
			return this.hack;
		}

		public void SetX(int x)
		{
			this.x = x;
		}

		public void SetY(int y)
		{
			this.y = y;
		}

		/// <summary>
		/// Returns true iff the player clicked "More Details"
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
			
			bool isHoverOverHack = this.x <= mouseX && mouseX <= this.x + HACK_DISPLAY_WIDTH && this.y <= mouseY && mouseY <= this.y + HEIGHT;
			bool isHoverOverMoreDetails = this.x + HACK_DISPLAY_WIDTH < mouseX && mouseX <= this.x + HACK_DISPLAY_WIDTH + MORE_DETAILS_WIDTH && this.y <= mouseY && mouseY <= this.y + HEIGHT;

			if (mouseInput.IsLeftMouseButtonPressed() && !previousMouseInput.IsLeftMouseButtonPressed() && isHoverOverHack)
				this.hasClickedOnHack = true;

			if (mouseInput.IsLeftMouseButtonPressed() && !previousMouseInput.IsLeftMouseButtonPressed() && isHoverOverMoreDetails)
				this.hasClickedOnMoreDetails = true;

			if (this.hasClickedOnHack && !mouseInput.IsLeftMouseButtonPressed() && previousMouseInput.IsLeftMouseButtonPressed())
			{
				this.hasClickedOnHack = false;

				if (this.allowResearchingHacks)
				{
					if (isHoverOverHack)
						soundOutput.PlaySound(GameSound.Click);

					if (isHoverOverHack && this.CanAffordHack())
						this.sessionState.AddResearchedHack(this.hack);
				}
			}

			bool returnValue = false;

			if (this.hasClickedOnMoreDetails && !mouseInput.IsLeftMouseButtonPressed() && previousMouseInput.IsLeftMouseButtonPressed())
			{
				this.hasClickedOnMoreDetails = false;

				if (isHoverOverMoreDetails)
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

		public void RenderButtonDisplay(IDisplayOutput<GameImage, GameFont> displayOutput)
		{
			DTColor hackDisplayBackgroundColor;
			DTColor moreDetailsBackgroundColor;

			bool hasResearchedHack = this.sessionState.GetResearchedHacks().Contains(this.hack);
			bool canAffordHack = this.CanAffordHack();

			if (hasResearchedHack)
				hackDisplayBackgroundColor = HackDisplay.GetResearchedHackBackgroundColor(theme: this.theme);
			else if (canAffordHack && this.hasClickedOnHack && this.allowResearchingHacks)
				hackDisplayBackgroundColor = ColorThemeUtil.GetClickColor(colorTheme: this.sessionState.GetColorTheme());
			else if (canAffordHack && this.allowResearchingHacks)
				hackDisplayBackgroundColor = new DTColor(235, 235, 235);
			else
				hackDisplayBackgroundColor = new DTColor(200, 200, 200);

			if (this.hasClickedOnMoreDetails)
				moreDetailsBackgroundColor = ColorThemeUtil.GetClickColor(colorTheme: this.sessionState.GetColorTheme());
			else
				moreDetailsBackgroundColor = hackDisplayBackgroundColor;

			displayOutput.DrawRectangle(
				x: this.x,
				y: this.y,
				width: HACK_DISPLAY_WIDTH,
				height: HEIGHT,
				color: hackDisplayBackgroundColor,
				fill: true);

			displayOutput.DrawRectangle(
				x: this.x,
				y: this.y,
				width: HACK_DISPLAY_WIDTH,
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
			
			displayOutput.DrawRectangle(
				x: this.x + HACK_DISPLAY_WIDTH,
				y: this.y,
				width: MORE_DETAILS_WIDTH,
				height: HEIGHT,
				color: moreDetailsBackgroundColor,
				fill: true);

			displayOutput.DrawRectangle(
				x: this.x + HACK_DISPLAY_WIDTH,
				y: this.y,
				width: MORE_DETAILS_WIDTH,
				height: HEIGHT,
				color: DTColor.Black(),
				fill: false);

			displayOutput.DrawText(
				x: this.x + HACK_DISPLAY_WIDTH + 3,
				y: this.y + 90,
				text: "More" + "\n" + "Details",
				font: GameFont.GameFont16Pt,
				color: DTColor.Black());
		}
	}
}
