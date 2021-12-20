
namespace ChessCompStompWithHacksEngine
{
	using System;
	using System.Collections.Generic;

	public class ComputeMovesKing
	{		
		public static void AddKingMoves(
			GameState gameState,
			int i,
			int j,
			List<ComputeMoves.MoveInfo> moves)
		{
			AddNonCastlingKingMoves(
				gameState: gameState,
				i: i,
				j: j,
				moves: moves);
			
			bool canSuperCastle = gameState.IsPlayerTurn() && gameState.Abilities.CanSuperCastle;
			
			if (canSuperCastle)
			{
				AddSuperCastlingKingMoves(
					gameState: gameState,
					i: i,
					j: j,
					moves: moves);
			}
			else
			{
				AddNormalCastlingKingMoves(
					gameState: gameState,
					i: i,
					j: j,
					moves: moves);
			}
		}

		private static void AddNormalCastlingKingMoves(
			GameState gameState,
			int i,
			int j,
			List<ComputeMoves.MoveInfo> moves)
		{
			ChessSquarePiece friendlyKing = gameState.IsWhiteTurn ? ChessSquarePiece.WhiteKing : ChessSquarePiece.BlackKing;
			ChessSquarePiece friendlyRook = gameState.IsWhiteTurn ? ChessSquarePiece.WhiteRook : ChessSquarePiece.BlackRook;

			int backRank = gameState.IsWhiteTurn ? 0 : 7;

			bool canNormalCastleKingside = gameState.IsWhiteTurn
				? gameState.Castling.CanWhiteCastleKingside
				: gameState.Castling.CanBlackCastleKingside;

			bool canNormalCastleQueenside = gameState.IsWhiteTurn
				? gameState.Castling.CanWhiteCastleQueenside
				: gameState.Castling.CanBlackCastleQueenside;

			if (canNormalCastleKingside)
			{
				if (gameState.Board.GetPiece(4, backRank) == friendlyKing
					&& gameState.Board.GetPiece(5, backRank) == ChessSquarePiece.Empty
					&& gameState.Board.GetPiece(6, backRank) == ChessSquarePiece.Empty
					&& gameState.Board.GetPiece(7, backRank) == friendlyRook)
				{
					ChessSquarePieceArray interimBoard = gameState.Board.SetPiece(4, backRank, ChessSquarePiece.Empty).SetPiece(5, backRank, friendlyKing);
					ChessSquarePieceArray newBoard = gameState.Board
						.SetPiece(4, backRank, ChessSquarePiece.Empty)
						.SetPiece(5, backRank, friendlyRook)
						.SetPiece(6, backRank, friendlyKing)
						.SetPiece(7, backRank, ChessSquarePiece.Empty);
					GetMoveInfosForCastlingAndSuperCastlingMoves(
						startingFile: i,
						startingRank: j,
						endingFile: 6,
						endingRank: backRank,
						interimBoard: interimBoard,
						newBoard: newBoard,
						gameState: gameState,
						moves: moves);
				}
			}

			if (canNormalCastleQueenside)
			{
				if (gameState.Board.GetPiece(4, backRank) == friendlyKing
					&& gameState.Board.GetPiece(3, backRank) == ChessSquarePiece.Empty
					&& gameState.Board.GetPiece(2, backRank) == ChessSquarePiece.Empty
					&& gameState.Board.GetPiece(1, backRank) == ChessSquarePiece.Empty
					&& gameState.Board.GetPiece(0, backRank) == friendlyRook)
				{
					ChessSquarePieceArray interimBoard = gameState.Board.SetPiece(4, backRank, ChessSquarePiece.Empty).SetPiece(3, backRank, friendlyKing);
					ChessSquarePieceArray newBoard = gameState.Board
						.SetPiece(4, backRank, ChessSquarePiece.Empty)
						.SetPiece(3, backRank, friendlyRook)
						.SetPiece(2, backRank, friendlyKing)
						.SetPiece(1, backRank, ChessSquarePiece.Empty)
						.SetPiece(0, backRank, ChessSquarePiece.Empty);
					GetMoveInfosForCastlingAndSuperCastlingMoves(
						startingFile: i,
						startingRank: j,
						endingFile: 2,
						endingRank: backRank,
						interimBoard: interimBoard,
						newBoard: newBoard,
						gameState: gameState,
						moves: moves);
				}
			}
		}

