
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;

	public class HackExplanation_TacticalNuke : HackExplanationFrameUtil.IHackExplanation
	{
		private NukeRenderer nukeRenderer;

		public HackExplanation_TacticalNuke(ColorTheme colorTheme, ITimer timer, int elapsedMicrosPerFrame)
		{
			this.nukeRenderer = NukeRenderer.GetNukeRenderer(
				hasNukeAbility: true,
				hasUsedNuke: false,
				isNukeSelected: false,
				isHoverOverNuke: null,
				turnCount: GetTurnCount(),
				timer: timer,
				colorTheme: colorTheme,
				isClickingOnNuke: false);

			this.nukeRenderer = this.nukeRenderer.ProcessFrame(
				hasUsedNuke: false,
				isNukeSelected: false,
				isHoverOverNuke: null,
				turnCount: GetTurnCount(),
				elapsedMicrosPerFrame: elapsedMicrosPerFrame,
				mouseInput: new EmptyMouse());
		}

		private static int GetTurnCount()
		{
			return TacticalNukeUtil.NumberOfMovesPlayedBeforeNukeIsAvailable + 1;
		}

		public void ProcessFrame(IMouse mouseInput, IMouse previousMouseInput, IDisplayProcessing<GameImage> displayProcessing, int elapsedMicrosPerFrame)
		{
			this.nukeRenderer = this.nukeRenderer.ProcessFrame(
				hasUsedNuke: false,
				isNukeSelected: false,
				isHoverOverNuke: null,
				turnCount: GetTurnCount(),
				elapsedMicrosPerFrame: elapsedMicrosPerFrame,
				mouseInput: new EmptyMouse());
		}

		public void Render(IDisplayOutput<GameImage, GameFont> displayOutput, bool isMobileDisplayType)
		{
			bool isMobilePortrait = isMobileDisplayType && !displayOutput.IsMobileInLandscapeOrientation();

			displayOutput.DrawText(
				x: isMobilePortrait ? 209 : 359,
				y: isMobilePortrait ? HackExplanationFrameUtil.TITLE_TEXT_Y_OFFSET_MOBILE_PORTRAIT : HackExplanationFrameUtil.TITLE_TEXT_Y_OFFSET_DESKTOP_AND_MOBILE_LANDSCAPE,
				text: Hack.TacticalNuke.GetHackNameForHackExplanationPanel(),
				font: GameFont.GameFont20Pt,
				color: DTColor.Black());

			string explanation;

			if (isMobilePortrait)
				explanation = "You start each game with a nuke." + "\n"
					+ "\n"
					+ "The nuke requires " + TacticalNukeUtil.NumberOfMovesPlayedBeforeNukeIsAvailable.ToStringCultureInvariant() + " turns before it is" + "\n"
					+ "operational." + "\n"
					+ "\n"
					+ "The nuke cannot target the opponent's king.";
			else
				explanation = "You start each game with a" + "\n"
					+ "nuke." + "\n"
					+ "\n"
					+ "The nuke requires " + TacticalNukeUtil.NumberOfMovesPlayedBeforeNukeIsAvailable.ToStringCultureInvariant() + " turns" + "\n"
					+ "before it is operational." + "\n"
					+ "\n"
					+ "The nuke cannot target the" + "\n"
					+ "opponent's king.";

			displayOutput.DrawText(
				x: isMobilePortrait ? HackExplanationFrameUtil.EXPLANATION_TEXT_X_OFFSET_MOBILE_PORTRAIT : HackExplanationFrameUtil.EXPLANATION_TEXT_X_OFFSET_DESKTOP_AND_MOBILE_LANDSCAPE,
				y: isMobilePortrait ? HackExplanationFrameUtil.EXPLANATION_TEXT_Y_OFFSET_MOBILE_PORTRAIT : HackExplanationFrameUtil.EXPLANATION_TEXT_Y_OFFSET_DESKTOP_AND_MOBILE_LANDSCAPE,
				text: explanation,
				font: GameFont.GameFont16Pt,
				color: DTColor.Black());
			
			this.nukeRenderer.Render(
				endingY: 1100, // doesn't really matter what endingY is in this case
				scalingFactorScaled: 128,
				displayOutput: new TranslatedDisplayOutput<GameImage, GameFont>(
					display: displayOutput,
					xOffsetInPixels: isMobilePortrait ? 234 : 532,
					yOffsetInPixels: isMobilePortrait ? 340 : 40),
				isMobileDisplayType: isMobileDisplayType);
		}
	}
}
