
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System.Collections.Generic;

	public class Button
	{
		private int x;
		private int y;
		private int width;
		private int height;
		private DTColor backgroundColor;
		private DTColor hoverColor;
		private DTColor clickColor;
		private string text;
		private int textXOffset;
		private int textYOffset;
		private ChessFont font;

		private bool isHover;
		private bool isClicked;

		public Button(
			int x,
			int y,
			int width,
			int height,
			DTColor backgroundColor,
			DTColor hoverColor,
			DTColor clickColor,
			string text,
			int textXOffset,
			int textYOffset,
			ChessFont font)
		{
			this.x = x;
			this.y = y;
			this.width = width;
			this.height = height;
			this.backgroundColor = backgroundColor;
			this.hoverColor = hoverColor;
			this.clickColor = clickColor;
			this.text = text;
			this.textXOffset = textXOffset;
			this.textYOffset = textYOffset;
			this.font = font;

			this.isHover = false;
			this.isClicked = false;
		}

		public bool IsHover(IMouse mouseInput)
		{
			int mouseX = mouseInput.GetX();
			int mouseY = mouseInput.GetY();
			return this.x <= mouseX && mouseX <= this.x + this.width && this.y <= mouseY && mouseY <= this.y + this.height;
		}

		/// <summary>
		/// Returns whether or not the user has clicked the button
		/// </summary>
		public bool ProcessFrame(
			IMouse mouseInput,
			IMouse previousMouseInput)
		{
			bool inRange = this.IsHover(mouseInput);

			this.isHover = inRange;

			if (mouseInput.IsLeftMouseButtonPressed() && !previousMouseInput.IsLeftMouseButtonPressed())
			{
				if (inRange)
					this.isClicked = true;
			}
			
			if (this.isClicked && !mouseInput.IsLeftMouseButtonPressed() && previousMouseInput.IsLeftMouseButtonPressed())
			{
				this.isClicked = false;

				if (inRange)
					return true;
			}

			return false;
		}

		public void Render(IDisplayOutput<ChessImage, ChessFont> displayOutput)
		{
			displayOutput.DrawRectangle(
				x: this.x,
				y: this.y,
				width: this.width - 1,
				height: this.height - 1,
				color: this.isClicked ? this.clickColor : (this.isHover ? this.hoverColor : this.backgroundColor),
				fill: true);

			displayOutput.DrawRectangle(
				x: this.x,
				y: this.y,
				width: this.width,
				height: this.height,
				color: DTColor.Black(),
				fill: false);

			displayOutput.DrawText(
				x: this.x + this.textXOffset,
				y: this.y + this.height - this.textYOffset,
				text: this.text,
				font: this.font,
				color: DTColor.Black());
		}
	}
}
