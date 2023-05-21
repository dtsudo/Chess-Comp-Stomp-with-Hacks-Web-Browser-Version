
namespace ChessCompStompWithHacksEngine
{
	using System.Collections.Generic;

	public class ComputeMovesPawn
	{
		public static void AddPawnMoves(
			GameState gameState,
			int i,
			int j,
			int kingFile,
			int kingRank,
			List<ComputeMoves.MoveInfo> moves)
		{
			AddNonCapturePawnMoves(
				gameState: gameState,
				i: i,
				j: j,
				kingFile: kingFile,
				kingRank: kingRank,
				moves: moves);

			AddCapturingPawnMoves(
				gameState: gameState,
				i: i,
				j: j,
				kingFile: kingFile,
				kingRank: kingRank,
				moves: moves);
		}

		private static void AddNonCapturePawnMoves(
			GameState gameState,
			int i,
			int j,
			int kingFile,
			int kingRank,
			List<ComputeMoves.MoveInfo> moves)
		{
			int nextRank;
			int nextNextRank;
			int nextNextNextRank;
			ChessSquarePiece piece;

			if (gameState.IsWhiteTurn)
			{
				nextRank = j + 1;
				nextNextRank = j + 2;
				nextNextNextRank = j + 3;
				piece = ChessSquarePiece.WhitePawn;
			}
			else
			{
				nextRank = j - 1;
				nextNextRank = j - 2;
				nextNextNextRank = j - 3;
				piece = ChessSquarePiece.BlackPawn;
			}

			if (gameState.Board.GetPiece(i, nextRank) == ChessSquarePiece.Empty)
			{
				ComputeMoves.AddMoveInfosForNonEnPassantNonCastlingMoves(
					startingFile: i,
					startingRank: j,
					endingFile: i,
					endingRank: nextRank,
					piece: piece,
					gameState: gameState,
					kingFile: kingFile,
					kingRank: kingRank,
					moves: moves);

				if (gameState.UnmovedPawns.HasUnmovedPawn(i, j)
						&& nextNextRank >= 0
						&& nextNextRank < 8
						&& gameState.Board.GetPiece(i, nextNextRank) == ChessSquarePiece.Empty)
				{
					ComputeMoves.AddMoveInfosForNonEnPassantNonCastlingMoves(
						startingFile: i,
						startingRank: j,
						endingFile: i,
						endingRank: nextNextRank,
						piece: piece,
						gameState: gameState,
						kingFile: kingFile,
						kingRank: kingRank,
						moves: moves);

					if (gameState.IsPlayerTurn()
							&& gameState.Abilities.CanPawnsMoveThreeSpacesInitially
							&& nextNextNextRank >= 0
							&& nextNextNextRank < 8
							&& gameState.Board.GetPiece(i, nextNextNextRank) == ChessSquarePiece.Empty)
					{
						ComputeMoves.AddMoveInfosForNonEnPassantNonCastlingMoves(
							startingFile: i,
							startingRank: j,
							endingFile: i,
							endingRank: nextNextNextRank,
							piece: piece,
							gameState: gameState,
							kingFile: kingFile,
							kingRank: kingRank,
							moves: moves);
					}
				}
			}
		}

