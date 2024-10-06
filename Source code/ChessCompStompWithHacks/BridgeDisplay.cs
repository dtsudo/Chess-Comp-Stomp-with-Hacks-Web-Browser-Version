
namespace ChessCompStompWithHacks
{
	using Bridge;
	using ChessCompStompWithHacksLibrary;
	using DTLibrary;

	public class BridgeDisplay : DTDisplay<GameImage, GameFont>
	{
		public interface ICanvasWidthAndHeightInfo
		{
			int GetCurrentCanvasWidth();
			int GetCurrentCanvasHeight();
		}

		private ICanvasWidthAndHeightInfo canvasWidthAndHeightInfo;

		private BridgeDisplayRectangle bridgeDisplayRectangle;
		private BridgeDisplayImages bridgeDisplayImages;
		private BridgeDisplayFont bridgeDisplayFont;

		public BridgeDisplay(ICanvasWidthAndHeightInfo canvasWidthAndHeightInfo, int canvasScalingFactor)
		{
			this.canvasWidthAndHeightInfo = canvasWidthAndHeightInfo;

			this.bridgeDisplayRectangle = new BridgeDisplayRectangle(canvasWidthAndHeightInfo: canvasWidthAndHeightInfo);
			this.bridgeDisplayImages = new BridgeDisplayImages(canvasWidthAndHeightInfo: canvasWidthAndHeightInfo, canvasScalingFactor: canvasScalingFactor);
			this.bridgeDisplayFont = new BridgeDisplayFont(canvasWidthAndHeightInfo: canvasWidthAndHeightInfo);
		}
		
		public override void DrawInitialLoadingScreen()
		{
		}

		public override void DrawRectangle(int x, int y, int width, int height, DTColor color, bool fill)
		{
			this.bridgeDisplayRectangle.DrawRectangle(
				x: x,
				y: y,
				width: width,
				height: height,
				color: color,
				fill: fill);
		}
		
		public override bool LoadImages()
		{
			bool finishedLoadingFonts = this.bridgeDisplayFont.LoadFonts();
			bool finishedLoadingImages = this.bridgeDisplayImages.LoadImages();
			
			return finishedLoadingImages && finishedLoadingFonts;
		}

		public override int GetNumElementsLoaded()
		{
			return this.bridgeDisplayImages.GetNumElementsLoaded() + this.bridgeDisplayFont.GetNumElementsLoaded();
		}

		public override int? GetNumTotalElementsToLoad()
		{
			int? numImageElements = this.bridgeDisplayImages.GetNumTotalElementsToLoad();
			int? numFontElements = this.bridgeDisplayFont.GetNumTotalElementsToLoad();

			if (!numImageElements.HasValue)
				return null;
			if (!numFontElements.HasValue)
				return null;

			return numImageElements.Value + numFontElements.Value;
		}

		public override void DrawImageRotatedClockwise(GameImage image, int x, int y, int degreesScaled, int scalingFactorScaled)
		{
			this.bridgeDisplayImages.DrawImageRotatedClockwise(
				image: image,
				x: x,
				y: y,
				degreesScaled: degreesScaled,
				scalingFactorScaled: scalingFactorScaled);
		}
		
		public override void DrawImageRotatedClockwise(GameImage image, int imageX, int imageY, int imageWidth, int imageHeight, int x, int y, int degreesScaled, int scalingFactorScaled)
		{
			this.bridgeDisplayImages.DrawImageRotatedClockwise(
				image: image,
				imageX: imageX,
				imageY: imageY,
				imageWidth: imageWidth,
				imageHeight: imageHeight,
				x: x,
				y: y,
				degreesScaled: degreesScaled,
				scalingFactorScaled: scalingFactorScaled);
		}
		
		public override int GetWidth(GameImage image)
		{
			return this.bridgeDisplayImages.GetWidth(image: image);
		}
		
		public override int GetHeight(GameImage image)
		{
			return this.bridgeDisplayImages.GetHeight(image: image);
		}

		public override void DrawText(int x, int y, string text, GameFont font, DTColor color)
		{
			this.bridgeDisplayFont.DrawText(
				x: x,
				y: y,
				text: text,
				font: font,
				color: color);
		}

		public override void TryDrawText(int x, int y, string text, GameFont font, DTColor color)
		{
			this.bridgeDisplayFont.TryDrawText(
				x: x,
				y: y,
				text: text,
				font: font,
				color: color);
		}
		
		public override void DisposeImages()
		{
		}

		public override int GetMobileScreenWidth()
		{
			return this.canvasWidthAndHeightInfo.GetCurrentCanvasWidth();
		}

		public override int GetMobileScreenHeight()
		{
			return this.canvasWidthAndHeightInfo.GetCurrentCanvasHeight();
		}

		public override string Debug_GetBrowserInfo(string stringToEval)
		{
			string result = Script.Eval<string>(stringToEval);

			return result + "";
		}
	}
}
