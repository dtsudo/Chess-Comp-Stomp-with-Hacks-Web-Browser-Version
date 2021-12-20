
namespace ChessCompStompWithHacksEngine
{
	using System;
	using System.Collections.Generic;

	public class ComputeMoves
	{
		public enum GameStatus
		{
			InProgress,
			/*
				Note that if the player has the StalemateIsVictory hack,
				then this never happens. (Instead, the player just wins.)
			*/
			Stalemate,
			WhiteVictory,
			BlackVictory
		}

		public class Result
		{
			public Result(List<Move> moves, GameStatus gameStatus)
			{
				this.Moves = moves;
				this.GameStatus = gameStatus;
			}

			public List<Move> Moves { get; private set; }
			public GameStatus GameStatus { get; private set; }
		}
		
		public class MoveInfo
		{
			public MoveInfo(
				Move move,
				bool isCaptureMove)
			{
				this.Move = move;
				this.IsCaptureMove = isCaptureMove;
			}

			public Move Move { get; private set; }

			// Tactical nuke is not considered a capture move
			public bool IsCaptureMove { get; private set; }
		}
		
		public static Result GetMoves(GameState gameState)
		{
			int kingFile = -1;
			int kingRank = -1;

			ChessSquarePiece king = gameState.IsWhiteTurn ? ChessSquarePiece.WhiteKing : ChessSquarePiece.BlackKing;

			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					if (gameState.Board.GetPiece(i, j) == king)
					{
						kingFile = i;
						kingRank = j;
						break;
					}
				}

				if (kingFile != -1)
					break;
			}

			if (kingFile == -1)
				throw new Exception();

			List<MoveInfo> moves = new List<MoveInfo>();

			// Populate moves by looking at all 64 squares and seeing what moves are available
			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					ChessSquarePiece piece = gameState.Board.GetPiece(i, j);

					if (gameState.IsWhiteTurn)
					{
						if (!piece.IsWhite())
							continue;
					}
					else
					{
						if (!piece.IsBlack())
							continue;
					}

