
namespace ChessCompStompWithHacks
{
	using ChessCompStompWithHacksLibrary;
	using DTLibrary;
	using Bridge;
	
	public class BridgeMouse : IMouse
	{
		public BridgeMouse()
		{
			Script.Eval(@"
				window.ChessCompStompWithHacksBridgeMouseJavascript = ((function () {
					'use strict';
					
					var mouseXPosition = 0;
					var mouseYPosition = 0;
					
					var canvas = null;
					
					var mouseMoveHandler = function (e) {
					
						if (canvas === null) {
							canvas = document.getElementById('chessCompStompWithHacksCanvas');
							
							if (canvas === null)
								return;
						}
						
						var xPosition = (e.pageX !== null && e.pageX !== undefined ? e.pageX : e.clientX) - canvas.offsetLeft;
												
						if (xPosition < -5)
							xPosition = -5;
						
						if (xPosition > canvas.width + 5)
							xPosition = canvas.width + 5;
						
						var yPosition = (e.pageY !== null && e.pageY !== undefined ? e.pageY : e.clientY) - canvas.offsetTop;
						
						if (yPosition < -5)
							yPosition = -5;
						
						if (yPosition > canvas.height + 5)
							yPosition = canvas.height + 5;
						
						mouseXPosition = xPosition;
						mouseYPosition = canvas.height - yPosition - 1;
					};
					
					var isLeftMouseButtonPressed = false;
					var isRightMouseButtonPressed = false;
					
					var checkMouseButtonHandler = function (e) {
						if ((e.buttons & 1) === 1)
							isLeftMouseButtonPressed = true;
						else
							isLeftMouseButtonPressed = false;
						
						if ((e.buttons & 2) === 2)
							isRightMouseButtonPressed = true;
						else
							isRightMouseButtonPressed = false;
					};
										
					var disableContextMenu;
					disableContextMenu = function () {
						if (canvas === null) {
							canvas = document.getElementById('chessCompStompWithHacksCanvas');
							
							if (canvas === null) {
								setTimeout(disableContextMenu, 50);
								return;
							}
						}
						
						canvas.addEventListener('contextmenu', function (e) { e.preventDefault(); });
					};
					disableContextMenu();
					
					document.addEventListener('mousemove', function (e) { mouseMoveHandler(e); checkMouseButtonHandler(e); }, false);
					document.addEventListener('mousedown', function (e) { checkMouseButtonHandler(e); }, false);
					document.addEventListener('mouseup', function (e) { checkMouseButtonHandler(e); }, false);
					
					return {
						isLeftMouseButtonPressed: function () { return isLeftMouseButtonPressed; },
						isRightMouseButtonPressed: function () { return isRightMouseButtonPressed; },
						getMouseX: function () { return Math.round(mouseXPosition); },
						getMouseY: function () { return Math.round(mouseYPosition); }
					};
				})());
			");
		}
		
		public int GetX()
		{
			return Script.Write<int>("window.ChessCompStompWithHacksBridgeMouseJavascript.getMouseX()");
		}

		public int GetY()
		{
			return Script.Write<int>("window.ChessCompStompWithHacksBridgeMouseJavascript.getMouseY()");
		}

		public bool IsLeftMouseButtonPressed()
		{
			return Script.Write<bool>("window.ChessCompStompWithHacksBridgeMouseJavascript.isLeftMouseButtonPressed()");
		}

		public bool IsRightMouseButtonPressed()
		{
			return Script.Write<bool>("window.ChessCompStompWithHacksBridgeMouseJavascript.isRightMouseButtonPressed()");
		}
	}
}
