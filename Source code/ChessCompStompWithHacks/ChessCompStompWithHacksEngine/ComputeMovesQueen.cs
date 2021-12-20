
namespace ChessCompStompWithHacksEngine
{
	using System;
	using System.Collections.Generic;

	public class ComputeMovesQueen
	{
		public static void AddQueenMoves(
			GameState gameState,
			int i,
			int j,
			int kingFile,
			int kingRank,
			List<ComputeMoves.MoveInfo> moves)
		{
			ChessSquarePiece piece = gameState.IsWhiteTurn ? ChessSquarePiece.WhiteQueen : ChessSquarePiece.BlackQueen;

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

					ChessSquarePiece pieceAtDestination = gameState.Board.GetPiece(endI, endJ);

					if (gameState.IsWhiteTurn && !pieceAtDestination.IsWhite() || !gameState.IsWhiteTurn && !pieceAtDestination.IsBlack())
						ComputeMoves.AddMoveInfosForNonEnPassantNonCastlingMoves(startingFile: i, startingRank: j, endingFile: endI, endingRank: endJ, piece: piece, gameState: gameState, kingFile: kingFile, kingRank: kingRank, moves: moves);

					if (pieceAtDestination != ChessSquarePiece.Empty)
						break;
				}
			}

			if (gameState.IsPlayerTurn() && gameState.Abilities.CanQueensMoveLikeKnights)
			{
				List<Tuple<int, int>> knightMoves = new List<Tuple<int, int>>();
				knightMoves.Add(new Tuple<int, int>(i + 1, j + 2));
				knightMoves.Add(new Tuple<int, int>(i + 1, j - 2));
				knightMoves.Add(new Tuple<int, int>(i - 1, j + 2));
				knightMoves.Add(new Tuple<int, int>(i - 1, j - 2));
				knightMoves.Add(new Tuple<int, int>(i + 2, j + 1));
				knightMoves.Add(new Tuple<int, int>(i + 2, j - 1));
				knightMoves.Add(new Tuple<int, int>(i - 2, j + 1));
				knightMoves.Add(new Tuple<int, int>(i - 2, j - 1));
				if (gameState.IsPlayerTurn() && gameState.Abilities.CanKnightsMakeLargeKnightsMove)
				{
					knightMoves.Add(new Tuple<int, int>(i + 1, j + 3));
					knightMoves.Add(new Tuple<int, int>(i + 1, j - 3));
					knightMoves.Add(new Tuple<int, int>(i - 1, j + 3));
					knightMoves.Add(new Tuple<int, int>(i - 1, j - 3));
					knightMoves.Add(new Tuple<int, int>(i + 3, j + 1));
					knightMoves.Add(new Tuple<int, int>(i + 3, j - 1));
					knightMoves.Add(new Tuple<int, int>(i - 3, j + 1));
					knightMoves.Add(new Tuple<int, int>(i - 3, j - 1));
				}

				foreach (Tuple<int, int> knightMove in knightMoves)
				{
					if (0 <= knightMove.Item1 && knightMove.Item1 < 8 && 0 <= knightMove.Item2 && knightMove.Item2 < 8)
					{
						ChessSquarePiece pieceAtDestination = gameState.Board.GetPiece(knightMove.Item1, knightMove.Item2);

						if (gameState.IsWhiteTurn && !pieceAtDestination.IsWhite() || !gameState.IsWhiteTurn && !pieceAtDestination.IsBlack())
							ComputeMoves.AddMoveInfosForNonEnPassantNonCastlingMoves(startingFile: i, startingRank: j, endingFile: knightMove.Item1, endingRank: knightMove.Item2, piece: piece, gameState: gameState, kingFile: kingFile, kingRank: kingRank, moves: moves);
					}
				}
			}
		}
	}
}
