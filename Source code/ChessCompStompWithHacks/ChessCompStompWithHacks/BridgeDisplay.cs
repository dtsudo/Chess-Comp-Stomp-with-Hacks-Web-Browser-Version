
namespace ChessCompStompWithHacks
{
	using ChessCompStompWithHacksLibrary;
	using DTLibrary;
	using System;
	using System.Collections.Generic;
	using Bridge;
	
	public class BridgeDisplay : DTDisplay<ChessImage, ChessFont>
	{
		private BridgeDisplayRectangle bridgeDisplayRectangle;
		private BridgeDisplayImages bridgeDisplayImages;
		private BridgeDisplayFont bridgeDisplayFont;
		
		public BridgeDisplay()
		{
			this.bridgeDisplayRectangle = new BridgeDisplayRectangle();
			this.bridgeDisplayImages = new BridgeDisplayImages();
			this.bridgeDisplayFont = new BridgeDisplayFont();
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
			bool finishedLoadingImages = this.bridgeDisplayImages.LoadImages();
			
			if (!finishedLoadingImages)
				return false;
			
			return this.bridgeDisplayFont.LoadFonts();
		}
		
		public override void DrawImageRotatedClockwise(ChessImage image, int x, int y, int degreesScaled, int scalingFactorScaled)
		{
			this.bridgeDisplayImages.DrawImageRotatedClockwise(
				image: image,
				x: x,
				y: y,
				degreesScaled: degreesScaled,
				scalingFactorScaled: scalingFactorScaled);
		}
		
		public override int GetWidth(ChessImage image)
		{
			return this.bridgeDisplayImages.GetWidth(image: image);
		}
		
		public override int GetHeight(ChessImage image)
		{
			return this.bridgeDisplayImages.GetHeight(image: image);
		}

		public override void DrawText(int x, int y, string text, ChessFont font, DTColor color)
		{
			this.bridgeDisplayFont.DrawText(
				x: x,
				y: y,
				text: text,
				font: font,
				color: color);
		}
		
		public override void DisposeImages()
		{
		}
	}
}
