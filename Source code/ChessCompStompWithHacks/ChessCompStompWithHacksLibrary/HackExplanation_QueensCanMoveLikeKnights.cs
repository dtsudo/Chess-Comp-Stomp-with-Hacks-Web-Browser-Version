
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System;
	using System.Collections.Generic;

	public class HackExplanation_QueensCanMoveLikeKnights : HackExplanationFrameUtil.IHackExplanation
	{
		private ChessPiecesRenderer chessPiecesRenderer;
		private ChessPiecesRendererPieceAnimation chessPiecesRendererPieceAnimation;

		private ChessSquare queenSquare;
		private List<ChessSquare> potentialNormalQueenMoves;
		private List<ChessSquare> potentialKnightQueenMoves;
		private DTImmutableList<ChessSquare> previousMoveSquares;

		private int moveCooldown;

		private IDTRandom random;

		private ColorTheme colorTheme;

		public HackExplanation_QueensCanMoveLikeKnights(ColorTheme colorTheme, IDTRandom random)
		{
			this.colorTheme = colorTheme;

			this.random = random;

			this.queenSquare = new ChessSquare(
				file: random.NextInt(8),
				rank: random.NextInt(8));

			this.potentialNormalQueenMoves = GetPossibleNormalQueenMoves(queenSquare: this.queenSquare);
			this.potentialKnightQueenMoves = GetPossibleKnightQueenMoves(queenSquare: this.queenSquare);

			this.moveCooldown = HackExplanationFrameUtil.ELAPSED_MICROS_BEFORE_PIECE_MOVES;

			ChessSquarePieceArray board = ChessSquarePieceArray.EmptyBoard();
			board = board.SetPiece(file: this.queenSquare.File, rank: this.queenSquare.Rank, piece: ChessSquarePiece.WhiteQueen);

			this.previousMoveSquares = DTImmutableList<ChessSquare>.EmptyList();

			this.chessPiecesRenderer = ChessPiecesRenderer.GetChessPiecesRenderer(
				pieces: board,
				kingInDangerSquare: null,
				previousMoveSquares: this.previousMoveSquares,
				renderFromWhitePerspective: true,
				colorTheme: this.colorTheme);
			
			this.chessPiecesRendererPieceAnimation = ChessPiecesRendererPieceAnimation.EmptyChessPiecesRendererPieceAnimation();
		}

		private static List<ChessSquare> GetPossibleKnightQueenMoves(ChessSquare queenSquare)
		{
			List<ChessSquare> list = new List<ChessSquare>();

			int i = queenSquare.File;
			int j = queenSquare.Rank;

			List<Tuple<int, int>> knightMoves = new List<Tuple<int, int>>();
			knightMoves.Add(new Tuple<int, int>(i + 1, j + 2));
			knightMoves.Add(new Tuple<int, int>(i + 1, j - 2));
			knightMoves.Add(new Tuple<int, int>(i - 1, j + 2));
			knightMoves.Add(new Tuple<int, int>(i - 1, j - 2));
			knightMoves.Add(new Tuple<int, int>(i + 2, j + 1));
			knightMoves.Add(new Tuple<int, int>(i + 2, j - 1));
			knightMoves.Add(new Tuple<int, int>(i - 2, j + 1));
			knightMoves.Add(new Tuple<int, int>(i - 2, j - 1));

			foreach (Tuple<int, int> knightMove in knightMoves)
			{
				if (0 <= knightMove.Item1 && knightMove.Item1 < 8 && 0 <= knightMove.Item2 && knightMove.Item2 < 8)
					list.Add(new ChessSquare(file: knightMove.Item1, rank: knightMove.Item2));
			}

			return list;
		}

		private static List<ChessSquare> GetPossibleNormalQueenMoves(ChessSquare queenSquare)
		{
			List<ChessSquare> list = new List<ChessSquare>();

			int i = queenSquare.File;
			int j = queenSquare.Rank;

			List<Tuple<int, int>> deltas = new List<Tuple<int, int>>();
			deltas.Add(new Tuple<int, int>(0, 1));
			deltas.Add(new Tuple<int, int>(0, -1));
			deltas.Add(new Tuple<int, int>(1, 0));
			deltas.Add(new Tuple<int, int>(-1, 0));
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

		public void ProcessFrame(IMouse mouseInput, IMouse previousMouseInput, IDisplayProcessing<GameImage> displayProcessing, int elapsedMicrosPerFrame)
		{
			this.moveCooldown -= elapsedMicrosPerFrame;
			if (this.moveCooldown <= 0)
			{
				this.moveCooldown += HackExplanationFrameUtil.ELAPSED_MICROS_BEFORE_PIECE_MOVES;
				if (this.moveCooldown <= 0)
					this.moveCooldown = 0;

				ChessSquare originalQueenSquare = this.queenSquare;

				if (this.random.NextInt(100) < 70)
					this.queenSquare = this.potentialKnightQueenMoves[this.random.NextInt(this.potentialKnightQueenMoves.Count)];
				else
					this.queenSquare = this.potentialNormalQueenMoves[this.random.NextInt(this.potentialNormalQueenMoves.Count)];

				this.potentialNormalQueenMoves = GetPossibleNormalQueenMoves(queenSquare: this.queenSquare);
				this.potentialKnightQueenMoves = GetPossibleKnightQueenMoves(queenSquare: this.queenSquare);

				this.previousMoveSquares = new DTImmutableList<ChessSquare>(new HashSet<ChessSquare>()
				{
					originalQueenSquare,
					this.queenSquare
				});

				this.chessPiecesRendererPieceAnimation = this.chessPiecesRendererPieceAnimation.AddRawMove(
					startingFile: originalQueenSquare.File,
					startingRank: originalQueenSquare.Rank,
					endingFile: this.queenSquare.File,
					endingRank: this.queenSquare.Rank,
					piece: ChessSquarePiece.WhiteQueen);
			}
			
			ChessSquarePieceArray board = ChessSquarePieceArray.EmptyBoard();
			board = board.SetPiece(file: this.queenSquare.File, rank: this.queenSquare.Rank, piece: ChessSquarePiece.WhiteQueen);

			List<ChessSquare> allQueenMoves = new List<ChessSquare>();
			allQueenMoves.AddRange(this.potentialNormalQueenMoves);
			allQueenMoves.AddRange(this.potentialKnightQueenMoves);

			this.chessPiecesRenderer = this.chessPiecesRenderer.ProcessFrame(
				pieces: board,
				kingInDangerSquare: null,
				previousMoveSquares: this.previousMoveSquares,
				selectedPieceSquare: null,
				possibleMoveSquares: DTImmutableList<ChessSquare>.AsImmutableList(allQueenMoves),
				potentialNukeSquaresInfo: null,
				hoverSquare: null,
				hoverPieceInfo: null,
				elapsedMicrosPerFrame: elapsedMicrosPerFrame);

			this.chessPiecesRendererPieceAnimation = this.chessPiecesRendererPieceAnimation.ProcessFrame(
				elapsedMicrosPerFrame: elapsedMicrosPerFrame);
		}

		public void Render(IDisplayOutput<GameImage, GameFont> displayOutput)
		{
			displayOutput.DrawText(
				x: 344,
				y: HackExplanationFrameUtil.TITLE_TEXT_Y_OFFSET,
				text: Hack.QueensCanMoveLikeKnights.GetHackNameForHackExplanationPanel(),
				font: GameFont.GameFont20Pt,
				color: DTColor.Black());

			string explanation = "Your queen may also move as" + "\n"
				+ "if it were a knight.";

			displayOutput.DrawText(
				x: HackExplanationFrameUtil.EXPLANATION_TEXT_X_OFFSET,
				y: HackExplanationFrameUtil.EXPLANATION_TEXT_Y_OFFSET,
				text: explanation,
				font: GameFont.GameFont16Pt,
				color: DTColor.Black());

			this.chessPiecesRenderer.Render(
				displayOutput: new TranslatedDisplayOutput<GameImage, GameFont>(
					display: displayOutput, 
					xOffsetInPixels: HackExplanationFrameUtil.CHESS_PIECES_RENDERER_X_OFFSET,
					yOffsetInPixels: HackExplanationFrameUtil.CHESS_PIECES_RENDERER_Y_OFFSET),
				chessPiecesRendererPieceAnimation: this.chessPiecesRendererPieceAnimation);
		}
	}
}
