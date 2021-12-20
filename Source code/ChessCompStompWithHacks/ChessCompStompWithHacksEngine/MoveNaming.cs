
namespace ChessCompStompWithHacksEngine
{
	using System;
	using System.Collections.Generic;

	public class MoveNaming
	{
		public static string GetNameOfMove(Move move, GameState originalGameState)
		{
			if (move.IsNuke)
				return GetNameOfNukeMove(nukeMove: move, originalGameState: originalGameState);

			int startingFile = move.StartingFile.Value;
			int startingRank = move.StartingRank.Value;

			ChessSquarePiece pieceBeingMoved = originalGameState.Board.GetPiece(startingFile, startingRank);

			if (pieceBeingMoved.IsPawn())
				return GetNameOfPawnMove(pawnMove: move, originalGameState: originalGameState);

			if (move.IsCastlingOrSuperCastling(originalBoard: originalGameState.Board))
				return GetNameOfCastlingMove(castlingMove: move, originalGameState: originalGameState);

			return GetNameOfNonPawnNonCastlingNonNukeMove(move: move, originalGameState: originalGameState);
		}

		private static string GetNameOfNonPawnNonCastlingNonNukeMove(Move move, GameState originalGameState)
		{
			string name;

			ChessSquarePiece pieceBeingMoved = originalGameState.Board.GetPiece(move.StartingFile.Value, move.StartingRank.Value);

			if (pieceBeingMoved.IsRook())
				name = "R";
			else if (pieceBeingMoved.IsKnight())
				name = "N";
			else if (pieceBeingMoved.IsBishop())
				name = "B";
			else if (pieceBeingMoved.IsQueen())
				name = "Q";
			else if (pieceBeingMoved.IsKing())
				name = "K";
			else
				throw new Exception();
			
			var allMoves = ComputeMoves.GetMoves(gameState: originalGameState).Moves;
			List<ChessSquare> piecesThatCanMakeThisMove = new List<ChessSquare>();
			foreach (Move m in allMoves)
			{
				if (m.IsNuke)
					continue;

				if (originalGameState.Board.GetPiece(m.StartingFile.Value, m.StartingRank.Value) != pieceBeingMoved)
					continue;

				if (m.EndingFile != move.EndingFile || m.EndingRank != move.EndingRank)
					continue;

				if (m.Promotion.HasValue && m.Promotion.Value != Move.PromotionType.PromoteToQueen)
					continue;

				piecesThatCanMakeThisMove.Add(new ChessSquare(m.StartingFile.Value, m.StartingRank.Value));
			}

			if (piecesThatCanMakeThisMove.Count > 1)
			{
				int fileCount = 0;
				foreach (ChessSquare cs in piecesThatCanMakeThisMove)
				{
					if (cs.File == move.StartingFile.Value)
						fileCount++;
				}

				if (fileCount == 1)
				{
					name = name + GetFileName(move.StartingFile.Value);
				}
				else
				{
					int rankCount = 0;
					foreach (ChessSquare cs in piecesThatCanMakeThisMove)
					{
						if (cs.Rank == move.StartingRank.Value)
							rankCount++;
					}

					if (rankCount == 1)
						name = name + GetRankName(move.StartingRank.Value);
					else
						name = name + GetFileName(move.StartingFile.Value) + GetRankName(move.StartingRank.Value);
				}
			}

			if (move.IsCapturingMove(originalBoard: originalGameState.Board))
				name = name + "x";

			name = name + GetFileName(move.EndingFile) + GetRankName(move.EndingRank);

			if (move.Promotion != null)
				name = name + GetPromotionString(move: move);

			MoveResult moveResult = ApplyMove(move: move, originalGameState: originalGameState);

			if (moveResult.IsCheck)
				name = name + "+";
			if (moveResult.IsCheckmate)
				name = name + "#";

			return name;
		}

