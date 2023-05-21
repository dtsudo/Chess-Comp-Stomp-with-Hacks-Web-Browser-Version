
namespace DTLibrary
{
	public class TranslatedDisplayOutput<ImageEnum, FontEnum> : IDisplayOutput<ImageEnum, FontEnum>
	{
		private IDisplayOutput<ImageEnum, FontEnum> display;
		private int xOffsetInPixels;
		private int yOffsetInPixels;

		public TranslatedDisplayOutput(
			IDisplayOutput<ImageEnum, FontEnum> display,
			int xOffsetInPixels,
			int yOffsetInPixels)
		{
			this.display = display;
			this.xOffsetInPixels = xOffsetInPixels;
			this.yOffsetInPixels = yOffsetInPixels;
		}

		public void DrawRectangle(int x, int y, int width, int height, DTColor color, bool fill)
		{
			this.display.DrawRectangle(
				x: x + this.xOffsetInPixels,
				y: y + this.yOffsetInPixels,
				width: width,
				height: height,
				color: color,
				fill: fill);
		}

		public void DrawText(int x, int y, string text, FontEnum font, DTColor color)
		{
			this.display.DrawText(
				x: x + this.xOffsetInPixels,
				y: y + this.yOffsetInPixels,
				text: text,
				font: font,
				color: color);
		}

		public void TryDrawText(int x, int y, string text, FontEnum font, DTColor color)
		{
			this.display.TryDrawText(
				x: x + this.xOffsetInPixels,
				y: y + this.yOffsetInPixels,
				text: text,
				font: font,
				color: color);
		}

		public void DrawInitialLoadingScreen()
		{
			this.display.DrawInitialLoadingScreen();
		}

		public void DrawImage(ImageEnum image, int x, int y)
		{
			this.DrawImageRotatedClockwise(image: image, x: x, y: y, degreesScaled: 0, scalingFactorScaled: 128);
		}

		// Degrees = DegreesScaled / 128.0
		public void DrawImageRotatedClockwise(ImageEnum image, int x, int y, int degreesScaled)
		{
			this.DrawImageRotatedClockwise(
				image: image,
				x: x,
				y: y,
				degreesScaled: degreesScaled,
				scalingFactorScaled: 128);
		}

		public void DrawImageRotatedClockwise(ImageEnum image, int x, int y, int degreesScaled, int scalingFactorScaled)
		{
			this.display.DrawImageRotatedClockwise(
				image: image,
				x: x + this.xOffsetInPixels,
				y: y + this.yOffsetInPixels,
				degreesScaled: degreesScaled,
				scalingFactorScaled: scalingFactorScaled);
		}

		public void DrawImageRotatedClockwise(ImageEnum image, int imageX, int imageY, int imageWidth, int imageHeight, int x, int y, int degreesScaled, int scalingFactorScaled)
		{
			this.display.DrawImageRotatedClockwise(
				image: image,
				imageX: imageX,
				imageY: imageY,
				imageWidth: imageWidth,
				imageHeight: imageHeight,
				x: x + this.xOffsetInPixels,
				y: y + this.yOffsetInPixels,
				degreesScaled: degreesScaled,
				scalingFactorScaled: scalingFactorScaled);
		}

		public int GetWidth(ImageEnum image)
		{
			return this.display.GetWidth(image: image);
		}

		public int GetHeight(ImageEnum image)
		{
			return this.display.GetHeight(image: image);
		}
	}
}
