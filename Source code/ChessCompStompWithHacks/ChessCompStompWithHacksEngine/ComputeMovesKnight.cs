
namespace ChessCompStompWithHacksEngine
{
	using System;
	using System.Collections.Generic;

	public class ComputeMovesKnight
	{
		public static void AddKnightMoves(
			GameState gameState,
			int i,
			int j,
			int kingFile,
			int kingRank,
			List<ComputeMoves.MoveInfo> moves)
		{
			ChessSquarePiece piece = gameState.IsWhiteTurn ? ChessSquarePiece.WhiteKnight : ChessSquarePiece.BlackKnight;

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
					{
						ComputeMoves.AddMoveInfosForNonEnPassantNonCastlingMoves(
							startingFile: i, 
							startingRank: j, 
							endingFile: knightMove.Item1, 
							endingRank: knightMove.Item2, 
							piece: piece, 
							gameState: gameState, 
							kingFile: kingFile, 
							kingRank: kingRank,
							moves: moves);
					}
				}
			}
		}
	}
}
