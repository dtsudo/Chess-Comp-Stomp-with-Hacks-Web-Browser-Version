
namespace ChessCompStompWithHacksEngine
{
	using DTLibrary;
	using System;
	using System.Collections.Generic;

	public class TacticalNukeUtil
	{
		public const int NumberOfMovesPlayedBeforeNukeIsAvailable = 10;

		public static List<ChessSquare> GetNukedSquares(ChessSquare chessSquare)
		{
			return GetNukedSquares(file: chessSquare.File, rank: chessSquare.Rank);
		}

		public static List<ChessSquare> GetNukedSquares(int file, int rank)
		{
			if (file < 0 || file >= 8)
				throw new Exception("File not in range: " + file.ToStringCultureInvariant());

			if (rank < 0 || rank >= 8)
				throw new Exception("Rank not in range: " + rank.ToStringCultureInvariant());

			List<ChessSquare> list = new List<ChessSquare>();

			TryAddSquare(list: list, file: file - 3, rank: rank);
			TryAddSquare(list: list, file: file + 3, rank: rank);

			for (int j = -1; j <= 1; j++)
			{
				TryAddSquare(list: list, file: file - 2, rank: rank + j);
				TryAddSquare(list: list, file: file + 2, rank: rank + j);
			}

			for (int j = -2; j <= 2; j++)
			{
				TryAddSquare(list: list, file: file - 1, rank: rank + j);
				TryAddSquare(list: list, file: file + 1, rank: rank + j);
			}

			for (int j = -3; j <= 3; j++)
				TryAddSquare(list: list, file: file, rank: rank + j);

			return list;
		}

		private static void TryAddSquare(List<ChessSquare> list, int file, int rank)
		{
			if (0 <= file && file < 8 && 0 <= rank && rank < 8)
				list.Add(new ChessSquare(file: file, rank: rank));
		}
	}
}
