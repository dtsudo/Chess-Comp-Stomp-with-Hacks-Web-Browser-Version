
namespace ChessCompStompWithHacksEngine
{
	using System;

	public class CastlingUtil
	{
		public class CastlingInfo
		{
			public CastlingInfo(
				ChessSquare originalLocationOfKing,
				ChessSquare newLocationOfKing,
				ChessSquare originalLocationOfRook,
				ChessSquare newLocationOfRook)
			{
				this.OriginalLocationOfKing = originalLocationOfKing;
				this.NewLocationOfKing = newLocationOfKing;
				this.OriginalLocationOfRook = originalLocationOfRook;
				this.NewLocationOfRook = newLocationOfRook;
			}

			public ChessSquare OriginalLocationOfKing { get; private set; }
			public ChessSquare NewLocationOfKing { get; private set; }

			public ChessSquare OriginalLocationOfRook { get; private set; }
			public ChessSquare NewLocationOfRook { get; private set; }
		}
		
		public static CastlingInfo GetCastlingOrSuperCastlingMoveInfo(ChessSquarePieceArray originalBoard, Move castlingMove)
		{
			if (!castlingMove.IsCastlingOrSuperCastling(originalBoard: originalBoard))
				return null;

			ChessSquare originalLocationOfKing = new ChessSquare(castlingMove.StartingFile.Value, castlingMove.StartingRank.Value);

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

			ChessSquare originalLocationOfRook = originalLocationOfKing;
			while (true)
			{
				originalLocationOfRook = new ChessSquare(originalLocationOfRook.File + direction.Item1, originalLocationOfRook.Rank + direction.Item2);
				if (originalBoard.GetPiece(originalLocationOfRook).IsRook())
					break;
			}

			return new CastlingInfo(
				originalLocationOfKing: originalLocationOfKing,
				newLocationOfKing: new ChessSquare(file: castlingMove.EndingFile, rank: castlingMove.EndingRank),
				originalLocationOfRook: originalLocationOfRook,
				newLocationOfRook: new ChessSquare(file: (castlingMove.StartingFile.Value + castlingMove.EndingFile) / 2, rank: (castlingMove.StartingRank.Value + castlingMove.EndingRank) / 2));
		}
	}
}
