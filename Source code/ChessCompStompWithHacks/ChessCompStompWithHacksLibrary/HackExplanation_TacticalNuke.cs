
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
				colorTheme: colorTheme);

			this.nukeRenderer = this.nukeRenderer.ProcessFrame(
				hasUsedNuke: false,
				isNukeSelected: false,
				isHoverOverNuke: null,
				turnCount: GetTurnCount(),
				elapsedMicrosPerFrame: elapsedMicrosPerFrame);
		}

		private static int GetTurnCount()
		{
			return TacticalNukeUtil.NumberOfMovesPlayedBeforeNukeIsAvailable + 1;
		}

		public void ProcessFrame(IMouse mouseInput, IMouse previousMouseInput, IDisplayProcessing<ChessImage> displayProcessing, int elapsedMicrosPerFrame)
		{
			this.nukeRenderer = this.nukeRenderer.ProcessFrame(
				hasUsedNuke: false,
				isNukeSelected: false,
				isHoverOverNuke: null,
				turnCount: GetTurnCount(),
				elapsedMicrosPerFrame: elapsedMicrosPerFrame);
		}

		public void Render(IDisplayOutput<ChessImage, ChessFont> displayOutput)
		{
			displayOutput.DrawText(
				x: 359,
				y: HackExplanationFrameUtil.TITLE_TEXT_Y_OFFSET,
				text: Hack.TacticalNuke.GetHackNameForHackExplanationPanel(),
				font: ChessFont.ChessFont20Pt,
				color: DTColor.Black());

			string explanation = "You start each game with a" + "\n"
				+ "nuke." + "\n"
				+ "\n"
				+ "The nuke requires " + TacticalNukeUtil.NumberOfMovesPlayedBeforeNukeIsAvailable.ToStringCultureInvariant() + " turns" + "\n"
				+ "before it is operational." + "\n"
				+ "\n"
				+ "The nuke cannot target the" + "\n"
				+ "opponent's king.";

			displayOutput.DrawText(
				x: HackExplanationFrameUtil.EXPLANATION_TEXT_X_OFFSET,
				y: HackExplanationFrameUtil.EXPLANATION_TEXT_Y_OFFSET,
				text: explanation,
				font: ChessFont.ChessFont16Pt,
				color: DTColor.Black());
			
			this.nukeRenderer.Render(displayOutput: new TranslatedDisplayOutput<ChessImage, ChessFont>(
				display: displayOutput,
				xOffsetInPixels: 532,
				yOffsetInPixels: 40));
		}
	}
}