		private static void AddSuperCastlingKingMoves(
			GameState gameState,
			int i,
			int j,
			List<ComputeMoves.MoveInfo> moves)
		{
			ChessSquarePiece friendlyKing = gameState.IsWhiteTurn ? ChessSquarePiece.WhiteKing : ChessSquarePiece.BlackKing;
			ChessSquarePiece friendlyRook = gameState.IsWhiteTurn ? ChessSquarePiece.WhiteRook : ChessSquarePiece.BlackRook;

			List<Tuple<int, int>> deltas = new List<Tuple<int, int>>();
			if (i + 2 < 8)
				deltas.Add(new Tuple<int, int>(1, 0));
			if (i - 2 >= 0)
				deltas.Add(new Tuple<int, int>(-1, 0));
			if (j + 2 < 8)
				deltas.Add(new Tuple<int, int>(0, 1));
			if (j - 2 >= 0)
				deltas.Add(new Tuple<int, int>(0, -1));

			foreach (Tuple<int, int> delta in deltas)
			{
				int kingI = i;
				int kingJ = j;
				int count = 0;
				while (true)
				{
					kingI = kingI + delta.Item1;
					kingJ = kingJ + delta.Item2;
					count++;

					if (kingI < 0 || kingI >= 8 || kingJ < 0 || kingJ >= 8)
						break;

					if (gameState.Board.GetPiece(kingI, kingJ) == friendlyRook)
					{
						if (count != 1 || gameState.Board.GetPiece(i + delta.Item1 + delta.Item1, j + delta.Item2 + delta.Item2) == ChessSquarePiece.Empty)
						{
							ChessSquarePieceArray interimBoard = gameState.Board
								.SetPiece(i, j, ChessSquarePiece.Empty)
								.SetPiece(i + delta.Item1, j + delta.Item2, friendlyKing);
							ChessSquarePieceArray newBoard = gameState.Board
								.SetPiece(i, j, ChessSquarePiece.Empty)
								.SetPiece(kingI, kingJ, ChessSquarePiece.Empty)
								.SetPiece(i + delta.Item1, j + delta.Item2, friendlyRook)
								.SetPiece(i + delta.Item1 + delta.Item1, j + delta.Item2 + delta.Item2, friendlyKing);

							GetMoveInfosForCastlingAndSuperCastlingMoves(
								startingFile: i,
								startingRank: j,
								endingFile: i + delta.Item1 + delta.Item1,
								endingRank: j + delta.Item2 + delta.Item2,
								interimBoard: interimBoard,
								newBoard: newBoard,
								gameState: gameState,
								moves: moves);
						}
						break;
					}

					if (gameState.Board.GetPiece(kingI, kingJ) != ChessSquarePiece.Empty)
						break;
				}
			}
		}

