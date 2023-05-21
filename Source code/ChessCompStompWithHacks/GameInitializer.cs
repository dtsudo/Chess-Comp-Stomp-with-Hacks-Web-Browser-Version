
namespace ChessCompStompWithHacks
{
	using Bridge;
	using ChessCompStompWithHacksLibrary;
	using DTLibrary;
	using System.Collections.Generic;

	public class GameInitializer
	{
		private static IKeyboard bridgeKeyboard;
		private static IMouse bridgeMouse;
		private static IKeyboard previousKeyboard;
		private static IMouse previousMouse;
		
		private static DTDisplay<GameImage, GameFont> display;
		private static ISoundOutput<GameSound> soundOutput;
		private static IMusic<GameMusic> music;
		
		private static DisplayLogger displayLogger;
		private static bool shouldRenderDisplayLogger;
		
		private static HashSet<string> completedAchievements;
		
		private static IFrame<GameImage, GameFont, GameSound, GameMusic> frame;
		
		private static bool hasInitializedClearCanvasJavascript;
		
		private static string clickUrl;
		
		private static void InitializeClearCanvasJavascript()
		{
			Script.Eval(@"
				window.BridgeClearCanvasJavascript = ((function () {
					'use strict';
					
					var canvas = null;
					var context = null;
								
					var clearCanvas = function () {
						if (canvas === null) {
							canvas = document.getElementById('bridgeCanvas');
							if (canvas === null)
								return;	
							context = canvas.getContext('2d', { alpha: false });
						}
						
						context.clearRect(0, 0, canvas.width, canvas.height);
					};
					
					return {
						clearCanvas: clearCanvas
					};
				})());
			");
		}
		
		private static void ClearCanvas()
		{
			if (!hasInitializedClearCanvasJavascript)
			{
				InitializeClearCanvasJavascript();
				hasInitializedClearCanvasJavascript = true;
			}
			
			Script.Write("window.BridgeClearCanvasJavascript.clearCanvas()");
		}
		
		private static void ClearClickUrl()
		{
			Script.Eval("window.bridgeClickUrl = null;");
		}
		
		private static void UpdateClickUrl(string clickUrl)
		{
			Script.Eval("window.bridgeClickUrl = '" + clickUrl + "';");
		}
		
		private static void AddClickUrlListener()
		{
			Script.Eval(@"
				document.addEventListener('click', function (e) {
					if (window.bridgeClickUrl !== undefined
							&& window.bridgeClickUrl !== null
							&& window.bridgeClickUrl !== '')
						window.open(window.bridgeClickUrl, '_blank');
				}, false);
			");
		}
		
		private static void AddResizeCanvasLogic()
		{
			Script.Write(@"
				((function () {
					let canvas;
					let canvasWidth;
					let canvasHeight;
					
					setInterval(function () {
						if (!window)
							return;
					
						if (!document)
							return;
						
						if (!canvas) {
							canvas = document.getElementById('bridgeCanvas');
							
							if (!canvas)
								return;
						
							canvasWidth = canvas.width;
							canvasHeight = canvas.height;
						}
					
						let innerWidth = window.innerWidth;
						let innerHeight = window.innerHeight;
							
						let canvasScalingX = innerWidth / canvasWidth;
						let canvasScalingY = innerHeight / canvasHeight;
						
						let canvasScaling = Math.min(canvasScalingX, canvasScalingY);
						
						let newCanvasCssWidth = Math.floor(canvasWidth * canvasScaling);
						let newCanvasCssHeight = Math.floor(canvasHeight * canvasScaling);
						
						canvas.style.width = newCanvasCssWidth + 'px';
						canvas.style.height = newCanvasCssHeight + 'px';
						
						let canvasMarginTop;
						
						if (innerHeight > newCanvasCssHeight) {
							canvasMarginTop = Math.floor((innerHeight - newCanvasCssHeight) / 2);
						} else {
							canvasMarginTop = 0;
						}
						
						canvas.style.marginTop = canvasMarginTop + 'px';
					}, 250);
				})());
			");
		}
		
		private static void RemoveMarginOnBody()
		{
			Script.Eval(@"
				((function () {
					var removeMargin;
					
					removeMargin = function () {
						var bodyElement = document.body;
						
						if (!bodyElement) {
							setTimeout(removeMargin, 50);
							return;
						}
						
						bodyElement.style.margin = '0px';
					};
					
					removeMargin();
				})());
			");
		}
		
		public static void Start(
			int fps, 
			bool isEmbeddedVersion,
			bool isElectronVersion,
			bool debugMode)
		{
			hasInitializedClearCanvasJavascript = false;
			
			shouldRenderDisplayLogger = true;
			
			clickUrl = null;
			
			completedAchievements = new HashSet<string>();
			
			ClearClickUrl();
			
			AddClickUrlListener();
			
			BuildType buildType;
			
			if (isEmbeddedVersion)
				buildType = BuildType.WebEmbedded;
			else if (isElectronVersion)
				buildType = BuildType.Electron;
			else
				buildType = BuildType.WebStandAlone;
			
			if (buildType == BuildType.WebEmbedded || buildType == BuildType.Electron)
				RemoveMarginOnBody();
			
			if (buildType == BuildType.Electron)
				AddResizeCanvasLogic();
			
			IDTLogger logger;
			if (debugMode)
			{
				displayLogger = new DisplayLogger(x: 5, y: 95);
				logger = displayLogger;
			}
			else
			{
				displayLogger = null;
				logger = new EmptyLogger();
			}

			GlobalState globalState = new GlobalState(
					fps: fps,
					rng: new DTRandom(),
					guidGenerator: new GuidGenerator(guidString: "94197619109494365160"),
					logger: logger,
					timer: new SimpleTimer(),
					fileIO: new BridgeFileIO(),
					buildType: buildType,
					debugMode: debugMode,
					useDebugAI: false,
					initialMusicVolume: null);
			
			frame = GameInitialization.GetFirstFrame(globalState: globalState);

			bridgeKeyboard = new BridgeKeyboard(disableArrowKeyScrolling: buildType == BuildType.WebEmbedded || buildType == BuildType.Electron);
			bridgeMouse = new BridgeMouse();
						
			display = new BridgeDisplay();
			soundOutput = new BridgeSoundOutput(elapsedMicrosPerFrame: globalState.ElapsedMicrosPerFrame);
			music = new BridgeMusic();

			previousKeyboard = new EmptyKeyboard();
			previousMouse = new EmptyMouse();
			
			ClearCanvas();
			frame.Render(display);
			frame.RenderMusic(music);
			if (displayLogger != null && shouldRenderDisplayLogger)
				displayLogger.Render(displayOutput: display, font: GameFont.GameFont14Pt, color: DTColor.Black());
		}
		
		public static void ProcessExtraTime(int milliseconds)
		{
			frame.ProcessExtraTime(milliseconds: milliseconds);
		}
		
		private static void AddAchievementToJavascriptArray(string achievement)
		{
			Script.Eval("if (!window.BridgeCompletedAchievements) window.BridgeCompletedAchievements = [];");
			Script.Eval("window.BridgeCompletedAchievements.push('" + achievement + "');");
		}
		
		public static void ComputeAndRenderNextFrame()
		{
			IKeyboard currentKeyboard = new CopiedKeyboard(bridgeKeyboard);
			IMouse currentMouse = new CopiedMouse(bridgeMouse);
			
			frame = frame.GetNextFrame(currentKeyboard, currentMouse, previousKeyboard, previousMouse, display, soundOutput, music);
			frame.ProcessMusic();
			soundOutput.ProcessFrame();
			ClearCanvas();
			frame.Render(display);
			frame.RenderMusic(music);

			HashSet<string> newCompletedAchievements = frame.GetCompletedAchievements();

			if (newCompletedAchievements != null)
			{
				foreach (string completedAchievement in newCompletedAchievements)
				{
					bool wasAdded = completedAchievements.Add(completedAchievement);

					if (wasAdded)
						AddAchievementToJavascriptArray(completedAchievement);
				}
			}
			
			string newClickUrl = frame.GetClickUrl();
			
			if (clickUrl != newClickUrl)
			{
				clickUrl = newClickUrl;
				if (clickUrl == null)
					ClearClickUrl();
				else
					UpdateClickUrl(clickUrl: clickUrl);
			}
			
			if (displayLogger != null && shouldRenderDisplayLogger)
				displayLogger.Render(displayOutput: display, font: GameFont.GameFont14Pt, color: DTColor.Black());
			
			if (currentKeyboard.IsPressed(Key.L) && !previousKeyboard.IsPressed(Key.L))
				shouldRenderDisplayLogger = !shouldRenderDisplayLogger;
			
			previousKeyboard = currentKeyboard;
			previousMouse = currentMouse;
		}
	}
}
