
namespace ChessCompStompWithHacksEngine
{
	using System.Collections.Generic;

	public class ComputeMovesTacticalNuke
	{
		public static void AddTacticalNukeMoves(
			GameState gameState,
			int kingFile,
			int kingRank,
			List<ComputeMoves.MoveInfo> moves)
		{
			if (!gameState.IsPlayerTurn())
				return;

			if (!gameState.Abilities.HasTacticalNuke)
				return;

			if (gameState.HasUsedNuke)
				return;

			if (gameState.TurnCount <= TacticalNukeUtil.NumberOfMovesPlayedBeforeNukeIsAvailable)
				return;
			
			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					List<ChessSquare> nukedSquares = TacticalNukeUtil.GetNukedSquares(i, j);

					bool eitherKingGetsNuked = false;
					ChessSquarePieceArray nukedBoard = gameState.Board;

					foreach (ChessSquare nukedSquare in nukedSquares)
					{
						if (gameState.Board.GetPiece(nukedSquare.File, nukedSquare.Rank).IsKing())
						{
							eitherKingGetsNuked = true;
							break;
						}

						nukedBoard = nukedBoard.SetPiece(nukedSquare.File, nukedSquare.Rank, ChessSquarePiece.Empty);
					}

					if (eitherKingGetsNuked)
						continue;

					if (!CheckKingUnderAttack.IsKingUnderThreat(
							board: nukedBoard,
							playerAbilities: gameState.Abilities,
							checkWhiteKingUnderAttack: gameState.IsWhiteTurn,
							isPlayerWhite: gameState.IsPlayerWhite,
							kingFile: kingFile,
							kingRank: kingRank))
					{
						moves.Add(new ComputeMoves.MoveInfo(move: Move.TacticalNukeMove(i, j), isCaptureMove: false));
					}
				}
			}
		}
	}
}