		private static void AddNonCastlingKingMoves(
			GameState gameState,
			int i,
			int j,
			List<ComputeMoves.MoveInfo> moves)
		{
			ChessSquarePiece piece = gameState.IsWhiteTurn ? ChessSquarePiece.WhiteKing : ChessSquarePiece.BlackKing;

			List<Tuple<int, int>> kingMoves = new List<Tuple<int, int>>();
			kingMoves.Add(new Tuple<int, int>(i - 1, j - 1));
			kingMoves.Add(new Tuple<int, int>(i - 1, j));
			kingMoves.Add(new Tuple<int, int>(i - 1, j + 1));
			kingMoves.Add(new Tuple<int, int>(i, j - 1));
			kingMoves.Add(new Tuple<int, int>(i, j + 1));
			kingMoves.Add(new Tuple<int, int>(i + 1, j - 1));
			kingMoves.Add(new Tuple<int, int>(i + 1, j));
			kingMoves.Add(new Tuple<int, int>(i + 1, j + 1));

			foreach (Tuple<int, int> kingMove in kingMoves)
			{
				if (0 <= kingMove.Item1 && kingMove.Item1 < 8 && 0 <= kingMove.Item2 && kingMove.Item2 < 8)
				{
					ChessSquarePiece pieceAtDestination = gameState.Board.GetPiece(kingMove.Item1, kingMove.Item2);

					if (gameState.IsWhiteTurn && !pieceAtDestination.IsWhite() || !gameState.IsWhiteTurn && !pieceAtDestination.IsBlack())
					{
						if (pieceAtDestination.IsPawn() == false || gameState.IsPlayerTurn() || gameState.Abilities.HasPawnsDestroyCapturingPiece == false)
							ComputeMoves.AddMoveInfosForNonEnPassantNonCastlingMoves(
								startingFile: i,
								startingRank: j,
								endingFile: kingMove.Item1,
								endingRank: kingMove.Item2,
								piece: piece,
								gameState: gameState,
								kingFile: kingMove.Item1,
								kingRank: kingMove.Item2,
								moves: moves);
					}
				}
			}
		}

		private static void GetMoveInfosForCastlingAndSuperCastlingMoves(
			int startingFile,
			int startingRank,
			int endingFile,
			int endingRank,
			ChessSquarePieceArray interimBoard,
			ChessSquarePieceArray newBoard,
			GameState gameState,
			List<ComputeMoves.MoveInfo> moves)
		{
			if (CheckKingUnderAttack.IsKingUnderThreat(
					board: gameState.Board,
					playerAbilities: gameState.Abilities,
					checkWhiteKingUnderAttack: gameState.IsWhiteTurn,
					isPlayerWhite: gameState.IsPlayerWhite,
					kingFile: startingFile,
					kingRank: startingRank))
				return;

			if (CheckKingUnderAttack.IsKingUnderThreat(
					board: interimBoard,
					playerAbilities: gameState.Abilities,
					checkWhiteKingUnderAttack: gameState.IsWhiteTurn,
					isPlayerWhite: gameState.IsPlayerWhite,
					kingFile: (startingFile + endingFile) / 2,
					kingRank: (startingRank + endingRank) / 2))
				return;

			if (CheckKingUnderAttack.IsKingUnderThreat(
					board: newBoard,
					playerAbilities: gameState.Abilities,
					checkWhiteKingUnderAttack: gameState.IsWhiteTurn,
					isPlayerWhite: gameState.IsPlayerWhite,
					kingFile: endingFile,
					kingRank: endingRank))
				return;
			
			if (gameState.IsWhiteTurn)
			{
				bool isPromotion;

				isPromotion = startingRank == 7 && endingRank == 7 && gameState.IsPlayerTurn() && gameState.Abilities.HasAnyPieceCanPromote;

				if (isPromotion)
					ComputeMoves.AddPromotionMoveInfos(startingFile: startingFile, startingRank: startingRank, endingFile: endingFile, endingRank: endingRank, isCaptureMove: false, moves: moves);
				else
					moves.Add(new ComputeMoves.MoveInfo(move: Move.NormalMove(startingFile: startingFile, startingRank: startingRank, endingFile: endingFile, endingRank: endingRank), isCaptureMove: false));
			}
			else
			{
				bool isPromotion;

				isPromotion = startingRank == 0 && endingRank == 0 && gameState.IsPlayerTurn() && gameState.Abilities.HasAnyPieceCanPromote;

				if (isPromotion)
					 ComputeMoves.AddPromotionMoveInfos(startingFile: startingFile, startingRank: startingRank, endingFile: endingFile, endingRank: endingRank, isCaptureMove: false, moves: moves);
				else
					moves.Add(new ComputeMoves.MoveInfo(move: Move.NormalMove(startingFile: startingFile, startingRank: startingRank, endingFile: endingFile, endingRank: endingRank), isCaptureMove: false));
			}
		}
	}
}
