
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System;
	using System.Collections.Generic;
	
	public class ChessPiecesRendererUtil
	{
		public static ChessSquare GetKingInDangerSquare(GameState gameState)
		{
			int? kingFile = null;
			int? kingRank = null;

			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					if (gameState.Board.GetPiece(i, j).IsKing() && gameState.Board.GetPiece(i, j).IsWhite() == gameState.IsWhiteTurn)
					{
						kingFile = i;
						kingRank = j;
						break;
					}
				}

				if (kingFile.HasValue)
					break;
			}

			bool isKingUnderAttack = CheckKingUnderAttack.IsKingUnderThreat(
				board: gameState.Board,
				playerAbilities: gameState.Abilities,
				checkWhiteKingUnderAttack: gameState.IsWhiteTurn,
				isPlayerWhite: gameState.IsPlayerWhite,
				kingFile: kingFile.Value,
				kingRank: kingRank.Value);

			if (isKingUnderAttack)
				return new ChessSquare(file: kingFile.Value, rank: kingRank.Value);
			return null;
		}

		public static DTImmutableList<ChessSquare> GetPreviousMoveSquares(GameState originalGameState, DisplayMove displayMove)
		{
			return GetPreviousMoveSquares(originalGameState: originalGameState, move: displayMove.Move);
		}

		public static DTImmutableList<ChessSquare> GetPreviousMoveSquares(GameState originalGameState, Move move)
		{
			if (move.IsNuke)
			{
				List<ChessSquare> nukedSquares = TacticalNukeUtil.GetNukedSquares(file: move.EndingFile, rank: move.EndingRank);

				return DTImmutableList<ChessSquare>.AsImmutableList(nukedSquares);
			}

			if (move.IsCastlingOrSuperCastling(originalBoard: originalGameState.Board))
			{
				HashSet<ChessSquare> squares = new HashSet<ChessSquare>();
				
				ChessSquare king = new ChessSquare(move.StartingFile.Value, move.StartingRank.Value);

				Tuple<int, int> direction;
				if (move.EndingFile - move.StartingFile.Value == 2)
					direction = new Tuple<int, int>(1, 0);
				else if (move.EndingFile - move.StartingFile.Value == -2)
					direction = new Tuple<int, int>(-1, 0);
				else if (move.EndingRank - move.StartingRank.Value == 2)
					direction = new Tuple<int, int>(0, 1);
				else if (move.EndingRank - move.StartingRank.Value == -2)
					direction = new Tuple<int, int>(0, -1);
				else
					throw new Exception();

				ChessSquare rook = king;
				while (true)
				{
					rook = new ChessSquare(rook.File + direction.Item1, rook.Rank + direction.Item2);
					if (originalGameState.Board.GetPiece(rook).IsRook())
						break;
				}

				squares.Add(king);
				squares.Add(rook);
				squares.Add(new ChessSquare(file: move.EndingFile, rank: move.EndingRank));
				squares.Add(new ChessSquare(file: (move.StartingFile.Value + move.EndingFile) / 2, rank: (move.StartingRank.Value + move.EndingRank) / 2));

				return new DTImmutableList<ChessSquare>(squares);
			}
			else
			{
				List<ChessSquare> squares = new List<ChessSquare>();
				squares.Add(new ChessSquare(file: move.StartingFile.Value, rank: move.StartingRank.Value));
				squares.Add(new ChessSquare(file: move.EndingFile, rank: move.EndingRank));

				return DTImmutableList<ChessSquare>.AsImmutableList(squares);
			}
		}
	}
}
