
namespace ChessCompStompWithHacks
{
	using ChessCompStompWithHacksLibrary;
	using DTLibrary;
	using System;
	using System.Collections.Generic;
	using Bridge;
	
	public class BridgeDisplayRectangle
	{
		public BridgeDisplayRectangle()
		{
			Script.Eval(@"
				window.ChessCompStompWithHacksBridgeDisplayRectangleJavascript = ((function () {
					'use strict';
					
					var canvas = null;
					var context = null;
							
					var drawRectangle = function (x, y, width, height, red, green, blue, alpha, fill) {
								
						if (canvas === null) {
							canvas = document.getElementById('chessCompStompWithHacksCanvas');		
							if (canvas === null)
								return;	
							context = canvas.getContext('2d');
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
			y = ChessCompStompWithHacks.WINDOW_HEIGHT - y - height;
			
			int red = color.R;
			int green = color.G;
			int blue = color.B;
			int alpha = color.Alpha;
					
			Script.Call("window.ChessCompStompWithHacksBridgeDisplayRectangleJavascript.drawRectangle", x, y, width, height, red, green, blue, alpha, fill);
		}
	}
}
