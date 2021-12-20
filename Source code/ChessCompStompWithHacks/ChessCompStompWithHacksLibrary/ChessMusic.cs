
namespace ChessCompStompWithHacksLibrary
{
	using System;

	public enum ChessMusic
	{
	}

	public static class ChessMusicUtil
	{
		public static string GetMusicFilename(this ChessMusic music)
		{
			switch (music)
			{
				default: throw new Exception();
			}
		}

		// From 0 to 100 (both inclusive)
		public static int GetMusicVolume(this ChessMusic music)
		{
			switch (music)
			{
				default: throw new Exception();
			}
		}
	}
}
