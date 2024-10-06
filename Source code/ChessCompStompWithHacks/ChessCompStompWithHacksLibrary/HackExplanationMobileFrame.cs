
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System.Collections.Generic;

	public class HackExplanationMobileFrame : IFrame<GameImage, GameFont, GameSound, GameMusic>
	{
		private GlobalState globalState;
		private SessionState sessionState;
		private Hack hack;
		private HackExplanationFrameUtil.IHackExplanation hackExplanation;
		private IFrame<GameImage, GameFont, GameSound, GameMusic> underlyingFrame;
		
		private bool crossIconSelected;

		private const int CROSS_ICON_SCALING_FACTOR_SCALED = 128 / 2;

		public const int HACK_EXPLANATION_PANEL_WIDTH_LANDSCAPE = 900;
		public const int HACK_EXPLANATION_PANEL_WIDTH_PORTRAIT = 600;

		public const int HACK_EXPLANATION_PANEL_HEIGHT_LANDSCAPE = 600;
		public const int HACK_EXPLANATION_PANEL_HEIGHT_PORTRAIT = 900;
		
		public HackExplanationMobileFrame(
			GlobalState globalState,
			SessionState sessionState,
			Hack hack,
			IFrame<GameImage, GameFont, GameSound, GameMusic> underlyingFrame)
		{
			bool hasExtraPawnFirstHack = sessionState.GetResearchedHacks().Contains(Hack.ExtraPawnFirst);

			this.globalState = globalState;
			this.sessionState = sessionState;
			this.hack = hack;
			this.hackExplanation = HackExplanationFrameUtil.GetHackExplanation(
				hack: hack, 
				colorTheme: sessionState.GetColorTheme(), 
				random: globalState.Rng, 
				hasExtraPawnFirstHack: hasExtraPawnFirstHack,
				timer: globalState.Timer,
				elapsedMicrosPerFrame: globalState.ElapsedMicrosPerFrame);
			this.underlyingFrame = underlyingFrame;
			
			this.crossIconSelected = false;
		}

		public string GetClickUrl()
		{
			return null;
		}

		public HashSet<string> GetCompletedAchievements()
		{
			return new HashSet<string>();
		}

		public IFrame<GameImage, GameFont, GameSound, GameMusic> ProcessDisplayType(DisplayType displayType, IDisplayProcessing<GameImage> displayProcessing)
		{
			if (displayType == DisplayType.Desktop)
			{
				this.underlyingFrame = this.underlyingFrame.ProcessDisplayType(displayType: displayType, displayProcessing: displayProcessing);
				return new HackExplanationDesktopFrame(globalState: this.globalState, sessionState: this.sessionState, hack: this.hack, underlyingFrame: this.underlyingFrame);
			}

			return this;
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
			this.underlyingFrame = this.underlyingFrame.GetNextFrame(
				keyboardInput: new EmptyKeyboard(),
				mouseInput: new EmptyMouse(),
				previousKeyboardInput: new EmptyKeyboard(),
				previousMouseInput: new EmptyMouse(),
				displayProcessing: displayProcessing,
				soundOutput: soundOutput,
				musicProcessing: musicProcessing);

			if (keyboardInput.IsPressed(Key.Esc) && !previousKeyboardInput.IsPressed(Key.Esc)
				|| !mouseInput.IsLeftMouseButtonPressed() && previousMouseInput.IsLeftMouseButtonPressed()
				|| !mouseInput.IsRightMouseButtonPressed() && previousMouseInput.IsRightMouseButtonPressed())
			{
				soundOutput.PlaySound(GameSound.Click);
				return this.underlyingFrame;
			}

			bool isLandscape = displayProcessing.IsMobileInLandscapeOrientation();

			int panelWidth = isLandscape ? HACK_EXPLANATION_PANEL_WIDTH_LANDSCAPE : HACK_EXPLANATION_PANEL_WIDTH_PORTRAIT;
			int panelHeight = isLandscape ? HACK_EXPLANATION_PANEL_HEIGHT_LANDSCAPE : HACK_EXPLANATION_PANEL_HEIGHT_PORTRAIT;

			int panelX = (displayProcessing.GetMobileScreenWidth() - panelWidth) / 2;
			int panelY = (displayProcessing.GetMobileScreenHeight() - panelHeight) / 2;

			this.hackExplanation.ProcessFrame(
				mouseInput: new TranslatedMouse(mouse: mouseInput, xOffset: -panelX, yOffset: -panelY),
				previousMouseInput: new TranslatedMouse(mouse: previousMouseInput, xOffset: -panelX, yOffset: -panelY),
				displayProcessing: displayProcessing,
				elapsedMicrosPerFrame: this.globalState.ElapsedMicrosPerFrame);
			
			int crossImageWidth = displayProcessing.GetWidth(GameImage.Cross) * CROSS_ICON_SCALING_FACTOR_SCALED / 128;
			int crossImageHeight = displayProcessing.GetHeight(GameImage.Cross) * CROSS_ICON_SCALING_FACTOR_SCALED / 128;

			int mouseX = mouseInput.GetX();
			int mouseY = mouseInput.GetY();

			bool crossIconHover = panelX + panelWidth - crossImageWidth <= mouseX
				&& mouseX <= panelX + panelWidth
				&& panelY + panelHeight - crossImageHeight <= mouseY
				&& mouseY <= panelY + panelHeight;

			if (mouseInput.IsLeftMouseButtonPressed() && !previousMouseInput.IsLeftMouseButtonPressed() && crossIconHover)
				this.crossIconSelected = true;

			if (this.crossIconSelected && !mouseInput.IsLeftMouseButtonPressed())
				this.crossIconSelected = false;

			return this;
		}

		public void ProcessExtraTime(int milliseconds)
		{
			this.underlyingFrame.ProcessExtraTime(milliseconds: milliseconds);
		}

		public void ProcessMusic()
		{
			this.globalState.ProcessMusic();
		}

		public void Render(IDisplayOutput<GameImage, GameFont> displayOutput)
		{
			bool isLandscape = displayOutput.IsMobileInLandscapeOrientation();

			int panelWidth = isLandscape ? HACK_EXPLANATION_PANEL_WIDTH_LANDSCAPE : HACK_EXPLANATION_PANEL_WIDTH_PORTRAIT;
			int panelHeight = isLandscape ? HACK_EXPLANATION_PANEL_HEIGHT_LANDSCAPE : HACK_EXPLANATION_PANEL_HEIGHT_PORTRAIT;

			int panelX = (displayOutput.GetMobileScreenWidth() - panelWidth) / 2;
			int panelY = (displayOutput.GetMobileScreenHeight() - panelHeight) / 2;

			this.underlyingFrame.Render(display: displayOutput);
			
			displayOutput.DrawRectangle(
				x: 0,
				y: 0,
				width: displayOutput.GetMobileScreenWidth(),
				height: displayOutput.GetMobileScreenHeight(),
				color: new DTColor(r: 0, g: 0, b: 0, alpha: 64),
				fill: true);

			displayOutput.DrawRectangle(
				x: panelX,
				y: panelY,
				width: panelWidth,
				height: panelHeight,
				color: new DTColor(223, 220, 217),
				fill: true);

			displayOutput.DrawRectangle(
				x: panelX,
				y: panelY,
				width: panelWidth,
				height: panelHeight,
				color: DTColor.Black(),
				fill: false);

			IDisplayOutput<GameImage, GameFont> translatedDisplayOutput = new TranslatedDisplayOutput<GameImage, GameFont>(
				display: displayOutput, 
				xOffsetInPixels: panelX, 
				yOffsetInPixels: panelY);

			this.hackExplanation.Render(displayOutput: translatedDisplayOutput, isMobileDisplayType: true);

			GameImage crossImage;

			if (this.crossIconSelected)
				crossImage = GameImage.CrossSelected;
			else
				crossImage = GameImage.Cross;

			int crossImageWidth = displayOutput.GetWidth(image: crossImage) * CROSS_ICON_SCALING_FACTOR_SCALED / 128;
			int crossImageHeight = displayOutput.GetHeight(image: crossImage) * CROSS_ICON_SCALING_FACTOR_SCALED / 128;

			displayOutput.DrawImageRotatedClockwise(
				image: crossImage,
				x: panelX + panelWidth - crossImageWidth,
				y: panelY + panelHeight - crossImageHeight,
				degreesScaled: 0,
				scalingFactorScaled: CROSS_ICON_SCALING_FACTOR_SCALED);
		}

		public void RenderMusic(IMusicOutput<GameMusic> musicOutput)
		{
			this.globalState.RenderMusic(musicOutput: musicOutput);
		}
	}
}
