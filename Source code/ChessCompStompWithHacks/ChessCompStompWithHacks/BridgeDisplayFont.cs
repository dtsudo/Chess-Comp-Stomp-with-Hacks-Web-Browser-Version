
namespace ChessCompStompWithHacks
{
	using ChessCompStompWithHacksLibrary;
	using DTLibrary;
	using System;
	using System.Collections.Generic;
	using Bridge;
	
	public class BridgeDisplayFont
	{
		public BridgeDisplayFont()
		{
			Script.Eval(@"
				window.ChessCompStompWithHacksBridgeDisplayFontJavascript = ((function () {
					'use strict';
					
					var fontDictionary = {};
					
					var context = null;
					
					var fontFamilyCount = 0;
					var numberOfFontObjectsLoaded = 0;
					
					var loadFonts = function (fontNames) {
						var fontNamesArray = fontNames.split(',');
						
						var numberOfFontObjects = fontNamesArray.length;
						
						for (var i = 0; i < fontNamesArray.length; i++) {
							var fontName = fontNamesArray[i];
							
							if (fontDictionary[fontName])
								continue;
							
							var fontFamilyName = 'chessCompStompFontFamily' + fontFamilyCount;
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
						
						return numberOfFontObjects === numberOfFontObjectsLoaded;
					};
						
					var drawText = function (x, y, str, fontName, javascriptFontSize, javascriptLineHeight, red, green, blue, alpha) {
						if (context === null) {
							var canvas = document.getElementById('chessCompStompWithHacksCanvas');
							
							if (canvas === null)
								return;
							
							context = canvas.getContext('2d');
						}
						
						javascriptLineHeight = parseFloat(javascriptLineHeight);
						
						context.textBaseline = 'top';
						context.fillStyle = 'rgba(' + red.toString() + ', ' + green.toString() + ', ' + blue.toString() + ', ' + (alpha / 255).toString() + ')';
						context.strokeStyle = 'rgba(' + red.toString() + ', ' + green.toString() + ', ' + blue.toString() + ', ' + (alpha / 255).toString() + ')';
						context.font = javascriptFontSize + 'px ""' + fontDictionary[fontName].fontFamilyName + '""';
						
						var strArray = str.split('\n');
						var lineY = y;
						
						for (var i = 0; i < strArray.length; i++) {
							context.fillText(strArray[i], x, Math.round(lineY));
							lineY += javascriptLineHeight;
						}
					};
						
					return {
						loadFonts: loadFonts,
						drawText: drawText
					};
				})());
			");
		}
		
		public bool LoadFonts()
		{			
			// Two fonts might have the same WoffFontFilename
			HashSet<string> woffFontFilenames = new HashSet<string>();
			
			foreach (ChessFont font in Enum.GetValues(typeof(ChessFont)))
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
			
			return Script.Eval<bool>("window.ChessCompStompWithHacksBridgeDisplayFontJavascript.loadFonts('" + woffFontFilenamesAsString + "')");
		}
		
		public void DrawText(int x, int y, string text, ChessFont font, DTColor color)
		{
			y = ChessCompStompWithHacks.WINDOW_HEIGHT - y - 1;

			int red = color.R;
			int green = color.G;
			int blue = color.B;
			int alpha = color.Alpha;
			
			ChessFontUtil.FontInfo fontInfo = font.GetFontInfo();
			
			Script.Call(
				"window.ChessCompStompWithHacksBridgeDisplayFontJavascript.drawText",
				x,
				y,
				text,
				fontInfo.WoffFontFilename,
				fontInfo.JavascriptFontSize,
				fontInfo.JavascriptLineHeight,
				red,
				green,
				blue,
				alpha);
		}
	}
}
