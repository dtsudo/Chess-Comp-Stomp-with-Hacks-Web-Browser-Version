
namespace ChessCompStompWithHacksEngine
{
	using System;
	using System.Collections.Generic;

	public class DisplayMove
	{
		private DisplayMove(
			bool isNuke,
			int? startingFile,
			int? startingRank,
			int endingFile,
			int endingRank,
			Move.PromotionType? promotion,
			Move move)
		{
			this.IsNuke = isNuke;
			this.StartingFile = startingFile;
			this.StartingRank = startingRank;
			this.EndingFile = endingFile;
			this.EndingRank = endingRank;
			this.Promotion = promotion;
			this.Move = move;
		}

		private DisplayMove(Move move)
		{
			this.IsNuke = move.IsNuke;
			this.StartingFile = move.StartingFile;
			this.StartingRank = move.StartingRank;
			this.EndingFile = move.EndingFile;
			this.EndingRank = move.EndingRank;
			this.Promotion = move.Promotion;
			this.Move = move;
		}

		public static List<DisplayMove> GetDisplayMoves(List<Move> moves, GameState gameState)
		{
			List<DisplayMove> displayMoves = new List<DisplayMove>();

			foreach (Move move in moves)
			{
				displayMoves.Add(new DisplayMove(move));

				if (move.IsCastlingOrSuperCastling(gameState.Board))
				{
					ChessSquare king = new ChessSquare(move.StartingFile.Value, move.StartingRank.Value);

					Tuple<int, int> direction;
					if (move.EndingFile - move.StartingFile.Value == 2)
						direction = new Tuple<int, int>(1, 0);
					else if (move.EndingFile - move.StartingFile.Value == -2)
						direction = new Tuple<int, int>(-1, 0);
					else if (move.EndingRank - move.StartingRank.Value == 2)
						direction = new Tuple<int, int>(0, 1);
					else if (move.EndingRank - move.StartingRank.Value == -2)
						direction = new Tuple<int, int>(0, -1);
					else
						throw new Exception();

					ChessSquare rook = king;
					while (true)
					{
						rook = new ChessSquare(rook.File + direction.Item1, rook.Rank + direction.Item2);
						if (gameState.Board.GetPiece(rook).IsRook())
							break;
					}

					if (rook.File != move.EndingFile || rook.Rank != move.EndingRank)
					{
						displayMoves.Add(new DisplayMove(
							isNuke: false,
							startingFile: move.StartingFile.Value,
							startingRank: move.StartingRank.Value,
							endingFile: rook.File,
							endingRank: rook.Rank,
							promotion: move.Promotion,
							move: move));
					}

					displayMoves.Add(new DisplayMove(
							isNuke: false,
							startingFile: rook.File,
							startingRank: rook.Rank,
							endingFile: move.StartingFile.Value,
							endingRank: move.StartingRank.Value,
							promotion: move.Promotion,
							move: move));
				}
			}

			return displayMoves;
		}

		public bool IsNuke { get; private set; }

		public int? StartingFile { get; private set; }
		public int? StartingRank { get; private set; }

		public int EndingFile { get; private set; }
		public int EndingRank { get; private set; }

		public Move.PromotionType? Promotion { get; private set; }

		public Move Move { get; private set; }
	}
}
