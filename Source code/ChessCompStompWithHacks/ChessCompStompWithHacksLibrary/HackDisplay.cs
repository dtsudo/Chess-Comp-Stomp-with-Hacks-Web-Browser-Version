
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System;
	using System.Collections.Generic;

	public class HackDisplay
	{
		private Hack hack;
		private int x;
		private int y;
		private SessionState sessionState;

		private bool isHover;
		private bool isClick;

		private int mouseX;
		private int mouseY;

		private const int WIDTH = 190;
		private const int HEIGHT = 100;

		public HackDisplay(
			Hack hack,
			int x,
			int y,
			SessionState sessionState)
		{
			this.hack = hack;
			this.x = x;
			this.y = y;
			this.sessionState = sessionState;

			this.mouseX = 0;
			this.mouseY = 0;

			this.isHover = false;
			this.isClick = false;
		}

		public void ProcessFrame(
			IMouse mouseInput,
			IMouse previousMouseInput,
			IDisplayProcessing<ChessImage> displayProcessing)
		{
			int mouseX = mouseInput.GetX();
			int mouseY = mouseInput.GetY();

			this.mouseX = mouseX;
			this.mouseY = mouseY;

			bool isHover = this.x <= mouseX && mouseX <= this.x + WIDTH && this.y <= mouseY && mouseY <= this.y + HEIGHT;

			this.isHover = isHover;
			if (mouseInput.IsLeftMouseButtonPressed() && !previousMouseInput.IsLeftMouseButtonPressed() && isHover)
				this.isClick = true;

			if (this.isClick && !mouseInput.IsLeftMouseButtonPressed() && previousMouseInput.IsLeftMouseButtonPressed())
			{
				this.isClick = false;

				if (isHover && this.CanAffordHack())
					this.sessionState.AddResearchedHack(this.hack);
			}
		}

		private bool CanAffordHack()
		{
			return this.sessionState.GetUnusedHackPoints() >= this.hack.GetHackCost();
		}

		public void RenderHoverDisplay(IDisplayOutput<ChessImage, ChessFont> displayOutput)
		{
			if (this.isHover)
			{
				HackUtil.HackDescription hackDescription = this.hack.GetHackDescriptionForHackSelectionScreen();
				string text = hackDescription.Description;

				int numberOfNewLines = 0;
				for (int i = 0; i < text.Length; i++)
				{
					if (text[i] == '\n')
						numberOfNewLines++;
				}

				int width = hackDescription.Width;
				int height = 19 * (numberOfNewLines + 1) + 20;

				int x;
				if (this.mouseX + width > ChessCompStompWithHacks.WINDOW_WIDTH)
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
					color: new DTColor(255, 245, 171),
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
					font: ChessFont.Fetamont14Pt,
					color: DTColor.Black());
			}
		}

		public void RenderButtonDisplay(IDisplayOutput<ChessImage, ChessFont> displayOutput)
		{
			DTColor backgroundColor;

			bool hasResearchedHack = this.sessionState.GetResearchedHacks().Contains(this.hack);
			bool canAffordHack = this.CanAffordHack();

			if (hasResearchedHack)
				backgroundColor = new DTColor(201, 255, 196);
			else if (canAffordHack && this.isClick)
				backgroundColor = new DTColor(252, 251, 154);
			else if (canAffordHack && this.isHover)
				backgroundColor = new DTColor(250, 249, 200);
			else if (canAffordHack)
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
				x: this.x + 10,
				y: this.y + 90,
				text: this.hack.GetHackNameForHackSelectionScreen(),
				font: ChessFont.Fetamont16Pt,
				color: DTColor.Black());

			displayOutput.DrawText(
				x: this.x + 10,
				y: this.y + 39,
				text: "Cost: " + this.hack.GetHackCost().ToStringCultureInvariant() + " hack points",
				font: ChessFont.Fetamont12Pt,
				color: new DTColor(128, 128, 128));

			if (hasResearchedHack)
			{
				displayOutput.DrawText(
					x: this.x + 10,
					y: this.y + 20,
					text: "Hack implemented",
					font: ChessFont.Fetamont12Pt,
					color: new DTColor(128, 128, 128));
			}
		}
	}
}
