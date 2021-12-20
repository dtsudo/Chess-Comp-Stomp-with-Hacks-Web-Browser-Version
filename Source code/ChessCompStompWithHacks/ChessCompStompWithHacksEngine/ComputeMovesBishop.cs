
namespace ChessCompStompWithHacksEngine
{
	using System;
	using System.Collections.Generic;

	public class ComputeMovesBishop
	{
		public static void AddBishopMoves(
			GameState gameState,
			int i,
			int j,
			int kingFile,
			int kingRank,
			List<ComputeMoves.MoveInfo> moves)
		{
			ChessSquarePiece piece = gameState.IsWhiteTurn ? ChessSquarePiece.WhiteBishop : ChessSquarePiece.BlackBishop;

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

					ChessSquarePiece pieceAtDestination = gameState.Board.GetPiece(endI, endJ);

					if (gameState.IsWhiteTurn && !pieceAtDestination.IsWhite() || !gameState.IsWhiteTurn && !pieceAtDestination.IsBlack())
					{
						ComputeMoves.AddMoveInfosForNonEnPassantNonCastlingMoves(
							startingFile: i, 
							startingRank: j, 
							endingFile: endI, 
							endingRank: endJ, 
							piece: piece, 
							gameState: gameState, 
							kingFile: kingFile, 
							kingRank: kingRank,
							moves: moves);
					}

					if (pieceAtDestination != ChessSquarePiece.Empty)
						break;
				}
			}
		}
	}
}
