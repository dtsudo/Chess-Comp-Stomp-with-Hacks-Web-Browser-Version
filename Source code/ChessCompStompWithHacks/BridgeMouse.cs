
namespace ChessCompStompWithHacks
{
	using Bridge;
	using DTLibrary;
	
	public class BridgeMouse : IMouse
	{
		public BridgeMouse(int canvasScalingFactor)
		{
			Script.Eval(@"
				window.BridgeMouseJavascript = ((function () {
					'use strict';
					
					var mouseXPosition = -50;
					var mouseYPosition = -50;
					
					var canvas = null;
					
					var mouseMoveHandler = function (e) {
					
						if (canvas === null) {
							canvas = document.getElementById('bridgeCanvas');
							
							if (canvas === null)
								return;
						}
						
						var canvasCssWidth = canvas.offsetWidth;
						var canvasCssHeight = canvas.offsetHeight;
						
						var xPosition = (e.pageX !== null && e.pageX !== undefined ? e.pageX : e.clientX) - canvas.offsetLeft;
						
						var canvasXScaling = canvasCssWidth / canvas.width;
						if (canvasXScaling < 0.001)
							canvasXScaling = 0.001;
						
						xPosition = Math.round(xPosition / canvasXScaling);
									
						if (xPosition < -5)
							xPosition = -5;
						
						if (xPosition > canvas.width + 5)
							xPosition = canvas.width + 5;
						
						var yPosition = (e.pageY !== null && e.pageY !== undefined ? e.pageY : e.clientY) - canvas.offsetTop;
						
						var canvasYScaling = canvasCssHeight / canvas.height;
						if (canvasYScaling < 0.001)
							canvasYScaling = 0.001;
						
						yPosition = Math.round(yPosition / canvasYScaling);
						
						if (yPosition < -5)
							yPosition = -5;
						
						if (yPosition > canvas.height + 5)
							yPosition = canvas.height + 5;
						
						let canvasScalingFactor = " + canvasScalingFactor.ToStringCultureInvariant() + @";
						mouseXPosition = Math.floor(xPosition / canvasScalingFactor);
						mouseYPosition = Math.floor((canvas.height - yPosition - 1) / canvasScalingFactor);
					};
					
					var isLeftMouseButtonPressed = false;
					var isRightMouseButtonPressed = false;
					
					var previousIsLeftMouseButtonPressed = false;
					var previousIsRightMouseButtonPressed = false;

					var shouldProcessLeftMouseButtonPress = false;
					var shouldProcessRightMouseButtonPress = false;
					
					var checkMouseButtonHandler = function (e) {
						if ((e.buttons & 1) === 1) {
							isLeftMouseButtonPressed = true;
							if (!previousIsLeftMouseButtonPressed)
								shouldProcessLeftMouseButtonPress = true;
						} else {
							isLeftMouseButtonPressed = false;
						}
						
						if ((e.buttons & 2) === 2) {
							isRightMouseButtonPressed = true;
							if (!previousIsRightMouseButtonPressed)
								shouldProcessRightMouseButtonPress = true;
						} else {
							isRightMouseButtonPressed = false;
						}
					};

					var processedInputs = function () {
						previousIsLeftMouseButtonPressed = isLeftMouseButtonPressed || shouldProcessLeftMouseButtonPress;
						previousIsRightMouseButtonPressed = isRightMouseButtonPressed || shouldProcessRightMouseButtonPress;
						shouldProcessLeftMouseButtonPress = false;
						shouldProcessRightMouseButtonPress = false;
					};
										
					var disableContextMenu;
					disableContextMenu = function () {
						if (canvas === null) {
							canvas = document.getElementById('bridgeCanvas');
							
							if (canvas === null) {
								setTimeout(disableContextMenu, 50);
								return;
							}
						}
						
						canvas.addEventListener('contextmenu', function (e) { e.preventDefault(); });
					};
					disableContextMenu();
					
					document.addEventListener('pointermove', function (e) { mouseMoveHandler(e); checkMouseButtonHandler(e); });
					document.addEventListener('pointerdown', function (e) { mouseMoveHandler(e); checkMouseButtonHandler(e); });
					document.addEventListener('pointerup', function (e) { mouseMoveHandler(e); checkMouseButtonHandler(e); });
					document.addEventListener('pointercancel', function (e) { mouseMoveHandler(e); checkMouseButtonHandler(e); });
					
					return {
						isLeftMouseButtonPressed: function () { return isLeftMouseButtonPressed || shouldProcessLeftMouseButtonPress; },
						isRightMouseButtonPressed: function () { return isRightMouseButtonPressed || shouldProcessRightMouseButtonPress; },
						getMouseX: function () { return Math.round(mouseXPosition); },
						getMouseY: function () { return Math.round(mouseYPosition); },
						processedInputs: processedInputs
					};
				})());
			");
		}
		
		public int GetX()
		{
			return Script.Write<int>("window.BridgeMouseJavascript.getMouseX()");
		}

		public int GetY()
		{
			return Script.Write<int>("window.BridgeMouseJavascript.getMouseY()");
		}

		public bool IsLeftMouseButtonPressed()
		{
			return Script.Write<bool>("window.BridgeMouseJavascript.isLeftMouseButtonPressed()");
		}

		public bool IsRightMouseButtonPressed()
		{
			return Script.Write<bool>("window.BridgeMouseJavascript.isRightMouseButtonPressed()");
		}

		public void ProcessedInputs()
		{
			Script.Write("window.BridgeMouseJavascript.processedInputs()");
		}
	}
}
