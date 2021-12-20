
namespace ChessCompStompWithHacksEngine
{
	using System;
	using System.Collections.Generic;

	public class MoveImplementation
	{
		public static GameState ApplyMove(GameState gameState, DisplayMove displayMove)
		{
			return ApplyMove(gameState: gameState, move: displayMove.Move);
		}

		public static GameState ApplyMove(GameState gameState, Move move)
		{
			ChessSquarePieceArray newBoard = GetNewBoard(gameState: gameState, move: move);

			UnmovedPawnsArray unmovedPawns = gameState.UnmovedPawns;
			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					if (unmovedPawns.HasUnmovedPawn(i, j))
					{
						if (newBoard.GetPiece(i, j) != gameState.Board.GetPiece(i, j))
							unmovedPawns = unmovedPawns.PawnMoved(i, j);
					}
				}
			}

			int? previousPawnMoveFileForEnPassant;
			if (move.IsNuke)
				previousPawnMoveFileForEnPassant = null;
			else
			{
				ChessSquarePiece pieceThatMoved = gameState.Board.GetPiece(file: move.StartingFile.Value, rank: move.StartingRank.Value);
				bool pieceWasPawnAndMovedTwoSpaces = pieceThatMoved.IsPawn() && move.StartingFile.Value == move.EndingFile && Math.Abs(move.StartingRank.Value - move.EndingRank) == 2;
				bool pieceWasPawnAndMovedTwoSpacesFromSecondRank = pieceWasPawnAndMovedTwoSpaces && (move.StartingRank.Value == 1 || move.StartingRank.Value == 6);

				if (pieceWasPawnAndMovedTwoSpacesFromSecondRank)
					previousPawnMoveFileForEnPassant = move.StartingFile.Value;
				else
					previousPawnMoveFileForEnPassant = null;
			}

