
namespace ChessCompStompWithHacksLibrary
{
	using DTLibrary;
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class ScrollableTextDisplay
	{
		private int x;
		private int y;
		private int width;
		private int height;
		private int lineHeightInPixels;
		private int maxLinesOfTextToRender;
		private ChessFont font;
		private DTColor color;

		private Dictionary<int, int> lineIndexToPixelOffsetMapping;
		private int heightOfScrollBar;

		private List<string> textLines;
		private int indexOfFirstLineToRender;

		private bool clickedScrollBar;
		private int clickedMouseY;
		private int clickedScrollBarPixelOffset;

		private const int WIDTH_OF_ARROW = 25;
		private const int HEIGHT_OF_ARROW = 25;

		public ScrollableTextDisplay(
			int x,
			int y,
			int width,
			int height,
			int lineHeightInPixels,
			int maxLinesOfTextToRender,
			ChessFont font,
			DTColor color,
			string text)
		{
			this.maxLinesOfTextToRender = maxLinesOfTextToRender;

			this.textLines = new List<string>();

			string[] textArray = text.Split('\n');
			foreach (string str in textArray)
				this.textLines.Add(str);

			if (this.textLines.Count > maxLinesOfTextToRender)
			{
				this.height = Math.Max(height, HEIGHT_OF_ARROW + HEIGHT_OF_ARROW + 10);

				this.lineIndexToPixelOffsetMapping = new Dictionary<int, int>();
				
				int lineIndex = 0;
				while (true)
				{
					if (lineIndex > this.textLines.Count - maxLinesOfTextToRender)
						break;

					this.lineIndexToPixelOffsetMapping[lineIndex] = lineIndex * (this.height - HEIGHT_OF_ARROW - HEIGHT_OF_ARROW) / this.textLines.Count;

					lineIndex++;
				}

				this.heightOfScrollBar = maxLinesOfTextToRender * (this.height - HEIGHT_OF_ARROW - HEIGHT_OF_ARROW) / this.textLines.Count;
			}
			else
			{
				this.height = height;

				this.lineIndexToPixelOffsetMapping = new Dictionary<int, int>();
				this.lineIndexToPixelOffsetMapping[0] = 0;

				this.heightOfScrollBar = 0;
			}
			
			this.indexOfFirstLineToRender = 0;

			this.clickedScrollBar = false;
			this.clickedMouseY = 0;
			this.clickedScrollBarPixelOffset = 0;

			this.x = x;
			this.y = y;
			this.width = width;
			this.lineHeightInPixels = lineHeightInPixels;
			this.font = font;
			this.color = color;
		}

		public void ProcessFrame(
			IKeyboard keyboardInput,
			IMouse mouseInput,
			IKeyboard previousKeyboardInput,
			IMouse previousMouseInput,
			IDisplayProcessing<ChessImage> displayProcessing,
			ISoundOutput<ChessSound> soundOutput)
		{
			if (this.textLines.Count > this.maxLinesOfTextToRender)
			{
				int mouseX = mouseInput.GetX();
				int mouseY = mouseInput.GetY();

				bool isHoverOverScrollAreaX = this.x + this.width - WIDTH_OF_ARROW <= mouseX && mouseX <= this.x + this.width;

				int topOfScrollBarY = this.y + this.height - HEIGHT_OF_ARROW - this.lineIndexToPixelOffsetMapping[this.indexOfFirstLineToRender];
				int bottomOfScrollBarY = topOfScrollBarY - this.heightOfScrollBar;

				if (mouseInput.IsLeftMouseButtonPressed() && !previousMouseInput.IsLeftMouseButtonPressed()
					&& isHoverOverScrollAreaX
					&& bottomOfScrollBarY <= mouseY && mouseY <= topOfScrollBarY)
				{
					this.clickedScrollBar = true;
					this.clickedMouseY = mouseY;
					this.clickedScrollBarPixelOffset = this.lineIndexToPixelOffsetMapping[this.indexOfFirstLineToRender];
				}

				if (this.clickedScrollBar)
				{
					if (!mouseInput.IsLeftMouseButtonPressed())
						this.clickedScrollBar = false;
					else
					{
						int scrollBarPixelOffsetOverride = this.clickedScrollBarPixelOffset - (mouseY - this.clickedMouseY);

						int bestLineIndex = this.indexOfFirstLineToRender;
						int bestDelta = Math.Abs(this.lineIndexToPixelOffsetMapping[bestLineIndex] - scrollBarPixelOffsetOverride);

						foreach (KeyValuePair<int, int> mapping in this.lineIndexToPixelOffsetMapping.OrderBy(x => x.Key))
						{
							int delta = Math.Abs(mapping.Value - scrollBarPixelOffsetOverride);
							if (delta < bestDelta)
							{
								bestDelta = delta;
								bestLineIndex = mapping.Key;
							}
						}

						this.indexOfFirstLineToRender = bestLineIndex;
					}
				}

				if (keyboardInput.IsPressed(Key.DownArrow) && !previousKeyboardInput.IsPressed(Key.DownArrow))
					this.indexOfFirstLineToRender++;

				if (keyboardInput.IsPressed(Key.UpArrow) && !previousKeyboardInput.IsPressed(Key.UpArrow))
					this.indexOfFirstLineToRender--;

				if (mouseInput.IsLeftMouseButtonPressed() && !previousMouseInput.IsLeftMouseButtonPressed()
						&& isHoverOverScrollAreaX
						&& this.y <= mouseY && mouseY < this.y + HEIGHT_OF_ARROW)
					this.indexOfFirstLineToRender++;

				if (mouseInput.IsLeftMouseButtonPressed() && !previousMouseInput.IsLeftMouseButtonPressed()
						&& isHoverOverScrollAreaX
						&& this.y + this.height - HEIGHT_OF_ARROW < mouseY && mouseY <= this.y + this.height)
					this.indexOfFirstLineToRender--;

				if (mouseInput.IsLeftMouseButtonPressed() && !previousMouseInput.IsLeftMouseButtonPressed()
						&& isHoverOverScrollAreaX
						&& this.y + HEIGHT_OF_ARROW < mouseY && mouseY < bottomOfScrollBarY)
					this.indexOfFirstLineToRender += this.maxLinesOfTextToRender;

				if (mouseInput.IsLeftMouseButtonPressed() && !previousMouseInput.IsLeftMouseButtonPressed()
						&& isHoverOverScrollAreaX
						&& topOfScrollBarY < mouseY && mouseY < this.y + this.height - HEIGHT_OF_ARROW)
					this.indexOfFirstLineToRender -= this.maxLinesOfTextToRender;

				if (this.indexOfFirstLineToRender < 0)
					this.indexOfFirstLineToRender = 0;
				if (this.indexOfFirstLineToRender > this.textLines.Count - this.maxLinesOfTextToRender)
					this.indexOfFirstLineToRender = this.textLines.Count - this.maxLinesOfTextToRender;
			}
		}

		public void Render(IDisplayOutput<ChessImage, ChessFont> displayOutput)
		{
			int lineIndex = this.indexOfFirstLineToRender;
			int y = this.y + this.height;
			for (int i = 0; i < this.maxLinesOfTextToRender; i++)
			{
				if (lineIndex >= this.textLines.Count)
					break;

				displayOutput.DrawText(
					x: this.x,
					y: y,
					text: this.textLines[lineIndex],
					font: this.font,
					color: this.color);

				y -= this.lineHeightInPixels;
				lineIndex++;
			}

			if (this.textLines.Count > this.maxLinesOfTextToRender)
			{
				displayOutput.DrawImageRotatedClockwise(
					image: ChessImage.Up,
					x: this.x + this.width - WIDTH_OF_ARROW,
					y: this.y + this.height - HEIGHT_OF_ARROW,
					degreesScaled: 0,
					scalingFactorScaled: 128 / 2);

				displayOutput.DrawImageRotatedClockwise(
					image: ChessImage.Down,
					x: this.x + this.width - WIDTH_OF_ARROW,
					y: this.y,
					degreesScaled: 0,
					scalingFactorScaled: 128 / 2);

				displayOutput.DrawRectangle(
					x: this.x + this.width - WIDTH_OF_ARROW,
					y: this.y + HEIGHT_OF_ARROW + 1,
					width: WIDTH_OF_ARROW,
					height: (this.height - HEIGHT_OF_ARROW - HEIGHT_OF_ARROW - 1) - 1,
					color: DTColor.Black(),
					fill: false);

				displayOutput.DrawRectangle(
					x: this.x + this.width - WIDTH_OF_ARROW,
					y: this.y + this.height - HEIGHT_OF_ARROW - this.lineIndexToPixelOffsetMapping[this.indexOfFirstLineToRender] - this.heightOfScrollBar,
					width: WIDTH_OF_ARROW,
					height: this.heightOfScrollBar,
					color: DTColor.Black(),
					fill: true);
			}
		}
	}
}
