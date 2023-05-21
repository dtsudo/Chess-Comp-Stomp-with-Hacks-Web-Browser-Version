
namespace ChessCompStompWithHacks
{
	using Bridge;
	using ChessCompStompWithHacksLibrary;
	using DTLibrary;
	using System;
	using System.Collections.Generic;
	
	public class BridgeDisplayRectangle
	{
		public BridgeDisplayRectangle()
		{
			Script.Eval(@"
				window.BridgeDisplayRectangleJavascript = ((function () {
					'use strict';
					
					var canvas = null;
					var context = null;
							
					var drawRectangle = function (x, y, width, height, red, green, blue, alpha, fill) {
								
						if (canvas === null) {
							canvas = document.getElementById('bridgeCanvas');		
							if (canvas === null)
								return;	
							context = canvas.getContext('2d', { alpha: false });
						}
						
						context.fillStyle = 'rgba(' + red.toString() + ', ' + green.toString() + ', ' + blue.toString() + ', ' + (alpha / 255).toString() + ')';
						context.strokeStyle = 'rgba(' + red.toString() + ', ' + green.toString() + ', ' + blue.toString() + ', ' + (alpha / 255).toString() + ')';
						
						if (fill)
							context.fillRect(x, y, width, height);
						else
							context.strokeRect(x, y, width, height);
					};
										
					return {
						drawRectangle: drawRectangle
					};
				})());
			");
		}
		
		public void DrawRectangle(int x, int y, int width, int height, DTColor color, bool fill)
		{
			y = GlobalConstants.WINDOW_HEIGHT - y - height;
			
			int red = color.R;
			int green = color.G;
			int blue = color.B;
			int alpha = color.Alpha;
					
			Script.Call("window.BridgeDisplayRectangleJavascript.drawRectangle", x, y, width, height, red, green, blue, alpha, fill);
		}
	}
}
