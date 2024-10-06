﻿
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

		public void ProcessFrame(IMouse mouseInput, IMouse previousMouseInput, IDisplayProcessing<GameImage> displayProcessing, int elapsedMicrosPerFrame)
		{
		}

		public void Render(IDisplayOutput<GameImage, GameFont> displayOutput, bool isMobileDisplayType)
		{
			bool isMobilePortrait = isMobileDisplayType && !displayOutput.IsMobileInLandscapeOrientation();
			
			displayOutput.DrawText(
				x: isMobilePortrait ? 223 : 373,
				y: isMobilePortrait ? HackExplanationFrameUtil.TITLE_TEXT_Y_OFFSET_MOBILE_PORTRAIT : HackExplanationFrameUtil.TITLE_TEXT_Y_OFFSET_DESKTOP_AND_MOBILE_LANDSCAPE,
				text: Hack.ExtraPawnFirst.GetHackNameForHackExplanationPanel(),
				font: GameFont.GameFont20Pt,
				color: DTColor.Black());

			string explanation;

			if (isMobilePortrait)
				explanation = "Start with an extra pawn." + "\n"
					+ "\n"
					+ "Like other pawns, this extra pawn may" + "\n"
					+ "advance 2 squares on its first move." + "\n"
					+ "\n"
					+ "This pawn may not be captured en passant.";
			else
				explanation = "Start with an extra pawn." + "\n"
					+ "\n"
					+ "Like other pawns, this extra" + "\n"
					+ "pawn may advance 2 squares" + "\n"
					+ "on its first move." + "\n"
					+ "\n"
					+ "This pawn may not be" + "\n"
					+ "captured en passant.";
			
			displayOutput.DrawText(
				x: isMobilePortrait ? HackExplanationFrameUtil.EXPLANATION_TEXT_X_OFFSET_MOBILE_PORTRAIT : HackExplanationFrameUtil.EXPLANATION_TEXT_X_OFFSET_DESKTOP_AND_MOBILE_LANDSCAPE,
				y: isMobilePortrait ? HackExplanationFrameUtil.EXPLANATION_TEXT_Y_OFFSET_MOBILE_PORTRAIT : HackExplanationFrameUtil.EXPLANATION_TEXT_Y_OFFSET_DESKTOP_AND_MOBILE_LANDSCAPE,
				text: explanation,
				font: GameFont.GameFont16Pt,
				color: DTColor.Black());

			this.chessPiecesRenderer.Render(
				displayOutput: new TranslatedDisplayOutput<GameImage, GameFont>(
					display: displayOutput, 
					xOffsetInPixels: isMobilePortrait ? HackExplanationFrameUtil.CHESS_PIECES_RENDERER_X_OFFSET_MOBILE_PORTRAIT : HackExplanationFrameUtil.CHESS_PIECES_RENDERER_X_OFFSET_DESKTOP_AND_MOBILE_LANDSCAPE,
					yOffsetInPixels: isMobilePortrait ? HackExplanationFrameUtil.CHESS_PIECES_RENDERER_Y_OFFSET_MOBILE_PORTRAIT : HackExplanationFrameUtil.CHESS_PIECES_RENDERER_Y_OFFSET_DESKTOP_AND_MOBILE_LANDSCAPE),
				chessPiecesRendererPieceAnimation: this.chessPiecesRendererPieceAnimation,
				chessPieceScalingFactor: GameImageUtil.HackExplanationChessPieceScalingFactor,
				isMobileDisplayType: isMobileDisplayType);
		}
	}
}
