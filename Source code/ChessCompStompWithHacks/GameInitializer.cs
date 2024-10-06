
namespace ChessCompStompWithHacks
{
	using Bridge;
	using ChessCompStompWithHacksLibrary;
	using DTLibrary;
	using System.Collections.Generic;

	public class GameInitializer
	{
		private class CanvasWidthAndHeightInfo : BridgeDisplay.ICanvasWidthAndHeightInfo
		{
			private int canvasWidth;
			private int canvasHeight;

			public CanvasWidthAndHeightInfo()
			{
				this.canvasWidth = GlobalConstants.DESKTOP_WINDOW_WIDTH;
				this.canvasHeight = GlobalConstants.DESKTOP_WINDOW_HEIGHT;
			}

			public void SetCurrentCanvasWidth(int width)
			{
				this.canvasWidth = width;
			}

			public void SetCurrentCanvasHeight(int height)
			{
				this.canvasHeight = height;
			}

			public int GetCurrentCanvasWidth()
			{
				return this.canvasWidth;
			}

			public int GetCurrentCanvasHeight()
			{
				return this.canvasHeight;
			}
		}

		private static BridgeKeyboard bridgeKeyboard;
		private static BridgeMouse bridgeMouse;
		private static IKeyboard previousKeyboard;
		private static IMouse previousMouse;
		
		private static DTDisplay<GameImage, GameFont> display;
		private static ISoundOutput<GameSound> soundOutput;
		private static IMusic<GameMusic> music;

		private static CanvasWidthAndHeightInfo canvasWidthAndHeightInfo;
		private static int canvasScalingFactor;

		private static DisplayLogger displayLogger;
		private static bool shouldRenderDisplayLogger;
		
		private static HashSet<string> completedAchievements;
		
		private static IFrame<GameImage, GameFont, GameSound, GameMusic> frame;
		
		private static bool hasInitializedClearCanvasJavascript;
		
		private static string clickUrl;