		/// <summary>
		/// Returns the number of squares the rook moves
		/// </summary>
		private static int GetLengthOfCastlingMove(Move castlingMove, GameState originalGameState)
		{
			Tuple<int, int> direction;
			if (castlingMove.EndingFile - castlingMove.StartingFile.Value == 2)
				direction = new Tuple<int, int>(1, 0);
			else if (castlingMove.EndingFile - castlingMove.StartingFile.Value == -2)
				direction = new Tuple<int, int>(-1, 0);
			else if (castlingMove.EndingRank - castlingMove.StartingRank.Value == 2)
				direction = new Tuple<int, int>(0, 1);
			else if (castlingMove.EndingRank - castlingMove.StartingRank.Value == -2)
				direction = new Tuple<int, int>(0, -1);
			else
				throw new Exception();

			ChessSquare rook = new ChessSquare(castlingMove.StartingFile.Value, castlingMove.StartingRank.Value);
			int count = 0;
			while (true)
			{
				rook = new ChessSquare(rook.File + direction.Item1, rook.Rank + direction.Item2);
				count++;
				if (originalGameState.Board.GetPiece(rook).IsRook())
					break;
			}

			return count - 1;
		}

		private static string GetNameOfCastlingMove(Move castlingMove, GameState originalGameState)
		{
			int castlingLength = GetLengthOfCastlingMove(castlingMove: castlingMove, originalGameState: originalGameState);

			List<Move> allMoves = ComputeMoves.GetMoves(gameState: originalGameState).Moves;

			int count = 0;
			foreach (Move m in allMoves)
			{
				if (m.IsCastlingOrSuperCastling(originalBoard: originalGameState.Board))
				{
					if (m.Promotion == null || m.Promotion.Value == Move.PromotionType.PromoteToQueen)
					{
						int length = GetLengthOfCastlingMove(castlingMove: m, originalGameState: originalGameState);
						if (length == castlingLength)
							count++;
					}
				}
			}
			bool isAmbiguous = count > 1;

			string name;

			if (isAmbiguous || castlingLength == 0)
			{
				name = "K" + GetFileName(castlingMove.EndingFile) + GetRankName(castlingMove.EndingRank);
			}
			else
			{
				switch (castlingLength)
				{
					case 1: name = "0"; break;
					case 2: name = "0-0"; break;
					case 3: name = "0-0-0"; break;
					case 4: name = "0-0-0-0"; break;
					case 5: name = "0-0-0-0-0"; break;
					case 6: name = "0-0-0-0-0-0"; break;
					default: throw new Exception();
				}
			}

			if (castlingMove.Promotion != null)
				name = name + GetPromotionString(move: castlingMove);

			MoveResult moveResult = ApplyMove(move: castlingMove, originalGameState: originalGameState);

			if (moveResult.IsCheck)
				name = name + "+";
			if (moveResult.IsCheckmate)
				name = name + "#";

			return name;
		}

		private static string GetNameOfPawnMove(Move pawnMove, GameState originalGameState)
		{
			string name;
			bool isEnPassantOrSuperEnPassant;

			MoveResult moveResult = ApplyMove(move: pawnMove, originalGameState: originalGameState);

			if (pawnMove.IsCapturingMove(originalBoard: originalGameState.Board))
			{
				name = GetFileName(pawnMove.StartingFile.Value) + "x" + GetFileName(pawnMove.EndingFile) + GetRankName(pawnMove.EndingRank);

				ChessSquare potentialEnPassantSquare = new ChessSquare(pawnMove.EndingFile, pawnMove.StartingRank.Value);
				isEnPassantOrSuperEnPassant = originalGameState.Board.GetPiece(potentialEnPassantSquare) != moveResult.NewGameState.Board.GetPiece(potentialEnPassantSquare);
			}
			else
			{
				name = GetFileName(pawnMove.EndingFile) + GetRankName(pawnMove.EndingRank);
				isEnPassantOrSuperEnPassant = false;
			}
			
			if (pawnMove.Promotion.HasValue)
				name = name + GetPromotionString(move: pawnMove);

			if (moveResult.IsCheck)
				name = name + "+";
			if (moveResult.IsCheckmate)
				name = name + "#";

			if (isEnPassantOrSuperEnPassant)
				name = name + " e.p.";

			return name;
		}

