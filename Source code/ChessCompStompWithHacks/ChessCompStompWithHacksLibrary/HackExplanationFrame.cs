
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System.Collections.Generic;

	public class HackExplanationFrame : IFrame<GameImage, GameFont, GameSound, GameMusic>
	{
		private GlobalState globalState;
		private SessionState sessionState;
		private HackExplanationFrameUtil.IHackExplanation hackExplanation;
		private IFrame<GameImage, GameFont, GameSound, GameMusic> underlyingFrame;

		private bool crossIconHover;
		private bool crossIconSelected;

		private const int CROSS_ICON_SCALING_FACTOR_SCALED = 128 / 2;

		private const int HACK_EXPLANATION_PANEL_X = 50;
		private const int HACK_EXPLANATION_PANEL_Y = 50;

		private const int HACK_EXPLANATION_PANEL_WIDTH = GlobalConstants.WINDOW_WIDTH - HACK_EXPLANATION_PANEL_X - HACK_EXPLANATION_PANEL_X;
		private const int HACK_EXPLANATION_PANEL_HEIGHT = GlobalConstants.WINDOW_HEIGHT - HACK_EXPLANATION_PANEL_Y - HACK_EXPLANATION_PANEL_Y;

		public HackExplanationFrame(
			GlobalState globalState,
			SessionState sessionState,
			Hack hack,
			IFrame<GameImage, GameFont, GameSound, GameMusic> underlyingFrame)
		{
			bool hasExtraPawnFirstHack = sessionState.GetResearchedHacks().Contains(Hack.ExtraPawnFirst);

			this.globalState = globalState;
			this.sessionState = sessionState;
			this.hackExplanation = HackExplanationFrameUtil.GetHackExplanation(
				hack: hack, 
				colorTheme: sessionState.GetColorTheme(), 
				random: globalState.Rng, 
				hasExtraPawnFirstHack: hasExtraPawnFirstHack,
				timer: globalState.Timer,
				elapsedMicrosPerFrame: globalState.ElapsedMicrosPerFrame);
			this.underlyingFrame = underlyingFrame;

			this.crossIconHover = false;
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

		public IFrame<GameImage, GameFont, GameSound, GameMusic> GetNextFrame(
			IKeyboard keyboardInput, 
			IMouse mouseInput, 
			IKeyboard previousKeyboardInput, 
			IMouse previousMouseInput, 
			IDisplayProcessing<GameImage> displayProcessing, 
			ISoundOutput<GameSound> soundOutput, 
			IMusicProcessing musicProcessing)
		{
			if (keyboardInput.IsPressed(Key.Esc) && !previousKeyboardInput.IsPressed(Key.Esc)
				|| !mouseInput.IsLeftMouseButtonPressed() && previousMouseInput.IsLeftMouseButtonPressed()
				|| !mouseInput.IsRightMouseButtonPressed() && previousMouseInput.IsRightMouseButtonPressed())
			{
				soundOutput.PlaySound(GameSound.Click);
				return this.underlyingFrame;
			}

			this.hackExplanation.ProcessFrame(
				mouseInput: new TranslatedMouse(mouse: mouseInput, xOffset: -HACK_EXPLANATION_PANEL_X, yOffset: -HACK_EXPLANATION_PANEL_Y),
				previousMouseInput: new TranslatedMouse(mouse: previousMouseInput, xOffset: -HACK_EXPLANATION_PANEL_X, yOffset: -HACK_EXPLANATION_PANEL_Y),
				displayProcessing: displayProcessing,
				elapsedMicrosPerFrame: this.globalState.ElapsedMicrosPerFrame);
			
			int crossImageWidth = displayProcessing.GetWidth(GameImage.Cross) * CROSS_ICON_SCALING_FACTOR_SCALED / 128;
			int crossImageHeight = displayProcessing.GetHeight(GameImage.Cross) * CROSS_ICON_SCALING_FACTOR_SCALED / 128;

			int mouseX = mouseInput.GetX();
			int mouseY = mouseInput.GetY();

			this.crossIconHover = HACK_EXPLANATION_PANEL_X + HACK_EXPLANATION_PANEL_WIDTH - crossImageWidth <= mouseX
				&& mouseX <= HACK_EXPLANATION_PANEL_X + HACK_EXPLANATION_PANEL_WIDTH
				&& HACK_EXPLANATION_PANEL_Y + HACK_EXPLANATION_PANEL_HEIGHT - crossImageHeight <= mouseY
				&& mouseY <= HACK_EXPLANATION_PANEL_Y + HACK_EXPLANATION_PANEL_HEIGHT;

			if (mouseInput.IsLeftMouseButtonPressed() && !previousMouseInput.IsLeftMouseButtonPressed() && this.crossIconHover)
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
			this.underlyingFrame.Render(display: displayOutput);
			
			displayOutput.DrawRectangle(
				x: 0,
				y: 0,
				width: GlobalConstants.WINDOW_WIDTH,
				height: GlobalConstants.WINDOW_HEIGHT,
				color: new DTColor(r: 0, g: 0, b: 0, alpha: 64),
				fill: true);

			displayOutput.DrawRectangle(
				x: HACK_EXPLANATION_PANEL_X,
				y: HACK_EXPLANATION_PANEL_Y,
				width: HACK_EXPLANATION_PANEL_WIDTH - 1,
				height: HACK_EXPLANATION_PANEL_HEIGHT - 1,
				color: new DTColor(223, 220, 217),
				fill: true);

			displayOutput.DrawRectangle(
				x: HACK_EXPLANATION_PANEL_X,
				y: HACK_EXPLANATION_PANEL_Y,
				width: HACK_EXPLANATION_PANEL_WIDTH,
				height: HACK_EXPLANATION_PANEL_HEIGHT,
				color: DTColor.Black(),
				fill: false);

			IDisplayOutput<GameImage, GameFont> translatedDisplayOutput = new TranslatedDisplayOutput<GameImage, GameFont>(
				display: displayOutput, 
				xOffsetInPixels: HACK_EXPLANATION_PANEL_X, 
				yOffsetInPixels: HACK_EXPLANATION_PANEL_Y);

			this.hackExplanation.Render(displayOutput: translatedDisplayOutput);

			GameImage crossImage;

			if (this.crossIconSelected)
				crossImage = GameImage.CrossSelected;
			else if (this.crossIconHover)
				crossImage = GameImage.CrossHover;
			else
				crossImage = GameImage.Cross;

			int crossImageWidth = displayOutput.GetWidth(image: crossImage) * CROSS_ICON_SCALING_FACTOR_SCALED / 128;
			int crossImageHeight = displayOutput.GetHeight(image: crossImage) * CROSS_ICON_SCALING_FACTOR_SCALED / 128;

			displayOutput.DrawImageRotatedClockwise(
				image: crossImage,
				x: HACK_EXPLANATION_PANEL_X + HACK_EXPLANATION_PANEL_WIDTH - crossImageWidth,
				y: HACK_EXPLANATION_PANEL_Y + HACK_EXPLANATION_PANEL_HEIGHT - crossImageHeight,
				degreesScaled: 0,
				scalingFactorScaled: CROSS_ICON_SCALING_FACTOR_SCALED);
		}

		public void RenderMusic(IMusicOutput<GameMusic> musicOutput)
		{
			this.globalState.RenderMusic(musicOutput: musicOutput);
		}
	}
}
