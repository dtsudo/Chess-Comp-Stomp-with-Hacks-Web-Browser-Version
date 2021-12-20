
namespace ChessCompStompWithHacks
{
	using ChessCompStompWithHacksLibrary;
	using DTLibrary;
	using Bridge;

	public class ChessCompStompWithHacksInitializer
	{
		private static IKeyboard bridgeKeyboard;
		private static IMouse bridgeMouse;
		private static IKeyboard previousKeyboard;
		private static IMouse previousMouse;
		
		private static DTDisplay<ChessImage, ChessFont> display;
		private static ISoundOutput<ChessSound> soundOutput;
		private static IMusic<ChessMusic> music;
		
		private static IFrame<ChessImage, ChessFont, ChessSound, ChessMusic> frame;
		
		private static bool hasInitializedClearCanvasJavascript;
		
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
		
		public static void Start(
			int fps, 
			bool debugMode)
		{
			hasInitializedClearCanvasJavascript = false;
			
			IDTLogger logger;
			if (debugMode)
				logger = new ConsoleLogger();
			else
				logger = new EmptyLogger();

			GlobalState globalState = new GlobalState(
					fps: fps,
					rng: new DTRandom(),
					guidGenerator: new GuidGenerator(guidString: "94197619109494365160"),
					logger: logger,
					timer: new SimpleTimer(),
					isWebBrowserVersion: true,
					debugMode: debugMode,
					initialMusicVolume: null,
					showSoundAndMusicVolumePicker: false);
			
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
		}
		
		public static void ProcessExtraTime(int milliseconds)
		{
			frame.ProcessExtraTime(milliseconds: milliseconds);
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
			
			previousKeyboard = new CopiedKeyboard(currentKeyboard);
			previousMouse = new CopiedMouse(currentMouse);
		}
	}
}
