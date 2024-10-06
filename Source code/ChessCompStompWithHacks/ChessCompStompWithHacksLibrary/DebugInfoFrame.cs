
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System;
	using System.Collections.Generic;

	public class DebugInfoFrame : IFrame<GameImage, GameFont, GameSound, GameMusic>
	{
		private GlobalState globalState;
		private SessionState sessionState;
		private DisplayType displayType;

		private string debugText;

		public DebugInfoFrame(
			GlobalState globalState,
			SessionState sessionState,
			IDisplayProcessing<GameImage> display,
			bool isMobileDisplayType)
		{
			this.globalState = globalState;
			this.sessionState = sessionState;

			if (isMobileDisplayType)
				this.displayType = display.IsMobileInLandscapeOrientation() ? DisplayType.MobileLandscape : DisplayType.MobilePortrait;
			else
				this.displayType = DisplayType.Desktop;

			this.debugText = "";
		}

		public string GetClickUrl()
		{
			return null;
		}

		public HashSet<string> GetCompletedAchievements()
		{
			return new HashSet<string>();
		}

		public void ProcessExtraTime(int milliseconds)
		{
		}

		public IFrame<GameImage, GameFont, GameSound, GameMusic> ProcessDisplayType(DisplayType displayType, IDisplayProcessing<GameImage> displayProcessing)
		{
			this.displayType = displayType;

			return this;
		}

		private static string ProcessLine(string str)
		{
			if (str.Length <= 50)
				return str;

			return str.Substring(0, 50) + "\n    " + ProcessLine(str.Substring(50));
		}

		public IFrame<GameImage, GameFont, GameSound, GameMusic> GetNextFrame(
			IKeyboard keyboardInput,
			IMouse mouseInput,
			IKeyboard previousKeyboardInput,
			IMouse previousMouseInput,
			IDisplayProcessing<GameImage> displayProcessing,
			ISoundOutput<GameSound> soundOutput,
			IMusicProcessing musicProcessing)
		{
			VersionInfo versionInfo = VersionHistory.GetVersionInfo();

			this.debugText = "FPS: " + this.globalState.Fps.ToStringCultureInvariant() + "\n";
			this.debugText += "Version: " + versionInfo.Version + "\n";
			this.debugText += "Version guid: " + versionInfo.AlphanumericVersionGuid + "\n";
			this.debugText += "User agent: \n    " + ProcessLine(displayProcessing.Debug_GetBrowserInfo("(window.navigator.userAgent + '')")) + "\n";
			this.debugText += "Screen width: " + displayProcessing.Debug_GetBrowserInfo("(window.screen.width + '')") + "\n";
			this.debugText += "Screen height: " + displayProcessing.Debug_GetBrowserInfo("(window.screen.height + '')") + "\n";
			this.debugText += "matchMedia('(pointer:fine)').matches: " + displayProcessing.Debug_GetBrowserInfo("(window.matchMedia('(pointer:fine)').matches ? 'true' : 'false')") + "\n";

			string completedAchievements = "";
			bool isFirst = true;
			foreach (string completedAchievement in this.sessionState.GetCompletedAchievements())
			{
				if (isFirst)
					isFirst = false;
				else
					completedAchievements += ", ";
				completedAchievements += completedAchievement;
			}

			this.debugText += "Completed achievements: \n    " + ProcessLine(completedAchievements);

			if (keyboardInput.IsPressed(Key.Esc) && !previousKeyboardInput.IsPressed(Key.Esc))
			{
				if (this.displayType == DisplayType.Desktop)
					return new TitleScreenDesktopFrame(globalState: this.globalState, sessionState: this.sessionState);
				else
					return new TitleScreenMobileFrame(globalState: this.globalState, sessionState: this.sessionState, display: displayProcessing);
			}

			return this;
		}

		public void ProcessMusic()
		{
			this.globalState.ProcessMusic();
		}

		public void Render(IDisplayOutput<GameImage, GameFont> displayOutput)
		{
			int windowHeight = this.displayType == DisplayType.Desktop ? GlobalConstants.DESKTOP_WINDOW_HEIGHT : displayOutput.GetMobileScreenHeight();

			displayOutput.DrawRectangle(
				x: 0,
				y: 0,
				width: this.displayType == DisplayType.Desktop ? GlobalConstants.DESKTOP_WINDOW_WIDTH : displayOutput.GetMobileScreenWidth(),
				height: windowHeight,
				color: new DTColor(223, 220, 217),
				fill: true);

			displayOutput.DrawText(
				x: 50,
				y: windowHeight - 50,
				text: this.debugText,
				font: GameFont.GameFont16Pt,
				color: DTColor.Black());
		}

		public void RenderMusic(IMusicOutput<GameMusic> musicOutput)
		{
			this.globalState.RenderMusic(musicOutput: musicOutput);
		}
	}
}