		private static string GetPromotionString(Move move)
		{
			if (move.Promotion == null)
				return "";

			switch (move.Promotion.Value)
			{
				case Move.PromotionType.PromoteToRook: return "=R";
				case Move.PromotionType.PromoteToKnight: return "=N";
				case Move.PromotionType.PromoteToBishop: return "=B";
				case Move.PromotionType.PromoteToQueen: return "=Q";
				default: throw new Exception();
			}
		}

		private static string GetNameOfNukeMove(Move nukeMove, GameState originalGameState)
		{
			string name = "ICBM";
			name = name + GetFileName(nukeMove.EndingFile) + GetRankName(nukeMove.EndingRank);

			MoveResult result = ApplyMove(move: nukeMove, originalGameState: originalGameState);

			if (result.IsCheck)
				return name + "+";
			if (result.IsCheckmate)
				return name + "#";
			return name;
		}

		private class MoveResult
		{
			public MoveResult(GameState newGameState, bool isCheck, bool isCheckmate)
			{
				this.NewGameState = newGameState;
				this.IsCheck = isCheck;
				this.IsCheckmate = isCheckmate;
			}

			public GameState NewGameState { get; private set; }
			public bool IsCheck { get; private set; }
			public bool IsCheckmate { get; private set; }
		}

		private static MoveResult ApplyMove(Move move, GameState originalGameState)
		{
			GameState newGameState = MoveImplementation.ApplyMove(gameState: originalGameState, move: move);
			ComputeMoves.Result result = ComputeMoves.GetMoves(gameState: newGameState);

			if (originalGameState.IsWhiteTurn && result.GameStatus == ComputeMoves.GameStatus.WhiteVictory
				|| !originalGameState.IsWhiteTurn && result.GameStatus == ComputeMoves.GameStatus.BlackVictory)
			{
				return new MoveResult(
					newGameState: newGameState,
					isCheck: false,
					isCheckmate: true);
			}

			int? kingFile = null;
			int? kingRank = null;

			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					ChessSquarePiece piece = newGameState.Board.GetPiece(i, j);
					if (piece.IsKing())
					{
						if (newGameState.IsWhiteTurn && piece.IsWhite() || !newGameState.IsWhiteTurn && piece.IsBlack())
						{
							kingFile = i;
							kingRank = j;
							break;
						}
					}
				}

				if (kingFile != null)
					break;
			}

			bool isCheck = CheckKingUnderAttack.IsKingUnderThreat(
				board: newGameState.Board,
				playerAbilities: newGameState.Abilities,
				checkWhiteKingUnderAttack: newGameState.IsWhiteTurn,
				isPlayerWhite: newGameState.IsPlayerWhite,
				kingFile: kingFile.Value,
				kingRank: kingRank.Value);

			return new MoveResult(newGameState: newGameState, isCheck: isCheck, isCheckmate: false);
		}
		
		private static string GetRankName(int rank)
		{
			switch (rank)
			{
				case 0: return "1";
				case 1: return "2";
				case 2: return "3";
				case 3: return "4";
				case 4: return "5";
				case 5: return "6";
				case 6: return "7";
				case 7: return "8";
				default: throw new Exception();
			}
		}

		private static string GetFileName(int file)
		{
			switch (file)
			{
				case 0: return "a";
				case 1: return "b";
				case 2: return "c";
				case 3: return "d";
				case 4: return "e";
				case 5: return "f";
				case 6: return "g";
				case 7: return "h";
				default: throw new Exception();
			}
		}
	}
}
