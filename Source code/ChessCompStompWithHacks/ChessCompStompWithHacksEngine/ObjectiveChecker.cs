
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

			if (PlayedAStupidOpening(originalGameState: originalGameState, move: move))
				completedObjectives.Add(Objective.PlayAStupidOpening);

			if (NukedOwnPieces(originalGameState: originalGameState, move: move))
				completedObjectives.Add(Objective.NukeYourOwnPieces);

			if (WonByCastlingVeryLongAndPromotingToQueen(originalGameState: originalGameState, move: move))
				completedObjectives.Add(Objective.WinByCastlingVeryLongAndPromotingRookToQueen);

			return completedObjectives;
		}

		private static bool WonByCastlingVeryLongAndPromotingToQueen(GameState originalGameState, Move move)
		{
			if (!originalGameState.IsPlayerTurn())
				return false;

			if (!move.IsCastlingOrSuperCastling(originalBoard: originalGameState.Board))
				return false;

			string requiredMoveName = GetNameOfCastlingVeryLongAndPromotingToQueenWithCheckmateMove();

			string moveName = MoveNaming.GetNameOfMove(move: move, originalGameState: originalGameState);

			return moveName == requiredMoveName;
		}

		public static string GetNameOfCastlingVeryLongAndPromotingToQueenWithCheckmateMove()
		{
			ChessSquarePiece[][] underlyingBoard = new ChessSquarePiece[8][];
			for (int i = 0; i < 8; i++)
			{
				underlyingBoard[i] = new ChessSquarePiece[8];
				for (int j = 0; j < 8; j++)
					underlyingBoard[i][j] = ChessSquarePiece.Empty;
			}

			underlyingBoard[0][7] = ChessSquarePiece.WhiteKing;
			underlyingBoard[7][7] = ChessSquarePiece.WhiteRook;
			underlyingBoard[0][6] = ChessSquarePiece.WhiteRook;
			underlyingBoard[2][6] = ChessSquarePiece.WhiteRook;
			underlyingBoard[1][0] = ChessSquarePiece.BlackKing;

			ChessSquarePieceArray board = new ChessSquarePieceArray(board: underlyingBoard);

			bool[][] underlyingUnmovedPawns = new bool[8][];
			for (int i = 0; i < 8; i++)
			{
				underlyingUnmovedPawns[i] = new bool[8];
				for (int j = 0; j < 8; j++)
					underlyingUnmovedPawns[i][j] = false;
			}
			UnmovedPawnsArray unmovedPawnsArray = new UnmovedPawnsArray(board: underlyingUnmovedPawns);

			GameState gameState = new GameState(
				board: board,
				unmovedPawns: unmovedPawnsArray,
				turnCount: 101,
				hasUsedNuke: false,
				isPlayerWhite: true,
				isWhiteTurn: true,
				previousPawnMoveFileForEnPassant: null,
				previousPawnMoveRankForEnPassant: null,
				castlingRights: new GameState.CastlingRights(
					canWhiteCastleKingside: false,
					canWhiteCastleQueenside: false,
					canBlackCastleKingside: false,
					canBlackCastleQueenside: false),
				playerAbilities: new GameState.PlayerAbilities(
					canPawnsMoveThreeSpacesInitially: false,
					canSuperEnPassant: false,
					canRooksMoveLikeBishops: false,
					canSuperCastle: true,
					canRooksCaptureLikeCannons: false,
					canKnightsMakeLargeKnightsMove: false,
					canQueensMoveLikeKnights: false,
					hasTacticalNuke: false,
					hasAnyPieceCanPromote: true,
					hasStalemateIsVictory: false,
					hasOpponentMustCaptureWhenPossible: false,
					hasPawnsDestroyCapturingPiece: false));

			List<Move> moves = ComputeMoves.GetMoves(gameState: gameState).Moves;

			foreach (Move move in moves)
			{
				if (!move.IsNuke
					&& move.StartingFile.Value == 0
					&& move.StartingRank.Value == 7
					&& move.EndingFile == 2
					&& move.EndingRank == 7
					&& move.Promotion.HasValue
					&& move.Promotion.Value == Move.PromotionType.PromoteToQueen)
				{
					return MoveNaming.GetNameOfMove(move: move, originalGameState: gameState);
				}
			}

			throw new Exception();
		}

		private static bool NukedOwnPieces(GameState originalGameState, Move move)
		{
			if (!originalGameState.IsPlayerTurn())
				return false;

			if (!move.IsNuke)
				return false;

			int numPlayerPiecesNuked = 0;
			int numOpponentPiecesNuked = 0;

			int playerPieceValueNuked = 0;
			int opponentPieceValueNuked = 0;

			List<ChessSquare> nukedSquares = TacticalNukeUtil.GetNukedSquares(file: move.EndingFile, rank: move.EndingRank);

			foreach (ChessSquare nukedSquare in nukedSquares)
			{
				ChessSquarePiece piece = originalGameState.Board.GetPiece(nukedSquare);

				if (piece == ChessSquarePiece.Empty)
					continue;

				bool isPlayerPiece = piece.IsWhite() == originalGameState.IsPlayerWhite;

				int pieceValue;
				if (piece.IsPawn())
					pieceValue = 1;
				else if (piece.IsBishop() || piece.IsKnight())
					pieceValue = 3;
				else if (piece.IsRook())
					pieceValue = 5;
				else if (piece.IsQueen())
					pieceValue = 9;
				else
					throw new Exception();

				if (isPlayerPiece)
				{
					numPlayerPiecesNuked++;
					playerPieceValueNuked += pieceValue;
				}
				else
				{
					numOpponentPiecesNuked++;
					opponentPieceValueNuked += pieceValue;
				}
			}
			
			if (numPlayerPiecesNuked < 4)
				return false;
			
			if (numPlayerPiecesNuked <= numOpponentPiecesNuked)
				return false;
			if (numPlayerPiecesNuked - numOpponentPiecesNuked < 2)
				return false;
			
			if (playerPieceValueNuked <= opponentPieceValueNuked)
				return false;
			if (playerPieceValueNuked - opponentPieceValueNuked < 4)
				return false;

			ChessSquare kingSquare = CheckKingUnderAttack.FindLocationOfKing(board: originalGameState.Board, findWhiteKing: originalGameState.IsPlayerWhite);

			bool wasPlayerInCheck = CheckKingUnderAttack.IsKingUnderThreat(
				board: originalGameState.Board,
				playerAbilities: originalGameState.Abilities,
				checkWhiteKingUnderAttack: originalGameState.IsPlayerWhite,
				isPlayerWhite: originalGameState.IsPlayerWhite,
				kingFile: kingSquare.File,
				kingRank: kingSquare.Rank);

			if (wasPlayerInCheck)
				return false;

			return true;
		}

		private static bool PlayedAStupidOpening(GameState originalGameState, Move move)
		{
			if (!originalGameState.IsPlayerTurn())
				return false;

			if (originalGameState.TurnCount != 3 && originalGameState.TurnCount != 4)
				return false;

			if (move.IsNuke)
				return false;

			ChessSquarePiece pieceBeingMoved = originalGameState.Board.GetPiece(file: move.StartingFile.Value, rank: move.StartingRank.Value);

			if (!pieceBeingMoved.IsKing())
				return false;

			return move.StartingFile.Value == move.EndingFile && move.StartingRank.Value != move.EndingRank;
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