					switch (piece)
					{
						case ChessSquarePiece.WhitePawn:
						case ChessSquarePiece.BlackPawn:
							ComputeMovesPawn.AddPawnMoves(
								gameState: gameState,
								i: i,
								j: j,
								kingFile: kingFile,
								kingRank: kingRank,
								moves: moves);
							break;
						case ChessSquarePiece.WhiteRook:
						case ChessSquarePiece.BlackRook:
							ComputeMovesRook.AddRookMoves(
								gameState: gameState,
								i: i,
								j: j,
								kingFile: kingFile,
								kingRank: kingRank,
								moves: moves);
							break;
						case ChessSquarePiece.WhiteKnight:
						case ChessSquarePiece.BlackKnight:
							ComputeMovesKnight.AddKnightMoves(
								gameState: gameState,
								i: i,
								j: j,
								kingFile: kingFile,
								kingRank: kingRank,
								moves: moves);
							break;
						case ChessSquarePiece.WhiteBishop:
						case ChessSquarePiece.BlackBishop:
							ComputeMovesBishop.AddBishopMoves(
								gameState: gameState,
								i: i,
								j: j,
								kingFile: kingFile,
								kingRank: kingRank,
								moves: moves);
							break;
						case ChessSquarePiece.WhiteQueen:
						case ChessSquarePiece.BlackQueen:
							ComputeMovesQueen.AddQueenMoves(
								gameState: gameState,
								i: i,
								j: j,
								kingFile: kingFile,
								kingRank: kingRank,
								moves: moves);
							break;
						case ChessSquarePiece.WhiteKing:
						case ChessSquarePiece.BlackKing:
							ComputeMovesKing.AddKingMoves(
								gameState: gameState,
								i: i,
								j: j,
								moves: moves);
							break;

						case ChessSquarePiece.Empty:
							break;

						default:
							throw new Exception();
					}
				}
			}
			
			ComputeMovesTacticalNuke.AddTacticalNukeMoves(gameState: gameState, kingFile: kingFile, kingRank: kingRank, moves: moves);
			
			if (!gameState.IsPlayerTurn() && gameState.Abilities.HasOpponentMustCaptureWhenPossible)
			{
				List<MoveInfo> captureMoves = new List<MoveInfo>();
				foreach (MoveInfo move in moves)
				{
					if (move.IsCaptureMove)
						captureMoves.Add(move);
				}

				if (captureMoves.Count > 0)
					moves = captureMoves;
			}

			GameStatus gameStatus;

			if (moves.Count > 0)
				gameStatus = GameStatus.InProgress;
			else
			{
				bool isKingUnderThreat = CheckKingUnderAttack.IsKingUnderThreat(
					board: gameState.Board,
					playerAbilities: gameState.Abilities,
					checkWhiteKingUnderAttack: gameState.IsWhiteTurn,
					isPlayerWhite: gameState.IsPlayerWhite,
					kingFile: kingFile,
					kingRank: kingRank);

				if (isKingUnderThreat)
				{
					gameStatus = gameState.IsWhiteTurn ? GameStatus.BlackVictory : GameStatus.WhiteVictory;
				}
				else
				{
					if (gameState.Abilities.HasStalemateIsVictory)
					{
						gameStatus = gameState.IsPlayerWhite ? GameStatus.WhiteVictory : GameStatus.BlackVictory;
					}
					else
					{
						gameStatus = GameStatus.Stalemate;
					}
				}
			}

			List<Move> returnValue = new List<Move>(capacity: moves.Count);
			for (int i = 0; i < moves.Count; i++)
				returnValue.Add(moves[i].Move);

			return new Result(
				moves: returnValue,
				gameStatus: gameStatus);
		}

		public static void AddMoveInfosForNonEnPassantNonCastlingMoves(
			int startingFile,
			int startingRank,
			int endingFile,
			int endingRank,
			ChessSquarePiece piece,
			GameState gameState,
			int kingFile, // if moving the king, this is the king's new location (i.e. endingFile)
			int kingRank, // if moving the king, this is the king's new location (i.e. endingRank)
			List<MoveInfo> moves)
		{
			bool isCaptureMove = gameState.Board.GetPiece(endingFile, endingRank) != ChessSquarePiece.Empty;

			bool shouldDestroyPiece = isCaptureMove
					&& !gameState.IsPlayerTurn()
					&& gameState.Abilities.HasPawnsDestroyCapturingPiece
					&& gameState.Board.GetPiece(endingFile, endingRank).IsPawn();

			ChessSquarePieceArray newBoard = gameState.Board
				.SetPiece(endingFile, endingRank, shouldDestroyPiece ? ChessSquarePiece.Empty : (gameState.IsWhiteTurn ? ChessSquarePiece.WhiteRook : ChessSquarePiece.BlackRook) /* doesn't really matter what piece we put here */)
				.SetPiece(startingFile, startingRank, ChessSquarePiece.Empty);

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
			
			if (gameState.IsWhiteTurn)
			{
				bool isPromotion;

				if (piece.IsPawn() && endingRank == 7)
					isPromotion = true;
				else if (gameState.IsPlayerWhite && gameState.Abilities.HasAnyPieceCanPromote
						&& (piece.IsRook() || piece.IsKnight() || piece.IsBishop() || piece.IsQueen())
						&& endingRank == 7)
					isPromotion = true;
				else
					isPromotion = false;

				if (isPromotion)
					AddPromotionMoveInfos(
						startingFile: startingFile,
						startingRank: startingRank,
						endingFile: endingFile,
						endingRank: endingRank,
						isCaptureMove: isCaptureMove,
						moves: moves);
				else
					moves.Add(new MoveInfo(Move.NormalMove(startingFile: startingFile, startingRank: startingRank, endingFile: endingFile, endingRank: endingRank), isCaptureMove: isCaptureMove));
			}
			else
			{
				bool isPromotion;

				if (piece.IsPawn() && endingRank == 0)
					isPromotion = true;
				else if (!gameState.IsPlayerWhite && gameState.Abilities.HasAnyPieceCanPromote
						&& (piece.IsRook() || piece.IsKnight() || piece.IsBishop() || piece.IsQueen())
						&& endingRank == 0)
					isPromotion = true;
				else
					isPromotion = false;

				if (isPromotion)
					AddPromotionMoveInfos(
						startingFile: startingFile,
						startingRank: startingRank,
						endingFile: endingFile,
						endingRank: endingRank,
						isCaptureMove: isCaptureMove,
						moves: moves);
				else
					moves.Add(new MoveInfo(Move.NormalMove(startingFile: startingFile, startingRank: startingRank, endingFile: endingFile, endingRank: endingRank), isCaptureMove: isCaptureMove));
			}
		}

		public static void AddPromotionMoveInfos(
			int startingFile,
			int startingRank,
			int endingFile,
			int endingRank,
			bool isCaptureMove,
			List<MoveInfo> moves)
		{
			moves.Add(new MoveInfo(
				move: Move.PromotionMove(startingFile: startingFile, startingRank: startingRank, endingFile: endingFile, endingRank: endingRank, promotion: Move.PromotionType.PromoteToRook),
				isCaptureMove: isCaptureMove));
			moves.Add(new MoveInfo(
				move: Move.PromotionMove(startingFile: startingFile, startingRank: startingRank, endingFile: endingFile, endingRank: endingRank, promotion: Move.PromotionType.PromoteToKnight),
				isCaptureMove: isCaptureMove));
			moves.Add(new MoveInfo(
				move: Move.PromotionMove(startingFile: startingFile, startingRank: startingRank, endingFile: endingFile, endingRank: endingRank, promotion: Move.PromotionType.PromoteToBishop),
				isCaptureMove: isCaptureMove));
			moves.Add(new MoveInfo(
				move: Move.PromotionMove(startingFile: startingFile, startingRank: startingRank, endingFile: endingFile, endingRank: endingRank, promotion: Move.PromotionType.PromoteToQueen),
				isCaptureMove: isCaptureMove));
		}
	}
}