		private static void InitializeDisplayTypeHandlingJavascript(bool isWebStandAlone, int canvasScalingFactor, bool debugMode)
		{
			Script.Eval(@"
				window.BridgeDisplayTypeHandlingJavascript = ((function () {
					'use strict';

					let debugMode = " + (debugMode ? "true" : "false") + @";

					let displayTypeOverride = null;
					let useUnscaledCanvas = false;

					if (debugMode) {
						document.addEventListener('keydown', function (e) {
							if (e.key === '0') {
								if (displayTypeOverride === null)
									displayTypeOverride = 'mobile';
								else
									displayTypeOverride = (displayTypeOverride === 'mobile') ? 'desktop' : 'mobile';
							}

							if (e.key === '9') {
								useUnscaledCanvas = !useUnscaledCanvas;
							}
						}, false);
					}

					let canvas = null;
					let context = null;
					let bodyElement = null;

					let canvasScalingFactor = " + canvasScalingFactor.ToStringCultureInvariant() + @";

					let isWebStandAlone = " + (isWebStandAlone ? "true" : "false") + @";
					let defaultWidth = canvasScalingFactor * " + GlobalConstants.DESKTOP_WINDOW_WIDTH.ToStringCultureInvariant() + @";
					let defaultHeight = canvasScalingFactor * " + GlobalConstants.DESKTOP_WINDOW_HEIGHT.ToStringCultureInvariant() + @";

					let isDesktop = true;
					let isMobileLandscape = false;
					let isMobilePortrait = false;

					let currentCanvasWidth = defaultWidth;
					let currentCanvasHeight = defaultHeight;

					let handleDisplayTypeChanges = function () {

						if (!window)
							return;
					
						if (!document)
							return;
						
						if (!bodyElement) {
							bodyElement = document.body;
							if (!bodyElement)
								return;
						}

						if (!canvas) {
							canvas = document.getElementById('bridgeCanvas');
							
							if (!canvas)
								return;

							context = canvas.getContext('2d', { alpha: false });
						}

						let innerWidth = window.innerWidth;
						let innerHeight = window.innerHeight;

						if (innerWidth < 5)
							innerWidth = 5;
						if (innerHeight < 5)
							innerHeight = 5;

						let isLibrem5Mobile = window.navigator.userAgent.toLowerCase().includes('aarch64')
							&& window.navigator.userAgent.toLowerCase().includes('linux')
							&& !window.navigator.userAgent.toLowerCase().includes('android')
							&& (window.screen.height / window.screen.width === 2 || window.screen.width / window.screen.height === 2);
						
						isDesktop = window.matchMedia('(pointer:fine)').matches
							&& !isLibrem5Mobile;

						if (displayTypeOverride === 'desktop')
							isDesktop = true;
						if (displayTypeOverride === 'mobile')
							isDesktop = false;

						if (isDesktop) {
							isMobileLandscape = false;
							isMobilePortrait = false;
						} else {
							isMobileLandscape = innerWidth > innerHeight;
							isMobilePortrait = !isMobileLandscape;
						}

						if (isDesktop && isWebStandAlone)
							bodyElement.style.margin = '8px';
						else
							bodyElement.style.margin = '0px';
						
						let newCanvasWidth;
						let newCanvasHeight;

						if (isDesktop) {
							newCanvasWidth = defaultWidth;
							newCanvasHeight = defaultHeight;
						} else if (isMobileLandscape) {
							newCanvasWidth = Math.max(defaultWidth, Math.round((innerWidth / innerHeight) * defaultHeight));
							newCanvasHeight = defaultHeight;
						} else {
							newCanvasWidth = defaultHeight;
							newCanvasHeight = Math.max(defaultWidth, Math.round((innerHeight / innerWidth) * defaultHeight));
						}

						if (newCanvasWidth !== canvas.width)
							canvas.width = newCanvasWidth;
						if (newCanvasHeight !== canvas.height)
							canvas.height = newCanvasHeight;

						currentCanvasWidth = canvas.width;
						currentCanvasHeight = canvas.height;

						context.setTransform(canvasScalingFactor, 0, 0, canvasScalingFactor, 0, 0);

						let canvasMarginTop;
						if ((isDesktop && !isWebStandAlone || !isDesktop) && !useUnscaledCanvas) {
							let canvasScalingX = innerWidth / canvas.width;
							let canvasScalingY = innerHeight / canvas.height;
						
							let canvasScaling = Math.min(canvasScalingX, canvasScalingY);
						
							let newCanvasCssWidth = Math.floor(canvas.width * canvasScaling);
							let newCanvasCssHeight = Math.floor(canvas.height * canvasScaling);
						
							canvas.style.width = newCanvasCssWidth + 'px';
							canvas.style.height = newCanvasCssHeight + 'px';
						
							if (innerHeight > newCanvasCssHeight) {
								canvasMarginTop = Math.floor((innerHeight - newCanvasCssHeight) / 2);
							} else {
								canvasMarginTop = 0;
							}
						} else {
							canvas.style.width = Math.floor(canvas.width / canvasScalingFactor) + 'px';
							canvas.style.height = Math.floor(canvas.height / canvasScalingFactor) + 'px';
							canvasMarginTop = 0;
						}
						
						canvas.style.marginTop = canvasMarginTop + 'px';

						if (isDesktop && isWebStandAlone) {
							bodyElement.style.backgroundColor = '#ebebeb';
						} else {
							bodyElement.style.backgroundColor = '#c7c2bc';
						}
					};

					setInterval(handleDisplayTypeChanges, 250);
					handleDisplayTypeChanges();

					let isDesktopDisplayType = function () {
						return isDesktop;
					};

					let isMobileLandscapeDisplayType = function () {
						return isMobileLandscape;
					};

					let isMobilePortraitDisplayType = function () {
						return isMobilePortrait;
					};

					return {
						isDesktopDisplayType: isDesktopDisplayType,
						isMobileLandscapeDisplayType: isMobileLandscapeDisplayType,
						isMobilePortraitDisplayType: isMobilePortraitDisplayType,
						getCurrentCanvasWidth: function () { return Math.floor(currentCanvasWidth / canvasScalingFactor); },
						getCurrentCanvasHeight: function () { return Math.floor(currentCanvasHeight / canvasScalingFactor); }
					};
				})());
			");
		}

		private static void InitializeClearCanvasJavascript(int canvasScalingFactor)
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
						let canvasScalingFactor = " + canvasScalingFactor.ToStringCultureInvariant() + @";
						context.setTransform(canvasScalingFactor, 0, 0, canvasScalingFactor, 0, 0);
					};
					
