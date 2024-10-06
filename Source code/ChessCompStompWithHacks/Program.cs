
namespace ChessCompStompWithHacks
{
	using Bridge;
	using System;

    public class Program
    {
        public static void Main(string[] args)
        {
			AddFpsDisplayJavascript();
			Initialize();
        }
		
		private static void AddFpsDisplayJavascript()
		{
			Script.Write(@"
				window.FpsDisplayJavascript = ((function () {
					'use strict';
					
					var numberOfFrames = 0;
					var hasAddedFpsLabel = false;
					var startTimeMillis = Date.now();
					var fpsNode = null;
					
					var frameComputedAndRendered = function () {
						numberOfFrames++;
					};
					
					var displayFps = function () {
						if (!hasAddedFpsLabel) {
							var fpsLabelNode = document.getElementById('fpsLabel');
							if (fpsLabelNode !== null) {
								fpsLabelNode.textContent = 'FPS: ';
								hasAddedFpsLabel = true;
							}
						}
						
						var currentTimeMillis = Date.now();
						if (currentTimeMillis - startTimeMillis > 2000) {
							var actualFps = numberOfFrames / 2;
							
							if (fpsNode === null)
								fpsNode = document.getElementById('fps');
							
							if (fpsNode !== null)
								fpsNode.textContent = actualFps.toString();
							
							numberOfFrames = 0;
							startTimeMillis = currentTimeMillis;
						}
					};
					
					return {
						frameComputedAndRendered: frameComputedAndRendered,
						displayFps: displayFps
					};
				})());
			");
		}

		private static void Initialize()
		{
			Script.Write(@"
				((function () {
					'use strict';
					
					var isEmbeddedVersion = false;
					
					var stopWaitingEvenIfMusicHasNotLoaded = false;

					var canvasScalingFactor = 2;
										
					var isElectronVersion = !isEmbeddedVersion
						&& (window.navigator.userAgent.indexOf('Electron') >= 0 || window.navigator.userAgent.indexOf('electron') >= 0);
					
					var isLibrem5 = window.navigator.userAgent.toLowerCase().includes('aarch64')
						&& window.navigator.userAgent.toLowerCase().includes('linux')
						&& !window.navigator.userAgent.toLowerCase().includes('android');
					
					var defaultFps;
					if (isLibrem5)
						defaultFps = 20;
					else if (window.navigator.userAgent.indexOf('Gecko/') >= 0)
						defaultFps = 30;
					else
						defaultFps = 60;
					
					var urlParams = (new URL(document.location)).searchParams;
					
					var showFps = urlParams.get('showfps') !== null
						? (urlParams.get('showfps') === 'true')
						: false;
					var fps = urlParams.get('fps') !== null
						? parseInt(urlParams.get('fps'), 10)
						: defaultFps;
					var debugMode = urlParams.get('debugmode') !== null
						? (urlParams.get('debugmode') === 'true')
						: false;
					
					window.ChessCompStompWithHacks.GameInitializer.Start(fps, isEmbeddedVersion, isElectronVersion, canvasScalingFactor, stopWaitingEvenIfMusicHasNotLoaded, debugMode);
					
					var computeAndRenderNextFrame;
					
					var nextTimeToAct = Date.now() + (1000.0 / fps);
					
					var hasProcessedExtraTime = false;
					
					computeAndRenderNextFrame = function () {
						var now = Date.now();
						
						if (nextTimeToAct > now) {
							if (!hasProcessedExtraTime) {
								var extraTime = Math.round(nextTimeToAct - now);
								if (extraTime > 0)
									window.ChessCompStompWithHacks.GameInitializer.ProcessExtraTime(extraTime);
								hasProcessedExtraTime = true;
								setTimeout(computeAndRenderNextFrame, 0);
							} else {
								setTimeout(computeAndRenderNextFrame, 5);
							}
							return;
						}
						
						hasProcessedExtraTime = false;
						
						if (nextTimeToAct < now - 5.0*(1000.0 / fps))
							nextTimeToAct = now - 5.0*(1000.0 / fps);
						
						nextTimeToAct = nextTimeToAct + (1000.0 / fps);
						
						window.ChessCompStompWithHacks.GameInitializer.ComputeAndRenderNextFrame();
						window.FpsDisplayJavascript.frameComputedAndRendered();
						
						if (showFps)
							window.FpsDisplayJavascript.displayFps();
						
						setTimeout(computeAndRenderNextFrame, 0);
					};
					
					setTimeout(computeAndRenderNextFrame, 0);
				})());
			");
		}
    }
}
