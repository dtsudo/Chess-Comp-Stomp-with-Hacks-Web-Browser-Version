
namespace ChessCompStompWithHacksEngine
{
	using System;
	using System.Collections.Generic;

	public class CheckKingUnderAttack
	{
		public static ChessSquare FindLocationOfKing(ChessSquarePieceArray board, bool findWhiteKing)
		{
			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					ChessSquarePiece piece = board.GetPiece(file: i, rank: j);
					if (piece != ChessSquarePiece.Empty && piece.IsKing() && piece.IsWhite() == findWhiteKing)
						return new ChessSquare(file: i, rank: j);
				}
			}

			throw new Exception();
		}

		/// <summary>
		/// Note that this method is used (in part) to determine whether a move is legal.
		/// So the board input may represent an invalid board position; e.g. maybe both
		/// kings are in check, or the kings are adjacent to each other, etc.
		/// </summary>
		public static bool IsKingUnderThreat(
			ChessSquarePieceArray board,
			GameState.PlayerAbilities playerAbilities,
			bool checkWhiteKingUnderAttack,
			bool isPlayerWhite,
			int kingFile,
			int kingRank)
		{
			bool isKingAttackedByPawn = IsKingUnderThreatByPawn(
				board: board,
				playerAbilities: playerAbilities,
				checkWhiteKingUnderAttack: checkWhiteKingUnderAttack,
				isPlayerWhite: isPlayerWhite,
				kingFile: kingFile,
				kingRank: kingRank);

			if (isKingAttackedByPawn)
				return true;

			bool isKingAttackedFromEightDirections = IsKingUnderThreatFromEightDirections(
				board: board,
				playerAbilities: playerAbilities,
				checkWhiteKingUnderAttack: checkWhiteKingUnderAttack,
				isPlayerWhite: isPlayerWhite,
				kingFile: kingFile,
				kingRank: kingRank);

			if (isKingAttackedFromEightDirections)
				return true;

			bool isKingAttackedViaKnightMove = IsKingUnderThreatByKnightOrLargeKnightMove(
				board: board,
				playerAbilities: playerAbilities,
				checkWhiteKingUnderAttack: checkWhiteKingUnderAttack,
				isPlayerWhite: isPlayerWhite,
				kingFile: kingFile,
				kingRank: kingRank);

			if (isKingAttackedViaKnightMove)
				return true;

			return false;
		}

		private static bool IsKingUnderThreatByPawn(
			ChessSquarePieceArray board,
			GameState.PlayerAbilities playerAbilities,
			bool checkWhiteKingUnderAttack,
			bool isPlayerWhite,
			int kingFile,
			int kingRank)
		{
			if (checkWhiteKingUnderAttack)
			{
				int pawnFile = kingFile - 1;
				int pawnRank = kingRank + 1;

				if (InRange(pawnFile) && InRange(pawnRank) && board.GetPiece(pawnFile, pawnRank) == ChessSquarePiece.BlackPawn)
					return true;

				pawnFile = kingFile + 1;
				pawnRank = kingRank + 1;

				if (InRange(pawnFile) && InRange(pawnRank) && board.GetPiece(pawnFile, pawnRank) == ChessSquarePiece.BlackPawn)
					return true;

				if (!isPlayerWhite && playerAbilities.CanSuperEnPassant)
				{
					pawnFile = kingFile - 1;
					pawnRank = kingRank;
					if (InRange(pawnFile) && InRange(pawnRank) && board.GetPiece(pawnFile, pawnRank) == ChessSquarePiece.BlackPawn
							&& InRange(kingRank - 1) && board.GetPiece(kingFile, kingRank - 1).IsBlack() == false)
						return true;

					pawnFile = kingFile + 1;
					pawnRank = kingRank;
					if (InRange(pawnFile) && InRange(pawnRank) && board.GetPiece(pawnFile, pawnRank) == ChessSquarePiece.BlackPawn
							&& InRange(kingRank - 1) && board.GetPiece(kingFile, kingRank - 1).IsBlack() == false)
						return true;
				}
			}
			else
			{
				int pawnFile = kingFile - 1;
				int pawnRank = kingRank - 1;

				if (InRange(pawnFile) && InRange(pawnRank) && board.GetPiece(pawnFile, pawnRank) == ChessSquarePiece.WhitePawn)
					return true;

				pawnFile = kingFile + 1;
				pawnRank = kingRank - 1;

				if (InRange(pawnFile) && InRange(pawnRank) && board.GetPiece(pawnFile, pawnRank) == ChessSquarePiece.WhitePawn)
					return true;

				if (isPlayerWhite && playerAbilities.CanSuperEnPassant)
				{
					pawnFile = kingFile - 1;
					pawnRank = kingRank;
					if (InRange(pawnFile) && InRange(pawnRank) && board.GetPiece(pawnFile, pawnRank) == ChessSquarePiece.WhitePawn
							&& InRange(kingRank + 1) && board.GetPiece(kingFile, kingRank + 1).IsWhite() == false)
						return true;

					pawnFile = kingFile + 1;
					pawnRank = kingRank;
					if (InRange(pawnFile) && InRange(pawnRank) && board.GetPiece(pawnFile, pawnRank) == ChessSquarePiece.WhitePawn
							&& InRange(kingRank + 1) && board.GetPiece(kingFile, kingRank + 1).IsWhite() == false)
						return true;
				}
			}

			return false;
		}

		private static bool IsKingUnderThreatFromEightDirections(
			ChessSquarePieceArray board,
			GameState.PlayerAbilities playerAbilities,
			bool checkWhiteKingUnderAttack,
			bool isPlayerWhite,
			int kingFile,
			int kingRank)
		{
			bool shouldCheckRookCannon = playerAbilities.CanRooksCaptureLikeCannons
				&& (isPlayerWhite && !checkWhiteKingUnderAttack || !isPlayerWhite && checkWhiteKingUnderAttack);
		
			ChessSquarePiece enemyKing = checkWhiteKingUnderAttack ? ChessSquarePiece.BlackKing : ChessSquarePiece.WhiteKing;
			ChessSquarePiece enemyQueen = checkWhiteKingUnderAttack ? ChessSquarePiece.BlackQueen : ChessSquarePiece.WhiteQueen;
			ChessSquarePiece enemyRook = checkWhiteKingUnderAttack ? ChessSquarePiece.BlackRook : ChessSquarePiece.WhiteRook;
			ChessSquarePiece enemyBishop = checkWhiteKingUnderAttack ? ChessSquarePiece.BlackBishop : ChessSquarePiece.WhiteBishop;

			List<ChessSquarePiece> piecesThatAttackHorizontallyOrVertically = new List<ChessSquarePiece>();
			piecesThatAttackHorizontallyOrVertically.Add(enemyRook);
			piecesThatAttackHorizontallyOrVertically.Add(enemyQueen);

			List<ChessSquarePiece> piecesThatAttackDiagonally = new List<ChessSquarePiece>();
			piecesThatAttackDiagonally.Add(enemyBishop);
			piecesThatAttackDiagonally.Add(enemyQueen);
			if (playerAbilities.CanRooksMoveLikeBishops
					&& (isPlayerWhite && !checkWhiteKingUnderAttack || !isPlayerWhite && checkWhiteKingUnderAttack))
			{
				piecesThatAttackDiagonally.Add(enemyRook);
			}

			List<Tuple<int, int, List<ChessSquarePiece>>> deltas = new List<Tuple<int, int, List<ChessSquarePiece>>>();
			deltas.Add(new Tuple<int, int, List<ChessSquarePiece>>(1, 0, piecesThatAttackHorizontallyOrVertically));
			deltas.Add(new Tuple<int, int, List<ChessSquarePiece>>(-1, 0, piecesThatAttackHorizontallyOrVertically));
			deltas.Add(new Tuple<int, int, List<ChessSquarePiece>>(0, 1, piecesThatAttackHorizontallyOrVertically));
			deltas.Add(new Tuple<int, int, List<ChessSquarePiece>>(0, -1, piecesThatAttackHorizontallyOrVertically));
			deltas.Add(new Tuple<int, int, List<ChessSquarePiece>>(1, 1, piecesThatAttackDiagonally));
			deltas.Add(new Tuple<int, int, List<ChessSquarePiece>>(1, -1, piecesThatAttackDiagonally));
			deltas.Add(new Tuple<int, int, List<ChessSquarePiece>>(-1, 1, piecesThatAttackDiagonally));
			deltas.Add(new Tuple<int, int, List<ChessSquarePiece>>(-1, -1, piecesThatAttackDiagonally));

			foreach (Tuple<int, int, List<ChessSquarePiece>> delta in deltas)
			{
				int i = kingFile;
				int j = kingRank;
				bool isFirstIteration = true;
				while (true)
				{
					i += delta.Item1;
					j += delta.Item2;

					if (i < 0 || i >= 8 || j < 0 || j >= 8)
						break;

					ChessSquarePiece pieceAtThisSquare = board.GetPiece(i, j);
					if (pieceAtThisSquare != ChessSquarePiece.Empty)
					{
						foreach (ChessSquarePiece enemyPiece in delta.Item3)
						{
							if (pieceAtThisSquare == enemyPiece)
								return true;
						}

						if (isFirstIteration && pieceAtThisSquare == enemyKing)
							return true;
					}
					
					isFirstIteration = false;

					if (pieceAtThisSquare != ChessSquarePiece.Empty)
					{
						if (shouldCheckRookCannon && delta.Item3.Contains(enemyRook))
						{
							while (true)
							{
								i += delta.Item1;
								j += delta.Item2;
								if (i < 0 || i >= 8 || j < 0 || j >= 8)
									break;

								if (board.GetPiece(i, j) == enemyRook)
									return true;

								if (board.GetPiece(i, j) != ChessSquarePiece.Empty)
									break;
							}
						}

						break;
					}
				}
			}
			
			return false;
		}

		private static bool IsKingUnderThreatByKnightOrLargeKnightMove(
			ChessSquarePieceArray board,
			GameState.PlayerAbilities playerAbilities,
			bool checkWhiteKingUnderAttack,
			bool isPlayerWhite,
			int kingFile,
			int kingRank)
		{
			List<Tuple<int, int>> deltas = new List<Tuple<int, int>>();
			deltas.Add(new Tuple<int, int>(2, 1));
			deltas.Add(new Tuple<int, int>(1, 2));
			deltas.Add(new Tuple<int, int>(2, -1));
			deltas.Add(new Tuple<int, int>(1, -2));
			deltas.Add(new Tuple<int, int>(-2, 1));
			deltas.Add(new Tuple<int, int>(-1, 2));
			deltas.Add(new Tuple<int, int>(-2, -1));
			deltas.Add(new Tuple<int, int>(-1, -2));

			if (playerAbilities.CanKnightsMakeLargeKnightsMove
				&& (checkWhiteKingUnderAttack && !isPlayerWhite || !checkWhiteKingUnderAttack && isPlayerWhite))
			{
				deltas.Add(new Tuple<int, int>(3, 1));
				deltas.Add(new Tuple<int, int>(1, 3));
				deltas.Add(new Tuple<int, int>(3, -1));
				deltas.Add(new Tuple<int, int>(1, -3));
				deltas.Add(new Tuple<int, int>(-3, 1));
				deltas.Add(new Tuple<int, int>(-1, 3));
				deltas.Add(new Tuple<int, int>(-3, -1));
				deltas.Add(new Tuple<int, int>(-1, -3));
			}

			ChessSquarePiece enemyKnight = checkWhiteKingUnderAttack ? ChessSquarePiece.BlackKnight : ChessSquarePiece.WhiteKnight;
			ChessSquarePiece enemyQueen = checkWhiteKingUnderAttack ? ChessSquarePiece.BlackQueen : ChessSquarePiece.WhiteQueen;
			
			foreach (Tuple<int, int> delta in deltas)
			{
				int i = kingFile + delta.Item1;
				int j = kingRank + delta.Item2;

				if (i < 0 || i >= 8 || j < 0 || j >= 8)
					continue;

				if (board.GetPiece(i, j) == enemyKnight)
					return true;

				if (playerAbilities.CanQueensMoveLikeKnights
						&& (checkWhiteKingUnderAttack && !isPlayerWhite || !checkWhiteKingUnderAttack && isPlayerWhite)
						&& board.GetPiece(i, j) == enemyQueen)
					return true;
			}
			
			return false;
		}

		private static bool InRange(int x)
		{
			return 0 <= x && x < 8;
		}
	}
}
