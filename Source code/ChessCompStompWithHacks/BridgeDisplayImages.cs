
namespace ChessCompStompWithHacks
{
	using Bridge;
	using ChessCompStompWithHacksLibrary;
	using DTLibrary;
	using System;
	using System.Collections.Generic;

	public class BridgeDisplayImages
	{
		private Dictionary<GameImage, int> widthDictionary;
		private Dictionary<GameImage, int> heightDictionary;
		
		public BridgeDisplayImages()
		{
			this.widthDictionary = new Dictionary<GameImage, int>();
			this.heightDictionary = new Dictionary<GameImage, int>();
			Script.Eval(@"
				window.BridgeDisplayImagesJavascript = ((function () {
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
							canvas = document.getElementById('bridgeCanvas');
							if (canvas !== null)
								context = canvas.getContext('2d', { alpha: false });
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
						
						if (degreesScaled !== 0) {
							context.translate(img.width / 2, img.height / 2);
							context.rotate(degreesScaled * radianConversion);
							context.translate(-img.width / 2, -img.height / 2);
						}
						
						context.drawImage(img, 0, 0);
									
						context.setTransform(1, 0, 0, 1, 0, 0);
					};
					
					var drawImageRotatedClockwise2 = function (imageName, imageX, imageY, imageWidth, imageHeight, x, y, degreesScaled, scalingFactorScaled) {
						if (canvas === null) {
							canvas = document.getElementById('bridgeCanvas');
							if (canvas !== null)
								context = canvas.getContext('2d', { alpha: false });
							else
								return;
						}
						
						var img = imgDict[imageName];
						
						var scalingFactor = scalingFactorScaled / 128.0;
						
						context.translate(x, y);
						context.scale(scalingFactor, scalingFactor);
						
						if (degreesScaled !== 0) {
							context.translate(imageWidth / 2, imageHeight / 2);
							context.rotate(degreesScaled * radianConversion);
							context.translate(-imageWidth / 2, -imageHeight / 2);
						}
						
						context.drawImage(img, imageX, imageY, imageWidth, imageHeight, 0, 0, imageWidth, imageHeight);
									
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
						drawImageRotatedClockwise2: drawImageRotatedClockwise2,
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
			
			foreach (GameImage gameImage in Enum.GetValues(typeof(GameImage)))
			{
				if (isFirst)
					isFirst = false;
				else
					imageNames = imageNames + ",";
				imageNames = imageNames + gameImage.GetImageFilename();
			}
			
			if (imageNames == "")
				return true;
			
			bool result = Script.Eval<bool>("window.BridgeDisplayImagesJavascript.loadImages('" + imageNames + "')");
				
			if (result)
				return true;
			return false;
		}
		
		public void DrawImageRotatedClockwise(GameImage image, int x, int y, int degreesScaled, int scalingFactorScaled)
		{
			int height = this.GetHeight(image: image);
			int scaledHeight = height * scalingFactorScaled / 128;
			y = GlobalConstants.WINDOW_HEIGHT - y - scaledHeight;
			
			Script.Call(
				"window.BridgeDisplayImagesJavascript.drawImageRotatedClockwise",
				image.GetImageFilename(),
				x,
				y,
				degreesScaled,
				scalingFactorScaled);
		}
		
		public void DrawImageRotatedClockwise(GameImage image, int imageX, int imageY, int imageWidth, int imageHeight, int x, int y, int degreesScaled, int scalingFactorScaled)
		{
			int height = imageHeight;
			int scaledHeight = height * scalingFactorScaled / 128;
			y = GlobalConstants.WINDOW_HEIGHT - y - scaledHeight;
			
			Script.Call(
				"window.BridgeDisplayImagesJavascript.drawImageRotatedClockwise2",
				image.GetImageFilename(),
				imageX,
				imageY,
				imageWidth,
				imageHeight,
				x,
				y,
				degreesScaled,
				scalingFactorScaled);
		}
		
		public int GetWidth(GameImage image)
		{
			if (this.widthDictionary.ContainsKey(image))
				return this.widthDictionary[image];
			
			int width = this.GetWidthFromJavascript(image: image);
			this.widthDictionary[image] = width;
			return width;
		}
		
		private int GetWidthFromJavascript(GameImage image)
		{
			return Script.Eval<int>("window.BridgeDisplayImagesJavascript.getWidth('" + image.GetImageFilename() + "')");
		}
		
		public int GetHeight(GameImage image)
		{
			if (this.heightDictionary.ContainsKey(image))
				return this.heightDictionary[image];
			
			int height = this.GetHeightFromJavascript(image: image);
			this.heightDictionary[image] = height;
			return height;
		}
		
		private int GetHeightFromJavascript(GameImage image)
		{
			return Script.Eval<int>("window.BridgeDisplayImagesJavascript.getHeight('" + image.GetImageFilename() + "')");
		}
	}
}
