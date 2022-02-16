
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System;
	using System.Collections.Generic;

	public class HackExplanation_RooksCanMoveLikeBishops : HackExplanationFrameUtil.IHackExplanation
	{
		private ChessPiecesRenderer chessPiecesRenderer;
		private ChessPiecesRendererPieceAnimation chessPiecesRendererPieceAnimation;

		private ChessSquare rookSquare;
		private List<ChessSquare> potentialNormalRookMoves;
		private List<ChessSquare> potentialBishopRookMoves;
		private DTImmutableList<ChessSquare> previousMoveSquares;

		private int moveCooldown;

		private IDTRandom random;

		private ColorTheme colorTheme;

		public HackExplanation_RooksCanMoveLikeBishops(ColorTheme colorTheme, IDTRandom random)
		{
			this.colorTheme = colorTheme;

			this.random = random;

			this.rookSquare = new ChessSquare(
				file: random.NextInt(8),
				rank: random.NextInt(8));

			this.potentialNormalRookMoves = GetPossibleNormalRookMoves(rookSquare: this.rookSquare);
			this.potentialBishopRookMoves = GetPossibleBishopRookMoves(rookSquare: this.rookSquare);

			this.moveCooldown = HackExplanationFrameUtil.ELAPSED_MICROS_BEFORE_PIECE_MOVES;

			ChessSquarePieceArray board = ChessSquarePieceArray.EmptyBoard();
			board = board.SetPiece(file: this.rookSquare.File, rank: this.rookSquare.Rank, piece: ChessSquarePiece.WhiteRook);

			this.previousMoveSquares = DTImmutableList<ChessSquare>.EmptyList();

			this.chessPiecesRenderer = ChessPiecesRenderer.GetChessPiecesRenderer(
				pieces: board,
				kingInDangerSquare: null,
				previousMoveSquares: this.previousMoveSquares,
				renderFromWhitePerspective: true,
				colorTheme: colorTheme);
			
			this.chessPiecesRendererPieceAnimation = ChessPiecesRendererPieceAnimation.EmptyChessPiecesRendererPieceAnimation();
		}

		private static List<ChessSquare> GetPossibleBishopRookMoves(ChessSquare rookSquare)
		{
			List<ChessSquare> list = new List<ChessSquare>();

			int i = rookSquare.File;
			int j = rookSquare.Rank;

			List<Tuple<int, int>> deltas = new List<Tuple<int, int>>();
			deltas.Add(new Tuple<int, int>(1, 1));
			deltas.Add(new Tuple<int, int>(1, -1));
			deltas.Add(new Tuple<int, int>(-1, 1));
			deltas.Add(new Tuple<int, int>(-1, -1));

			foreach (Tuple<int, int> delta in deltas)
			{
				int endI = i;
				int endJ = j;
				while (true)
				{
					endI = endI + delta.Item1;
					endJ = endJ + delta.Item2;
					if (endI < 0 || endI >= 8 || endJ < 0 || endJ >= 8)
						break;

					list.Add(new ChessSquare(file: endI, rank: endJ));
				}
			}

			return list;
		}

		private static List<ChessSquare> GetPossibleNormalRookMoves(ChessSquare rookSquare)
		{
			List<ChessSquare> list = new List<ChessSquare>();

			int i = rookSquare.File;
			int j = rookSquare.Rank;

			List<Tuple<int, int>> deltas = new List<Tuple<int, int>>();
			deltas.Add(new Tuple<int, int>(0, 1));
			deltas.Add(new Tuple<int, int>(0, -1));
			deltas.Add(new Tuple<int, int>(1, 0));
			deltas.Add(new Tuple<int, int>(-1, 0));

			foreach (Tuple<int, int> delta in deltas)
			{
				int endI = i;
				int endJ = j;
				while (true)
				{
					endI = endI + delta.Item1;
					endJ = endJ + delta.Item2;
					if (endI < 0 || endI >= 8 || endJ < 0 || endJ >= 8)
						break;

					list.Add(new ChessSquare(file: endI, rank: endJ));
				}
			}
			
			return list;
		}

		public void ProcessFrame(IMouse mouseInput, IMouse previousMouseInput, IDisplayProcessing<ChessImage> displayProcessing, int elapsedMicrosPerFrame)
		{
			this.moveCooldown -= elapsedMicrosPerFrame;
			if (this.moveCooldown <= 0)
			{
				this.moveCooldown += HackExplanationFrameUtil.ELAPSED_MICROS_BEFORE_PIECE_MOVES;
				if (this.moveCooldown <= 0)
					this.moveCooldown = 0;

				ChessSquare originalRookSquare = this.rookSquare;

				if (this.random.NextInt(100) < 70)
					this.rookSquare = this.potentialBishopRookMoves[this.random.NextInt(this.potentialBishopRookMoves.Count)];
				else
					this.rookSquare = this.potentialNormalRookMoves[this.random.NextInt(this.potentialNormalRookMoves.Count)];

				this.potentialNormalRookMoves = GetPossibleNormalRookMoves(rookSquare: this.rookSquare);
				this.potentialBishopRookMoves = GetPossibleBishopRookMoves(rookSquare: this.rookSquare);

				this.previousMoveSquares = new DTImmutableList<ChessSquare>(new HashSet<ChessSquare>()
				{
					originalRookSquare,
					this.rookSquare
				});

				this.chessPiecesRendererPieceAnimation = this.chessPiecesRendererPieceAnimation.AddRawMove(
					startingFile: originalRookSquare.File,
					startingRank: originalRookSquare.Rank,
					endingFile: this.rookSquare.File,
					endingRank: this.rookSquare.Rank,
					piece: ChessSquarePiece.WhiteRook);
			}
			
			ChessSquarePieceArray board = ChessSquarePieceArray.EmptyBoard();
			board = board.SetPiece(file: this.rookSquare.File, rank: this.rookSquare.Rank, piece: ChessSquarePiece.WhiteRook);

			List<ChessSquare> allRookMoves = new List<ChessSquare>();
			allRookMoves.AddRange(this.potentialNormalRookMoves);
			allRookMoves.AddRange(this.potentialBishopRookMoves);

			this.chessPiecesRenderer = this.chessPiecesRenderer.ProcessFrame(
				pieces: board,
				kingInDangerSquare: null,
				previousMoveSquares: this.previousMoveSquares,
				selectedPieceSquare: null,
				possibleMoveSquares: DTImmutableList<ChessSquare>.AsImmutableList(allRookMoves),
				potentialNukeSquaresInfo: null,
				hoverSquare: null,
				hoverPieceInfo: null,
				elapsedMicrosPerFrame: elapsedMicrosPerFrame);

			this.chessPiecesRendererPieceAnimation = this.chessPiecesRendererPieceAnimation.ProcessFrame(
				elapsedMicrosPerFrame: elapsedMicrosPerFrame);
		}

		public void Render(IDisplayOutput<ChessImage, ChessFont> displayOutput)
		{
			displayOutput.DrawText(
				x: 349,
				y: HackExplanationFrameUtil.TITLE_TEXT_Y_OFFSET,
				text: Hack.RooksCanMoveLikeBishops.GetHackNameForHackExplanationPanel(),
				font: ChessFont.ChessFont20Pt,
				color: DTColor.Black());

			string explanation = "In addition to their normal" + "\n"
				+ "moves, your rooks may also" + "\n"
				+ "move as if they were bishops.";

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
