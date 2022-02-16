
namespace ChessCompStompWithHacksLibrary
{
	using System;
	using System.Collections.Generic;

	public enum ChessMusic
	{
		TitleScreen,
		Level1,
		Level2,
		Level3,
		Ending
	}

	public static class ChessMusicUtil
	{
		private static List<Tuple<ChessMusic, int>> GetChessMusicIdMapping()
		{
			List<Tuple<ChessMusic, int>> list = new List<Tuple<ChessMusic, int>>();

			list.Add(new Tuple<ChessMusic, int>(ChessMusic.TitleScreen, 1));
			list.Add(new Tuple<ChessMusic, int>(ChessMusic.Level1, 2));
			list.Add(new Tuple<ChessMusic, int>(ChessMusic.Level2, 3));
			list.Add(new Tuple<ChessMusic, int>(ChessMusic.Level3, 4));
			list.Add(new Tuple<ChessMusic, int>(ChessMusic.Ending, 5));

			return list;
		}

		/// <summary>
		/// Returns null if the chessMusicId isn't valid
		/// </summary>
		public static ChessMusic? GetChessMusicFromChessMusicId(int chessMusicId)
		{
			List<Tuple<ChessMusic, int>> mapping = GetChessMusicIdMapping();

			foreach (Tuple<ChessMusic, int> tuple in mapping)
			{
				if (tuple.Item2 == chessMusicId)
					return tuple.Item1;
			}

			return null;
		}

		/// <summary>
		/// Maps a ChessMusic to an integer identifier (in a consistent but arbitrary way)
		/// </summary>
		public static int GetChessMusicId(this ChessMusic chessMusic)
		{
			List<Tuple<ChessMusic, int>> mapping = GetChessMusicIdMapping();

			foreach (Tuple<ChessMusic, int> tuple in mapping)
			{
				if (tuple.Item1 == chessMusic)
					return tuple.Item2;
			}

			throw new Exception();
		}

		public static ChessMusic GetChessMusic(ColorTheme colorTheme)
		{
			switch (colorTheme)
			{
				case ColorTheme.Initial: return ChessMusic.Level1;
				case ColorTheme.Progress1: return ChessMusic.Level2;
				case ColorTheme.Progress2: return ChessMusic.Level2;
				case ColorTheme.Progress3: return ChessMusic.Level2;
				case ColorTheme.Final: return ChessMusic.Level3;
				default: throw new Exception();
			}
		}

		public static string GetMusicFilename(this ChessMusic music)
		{
			switch (music)
			{
				case ChessMusic.TitleScreen: return "JuhaniJunkala/TitleScreen.wav";
				case ChessMusic.Level1: return "JuhaniJunkala/Level1.wav";
				case ChessMusic.Level2: return "JuhaniJunkala/Level2.wav";
				case ChessMusic.Level3: return "JuhaniJunkala/Level3.wav";
				case ChessMusic.Ending: return "JuhaniJunkala/Ending.wav";
				default: throw new Exception();
			}
		}

		// From 0 to 100 (both inclusive)
		public static int GetMusicVolume(this ChessMusic music)
		{
			switch (music)
			{
				case ChessMusic.TitleScreen: return 7;
				case ChessMusic.Level1: return 7;
				case ChessMusic.Level2: return 7;
				case ChessMusic.Level3: return 7;
				case ChessMusic.Ending: return 10;
				default: throw new Exception();
			}
		}
	}
}
