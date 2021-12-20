
namespace DTLibrary
{
	public interface IDisplayProcessing<ImageEnum>
	{
		bool LoadImages();
		int GetWidth(ImageEnum image);
		int GetHeight(ImageEnum image);
	}

	public interface IDisplayOutput<ImageEnum, FontEnum>
	{
		void DrawRectangle(int x, int y, int width, int height, DTColor color, bool fill);
		void DrawText(int x, int y, string text, FontEnum font, DTColor color);
		void DrawInitialLoadingScreen();
		void DrawImage(ImageEnum image, int x, int y);
		void DrawImageRotatedClockwise(ImageEnum image, int x, int y, int degreesScaled);
		void DrawImageRotatedClockwise(ImageEnum image, int x, int y, int degreesScaled, int scalingFactorScaled);
		int GetWidth(ImageEnum image);
		int GetHeight(ImageEnum image);
	}

	public interface IDisplayCleanup
	{
		void DisposeImages();
	}

	public abstract class DTDisplay<ImageEnum, FontEnum> : IDisplayProcessing<ImageEnum>, IDisplayOutput<ImageEnum, FontEnum>, IDisplayCleanup
	{
		/// <summary>
		/// Renders a rectangle on the screen, with the bottom-left corner occurring
		/// at (x, y), and with the specified width and height.
		/// 
		/// Also takes in a color, indicating what color the rectangle should be, as well
		/// as a boolean (fill) indicating whether the rectangle should be filled in.
		/// </summary>
		public abstract void DrawRectangle(int x, int y, int width, int height, DTColor color, bool fill);

		public abstract void DrawText(int x, int y, string text, FontEnum font, DTColor color);

		/// <summary>
		/// Once this.DisposeImages() is invoked, this function can no longer be called.
		/// </summary>
		public abstract void DrawInitialLoadingScreen();

		/// <summary>
		/// Must be repeatedly invoked until it returns true before invoking DrawImage(), DrawImageRotatedClockwise(),
		/// GetWidth(), GetHeight(), or DrawText()
		/// </summary>
		public abstract bool LoadImages();

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

		// Degrees = DegreesScaled / 128.0
		// ScalingFactor = scalingFactorScaled / 128.0
		public abstract void DrawImageRotatedClockwise(ImageEnum image, int x, int y, int degreesScaled, int scalingFactorScaled);

		public abstract int GetWidth(ImageEnum image);

		public abstract int GetHeight(ImageEnum image);

		/// <summary>
		/// Note that DisposeImages() must be called, and it gets called even
		/// if LoadImages() is never invoked (or was invoked but never returned true)
		/// 
		/// This function must be idempotent (and not fail if called multiple times).
		/// </summary>
		public abstract void DisposeImages();
	}

	public static class DisplayExtensions
	{
		public static void DrawThickRectangle<ImageEnum, FontEnum>(this IDisplayOutput<ImageEnum, FontEnum> displayOutput, int x, int y, int width, int height, int additionalThickness, DTColor color, bool fill)
		{
			displayOutput.DrawRectangle(x - additionalThickness, y - additionalThickness, width + additionalThickness * 2, 1 + additionalThickness * 2, color, true);
			displayOutput.DrawRectangle(x - additionalThickness, height - 1 + y - additionalThickness, width + additionalThickness * 2, 1 + additionalThickness * 2, color, true);
			displayOutput.DrawRectangle(x - additionalThickness, y - additionalThickness, 1 + additionalThickness * 2, height + additionalThickness * 2, color, true);
			displayOutput.DrawRectangle(width - 1 + x - additionalThickness, y - additionalThickness, 1 + additionalThickness * 2, height + additionalThickness * 2, color, true);

			if (fill)
				displayOutput.DrawRectangle(x, y, width, height, color, true);
		}
	}
}
