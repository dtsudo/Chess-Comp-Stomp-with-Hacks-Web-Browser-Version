
namespace ChessCompStompWithHacksEngine
{
	using System;

	public enum ChessSquarePiece
	{
		BlackPawn,
		BlackKnight,
		BlackBishop,
		BlackRook,
		BlackQueen,
		BlackKing,

		WhitePawn,
		WhiteKnight,
		WhiteBishop,
		WhiteRook,
		WhiteQueen,
		WhiteKing,

		Empty
	}

	public static class ChessSquarePieceUtil
	{
		public static int GetValueForHashCode(this ChessSquarePiece piece)
		{
			switch (piece)
			{
				case ChessSquarePiece.BlackPawn: return 1;
				case ChessSquarePiece.BlackKnight: return 2;
				case ChessSquarePiece.BlackBishop: return 3;
				case ChessSquarePiece.BlackRook: return 4;
				case ChessSquarePiece.BlackQueen: return 5;
				case ChessSquarePiece.BlackKing: return 6;
				case ChessSquarePiece.WhitePawn: return 7;
				case ChessSquarePiece.WhiteKnight: return 8;
				case ChessSquarePiece.WhiteBishop: return 9;
				case ChessSquarePiece.WhiteRook: return 10;
				case ChessSquarePiece.WhiteQueen: return 11;
				case ChessSquarePiece.WhiteKing: return 12;
				case ChessSquarePiece.Empty: return 13;
				default: throw new Exception();
			}
		}

		public static bool IsPawn(this ChessSquarePiece piece)
		{
			return piece == ChessSquarePiece.WhitePawn || piece == ChessSquarePiece.BlackPawn;
		}
		
		public static bool IsRook(this ChessSquarePiece piece)
		{
			return piece == ChessSquarePiece.WhiteRook || piece == ChessSquarePiece.BlackRook;
		}

		public static bool IsKnight(this ChessSquarePiece piece)
		{
			return piece == ChessSquarePiece.WhiteKnight || piece == ChessSquarePiece.BlackKnight;
		}

		public static bool IsBishop(this ChessSquarePiece piece)
		{
			return piece == ChessSquarePiece.WhiteBishop || piece == ChessSquarePiece.BlackBishop;
		}

		public static bool IsQueen(this ChessSquarePiece piece)
		{
			return piece == ChessSquarePiece.WhiteQueen || piece == ChessSquarePiece.BlackQueen;
		}

		public static bool IsKing(this ChessSquarePiece piece)
		{
			return piece == ChessSquarePiece.WhiteKing || piece == ChessSquarePiece.BlackKing;
		}

		public static bool IsWhite(this ChessSquarePiece piece)
		{
			switch (piece)
			{
				case ChessSquarePiece.BlackPawn:
				case ChessSquarePiece.BlackKnight:
				case ChessSquarePiece.BlackBishop:
				case ChessSquarePiece.BlackRook:
				case ChessSquarePiece.BlackQueen:
				case ChessSquarePiece.BlackKing:
					return false;

				case ChessSquarePiece.WhitePawn:
				case ChessSquarePiece.WhiteKnight:
				case ChessSquarePiece.WhiteBishop:
				case ChessSquarePiece.WhiteRook:
				case ChessSquarePiece.WhiteQueen:
				case ChessSquarePiece.WhiteKing:
					return true;

				case ChessSquarePiece.Empty:
					return false;

				default:
					throw new Exception();
			}
		}

		public static bool IsBlack(this ChessSquarePiece piece)
		{
			if (piece == ChessSquarePiece.Empty)
				return false;

			return !piece.IsWhite();
		}
	}
}