		private static void AddCapturingPawnMoves(
			GameState gameState,
			int i,
			int j,
			int kingFile,
			int kingRank,
			List<ComputeMoves.MoveInfo> moves)
		{
			bool hasSuperEnPassant = gameState.IsPlayerTurn() && gameState.Abilities.CanSuperEnPassant;

			int nextRank = gameState.IsWhiteTurn ? (j + 1) : (j - 1);

			List<int> endingFiles = new List<int>();
			if (i >= 1)
				endingFiles.Add(i - 1);
			if (i <= 6)
				endingFiles.Add(i + 1);

			foreach (int endingFile in endingFiles)
			{
				ChessSquarePiece capturingSquare = gameState.Board.GetPiece(endingFile, nextRank);
				ChessSquarePiece enPassantSquare = gameState.Board.GetPiece(endingFile, j);

				if (gameState.IsWhiteTurn && capturingSquare.IsWhite() || !gameState.IsWhiteTurn && capturingSquare.IsBlack())
					continue;

				bool shouldTakeEnPassant;
				bool hasCapturedOpponentPawn;

				if (hasSuperEnPassant)
				{
					if (!IsOpponentPiece(gameState: gameState, piece: capturingSquare) && !IsOpponentPiece(gameState: gameState, piece: enPassantSquare))
						continue;
					shouldTakeEnPassant = IsOpponentPiece(gameState: gameState, piece: enPassantSquare);
					hasCapturedOpponentPawn = shouldTakeEnPassant && enPassantSquare.IsPawn() || capturingSquare.IsPawn();
				}
				else
				{
					if (gameState.PreviousPawnMoveFileForEnPassant.HasValue
						&& gameState.PreviousPawnMoveFileForEnPassant.Value == endingFile
						&& gameState.PreviousPawnMoveRankForEnPassant.HasValue
						&& gameState.PreviousPawnMoveRankForEnPassant.Value == j
						&& enPassantSquare.IsPawn()
						&& capturingSquare == ChessSquarePiece.Empty)
					{
						shouldTakeEnPassant = true;
						hasCapturedOpponentPawn = true;
					}
					else
					{
						if (!IsOpponentPiece(gameState: gameState, piece: capturingSquare))
							continue;
						shouldTakeEnPassant = false;
						hasCapturedOpponentPawn = capturingSquare.IsPawn();
					}
				}

				ChessSquarePieceArray newBoard = gameState.Board
					.SetPiece(i, j, ChessSquarePiece.Empty);

				if (shouldTakeEnPassant)
					newBoard = newBoard.SetPiece(endingFile, j, ChessSquarePiece.Empty);

				if (!gameState.IsPlayerTurn() && gameState.Abilities.HasPawnsDestroyCapturingPiece && hasCapturedOpponentPawn)
					newBoard = newBoard.SetPiece(endingFile, nextRank, ChessSquarePiece.Empty);
				else
					newBoard = newBoard.SetPiece(endingFile, nextRank, gameState.IsWhiteTurn ? ChessSquarePiece.WhiteRook : ChessSquarePiece.BlackRook /* doesn't really matter what piece we put here */);

				AddMoveInfosForCapturingPawnMove(
					startingFile: i,
					startingRank: j,
					endingFile: endingFile,
					endingRank: nextRank,
					newBoard: newBoard,
					gameState: gameState,
					kingFile: kingFile,
					kingRank: kingRank,
					moves: moves);
			}
		}

		private static bool IsOpponentPiece(GameState gameState, ChessSquarePiece piece)
		{
			return gameState.IsWhiteTurn && piece.IsBlack() || !gameState.IsWhiteTurn && piece.IsWhite();
		}

		private static void AddMoveInfosForCapturingPawnMove(
			int startingFile,
			int startingRank,
			int endingFile,
			int endingRank,
			ChessSquarePieceArray newBoard,
			GameState gameState,
			int kingFile,
			int kingRank,
			List<ComputeMoves.MoveInfo> moves)
		{
			if (CheckKingUnderAttack.IsKingUnderThreat(
				board: newBoard,
				playerAbilities: gameState.Abilities,
				checkWhiteKingUnderAttack: gameState.IsWhiteTurn,
				isPlayerWhite: gameState.IsPlayerWhite,
				kingFile: kingFile,
				kingRank: kingRank))
			{
				return;
			}

			bool isPromotion = gameState.IsWhiteTurn ? (endingRank == 7) : (endingRank == 0);

			if (isPromotion)
				ComputeMoves.AddPromotionMoveInfos(
					startingFile: startingFile,
					startingRank: startingRank,
					endingFile: endingFile,
					endingRank: endingRank,
					isCaptureMove: true,
					moves: moves);
			else
				moves.Add(new ComputeMoves.MoveInfo(
					move: Move.NormalMove(startingFile: startingFile, startingRank: startingRank, endingFile: endingFile, endingRank: endingRank),
					isCaptureMove: true));
		}
	}
}
