
namespace ChessCompStompWithHacksEngine
{
	using System;

	public class Move
	{
		public enum PromotionType
		{
			PromoteToRook,
			PromoteToKnight,
			PromoteToBishop,
			PromoteToQueen
		}

		private Move(
			bool isNuke,
			int? startingFile,
			int? startingRank,
			int endingFile,
			int endingRank,
			PromotionType? promotion)
		{
			this.IsNuke = isNuke;
			this.StartingFile = startingFile;
			this.StartingRank = startingRank;
			this.EndingFile = endingFile;
			this.EndingRank = endingRank;
			this.Promotion = promotion;
		}

		public static Move NormalMove(int startingFile, int startingRank, int endingFile, int endingRank)
		{
			return new Move(
				isNuke: false,
				startingFile: startingFile,
				startingRank: startingRank,
				endingFile: endingFile,
				endingRank: endingRank,
				promotion: null);
		}

		public static Move PromotionMove(int startingFile, int startingRank, int endingFile, int endingRank, PromotionType promotion)
		{
			return new Move(
				isNuke: false,
				startingFile: startingFile,
				startingRank: startingRank,
				endingFile: endingFile,
				endingRank: endingRank,
				promotion: promotion);
		}
		
		public static Move TacticalNukeMove(int file, int rank)
		{
			return new Move(
				isNuke: true,
				startingFile: null,
				startingRank: null,
				endingFile: file,
				endingRank: rank,
				promotion: null);
		}

		public bool IsNuke { get; private set; }

		public int? StartingFile { get; private set; }
		public int? StartingRank { get; private set; }

		public int EndingFile { get; private set; }
		public int EndingRank { get; private set; }

		public PromotionType? Promotion { get; private set; }
	}

	public static class MoveUtil
	{
		public static bool IsCastlingOrSuperCastling(this Move move, ChessSquarePieceArray originalBoard)
		{
			if (move.IsNuke)
				return false;

			if (originalBoard.GetPiece(move.StartingFile.Value, move.StartingRank.Value).IsKing() == false)
				return false;

			return Math.Abs(move.StartingFile.Value - move.EndingFile) == 2 
				|| Math.Abs(move.StartingRank.Value - move.EndingRank) == 2;
		}

		public static bool IsCapturingMove(this Move move, ChessSquarePieceArray originalBoard)
		{
			if (move.IsNuke)
				return false;

			if (move.IsCastlingOrSuperCastling(originalBoard: originalBoard))
				return false;

			if (originalBoard.GetPiece(move.EndingFile, move.EndingRank) != ChessSquarePiece.Empty)
				return true;

			return originalBoard.GetPiece(move.StartingFile.Value, move.StartingRank.Value).IsPawn()
				&& move.StartingFile.Value != move.EndingFile;
		}

		public static bool IsPawnMove(this Move move, ChessSquarePieceArray originalBoard)
		{
			if (move.IsNuke)
				return false;

			return originalBoard.GetPiece(move.StartingFile.Value, move.StartingRank.Value).IsPawn();
		}
	}

	public static class PromotionTypeUtil
	{
		public static ChessSquarePiece GetPromotedPiece(this Move.PromotionType promotionType, bool isWhite)
		{
			switch (promotionType)
			{
				case Move.PromotionType.PromoteToRook: return isWhite ? ChessSquarePiece.WhiteRook : ChessSquarePiece.BlackRook;
				case Move.PromotionType.PromoteToKnight: return isWhite ? ChessSquarePiece.WhiteKnight : ChessSquarePiece.BlackKnight;
				case Move.PromotionType.PromoteToBishop: return isWhite ? ChessSquarePiece.WhiteBishop : ChessSquarePiece.BlackBishop;
				case Move.PromotionType.PromoteToQueen: return isWhite ? ChessSquarePiece.WhiteQueen : ChessSquarePiece.BlackQueen;
				default: throw new Exception();
			}
		}
	}
}
