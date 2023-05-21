
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System;
	using System.Collections.Generic;

	public class HackExplanation_KnightsCanMakeLargeKnightsMove : HackExplanationFrameUtil.IHackExplanation
	{
		private ChessPiecesRenderer chessPiecesRenderer;
		private ChessPiecesRendererPieceAnimation chessPiecesRendererPieceAnimation;

		private ChessSquare knightSquare;
		private List<ChessSquare> potentialNormalKnightMoves;
		private List<ChessSquare> potentialLargeKnightMoves;
		private DTImmutableList<ChessSquare> previousMoveSquares;

		private int moveCooldown;

		private IDTRandom random;

		private ColorTheme colorTheme;

		public HackExplanation_KnightsCanMakeLargeKnightsMove(ColorTheme colorTheme, IDTRandom random)
		{
			this.colorTheme = colorTheme;

			this.random = random;

			this.knightSquare = new ChessSquare(
				file: random.NextInt(8),
				rank: random.NextInt(8));

			this.potentialNormalKnightMoves = GetPossibleNormalKnightMoves(knightSquare: this.knightSquare);
			this.potentialLargeKnightMoves = GetPossibleLargeKnightMoves(knightSquare: this.knightSquare);

			this.moveCooldown = HackExplanationFrameUtil.ELAPSED_MICROS_BEFORE_PIECE_MOVES;

			ChessSquarePieceArray board = ChessSquarePieceArray.EmptyBoard();
			board = board.SetPiece(file: this.knightSquare.File, rank: this.knightSquare.Rank, piece: ChessSquarePiece.WhiteKnight);

			this.previousMoveSquares = DTImmutableList<ChessSquare>.EmptyList();

			this.chessPiecesRenderer = ChessPiecesRenderer.GetChessPiecesRenderer(
				pieces: board,
				kingInDangerSquare: null,
				previousMoveSquares: this.previousMoveSquares,
				renderFromWhitePerspective: true,
				colorTheme: colorTheme);

			this.chessPiecesRendererPieceAnimation = ChessPiecesRendererPieceAnimation.EmptyChessPiecesRendererPieceAnimation();
		}

		private static List<ChessSquare> GetPossibleLargeKnightMoves(ChessSquare knightSquare)
		{
			List<ChessSquare> list = new List<ChessSquare>();

			int i = knightSquare.File;
			int j = knightSquare.Rank;

			List<Tuple<int, int>> knightMoves = new List<Tuple<int, int>>();
			knightMoves.Add(new Tuple<int, int>(i + 1, j + 3));
			knightMoves.Add(new Tuple<int, int>(i + 1, j - 3));
			knightMoves.Add(new Tuple<int, int>(i - 1, j + 3));
			knightMoves.Add(new Tuple<int, int>(i - 1, j - 3));
			knightMoves.Add(new Tuple<int, int>(i + 3, j + 1));
			knightMoves.Add(new Tuple<int, int>(i + 3, j - 1));
			knightMoves.Add(new Tuple<int, int>(i - 3, j + 1));
			knightMoves.Add(new Tuple<int, int>(i - 3, j - 1));

			foreach (Tuple<int, int> knightMove in knightMoves)
			{
				if (0 <= knightMove.Item1 && knightMove.Item1 < 8 && 0 <= knightMove.Item2 && knightMove.Item2 < 8)
					list.Add(new ChessSquare(file: knightMove.Item1, rank: knightMove.Item2));
			}

			return list;
		}

		private static List<ChessSquare> GetPossibleNormalKnightMoves(ChessSquare knightSquare)
		{
			List<ChessSquare> list = new List<ChessSquare>();

			int i = knightSquare.File;
			int j = knightSquare.Rank;

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

		public void ProcessFrame(IMouse mouseInput, IMouse previousMouseInput, IDisplayProcessing<GameImage> displayProcessing, int elapsedMicrosPerFrame)
		{
			this.moveCooldown -= elapsedMicrosPerFrame;
			if (this.moveCooldown <= 0)
			{
				this.moveCooldown += HackExplanationFrameUtil.ELAPSED_MICROS_BEFORE_PIECE_MOVES;
				if (this.moveCooldown <= 0)
					this.moveCooldown = 0;

				ChessSquare originalKnightSquare = this.knightSquare;

				if (this.random.NextInt(100) < 70)
					this.knightSquare = this.potentialLargeKnightMoves[this.random.NextInt(this.potentialLargeKnightMoves.Count)];
				else
					this.knightSquare = this.potentialNormalKnightMoves[this.random.NextInt(this.potentialNormalKnightMoves.Count)];

				this.potentialNormalKnightMoves = GetPossibleNormalKnightMoves(knightSquare: this.knightSquare);
				this.potentialLargeKnightMoves = GetPossibleLargeKnightMoves(knightSquare: this.knightSquare);

				this.previousMoveSquares = new DTImmutableList<ChessSquare>(new HashSet<ChessSquare>()
				{
					originalKnightSquare,
					this.knightSquare
				});

				this.chessPiecesRendererPieceAnimation = this.chessPiecesRendererPieceAnimation.AddRawMove(
					startingFile: originalKnightSquare.File,
					startingRank: originalKnightSquare.Rank,
					endingFile: this.knightSquare.File,
					endingRank: this.knightSquare.Rank,
					piece: ChessSquarePiece.WhiteKnight);
			}
			
			ChessSquarePieceArray board = ChessSquarePieceArray.EmptyBoard();
			board = board.SetPiece(file: this.knightSquare.File, rank: this.knightSquare.Rank, piece: ChessSquarePiece.WhiteKnight);

			List<ChessSquare> allKnightMoves = new List<ChessSquare>();
			allKnightMoves.AddRange(this.potentialNormalKnightMoves);
			allKnightMoves.AddRange(this.potentialLargeKnightMoves);

			this.chessPiecesRenderer = this.chessPiecesRenderer.ProcessFrame(
				pieces: board,
				kingInDangerSquare: null,
				previousMoveSquares: this.previousMoveSquares,
				selectedPieceSquare: null,
				possibleMoveSquares: DTImmutableList<ChessSquare>.AsImmutableList(allKnightMoves),
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
				x: 334,
				y: HackExplanationFrameUtil.TITLE_TEXT_Y_OFFSET,
				text: Hack.KnightsCanMakeLargeKnightsMove.GetHackNameForHackExplanationPanel(),
				font: GameFont.GameFont20Pt,
				color: DTColor.Black());

			string explanation = "Your knights may make large" + "\n"
				+ "knight's moves (moving" + "\n"
				+ "forward 3 squares and 1" + "\n"
				+ "square to the side).";

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
