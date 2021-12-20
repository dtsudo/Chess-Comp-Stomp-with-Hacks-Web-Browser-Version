
namespace ChessCompStompWithHacksEngine
{
	using System;
	using System.Collections.Generic;

	public class ComputeMovesRook
	{
		public static void AddRookMoves(
			GameState gameState,
			int i,
			int j,
			int kingFile,
			int kingRank,
			List<ComputeMoves.MoveInfo> moves)
		{
			ChessSquarePiece piece = gameState.IsWhiteTurn ? ChessSquarePiece.WhiteRook : ChessSquarePiece.BlackRook;

			List<Tuple<int, int>> deltas = new List<Tuple<int, int>>();
			deltas.Add(new Tuple<int, int>(0, 1));
			deltas.Add(new Tuple<int, int>(0, -1));
			deltas.Add(new Tuple<int, int>(1, 0));
			deltas.Add(new Tuple<int, int>(-1, 0));
			if (gameState.IsPlayerTurn() && gameState.Abilities.CanRooksMoveLikeBishops)
			{
				deltas.Add(new Tuple<int, int>(1, 1));
				deltas.Add(new Tuple<int, int>(1, -1));
				deltas.Add(new Tuple<int, int>(-1, 1));
				deltas.Add(new Tuple<int, int>(-1, -1));
			}

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
					{
						if (gameState.IsPlayerTurn() && gameState.Abilities.CanRooksCaptureLikeCannons)
						{
							while (true)
							{
								endI = endI + delta.Item1;
								endJ = endJ + delta.Item2;
								if (endI < 0 || endI >= 8 || endJ < 0 || endJ >= 8)
									break;

								if (gameState.Board.GetPiece(endI, endJ) != ChessSquarePiece.Empty)
								{
									if (gameState.IsWhiteTurn && gameState.Board.GetPiece(endI, endJ).IsBlack()
										|| !gameState.IsWhiteTurn && gameState.Board.GetPiece(endI, endJ).IsWhite())
									{
										ComputeMoves.AddMoveInfosForNonEnPassantNonCastlingMoves(startingFile: i, startingRank: j, endingFile: endI, endingRank: endJ, piece: piece, gameState: gameState, kingFile: kingFile, kingRank: kingRank, moves: moves);
									}
									break;
								}
							}
						}
						break;
					}
				}
			}
		}
	}
}
