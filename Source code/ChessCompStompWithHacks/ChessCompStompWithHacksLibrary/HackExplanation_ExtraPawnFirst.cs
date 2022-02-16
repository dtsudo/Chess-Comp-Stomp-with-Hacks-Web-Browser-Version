
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System.Collections.Generic;

	public class HackExplanation_ExtraPawnFirst : HackExplanationFrameUtil.IHackExplanation
	{
		private ChessPiecesRenderer chessPiecesRenderer;
		private ChessPiecesRendererPieceAnimation chessPiecesRendererPieceAnimation;

		public HackExplanation_ExtraPawnFirst(ColorTheme colorTheme, int elapsedMicrosPerFrame)
		{
			GameState gameState = NewGameCreation.CreateNewGame(
				isPlayerWhite: true,
				researchedHacks: new DTImmutableList<Hack>(set: new HashSet<Hack>() { Hack.ExtraPawnFirst }),
				aiHackLevel: SessionState.AIHackLevel.Initial);

			this.chessPiecesRenderer = ChessPiecesRenderer.GetChessPiecesRenderer(
				pieces: gameState.Board,
				kingInDangerSquare: null,
				previousMoveSquares: DTImmutableList<ChessSquare>.EmptyList(),
				renderFromWhitePerspective: true,
				colorTheme: colorTheme);

			this.chessPiecesRenderer = this.chessPiecesRenderer.ProcessFrame(
				pieces: gameState.Board,
				kingInDangerSquare: null,
				previousMoveSquares: DTImmutableList<ChessSquare>.EmptyList(),
				selectedPieceSquare: null,
				possibleMoveSquares: DTImmutableList<ChessSquare>.EmptyList(),
				potentialNukeSquaresInfo: null,
				hoverSquare: null,
				hoverPieceInfo: null,
				elapsedMicrosPerFrame: elapsedMicrosPerFrame);

			this.chessPiecesRendererPieceAnimation = ChessPiecesRendererPieceAnimation.EmptyChessPiecesRendererPieceAnimation();
			this.chessPiecesRendererPieceAnimation = this.chessPiecesRendererPieceAnimation.ProcessFrame(elapsedMicrosPerFrame: elapsedMicrosPerFrame);
		}

		public void ProcessFrame(IMouse mouseInput, IMouse previousMouseInput, IDisplayProcessing<ChessImage> displayProcessing, int elapsedMicrosPerFrame)
		{
		}

		public void Render(IDisplayOutput<ChessImage, ChessFont> displayOutput)
		{
			displayOutput.DrawText(
				x: 373,
				y: HackExplanationFrameUtil.TITLE_TEXT_Y_OFFSET,
				text: Hack.ExtraPawnFirst.GetHackNameForHackExplanationPanel(),
				font: ChessFont.ChessFont20Pt,
				color: DTColor.Black());

			string explanation = "Start with an extra pawn." + "\n"
				+ "\n"
				+ "Like other pawns, this extra" + "\n"
				+ "pawn may advance 2 squares" + "\n"
				+ "on its first move.";

			displayOutput.DrawText(
				x: HackExplanationFrameUtil.EXPLANATION_TEXT_X_OFFSET,
				y: HackExplanationFrameUtil.EXPLANATION_TEXT_Y_OFFSET,
				text: explanation,
				font: ChessFont.ChessFont16Pt,
				color: DTColor.Black());

			this.chessPiecesRenderer.Render(
				displayOutput: new TranslatedDisplayOutput<ChessImage, ChessFont>(
					display: displayOutput, 
					xOffsetInPixels: HackExplanationFrameUtil.CHESS_PIECES_RENDERER_X_OFFSET,
					yOffsetInPixels: HackExplanationFrameUtil.CHESS_PIECES_RENDERER_Y_OFFSET),
				chessPiecesRendererPieceAnimation: this.chessPiecesRendererPieceAnimation);
		}
	}
}
