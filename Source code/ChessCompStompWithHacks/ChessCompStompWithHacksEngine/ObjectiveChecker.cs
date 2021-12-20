
namespace ChessCompStompWithHacksEngine
{
	using System;
	using System.Collections.Generic;

	public class ObjectiveChecker
	{
		public static HashSet<Objective> GetCompletedObjectives(
			GameState originalGameState, 
			Move move,
			bool isFinalBattle)
		{
			HashSet<Objective> completedObjectives = new HashSet<Objective>();

			if (move.IsNuke)
				completedObjectives.Add(Objective.LaunchANuke);

			if (originalGameState.IsPlayerTurn() && move.Promotion.HasValue && move.Promotion.Value == Move.PromotionType.PromoteToBishop)
				completedObjectives.Add(Objective.PromoteAPieceToABishop);

			GameState newGameState = MoveImplementation.ApplyMove(gameState: originalGameState, move: move);
			ComputeMoves.GameStatus newGameStatus = ComputeMoves.GetMoves(gameState: newGameState).GameStatus;

			bool hasPlayerWon = newGameStatus == ComputeMoves.GameStatus.WhiteVictory && newGameState.IsPlayerWhite
				|| newGameStatus == ComputeMoves.GameStatus.BlackVictory && !newGameState.IsPlayerWhite;

			if (hasPlayerWon)
				completedObjectives.Add(Objective.DefeatComputer);

			if (hasPlayerWon && isFinalBattle)
				completedObjectives.Add(Objective.WinFinalBattle);

			int numberOfMovesPlayedByPlayer;
			if (newGameState.IsPlayerWhite)
				numberOfMovesPlayedByPlayer = newGameState.TurnCount / 2;
			else
				numberOfMovesPlayedByPlayer = (newGameState.TurnCount - 1) / 2;

			if (hasPlayerWon && numberOfMovesPlayedByPlayer <= 25)
				completedObjectives.Add(Objective.DefeatComputerByPlayingAtMost25Moves);

			if (hasPlayerWon && AtLeast5QueensOnTheBoard(board: newGameState.Board))
				completedObjectives.Add(Objective.DefeatComputerWith5QueensOnTheBoard);

			if (HasPlayerDeliveredCheckmateUsingAKnight(hasPlayerWon: hasPlayerWon, gameState: newGameState))
				completedObjectives.Add(Objective.CheckmateUsingAKnight);
			
			return completedObjectives;
		}

		private static bool AtLeast5QueensOnTheBoard(ChessSquarePieceArray board)
		{
			int count = 0;
			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					if (board.GetPiece(i, j).IsQueen())
						count++;
				}
			}

			return count >= 5;
		}

		private static bool HasPlayerDeliveredCheckmateUsingAKnight(
			bool hasPlayerWon,
			GameState gameState)
		{
			if (!hasPlayerWon)
				return false;

			ChessSquarePiece playerKnight = gameState.IsPlayerWhite ? ChessSquarePiece.WhiteKnight : ChessSquarePiece.BlackKnight;
			ChessSquarePiece enemyKing = gameState.IsPlayerWhite ? ChessSquarePiece.BlackKing : ChessSquarePiece.WhiteKing;

			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					if (gameState.Board.GetPiece(i, j) == enemyKing)
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
						if (gameState.Abilities.CanKnightsMakeLargeKnightsMove)
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
								if (gameState.Board.GetPiece(knightMove.Item1, knightMove.Item2) == playerKnight)
									return true;
							}
						}
					}
				}
			}

			return false;
		}
	}
}
