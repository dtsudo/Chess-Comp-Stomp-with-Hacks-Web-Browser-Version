
namespace ChessCompStompWithHacks
{
	using Bridge;
	using ChessCompStompWithHacksLibrary;
	using DTLibrary;
	using System.Collections.Generic;

	public class ChessCompStompWithHacksInitializer
	{
		private static IKeyboard bridgeKeyboard;
		private static IMouse bridgeMouse;
		private static IKeyboard previousKeyboard;
		private static IMouse previousMouse;
		
		private static DTDisplay<ChessImage, ChessFont> display;
		private static ISoundOutput<ChessSound> soundOutput;
		private static IMusic<ChessMusic> music;
		
		private static DisplayLogger displayLogger;
		private static bool shouldRenderDisplayLogger;
		
		private static HashSet<string> completedAchievements;
		
		private static IFrame<ChessImage, ChessFont, ChessSound, ChessMusic> frame;
		
		private static bool hasInitializedClearCanvasJavascript;
		
		private static string clickUrl;
		
		private static void InitializeClearCanvasJavascript()
		{
			Script.Eval(@"
				window.ChessCompStompWithHacksBridgeClearCanvasJavascript = ((function () {
					'use strict';
					
					var canvas = null;
					var context = null;
								
					var clearCanvas = function () {
						if (canvas === null) {
							canvas = document.getElementById('chessCompStompWithHacksCanvas');
							if (canvas === null)
								return;	
							context = canvas.getContext('2d');
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
			
			Script.Write("window.ChessCompStompWithHacksBridgeClearCanvasJavascript.clearCanvas()");
		}
		
		private static void ClearClickUrl()
		{
			Script.Eval("window.chessCompStompWithHacksClickUrl = null;");
		}
		
		private static void UpdateClickUrl(string clickUrl)
		{
			Script.Eval("window.chessCompStompWithHacksClickUrl = '" + clickUrl + "';");
		}
		
		private static void AddClickUrlListener()
		{
			Script.Eval(@"
				document.addEventListener('click', function (e) {
					if (window.chessCompStompWithHacksClickUrl !== undefined
							&& window.chessCompStompWithHacksClickUrl !== null
							&& window.chessCompStompWithHacksClickUrl !== '')
						window.open(window.chessCompStompWithHacksClickUrl, '_blank');
				}, false);
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
			bool isWebPortalVersion,
			bool debugMode)
		{
			hasInitializedClearCanvasJavascript = false;
			
			shouldRenderDisplayLogger = true;
			
			clickUrl = null;
			
			completedAchievements = new HashSet<string>();
			
			ClearClickUrl();
			
			AddClickUrlListener();
			
			if (isWebPortalVersion)
				RemoveMarginOnBody();
			
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
					isWebBrowserVersion: true,
					isWebPortalVersion: isWebPortalVersion,
					debugMode: debugMode,
					useDebugAI: false,
					initialMusicVolume: null);
			
			frame = ChessCompStompWithHacks.GetFirstFrame(globalState: globalState);

			bridgeKeyboard = new BridgeKeyboard();
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
				displayLogger.Render(displayOutput: display, font: ChessFont.ChessFont14Pt, color: DTColor.Black());
		}
		
		public static void ProcessExtraTime(int milliseconds)
		{
			frame.ProcessExtraTime(milliseconds: milliseconds);
		}
		
		private static void AddAchievementToJavascriptArray(string achievement)
		{
			Script.Eval("if (!window.chessCompStompWithHacksCompletedAchievements) window.chessCompStompWithHacksCompletedAchievements = [];");
			
			Script.Eval("window.chessCompStompWithHacksCompletedAchievements.push('" + achievement + "');");
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
			
			foreach (string completedAchievement in newCompletedAchievements)
			{
				bool wasAdded = completedAchievements.Add(completedAchievement);
				
				if (wasAdded)
					AddAchievementToJavascriptArray(completedAchievement);
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
				displayLogger.Render(displayOutput: display, font: ChessFont.ChessFont14Pt, color: DTColor.Black());
			
			if (currentKeyboard.IsPressed(Key.L) && !previousKeyboard.IsPressed(Key.L))
				shouldRenderDisplayLogger = !shouldRenderDisplayLogger;
			
			previousKeyboard = new CopiedKeyboard(currentKeyboard);
			previousMouse = new CopiedMouse(currentMouse);
		}
	}
}