			return new GameState(
				board: newBoard,
				unmovedPawns: unmovedPawns,
				turnCount: gameState.TurnCount + 1,
				hasUsedNuke: move.IsNuke ? true : gameState.HasUsedNuke,
				isPlayerWhite: gameState.IsPlayerWhite,
				isWhiteTurn: !gameState.IsWhiteTurn,
				previousPawnMoveFileForEnPassant: previousPawnMoveFileForEnPassant,
				castlingRights: ComputeNewCastlingRights(gameState: gameState, newBoard: newBoard),
				playerAbilities: gameState.Abilities);
		}

		private static ChessSquarePieceArray GetNewBoard(GameState gameState, Move move)
		{
			if (move.IsNuke)
			{
				List<ChessSquare> nukedSquares = TacticalNukeUtil.GetNukedSquares(file: move.EndingFile, rank: move.EndingRank);

				ChessSquarePieceArray newBoard = gameState.Board;
				foreach (ChessSquare nukedSquare in nukedSquares)
					newBoard = newBoard.SetPiece(nukedSquare.File, nukedSquare.Rank, ChessSquarePiece.Empty);

				return newBoard;
			}
			else
			{
				ChessSquarePiece pieceThatMoved = gameState.Board.GetPiece(move.StartingFile.Value, move.StartingRank.Value);
				
				if (move.IsCastlingOrSuperCastling(originalBoard: gameState.Board))
					return GetNewBoard_CastlingOrSuperCastling(gameState: gameState, move: move);

				bool movedPawnAndCapturedSomething = pieceThatMoved.IsPawn()
					&& move.StartingFile.Value != move.EndingFile;

				bool wasEnPassantOrSuperEnPassant;
				
				if (!movedPawnAndCapturedSomething)
					wasEnPassantOrSuperEnPassant = false;
				else if (gameState.Board.GetPiece(move.EndingFile, move.EndingRank) == ChessSquarePiece.Empty)
					wasEnPassantOrSuperEnPassant = true;
				else if (gameState.IsPlayerTurn() == false)
					wasEnPassantOrSuperEnPassant = false;
				else if (gameState.Abilities.CanSuperEnPassant == false)
					wasEnPassantOrSuperEnPassant = false;
				else
				{
					ChessSquarePiece potentialEnPassantCapturedPiece = gameState.Board.GetPiece(move.EndingFile, move.StartingRank.Value);
					if (potentialEnPassantCapturedPiece == ChessSquarePiece.Empty)
						wasEnPassantOrSuperEnPassant = false;
					else
					{
						wasEnPassantOrSuperEnPassant = potentialEnPassantCapturedPiece.IsWhite() && gameState.IsWhiteTurn == false
							|| potentialEnPassantCapturedPiece.IsBlack() && gameState.IsWhiteTurn;
					}
				}

				ChessSquarePieceArray newBoard = gameState.Board
					.SetPiece(move.EndingFile, move.EndingRank, gameState.Board.GetPiece(move.StartingFile.Value, move.StartingRank.Value))
					.SetPiece(move.StartingFile.Value, move.StartingRank.Value, ChessSquarePiece.Empty);

				if (move.Promotion.HasValue)
					newBoard = newBoard.SetPiece(move.EndingFile, move.EndingRank, move.Promotion.Value.GetPromotedPiece(isWhite: gameState.IsWhiteTurn));

				if (gameState.IsPlayerTurn() == false && gameState.Abilities.HasPawnsDestroyCapturingPiece)
				{
					if (gameState.Board.GetPiece(move.EndingFile, move.EndingRank).IsPawn())
						newBoard = newBoard.SetPiece(move.EndingFile, move.EndingRank, ChessSquarePiece.Empty);
					else if (wasEnPassantOrSuperEnPassant) // We know this is the AI's turn and the AI can never have superEnPassant, meaning this is definitely regular enPassant
						newBoard = newBoard.SetPiece(move.EndingFile, move.EndingRank, ChessSquarePiece.Empty);
				}
				
				if (wasEnPassantOrSuperEnPassant)
					newBoard = newBoard.SetPiece(move.EndingFile, move.StartingRank.Value, ChessSquarePiece.Empty);
				
				return newBoard;
			}
		}
		
		private static ChessSquarePieceArray GetNewBoard_CastlingOrSuperCastling(GameState gameState, Move move)
		{
			Tuple<int, int> delta;

			if (move.EndingFile > move.StartingFile.Value)
				delta = new Tuple<int, int>(1, 0);
			else if (move.EndingFile < move.StartingFile.Value)
				delta = new Tuple<int, int>(-1, 0);
			else if (move.EndingRank > move.StartingRank.Value)
				delta = new Tuple<int, int>(0, 1);
			else if (move.EndingRank < move.StartingRank.Value)
				delta = new Tuple<int, int>(0, -1);
			else
				throw new Exception();

			ChessSquarePiece rookThatMoved = gameState.IsWhiteTurn ? ChessSquarePiece.WhiteRook : ChessSquarePiece.BlackRook;
			ChessSquarePiece rookThatMovedPossiblyPromoted = move.Promotion.HasValue ? move.Promotion.Value.GetPromotedPiece(isWhite: gameState.IsWhiteTurn) : rookThatMoved;
			
			int count = 1;
			while (true)
			{
				if (gameState.Board.GetPiece(move.StartingFile.Value + delta.Item1 * count, move.StartingRank.Value + delta.Item2 * count) == rookThatMoved)
					break;
				count++;
			}

			ChessSquarePieceArray newBoard = gameState.Board
				.SetPiece(move.StartingFile.Value, move.StartingRank.Value, ChessSquarePiece.Empty)
				.SetPiece(move.StartingFile.Value + delta.Item1, move.StartingRank.Value + delta.Item2, rookThatMovedPossiblyPromoted)
				.SetPiece(move.StartingFile.Value + delta.Item1 + delta.Item1, move.StartingRank.Value + delta.Item2 + delta.Item2, gameState.IsWhiteTurn ? ChessSquarePiece.WhiteKing : ChessSquarePiece.BlackKing);

			if (count > 2)
				newBoard = newBoard.SetPiece(move.StartingFile.Value + delta.Item1 * count, move.StartingRank.Value + delta.Item2 * count, ChessSquarePiece.Empty);

			return newBoard;
		}
		
		private static GameState.CastlingRights ComputeNewCastlingRights(GameState gameState, ChessSquarePieceArray newBoard)
		{
			bool canWhiteCastleKingside;
			if (gameState.Castling.CanWhiteCastleKingside)
				canWhiteCastleKingside = newBoard.GetPiece(4, 0) == ChessSquarePiece.WhiteKing && newBoard.GetPiece(7, 0) == ChessSquarePiece.WhiteRook;
			else
				canWhiteCastleKingside = false;

			bool canWhiteCastleQueenside;
			if (gameState.Castling.CanWhiteCastleQueenside)
				canWhiteCastleQueenside = newBoard.GetPiece(4, 0) == ChessSquarePiece.WhiteKing && newBoard.GetPiece(0, 0) == ChessSquarePiece.WhiteRook;
			else
				canWhiteCastleQueenside = false;

			bool canBlackCastleKingside;
			if (gameState.Castling.CanBlackCastleKingside)
				canBlackCastleKingside = newBoard.GetPiece(4, 7) == ChessSquarePiece.BlackKing && newBoard.GetPiece(7, 7) == ChessSquarePiece.BlackRook;
			else
				canBlackCastleKingside = false;

			bool canBlackCastleQueenside;
			if (gameState.Castling.CanBlackCastleQueenside)
				canBlackCastleQueenside = newBoard.GetPiece(4, 7) == ChessSquarePiece.BlackKing && newBoard.GetPiece(0, 7) == ChessSquarePiece.BlackRook;
			else
				canBlackCastleQueenside = false;

			return new GameState.CastlingRights(
				canWhiteCastleKingside: canWhiteCastleKingside,
				canWhiteCastleQueenside: canWhiteCastleQueenside,
				canBlackCastleKingside: canBlackCastleKingside,
				canBlackCastleQueenside: canBlackCastleQueenside);
		}
	}
}