					return {
						clearCanvas: clearCanvas
					};
				})());
			");
		}
		
		private static void ClearCanvas(int canvasScalingFactor)
		{
			if (!hasInitializedClearCanvasJavascript)
			{
				InitializeClearCanvasJavascript(canvasScalingFactor: canvasScalingFactor);
				hasInitializedClearCanvasJavascript = true;
			}
			
			Script.Write("window.BridgeClearCanvasJavascript.clearCanvas()");
		}
		
		private static void ClearClickUrl()
		{
			Script.Write("window.bridgeClickUrl = null;");
		}
		
		private static void UpdateClickUrl(string clickUrl)
		{
			Script.Eval("window.bridgeClickUrl = '" + clickUrl + "';");
		}
		
		private static void AddClickUrlListener()
		{
			Script.Write(@"
				document.addEventListener('click', function (e) {
					if (window.bridgeClickUrl !== undefined
							&& window.bridgeClickUrl !== null
							&& window.bridgeClickUrl !== '')
						window.open(window.bridgeClickUrl, '_blank');
				}, false);
			");
		}
		
		public static void Start(
			int fps, 
			bool isEmbeddedVersion,
			bool isElectronVersion,
			int canvasScalingFactor,
			bool stopWaitingEvenIfMusicHasNotLoaded,
			bool debugMode)
		{
			GameInitializer.canvasScalingFactor = canvasScalingFactor;

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
			
			InitializeDisplayTypeHandlingJavascript(isWebStandAlone: buildType == BuildType.WebStandAlone, canvasScalingFactor: canvasScalingFactor, debugMode: debugMode);
			
			canvasWidthAndHeightInfo = new CanvasWidthAndHeightInfo();

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
			bridgeMouse = new BridgeMouse(canvasScalingFactor: canvasScalingFactor);
						
			display = new BridgeDisplay(canvasWidthAndHeightInfo: canvasWidthAndHeightInfo, canvasScalingFactor: canvasScalingFactor);
			soundOutput = new BridgeSoundOutput(elapsedMicrosPerFrame: globalState.ElapsedMicrosPerFrame);
			music = new BridgeMusic(stopWaitingEvenIfMusicHasNotLoaded: stopWaitingEvenIfMusicHasNotLoaded);

			previousKeyboard = new EmptyKeyboard();
			previousMouse = new EmptyMouse();
			
			ClearCanvas(canvasScalingFactor: canvasScalingFactor);
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
			bridgeKeyboard.ProcessedInputs();
			bridgeMouse.ProcessedInputs();

			DisplayType displayType;
			bool isDesktopDisplayType = Script.Write<bool>("window.BridgeDisplayTypeHandlingJavascript.isDesktopDisplayType()");
			bool isMobileLandscapeDisplayType = Script.Write<bool>("window.BridgeDisplayTypeHandlingJavascript.isMobileLandscapeDisplayType()");

			int currentCanvasWidth = Script.Write<int>("window.BridgeDisplayTypeHandlingJavascript.getCurrentCanvasWidth()");
			int currentCanvasHeight = Script.Write<int>("window.BridgeDisplayTypeHandlingJavascript.getCurrentCanvasHeight()");

			canvasWidthAndHeightInfo.SetCurrentCanvasWidth(currentCanvasWidth);
			canvasWidthAndHeightInfo.SetCurrentCanvasHeight(currentCanvasHeight);

			if (isDesktopDisplayType)
				displayType = DisplayType.Desktop;
			else if (isMobileLandscapeDisplayType)
				displayType = DisplayType.MobileLandscape;
			else
				displayType = DisplayType.MobilePortrait;

			frame = frame.ProcessDisplayType(displayType: displayType, displayProcessing: display);

			frame = frame.GetNextFrame(currentKeyboard, currentMouse, previousKeyboard, previousMouse, display, soundOutput, music);
			frame.ProcessMusic();
			soundOutput.ProcessFrame();
			ClearCanvas(canvasScalingFactor: canvasScalingFactor);
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
