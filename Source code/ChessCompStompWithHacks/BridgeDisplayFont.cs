
namespace ChessCompStompWithHacks
{
	using Bridge;
	using ChessCompStompWithHacksLibrary;
	using DTLibrary;
	using System;
	using System.Collections.Generic;

	public class BridgeDisplayFont
	{
		private BridgeDisplay.ICanvasWidthAndHeightInfo canvasWidthAndHeightInfo;

		public BridgeDisplayFont(BridgeDisplay.ICanvasWidthAndHeightInfo canvasWidthAndHeightInfo)
		{
			this.canvasWidthAndHeightInfo = canvasWidthAndHeightInfo;

			Script.Write(@"
				window.BridgeDisplayFontJavascript = ((function () {
					'use strict';
					
					var fontDictionary = {};
					
					var context = null;
					
					var fontFamilyCount = 0;
					var numberOfFontObjectsLoaded = 0;
					var numberOfFontObjects = null;					
					
					var finishedLoading = false;
					
					var loadFonts = function (fontNames) {
						var fontNamesArray = fontNames.split(',');
						
						numberOfFontObjects = fontNamesArray.length;
						
						for (var i = 0; i < fontNamesArray.length; i++) {
							var fontName = fontNamesArray[i];
							
							if (fontDictionary[fontName])
								continue;
							
							var fontFamilyName = 'DTFontFamily' + fontFamilyCount;
							fontFamilyCount++;
							
							var font = new FontFace(fontFamilyName, 'url(Data/Font/' + fontName + ')');
							
							fontDictionary[fontName] = {
								font: font,
								fontFamilyName: fontFamilyName
							};
							
							font.load().then((function (f) {
								return function () {
									document.fonts.add(f);
									numberOfFontObjectsLoaded++;
								};
							})(font));
						}
						
						finishedLoading = numberOfFontObjects === numberOfFontObjectsLoaded;
						return finishedLoading;
					};

					var getNumElementsLoaded = function () {
						return numberOfFontObjectsLoaded;
					};

					var getNumTotalElementsToLoad = function () {
						return numberOfFontObjects;
					};
						
					var drawText = function (x, y, str, fontName, javascriptFontSize, lineHeight, red, green, blue, alpha) {
						if (context === null) {
							var canvas = document.getElementById('bridgeCanvas');
							
							if (canvas === null)
								return;
							
							context = canvas.getContext('2d', { alpha: false });
						}
						
						lineHeight = parseFloat(lineHeight);
						
						context.textBaseline = 'top';
						context.fillStyle = 'rgba(' + red.toString() + ', ' + green.toString() + ', ' + blue.toString() + ', ' + (alpha / 255).toString() + ')';
						context.strokeStyle = 'rgba(' + red.toString() + ', ' + green.toString() + ', ' + blue.toString() + ', ' + (alpha / 255).toString() + ')';
						context.font = javascriptFontSize + 'px ""' + fontDictionary[fontName].fontFamilyName + '""';
						
						var strArray = str.split('\n');
						var lineY = y;
						
						for (var i = 0; i < strArray.length; i++) {
							context.fillText(strArray[i], x, Math.round(lineY));
							lineY += lineHeight;
						}
					};
						
					var tryDrawText = function (x, y, str, fontName, javascriptFontSize, lineHeight, red, green, blue, alpha) {
						if (!finishedLoading)
							return;
						
						drawText(x, y, str, fontName, javascriptFontSize, lineHeight, red, green, blue, alpha);
					};
						
					return {
						loadFonts: loadFonts,
						getNumElementsLoaded: getNumElementsLoaded,
						getNumTotalElementsToLoad: getNumTotalElementsToLoad,
						drawText: drawText,
						tryDrawText: tryDrawText
					};
				})());
			");
		}
		
		public bool LoadFonts()
		{			
			// Two fonts might have the same WoffFontFilename
			HashSet<string> woffFontFilenames = new HashSet<string>();
			
			foreach (GameFont font in Enum.GetValues(typeof(GameFont)))
				woffFontFilenames.Add(font.GetFontInfo().WoffFontFilename);
		
			string woffFontFilenamesAsString = "";
			bool isFirst = true;
			foreach (string woffFontFilename in woffFontFilenames)
			{
				if (isFirst)
					isFirst = false;
				else
					woffFontFilenamesAsString = woffFontFilenamesAsString + ",";
				woffFontFilenamesAsString = woffFontFilenamesAsString + woffFontFilename;
			}

			if (woffFontFilenamesAsString == "")
				return true;
			
			return Script.Eval<bool>("window.BridgeDisplayFontJavascript.loadFonts('" + woffFontFilenamesAsString + "')");
		}

		public int GetNumElementsLoaded()
		{
			return Script.Call<int>("window.BridgeDisplayFontJavascript.getNumElementsLoaded");
		}

		public int? GetNumTotalElementsToLoad()
		{
			return Script.Call<int?>("window.BridgeDisplayFontJavascript.getNumTotalElementsToLoad");
		}

		public void DrawText(int x, int y, string text, GameFont font, DTColor color)
		{
			y = this.canvasWidthAndHeightInfo.GetCurrentCanvasHeight() - y - 1;

			int red = color.R;
			int green = color.G;
			int blue = color.B;
			int alpha = color.Alpha;
			
			GameFontUtil.FontInfo fontInfo = font.GetFontInfo();
			
			Script.Call(
				"window.BridgeDisplayFontJavascript.drawText",
				x,
				y,
				text,
				fontInfo.WoffFontFilename,
				fontInfo.JavascriptFontSize,
				fontInfo.LineHeight,
				red,
				green,
				blue,
				alpha);
		}
		
		public void TryDrawText(int x, int y, string text, GameFont font, DTColor color)
		{
			y = this.canvasWidthAndHeightInfo.GetCurrentCanvasHeight() - y - 1;

			int red = color.R;
			int green = color.G;
			int blue = color.B;
			int alpha = color.Alpha;
			
			GameFontUtil.FontInfo fontInfo = font.GetFontInfo();
			
			Script.Call(
				"window.BridgeDisplayFontJavascript.tryDrawText",
				x,
				y,
				text,
				fontInfo.WoffFontFilename,
				fontInfo.JavascriptFontSize,
				fontInfo.LineHeight,
				red,
				green,
				blue,
				alpha);
		}
	}
}
