
namespace ChessCompStompWithHacks
{
	using ChessCompStompWithHacksLibrary;
	using DTLibrary;
	using System;
	using System.Collections.Generic;
	using Bridge;
	
	public class BridgeDisplayImages
	{
		private Dictionary<ChessImage, int> widthDictionary;
		private Dictionary<ChessImage, int> heightDictionary;
		
		public BridgeDisplayImages()
		{
			this.widthDictionary = new Dictionary<ChessImage, int>();
			this.heightDictionary = new Dictionary<ChessImage, int>();
			Script.Eval(@"
				window.ChessCompStompWithHacksBridgeDisplayImagesJavascript = ((function () {
					'use strict';
					
					var imgDict = {};
					var widthDict = {};
					var heightDict = {};
					var canvas = null;
					var context = null;
					var radianConversion = 1.0 / 128.0 * (2.0 * Math.PI / 360.0);
					
					var numberOfImagesLoaded = 0;
					
					var loadImages = function (imageNames) {
						var imageNamesArray = imageNames.split(',');
						
						var count = 0;
						
						for (var i = 0; i < imageNamesArray.length; i++) {
							var imageName = imageNamesArray[i];
							
							if (imgDict[imageName])
								continue;
							
							var imgPath = 'Data/Images/' + imageName;
							var img = new Image();
							img.addEventListener('load', (function (a, b) {
								return function () {
									numberOfImagesLoaded++;
									widthDict[a] = b.naturalWidth;
									heightDict[a] = b.naturalHeight;
								};
							})(imageName, img));
							
							img.src = imgPath;
							imgDict[imageName] = img;
							
							count++;
							if (count === 15) // arbitrary
								return false;
						}
						
						return numberOfImagesLoaded === imageNamesArray.length;
					};
					
					var drawImageRotatedClockwise = function (imageName, x, y, degreesScaled, scalingFactorScaled) {
						if (canvas === null) {
							canvas = document.getElementById('chessCompStompWithHacksCanvas');
							if (canvas !== null)
								context = canvas.getContext('2d');
							else
								return;
						}
						
						var img = imgDict[imageName];
						
						if (degreesScaled === 0 && scalingFactorScaled === 128) {
							context.drawImage(img, x, y);
							return;
						}
						
						var scalingFactor = scalingFactorScaled / 128.0;
						
						context.translate(x, y);
						context.scale(scalingFactor, scalingFactor);
						
						context.translate(img.width / 2, img.height / 2);
						context.rotate(degreesScaled * radianConversion);
						context.translate(-img.width / 2, -img.height / 2);
						
						context.drawImage(img, 0, 0);
									
						context.setTransform(1, 0, 0, 1, 0, 0);
					};
					
					var getWidth = function (imageName) {
						return widthDict[imageName];
					};
					
					var getHeight = function (imageName) {
						return heightDict[imageName];
					};
					
					return {
						loadImages: loadImages,
						drawImageRotatedClockwise: drawImageRotatedClockwise,
						getWidth: getWidth,
						getHeight: getHeight
					};
				})());
			");
		}
		
		public bool LoadImages()
		{
			string imageNames = "";
			bool isFirst = true;
			
			foreach (ChessImage chessImage in Enum.GetValues(typeof(ChessImage)))
			{
				if (isFirst)
					isFirst = false;
				else
					imageNames = imageNames + ",";
				imageNames = imageNames + chessImage.GetImageFilename();
			}
			
			if (imageNames == "")
				return true;
			
			bool result = Script.Eval<bool>("window.ChessCompStompWithHacksBridgeDisplayImagesJavascript.loadImages('" + imageNames + "')");
				
			if (result)
				return true;
			return false;
		}
		
		public void DrawImageRotatedClockwise(ChessImage image, int x, int y, int degreesScaled, int scalingFactorScaled)
		{
			int height = this.GetHeight(image: image);
			int scaledHeight = height * scalingFactorScaled / 128;
			y = ChessCompStompWithHacks.WINDOW_HEIGHT - y - scaledHeight;
			
			Script.Call(
				"window.ChessCompStompWithHacksBridgeDisplayImagesJavascript.drawImageRotatedClockwise",
				image.GetImageFilename(),
				x,
				y,
				degreesScaled,
				scalingFactorScaled);
		}
		
		public int GetWidth(ChessImage image)
		{
			if (this.widthDictionary.ContainsKey(image))
				return this.widthDictionary[image];
			
			int width = this.GetWidthFromJavascript(image: image);
			this.widthDictionary[image] = width;
			return width;
		}
		
		private int GetWidthFromJavascript(ChessImage image)
		{
			return Script.Eval<int>("window.ChessCompStompWithHacksBridgeDisplayImagesJavascript.getWidth('" + image.GetImageFilename() + "')");
		}
		
		public int GetHeight(ChessImage image)
		{
			if (this.heightDictionary.ContainsKey(image))
				return this.heightDictionary[image];
			
			int height = this.GetHeightFromJavascript(image: image);
			this.heightDictionary[image] = height;
			return height;
		}
		
		private int GetHeightFromJavascript(ChessImage image)
		{
			return Script.Eval<int>("window.ChessCompStompWithHacksBridgeDisplayImagesJavascript.getHeight('" + image.GetImageFilename() + "')");
		}
	}
}
